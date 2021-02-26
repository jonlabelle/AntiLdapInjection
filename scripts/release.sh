#!/usr/bin/env bash

#
# resolve $source until the file is no longer a symlink
source="${BASH_SOURCE[0]}"
while [[ -L $source ]]; do
    scriptroot="$(cd -P "$(dirname "$source")" && pwd)"
    source="$(readlink "$source")"
    # if $source was a relative symlink, resolve it relative
    # to the path where the symlink file was located
    [[ $source != /* ]] && source="$scriptroot/$source"
done
scriptroot="$(cd -P "$(dirname "$source")" && pwd)"
# navigate up to project root
cd "$scriptroot" && cd ".." || exit 1
projectroot="$(pwd)"

if [[ $# -eq 0 || -z $1 ]]; then
    echo "Please provide a version for this release."
    exit 1
fi

version=$1
# oldversion="$(git describe --abbrev=0 --tags)"

#
# validate semver
if [[ ! ${version} =~ ^[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
    echo "[Error] '$version' is not a valid Semantic Version."
    exit 1
fi

#
# ensure on git master branch
git_branch=$(git rev-parse --abbrev-ref HEAD)
if [ "$git_branch" != "master" ]; then
    show_error "[ERROR] You must be on the master branch to make a release."
    exit 1
fi

#
# ensure clean git status
if ! git diff-index --quiet HEAD --; then
    echo "[ERROR] Uncommitted local changes. Please revert or commit all local changes before making a release."
    exit 1
fi

#
# compare local/remote git hashes (hat tip: https://stackoverflow.com/a/15119807)
git_local_hash="$(git rev-parse --verify origin/master)"
git_remote_hash="$(git ls-remote origin master | cut -d$'\t' -f 1)"
if [ "$git_local_hash" != "$git_remote_hash" ]; then
    echo "[ERROR] Git remote history differs. Pull remote changes first."
    exit 1
fi

#
# confirm release
while true; do
    read -r -p "Ready to release 'v$version'? (y/n): " yn
    case ${yn} in
        [Yy]*) break ;;
        [NnQq]*) exit ;;
        *) echo "Please answer [y]es or [n]o." ;;
    esac
done

#
# bump version
cd src/AntiLdapInjection || exit 1
project=AntiLdapInjection.csproj
tmp_project="${TMPDIR:-/tmp}/$project.$$"
sed -E s/'<Version>[0-9]+\.[0-9]+\.[0-9]+'/'<Version>'"$version"''/ $project > "$tmp_project" \
    && mv "$tmp_project" $project \
    && grep "$version" -C 1 $project \
    || echo "[ERROR] Bump project version failed." && exit 1
cd "$projectroot" || exit 1

#
# push to git
git commit -am "Bump to v$version" \
    && git tag -a "$version" -m "Tag v$version" \
    && git push \
    && git push --tags \
    || echo "[ERROR] Git push failed." && exit 1
