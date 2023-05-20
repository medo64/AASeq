#!/bin/bash
BASE_DIRECTORY="$( cd "$(dirname "$0")" >/dev/null 2>&1 ; pwd -P )"

PROJECT_FILE="$BASE_DIRECTORY/src/AASeq/AASeq.csproj"
TEST_PROJECT_FILE="$BASE_DIRECTORY/tests/AASeq.Core.Tests/AASeq.Core.Tests.csproj"
PACKAGE_CONTENT_FILES="Makefile Make.sh CONTRIBUTING.md ICON.png LICENSE.md README.md .editorconfig"
PACKAGE_CONTENT_DIRECTORIES="src/ tests/ examples/"


if [ -t 1 ]; then
    ANSI_RESET="$(tput sgr0)"
    ANSI_UNDERLINE="$(tput smul)"
    ANSI_RED="`[ $(tput colors) -ge 16 ] && tput setaf 9 || tput setaf 1 bold`"
    ANSI_GREEN="`[ $(tput colors) -ge 16 ] && tput setaf 10 || tput setaf 2 bold`"
    ANSI_YELLOW="`[ $(tput colors) -ge 16 ] && tput setaf 11 || tput setaf 3 bold`"
    ANSI_CYAN="`[ $(tput colors) -ge 16 ] && tput setaf 14 || tput setaf 6 bold`"
    ANSI_WHITE="`[ $(tput colors) -ge 16 ] && tput setaf 15 || tput setaf 7 bold`"
fi

while getopts ":h" OPT; do
    case $OPT in
        h)
            echo
            echo    "  SYNOPSIS"
            echo -e "  $(basename "$0") [${ANSI_UNDERLINE}operation${ANSI_RESET}]"
            echo
            echo -e "    ${ANSI_UNDERLINE}operation${ANSI_RESET}"
            echo    "    Operation to perform."
            echo
            echo    "  DESCRIPTION"
            echo    "  Make script compatible with both Windows and Linux."
            echo
            echo    "  SAMPLES"
            echo    "  $(basename "$0")"
            echo    "  $(basename "$0") dist"
            echo
            exit 0
        ;;

        \?) echo "${ANSI_RED}Invalid option: -$OPTARG!${ANSI_RESET}" >&2 ; exit 1 ;;
        :)  echo "${ANSI_RED}Option -$OPTARG requires an argument!${ANSI_RESET}" >&2 ; exit 1 ;;
    esac
done

trap "exit 255" SIGHUP SIGINT SIGQUIT SIGPIPE SIGTERM
trap "echo -n \"$ANSI_RESET\"" EXIT


if ! command -v dotnet >/dev/null; then
    echo "${ANSI_RED}No dotnet found!${ANSI_RESET}" >&2
    exit 1
fi
echo ".NET `dotnet --version`"

if [[ "$PROJECT_FILE" == "" ]]; then
    echo "${ANSI_RED}No project file found!${ANSI_RESET}" >&2
    exit 1
fi

PACKAGE_ID=`cat "$PROJECT_FILE" | grep "<PackageId>" | sed 's^</\?PackageId>^^g' | xargs`
PACKAGE_VERSION=`cat "$PROJECT_FILE" | grep "<Version>" | sed 's^</\?Version>^^g' | xargs`
PACKAGE_FRAMEWORKS=`cat "$PROJECT_FILE" | grep "<TargetFramework" | sed 's^</\?TargetFrameworks\?>^^g' | tr ';' ' ' | xargs`


function clean() {
    rm -r "$BASE_DIRECTORY/bin/" 2>/dev/null
    rm -r "$BASE_DIRECTORY/build/" 2>/dev/null
    find "$BASE_DIRECTORY/src" -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} + 2>/dev/null
    find "$BASE_DIRECTORY/tests" -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} + 2>/dev/null
    find "$BASE_DIRECTORY/examples" -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} + 2>/dev/null
    return 0
}

function distclean() {
    rm -r "$BASE_DIRECTORY/dist/" 2>/dev/null
    rm -r "$BASE_DIRECTORY/target/" 2>/dev/null
    return 0
}

