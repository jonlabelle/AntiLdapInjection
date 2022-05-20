#!/usr/bin/env bash

# Stop script on non-zero exit code
set -e
# Stop script if unbound variable found (use ${var:-} if intentional)
set -u
# By default cmd1 | cmd2 returns exit code of cmd2 regardless of cmd1 success
# This is causing it to fail
set -o pipefail

# shellcheck disable=SC2005,SC2034,SC2155
readonly SCRIPTDIR="$(cd "$(dirname "${0}")"; echo "$(pwd)")"
# shellcheck disable=SC2034,SC2155
readonly SCRIPTNAME="$(basename "${BASH_SOURCE[0]}")"


function show_usage() {
    echo "Usage: $SCRIPTNAME [OPTIONS ...] version"
    echo
    echo "Updates the project version and creates an isolated commit; the "
    echo "commit is then tagged and pushed to the remote/origin (GitHub)."
    echo
    echo "Upon successful execution of this script (and given the executor has "
    echo "the appropriate permissions) the Continuous Deployment workflow "
    echo "is triggered and publishes the package to NuGet.org."
    echo
    echo "Options:"
    echo
    echo "  -v <VERSION>, --version <VERSION>   Semantic Version number for the release."
    echo "  --show-current-version              Shows the latest version committed to the repository."
    echo "  -ci, --ci                           Use if running in continuous integration environments."
    echo "                                      Will not prompt to confirm release."
    echo "  -n, --dry-run                       Bumps the version number, but doesn't commit/push changes."
    echo "  --verbose                           Display diagnostics information. Useful for debugging purposes."
    echo "  -h, --help                          Shows this help message."
}

