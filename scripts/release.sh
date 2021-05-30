#!/usr/bin/env bash

#
# Release.sh
#
# Updates the project version and creates an isolated
# commit and tag of the change.

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
    echo "Make a release!"
    echo
    echo "Options:"
    echo
    echo "  -v <VERSION>, --version <VERSION>   Semantic Version number for the release."
    echo "  --current                           Shows the current version."
    echo "  -ci, --ci                           Specify when running in continuous integration environments."
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

    local branch

    branch=$(git rev-parse --abbrev-ref HEAD)

    if [ "$branch" != "master" ]; then
        say_err "You must be on the master branch to make a release."
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

function get_current_version() {
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

    local_hash="$(git rev-parse --verify origin/master)"
    remote_hash="$(git ls-remote origin master | cut -d$'\t' -f 1)"

    if [ "$local_hash" != "$remote_hash" ]; then
        say_err "Git local/remote histories differ. Update your local branch first."
        return 1
    fi

    return 0
}

function bump_version() {
    # shellcheck disable=2154,2086
    eval $invocation

    local project project_file temp_file_template temp_project_file failed

    project=AntiLdapInjection
    project_file="src/$project/$project.csproj"

    temp_file_template="${TMPDIR:-/tmp}/$project.XXXXXXXXX"
    temp_project_file="$(mktemp "$temp_file_template")"

    say_verbose "Temp project file path: $temp_project_file"

    failed=false

    # replace the existing .csproj version, with the new
    sed -E s/'<Version>[0-9]+\.[0-9]+\.[0-9]+'/'<Version>'"$VERSION"''/ $project_file > "$temp_project_file" \
        && mv "$temp_project_file" "$project_file" \
        && grep -q "$VERSION" -C 1 "$project_file" \
        || failed=true

    rm -rf "$temp_project_file"

    if [ "$failed" = true ]; then
        say_err "Bump version failed."
        return 1
    fi

    if [ "$DRYRUN" = true ]; then
        # reverts the change to .csproj file
        git checkout -- "$project_file"
        say_verbose "Reverted change to: $project_file"
    fi

    return 0
}

confirm_release() {
    # shellcheck disable=2154,2086
    eval $invocation

    if [ "$DRYRUN" = true ]; then
        say "Dry run only, changes will not be committed."
        exit 0
    fi

    if [ "$CI" = false ]; then
        while true; do
            read -r -p "Ready to release 'v$VERSION'? (y/n): " yn
            case ${yn} in
                [Yy]*)
                    break
                    ;;
                [NnQq]*)
                    exit 0
                    ;;
                *)
                    say_warning "Please answer [Y]es or [N]o"
                    ;;
            esac
        done
    fi
}

function commit_changes() {
    # shellcheck disable=2154,2086
    eval $invocation

    local failed
    failed=false

    git commit -am "Bump to v$version" || failed=true
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

    git push && git push --tags || failed=true

    if [ "$failed" = true ]; then
        say_err "Push changes failed."
        return 1
    fi

    return 0
}

# cd project root
cd "${SCRIPTDIR}" && cd ..

# shellcheck disable=SC1091
. scripts/inc/_output.sh

# shellcheck disable=SC2034
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
        --current)
            get_current_version
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
confirm_release
commit_changes
tag_commit
push_changes