function dist() {
    echo
    DIST_DIRECTORY="$BASE_DIRECTORY/build/dist"
    DIST_SUBDIRECTORY="$DIST_DIRECTORY/$PACKAGE_ID-$PACKAGE_VERSION"
    DIST_FILE=
    rm -r "$DIST_SUBDIRECTORY/" 2>/dev/null
    mkdir -p "$DIST_SUBDIRECTORY/"
    for DIRECTORY in $PACKAGE_CONTENT_FILES $PACKAGE_CONTENT_DIRECTORIES; do
        cp -r "$BASE_DIRECTORY/$DIRECTORY" "$DIST_SUBDIRECTORY/"
    done
    find "$DIST_SUBDIRECTORY/" -name ".vs" -type d -exec rm -rf {} \; 2>/dev/null
    find "$DIST_SUBDIRECTORY/" -name "bin" -type d -exec rm -rf {} \; 2>/dev/null
    find "$DIST_SUBDIRECTORY/" -name "obj" -type d -exec rm -rf {} \; 2>/dev/null
    find "$DIST_SUBDIRECTORY/" -name "TestResults" -type d -exec rm -rf {} \; 2>/dev/null
    tar -cz -C "$BASE_DIRECTORY/build/dist/" \
        --owner=0 --group=0 \
        -f "$DIST_SUBDIRECTORY.tar.gz" \
        "$PACKAGE_ID-$PACKAGE_VERSION/" || return 1
    mkdir -p "$BASE_DIRECTORY/dist/"
    mv "$DIST_SUBDIRECTORY.tar.gz" "$BASE_DIRECTORY/dist/" || return 1
    echo "${ANSI_GREEN}Output at ${ANSI_CYAN}dist/$PACKAGE_ID-$PACKAGE_VERSION.tar.gz${ANSI_RESET}"
    return 0
}

function debug() {
    echo
    mkdir -p "$BASE_DIRECTORY/bin/"
    mkdir -p "$BASE_DIRECTORY/build/debug/"
    dotnet build "$PROJECT_FILE" \
                 --configuration "Debug" \
                 --verbosity "minimal" \
                 || return 1
    ATLEAST_ONE_COPY=0
    for FRAMEWORK in $PACKAGE_FRAMEWORKS; do
        cp -r "$BASE_DIRECTORY/src/bin/Debug/$FRAMEWORK/" "$BASE_DIRECTORY/bin/" 2>/dev/null
        if [[ $? -eq 0 ]]; then ATLEAST_ONE_COPY=1; fi
    done
    if [[ "$ATLEAST_ONE_COPY" -eq 0 ]]; then return 1; fi
    echo
    echo "${ANSI_GREEN}Output in ${ANSI_CYAN}bin/${ANSI_RESET}"
}

function release() {
    echo
    if [[ `shell git status -s 2>/dev/null | wc -l` -gt 0 ]]; then
        echo "${ANSI_YELLOW}Uncommited changes present.${ANSI_RESET}" >&2
    fi
    mkdir -p "$BASE_DIRECTORY/bin/"
    mkdir -p "$BASE_DIRECTORY/build/release/"
    dotnet build "$PROJECT_FILE" \
                 --configuration "Release" \
                 --verbosity "minimal" \
                 || return 1
    ATLEAST_ONE_COPY=0
    for FRAMEWORK in $PACKAGE_FRAMEWORKS; do
        cp -r "$BASE_DIRECTORY/src/bin/Release/$FRAMEWORK/" "$BASE_DIRECTORY/bin/" 2>/dev/null
        if [[ $? -eq 0 ]]; then ATLEAST_ONE_COPY=1; fi
    done
    if [[ "$ATLEAST_ONE_COPY" -eq 0 ]]; then return 1; fi
    echo
    echo "${ANSI_GREEN}Output in ${ANSI_CYAN}bin/${ANSI_RESET}"
}

function test() {
    echo
    if [[ "$TEST_PROJECT_FILE" == "" ]]; then
        echo "${ANSI_RED}No test project file found!${ANSI_RESET}" >&2
        exit 1
    fi
    mkdir -p "$BASE_DIRECTORY/build/test/"
    dotnet test "$TEST_PROJECT_FILE" \
                --configuration "Debug" \
                --verbosity "minimal" \
                || return 1
    echo
    echo "${ANSI_GREEN}Testing completed${ANSI_RESET}"
}


while [ $# -gt 0 ]; do
    OPERATION="$1"
    case "$OPERATION" in
        all)        clean || break ;;
        clean)      clean || break ;;
        distclean)  distclean || break ;;
        dist)       dist || break ;;
        debug)      clean && debug || break ;;
        release)    clean && release || break ;;
        test)       clean && test || break ;;

        *)  echo "${ANSI_RED}Unknown operation '$OPERATION'!${ANSI_RESET}" >&2 ; exit 1 ;;
    esac

    shift
done

if [[ "$1" != "" ]]; then
    echo "${ANSI_RED}Error performing '$OPERATION' operation!${ANSI_RESET}" >&2
    exit 1
fi
