#!/usr/bin/env bash

set -e
set -o pipefail

export DOTNET_CLI_TELEMETRY_OPTOUT=true
export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
export DOTNET_NOLOGO=true

readonly PROJECT_NAME=AntiLdapInjection
readonly TEST_PROJECT_NAME="${PROJECT_NAME}.Tests"
readonly VERBOSITY_LEVEL=minimal
# shellcheck disable=SC2005
readonly REPO_ROOT="$(cd "$(dirname "${0}")"; echo "$(pwd)")"

show_usage() {
    local cmdname
    cmdname=$(basename "$0")
    cat << EOF
$cmdname - project build commands

Usage: $cmdname <command>

Commands:

  ci          Build project for Continuous Integration
  cd          Build project for Continuous Deployment
  release     Build project using the release build config
  package     Build nuget package artifacts
  test        Run unit tests
  coverage    Run code coverage and generate reports
  debug       Build project using the debug build config
  benchmark   Run benchmarks
  git-clean   Use git-clean to remove any extra folders and ignored files, but NOT newly added files
  help        Show usage information and exit
EOF
}

cd_or_fail() {
    local dir
    dir=$1

    if ! cd "$dir"; then
        echo "Could not cd to $dir." >&2
        exit 1
    fi
}

cd_or_fail "${REPO_ROOT}"

clean_artifacts() {
    echo '--------------------------------------------------------'
    echo ' Cleaning artifacts directory'
    echo '--------------------------------------------------------'
    echo
    if [ -d "artifacts" ]; then
        rm -rf "artifacts"
    fi
    echo 'Done.'
    echo
}

run_git_clean() {
    echo '--------------------------------------------------------'
    echo ' Running git-clean'
    echo '--------------------------------------------------------'
    echo
    git clean --force -d -X
    echo 'Done.'
    echo
}

restore_tools() {
    echo '--------------------------------------------------------'
    echo ' Restoring dotnet tools'
    echo '--------------------------------------------------------'
    echo
    dotnet tool restore --tool-manifest .config/dotnet-tools.json --disable-parallel --verbosity ${VERBOSITY_LEVEL} --no-cache
    echo
    echo 'Done.'
    echo
}

restore_project() {
    echo '--------------------------------------------------------'
    echo ' Restoring project dependencies'
    echo '--------------------------------------------------------'
    echo
    dotnet restore --disable-parallel --verbosity ${VERBOSITY_LEVEL}
    echo
    echo 'Done.'
    echo
}

build_project() {
    local build_config
    build_config=$1

    echo '--------------------------------------------------------'
    echo " Building ${PROJECT_NAME} project"
    echo '--------------------------------------------------------'
    echo
    dotnet build --configuration "${build_config}" --verbosity ${VERBOSITY_LEVEL}
    echo
    echo 'Done.'
    echo
}

build_package() {
    local build_config
    build_config=$1

    echo '--------------------------------------------------------'
    echo " Building ${PROJECT_NAME} package"
    echo '--------------------------------------------------------'
    echo
    dotnet pack --configuration "${build_config}" --verbosity ${VERBOSITY_LEVEL} --no-restore --no-build --output artifacts/nuget "./src/${PROJECT_NAME}/${PROJECT_NAME}.csproj"
    echo
    echo 'Done.'
    echo
}

run_tests() {
    local build_config
    build_config=$1

    echo '--------------------------------------------------------'
    echo " Running ${TEST_PROJECT_NAME} unit tests"
    echo '--------------------------------------------------------'
    echo
    dotnet test --configuration "${build_config}" --no-restore --verbosity ${VERBOSITY_LEVEL}
    echo
    echo 'Done.'
    echo
}

run_coverage() {
    echo '--------------------------------------------------------'
    echo " Generating code coverage reports"
    echo '--------------------------------------------------------'
    echo
    dotnet test --configuration "${build_config}" --no-restore --verbosity ${VERBOSITY_LEVEL} --logger "trx;verbosity=detailed" --collect:"XPlat Code Coverage" --settings "./test/${TEST_PROJECT_NAME}/coverlet.runsettings" "./test/${TEST_PROJECT_NAME}/${TEST_PROJECT_NAME}.csproj"
    # Generate reports (online CLI report generator tool: https://danielpalme.github.io/ReportGenerator/usage.html)
    dotnet tool run reportgenerator "-reports:./**/coverage.cobertura.xml" "-targetdir:./artifacts/coverage" "-reporttypes:Cobertura;HtmlInline_AzurePipelines;SonarQube"
    echo
    echo 'Done.'
    echo
}

main() {
    if [ -z "$1" ]; then
        printf "Build command not specified.\n\n"
        show_usage
        exit 1
    fi

    case "$1" in
        "ci" | "-ci")
            clean_artifacts
            restore_tools
            restore_project
            build_project "Release"
            run_tests "Release"
            ;;
        "cd" | "-cd")
            echo "CD build not yet implemented." >&2
            exit 1
            ;;
        "coverage" | "--coverage" | "-coverage")
            clean_artifacts
            restore_tools
            restore_project
            build_project "Release"
            run_coverage
            ;;
        "debug" | "--debug" | "-d")
            clean_artifacts
            restore_tools
            restore_project
            build_project "Debug"
            ;;
        "release" | "--release" | "-r")
            clean_artifacts
            restore_tools
            restore_project
            build_project "Release"
            ;;
        "package" | "--package" | "-p")
            clean_artifacts
            restore_tools
            restore_project
            build_project "Release"
            build_package "Release"
            ;;
        "test" | "--test" | "-test" | "-t")
            clean_artifacts
            restore_tools
            restore_project
            build_project "Release"
            run_tests "Release"
            ;;
        "git-clean" | "--git-clean" | "-git-clean")
            run_git_clean
            ;;
        "bench" | "--bench" | "-bench" | "benchmark" | "-benchmark" | "--benchmark" | "benchmarks" | "-benchmarks" | "--benchmarks")
            echo "benchmarks are not yet implemented." >&2
            exit 1
            ;;
        "help" | "--help" | "-help" | "-h")
            show_usage
            exit 0
            ;;
        *)
            printf "'%s' is not a recognized build command.\n\n" "$1"
            show_usage
            exit 1
            ;;
    esac
    shift

    echo '========================================================'
    echo
    echo "Finished."
    echo

    exit 0
}

main "$@"
