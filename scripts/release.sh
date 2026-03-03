#!/usr/bin/env bash

# Stop script on non-zero exit code
set -e
# Stop script if unbound variable found (use ${var:-} if intentional)
set -u
# By default cmd1 | cmd2 returns exit code of cmd2 regardless of cmd1 success
# This is causing it to fail
set -o pipefail

# shellcheck disable=SC2005,SC2034,SC2155
readonly SCRIPTDIR="$(
  cd "$(dirname "${0}")"
  echo "$(pwd)"
)"
# shellcheck disable=SC2034,SC2155
readonly SCRIPTNAME="$(basename "${BASH_SOURCE[0]}")"

function show_usage() {
  echo "Usage: ${SCRIPTNAME} [OPTIONS ...] version"
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

function require_command() {
  local cmd

  cmd="$1"
  if ! command -v "${cmd}" >/dev/null 2>&1; then
    say_err "Required command not found: ${cmd}"
    return 1
  fi

  return 0
}

function ensure_prerequisites() {
  # shellcheck disable=2154,2086
  eval "${invocation}"

  require_command git
  require_command sed
  require_command grep
  require_command mktemp

  return 0
}

function ensure_git_repo() {
  # shellcheck disable=2154,2086
  eval "${invocation}"

  if ! git rev-parse --is-inside-work-tree >/dev/null 2>&1; then
    say_err "Current directory is not a git repository."
    return 1
  fi

  return 0
}

function is_semver() {
  # shellcheck disable=2154,2086
  eval "${invocation}"

  local version

  version="$1"

  if [[ ! ${version} =~ ^[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
    say_err "Invalid Semantic Version: ${version}"
    return 1
  fi

  return 0
}

function ensure_release_branch() {
  # shellcheck disable=2154,2086
  eval "${invocation}"

  local current_branch

  current_branch=$(git rev-parse --abbrev-ref HEAD)

  if [[ "${current_branch}" != "${RELEASE_BRANCH}" ]]; then
    say_err "You must be on the ${RELEASE_BRANCH} branch to make a release."
    return 1
  fi

  return 0
}

function ensure_clean_working_dir() {
  # shellcheck disable=2154,2086
  eval "${invocation}"

  local status_porcelain

  status_porcelain="$(git status --porcelain --untracked-files=normal)"
  if [[ -n "${status_porcelain}" ]]; then
    say_err "Uncommitted local changes. Please revert or commit all local changes before making a release."
    return 1
  fi

  return 0
}

function show_current_version() {
  # shellcheck disable=2154,2086
  eval "${invocation}"

  local tag

  if ! tag="$(git describe --abbrev=0 --tags 2>/dev/null)"; then
    say_err "No tags found in the repository."
    return 1
  fi
  echo "${tag}"

  return 0
}

function ensure_branch_up_to_date() {
  # shellcheck disable=2154,2086
  eval "${invocation}"

  local local_hash remote_hash

  if ! git fetch --quiet origin "${RELEASE_BRANCH}"; then
    say_err "Unable to fetch latest commits from origin/${RELEASE_BRANCH}."
    return 1
  fi

  local_hash="$(git rev-parse --verify HEAD)"
  remote_hash="$(git rev-parse --verify "origin/${RELEASE_BRANCH}")"

  if [[ "${local_hash}" != "${remote_hash}" ]]; then
    say_err "Git local/remote histories differ. Update your local branch first."
    return 1
  fi

  return 0
}

function ensure_isolated_change() {
  # shellcheck disable=2154,2086
  eval "${invocation}"

  local changed_output changed_files numstat insertions deletions changed_file

  # ensure only one file changed, one insertion/deletion of 'AntiLdapInjection.csproj' file

  changed_output="$(git diff --name-only)"
  if [[ -z "${changed_output}" ]]; then
    say_err "No modified files found; expected a version bump in ${PROJECT_FILE}."
    return 1
  fi

  mapfile -t changed_files <<< "${changed_output}"
  if [[ "${#changed_files[@]}" -ne 1 ]] || [[ "${changed_files[0]}" != "${PROJECT_FILE}" ]]; then
    say_err "Expected exactly one modified file: ${PROJECT_FILE}."
    return 1
  fi

  numstat="$(git diff --numstat -- "${PROJECT_FILE}")"
  if [[ -z "${numstat}" ]]; then
    say_err "Unable to verify isolated change for ${PROJECT_FILE}."
    return 1
  fi

  IFS=$'\t' read -r insertions deletions changed_file <<< "${numstat}"
  if [[ "${insertions}" != "1" ]] || [[ "${deletions}" != "1" ]]; then
    say_err "Expected one insertion and one deletion in ${PROJECT_FILE}; got +${insertions}/-${deletions}."
    return 1
  fi
  if [[ "${changed_file}" != "${PROJECT_FILE}" ]]; then
    say_err "Unexpected file in diff stat: ${changed_file}."
    return 1
  fi

  return 0
}

function bump_version() {
  # shellcheck disable=2154,2086
  eval "${invocation}"

  local temp_file_template temp_project_file failed

  temp_file_template="${TMPDIR:-/tmp}/${PROJECT}.XXXXXXXXX"
  temp_project_file="$(mktemp "${temp_file_template}")"

  say_verbose "Temp project file path: ${temp_project_file}"

  failed=false

  # replace the existing .csproj version, with the new...
  sed -E "s#<Version>[0-9]+\.[0-9]+\.[0-9]+#<Version>${VERSION}#" "${PROJECT_FILE}" >"${temp_project_file}" &&
    mv "${temp_project_file}" "${PROJECT_FILE}" &&
    grep -q -C 1 "${VERSION}" "${PROJECT_FILE}" ||
    failed=true

  rm -f -- "${temp_project_file}"
  say_verbose "Temp project file removed: ${temp_project_file}"

  if [[ "${failed}" == true ]]; then
    say_err "Bump version failed."
    exit 1
  fi

  return 0
}

function _undo_changes() {
  # shellcheck disable=2154,2086
  eval "${invocation}"

  # reverts the change to .csproj file
  git checkout -- "${PROJECT_FILE}"
  say_verbose "Reverted changes to: ${PROJECT_FILE}"
}

confirm_release() {
  # shellcheck disable=2154,2086
  eval "${invocation}"

  local ask_result

  if [[ "${DRYRUN}" == true ]]; then
    _undo_changes
    say "Dry run only. File changes have been undone; commit was not applied, tagged or pushed."
    exit 0
  fi

  if [[ "${CI}" == false ]]; then
    set +e
    ask "Ready to release 'v${VERSION}'?" Y
    ask_result=$?
    set -e

    if [[ "${ask_result}" -eq 0 ]]; then
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
  eval "${invocation}"

  if ! git commit -am "build: bump to v${VERSION}"; then
    say_err "Commit changes failed."
    return 1
  fi

  return 0
}

function tag_commit() {
  # shellcheck disable=2154,2086
  eval "${invocation}"

  if ! git tag -a "${VERSION}" -m "Tag v${VERSION}"; then
    say_err "Tag commit failed."
    return 1
  fi

  return 0
}

function push_changes() {
  # shellcheck disable=2154,2086
  eval "${invocation}"

  if ! git push origin "${RELEASE_BRANCH}"; then
    say_err "Push changes failed."
    return 1
  fi

  if ! git push origin "${VERSION}"; then
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

PROJECT_FILE="src/${PROJECT}/${PROJECT}.csproj"
VERBOSE=false
CI=false
DRYRUN=false
VERSION=''
HAS_VERSION=false
# EXTRA_ARGS=''

if [[ $# -eq 0 || -z ${1:-} ]]; then
  say_err "Please provide a version for the release. See --help for more information."
  exit 1
fi

while [[ $# -ne 0 ]]; do
  name="${1}"
  case "${name}" in
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
    if [[ $# -eq 0 ]]; then
      say_err "Missing version value after ${name}."
      exit 1
    fi
    is_semver "${1}"
    VERSION="${1}"
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
    if [[ "${HAS_VERSION}" == false ]]; then
      is_semver "${1}"
      VERSION="${1}"
      HAS_VERSION=true
    else
      say_err "Unknown argument \`${name}\`"
      exit 1
    fi
    ;;
  esac

  shift
done

if [[ "${HAS_VERSION}" == false ]]; then
  say_err "You did not provide a version for the release."
  exit 1
fi

ensure_prerequisites
ensure_git_repo
ensure_release_branch
ensure_clean_working_dir
ensure_branch_up_to_date
bump_version
ensure_isolated_change
confirm_release
commit_changes
tag_commit
push_changes