function is_semver() {
    # shellcheck disable=2154,2086
    eval $invocation

    local version

    version="$1"

    if [[ ! ${version} =~ ^[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
        say_err "Invalid Semantic Version: $version"
        return 1
    fi

    return 0
}

function ensure_release_branch() {
    # shellcheck disable=2154,2086
    eval $invocation

    local current_branch

    current_branch=$(git rev-parse --abbrev-ref HEAD)

    if [ "$current_branch" != "$RELEASE_BRANCH" ]; then
        say_err "You must be on the $RELEASE_BRANCH branch to make a release."
        return 1
    fi

    return 0
}

function ensure_clean_working_dir() {
    # shellcheck disable=2154,2086
    eval $invocation

    if ! git diff-index --quiet HEAD --; then
        say_err "Uncommitted local changes. Please revert or commit all local changes before making a release."
        return 1
    fi

    return 0
}

function show_current_version() {
    # shellcheck disable=2154,2086
    eval $invocation

    local tag

    tag="$(git describe --abbrev=0 --tags)"
    echo "$tag"

    return 0
}

function ensure_branch_up_to_date() {
    # shellcheck disable=2154,2086
    eval $invocation

    local local_hash remote_hash

    # ref: https://stackoverflow.com/a/15119807
    local_hash="$(git rev-parse --verify "origin/${RELEASE_BRANCH}")"
    remote_hash="$(git ls-remote origin "$RELEASE_BRANCH" | cut -d$'\t' -f 1)"

    if [ "$local_hash" != "$remote_hash" ]; then
        say_err "Git local/remote histories differ. Update your local branch first."
        return 1
    fi

    return 0
}

function ensure_isolated_change() {
    # shellcheck disable=2154,2086
    eval $invocation

    # ensure only one file changed, one insertion/deletion of 'AntiLdapInjection.csproj' file

    # shellcheck disable=SC2076
    if [[ ! $(git diff --stat) =~ "1 file changed, 1 insertion(+), 1 deletion(-)" ]]; then
        say_err "expected '1 file changed, 1 insertion(+), 1 deletion(-)'. check git status and git diff."
        exit 1
    else
        return 0
    fi
}

function bump_version() {
    # shellcheck disable=2154,2086
    eval $invocation

    local temp_file_template temp_project_file failed

    temp_file_template="${TMPDIR:-/tmp}/$PROJECT.XXXXXXXXX"
    temp_project_file="$(mktemp "$temp_file_template")"

    say_verbose "Temp project file path: $temp_project_file"

    failed=false

    # replace the existing .csproj version, with the new...
    # shellcheck disable=SC2086
    sed -E s/'<Version>[0-9]+\.[0-9]+\.[0-9]+'/'<Version>'"$VERSION"''/ $PROJECT_FILE > "$temp_project_file" \
        && mv "$temp_project_file" "$PROJECT_FILE" \
        && grep -q "$VERSION" -C 1 "$PROJECT_FILE" \
        || failed=true

    rm -rf "$temp_project_file"
    say_verbose "Temp project file removed: $temp_project_file"

    if [ "$failed" = true ]; then
        say_err "Bump version failed."
        exit 1
    fi

    return 0
}

function _undo_changes() {
    # shellcheck disable=2154,2086
    eval $invocation

    # reverts the change to .csproj file
    git checkout -- "$PROJECT_FILE"
    say_verbose "Reverted changes to: $PROJECT_FILE"
}

confirm_release() {
    # shellcheck disable=2154,2086
    eval $invocation

    if [ "$DRYRUN" = true ]; then
        _undo_changes
        say "Dry run only. File changes have been undone; commit was not applied, tagged or pushed."
        exit 0
    fi

    if [ "$CI" = false ]; then
        if ask "Ready to release 'v$VERSION'?" Y; then
            say_verbose "Release was confirmed."
            return 0
        else
            say_verbose "Release was not confirmed. File changes have not been committed, tagged or pushed."
            exit 0
        fi
    fi
}

function commit_changes() {
    # shellcheck disable=2154,2086
    eval $invocation

    local failed
    failed=false

    git commit -am "Bump to v$VERSION" || failed=true
    if [ "$failed" = true ]; then
        say_err "Commit changes failed."
        return 1
    fi

    return 0
}

function tag_commit() {
    # shellcheck disable=2154,2086
    eval $invocation

    local failed
    failed=false

    git tag -a "$VERSION" -m "Tag v$VERSION" || failed=true
    if [ "$failed" = true ]; then
        say_err "Tag commit failed."
        return 1
    fi

    return 0
}

function push_changes() {
    # shellcheck disable=2154,2086
    eval $invocation

    local failed
    failed=false

    git push origin \
        && git push orgin "$VERSION" || failed=true

    if [ "$failed" = true ]; then
        say_err "Push changes failed."
        return 1
    fi

    return 0
}

# cd project root
cd "${SCRIPTDIR}" && cd ..

# shellcheck disable=SC1091
. scripts/inc/_util.sh

readonly RELEASE_BRANCH=main
readonly PROJECT=AntiLdapInjection

PROJECT_FILE="src/$PROJECT/$PROJECT.csproj"
VERBOSE=false
CI=false
DRYRUN=false
VERSION=''
HAS_VERSION=false
# EXTRA_ARGS=''

if [[ $# -eq 0 || -z $1 ]]; then
    say_err "Please provide a version for the release. See --help for more information."
    exit 1
fi

while [ $# -ne 0 ]; do
    name="$1"
    case "$name" in
        --show-current-version)
            show_current_version
            exit 0
            ;;
        -h | --help)
            show_usage
            exit 0
            ;;
        -v | --version | -[Vv]ersion)
            shift
            is_semver "$1" || exit 1
            VERSION="$1"
            HAS_VERSION=true
            ;;
        -ci | --ci)
            CI=true
            ;;
        -n | --dry-run)
            DRYRUN=true
            ;;
        --verbose)
            # shellcheck disable=SC2034
            VERBOSE=true
            ;;
        *)
            # EXTRA_ARGS="$EXTRA_ARGS $1"
            if [ "$HAS_VERSION" = false ]; then
                is_semver "$1" || exit 1
                VERSION="$1"
                HAS_VERSION=true
            else
                say_err "Unknown argument \`$name\`"
                exit 1
            fi
            ;;
    esac

    shift
done

if [ "$HAS_VERSION" = false ]; then
    say_err "You did not provide a version for the release."
    exit 1
fi

ensure_release_branch
ensure_clean_working_dir
ensure_branch_up_to_date
bump_version
ensure_isolated_change
confirm_release
commit_changes
tag_commit
push_changes
