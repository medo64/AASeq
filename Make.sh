#!/bin/bash

if [ -t 1 ]; then
    ANSI_RESET="$(tput sgr0)"
    ANSI_UNDERLINE="$(tput smul)"
    ANSI_RED="$(tput setaf 1)$(tput bold)"
    ANSI_YELLOW="$(tput setaf 3)$(tput bold)"
    ANSI_CYAN="$(tput setaf 6)$(tput bold)"
    ANSI_WHITE="$(tput setaf 7)$(tput bold)"
fi

while getopts ":h" OPT; do
    case $OPT in
        h)
            echo
            echo    "  SYNOPSIS"
            echo -e "  $(basename "$0") [${ANSI_UNDERLINE}targets${ANSI_RESET}]"
            echo
            echo -e "    ${ANSI_UNDERLINE}targets${ANSI_RESET}"
            echo    "    Targets to make. The following targests are available:"
            echo    "        clean, distclean dist, release, debug, test"
            echo
            echo    "  DESCRIPTION"
            echo    "  Make script compatible with both Windows and Linux."
            echo
            echo    "  SAMPLES"
            echo    "  $(basename "$0")"
            echo    "  $(basename "$0") all"
            echo
            exit 0
        ;;

        \?) echo "${ANSI_RED}Invalid option: -$OPTARG!${ANSI_RESET}" >&2 ; exit 1 ;;
        :)  echo "${ANSI_RED}Option -$OPTARG requires an argument!${ANSI_RESET}" >&2 ; exit 1 ;;
    esac
done

if ! command -v dotnet >/dev/null; then
    echo "${ANSI_RED}No dotnet found!${ANSI_RESET}" >&2
    exit 1
fi

trap "exit 255" SIGHUP SIGINT SIGQUIT SIGPIPE SIGTERM
trap "echo -n \"$ANSI_RESET\"" EXIT

BASE_DIRECTORY="$( cd "$(dirname "$0")" >/dev/null 2>&1 ; pwd -P )"



function clean() {
    rm -r "$BASE_DIRECTORY/bin/" 2>/dev/null
    rm -r "$BASE_DIRECTORY/build/" 2>/dev/null
    rm -r "$BASE_DIRECTORY/src/**/bin/" 2>/dev/null
    rm -r "$BASE_DIRECTORY/src/**/obj/" 2>/dev/null
    rm -r "$BASE_DIRECTORY/test/**/obj/" 2>/dev/null
    rm -r "$BASE_DIRECTORY/docs/web/out" 2>/dev/null
    return 0
}

function distclean() {
    clean
    rm -r "$BASE_DIRECTORY/dist/" 2>/dev/null
    rm -r "$BASE_DIRECTORY/target/" 2>/dev/null
    return 0
}

function dist() {
    DIST_DIRECTORY="$BASE_DIRECTORY/build/dist/$PACKAGE_ID-$PACKAGE_VERSION"
    DIST_FILE=
    rm -r "$DIST_DIRECTORY/" 2>/dev/null
    mkdir -p "$DIST_DIRECTORY/"
    for DIRECTORY in "Makefile" "Make.sh" "CONTRIBUTING.md" "ICON.png" "LICENSE.md" "README.md" "src" "test"; do
        cp -r "$BASE_DIRECTORY/$DIRECTORY" "$DIST_DIRECTORY/"
    done
    find "$DIST_DIRECTORY/src/" -name ".vs" -type d -exec rm -rf {} \; 2>/dev/null
    find "$DIST_DIRECTORY/src/" -name "bin" -type d -exec rm -rf {} \; 2>/dev/null
    find "$DIST_DIRECTORY/obj/" -name "bin" -type d -exec rm -rf {} \; 2>/dev/null
    tar -cz -C "$BASE_DIRECTORY/build/dist/" \
        --owner=0 --group=0 \
        -f "$DIST_DIRECTORY.tar.gz" \
        "$PACKAGE_ID-$PACKAGE_VERSION/" || return 1
    mkdir -p "$BASE_DIRECTORY/dist/"
    mv "$DIST_DIRECTORY.tar.gz" "$BASE_DIRECTORY/dist/" || return 1
    echo "${ANSI_CYAN}Output at 'dist/$PACKAGE_ID-$PACKAGE_VERSION.tar.gz'${ANSI_RESET}"
    return 0
}

function debug() {
    mkdir -p "$BASE_DIRECTORY/bin/"
    mkdir -p "$BASE_DIRECTORY/build/debug/"
    dotnet build "$BASE_DIRECTORY/src/AASeq.sln" \
                 --configuration "Debug" \
                 --output "$BASE_DIRECTORY/build/debug/" \
                 --verbosity "minimal" \
                 || return 1
    find "$BASE_DIRECTORY/build/debug/" -name "AASeq*.exe" -exec cp "{}" "$BASE_DIRECTORY/bin/" \; || return 1
    find "$BASE_DIRECTORY/build/debug/" -name "AASeq*.dll" -exec cp "{}" "$BASE_DIRECTORY/bin/" \; || return 1
    find "$BASE_DIRECTORY/build/debug/" -name "AASeq*.pdb" -exec cp "{}" "$BASE_DIRECTORY/bin/" \; || return 1
    echo "${ANSI_CYAN}Output in 'bin/'${ANSI_RESET}"
}

function release() {
    if [[ `shell git status -s 2>/dev/null | wc -l` -gt 0 ]]; then
        echo "${ANSI_YELLOW}Uncommited changes present.${ANSI_RESET}" >&2
    fi
    mkdir -p "$BASE_DIRECTORY/bin/"
    mkdir -p "$BASE_DIRECTORY/build/release/"
    dotnet build "$BASE_DIRECTORY/src/AASeq.sln" \
                 --configuration "Release" \
                 --output "$BASE_DIRECTORY/build/release/" \
                 --verbosity "minimal" \
                 || return 1
    find "$BASE_DIRECTORY/build/release/" -name "AASeq*.exe" -exec cp "{}" "$BASE_DIRECTORY/bin/" \; || return 1
    find "$BASE_DIRECTORY/build/release/" -name "AASeq*.dll" -exec cp "{}" "$BASE_DIRECTORY/bin/" \; || return 1
    find "$BASE_DIRECTORY/build/release/" -name "AASeq*.pdb" -exec cp "{}" "$BASE_DIRECTORY/bin/" \; || return 1
    echo "${ANSI_CYAN}Output in 'bin/'${ANSI_RESET}"
}

function test() {
    mkdir -p "$BASE_DIRECTORY/build/test/"
    echo ".NET `dotnet --version`"
    dotnet test "$BASE_DIRECTORY/src/AASeq.sln" \
                --configuration "Debug" \
                --output "$BASE_DIRECTORY/build/test/" \
                --verbosity "minimal" \
                || return 1
}

function web() {
    py -m mkdocs build -f docs/web/mkdocs.yml || return 1
}


PACKAGE_ID=`cat "$BASE_DIRECTORY/src/AASeq/AASeq.csproj" | grep "<PackageId>" | sed 's^</\?PackageId>^^g' | xargs`
PACKAGE_VERSION=`cat "$BASE_DIRECTORY/src/AASeq/AASeq.csproj" | grep "<Version>" | sed 's^</\?Version>^^g' | xargs`

while [ $# -gt 0 ]; do
    OPERATION="$1"
    case "$OPERATION" in
        all)        clean && release || break ;;
        clean)      clean || break ;;
        distclean)  distclean || break ;;
        dist)       dist || break ;;
        debug)      debug || break ;;
        release)    release || break ;;
        test)       test || break ;;
        web)        web || break ;;

        *)  echo "${ANSI_RED}Unknown operation '$OPERATION'!${ANSI_RESET}" >&2 ; exit 1 ;;
    esac

    shift
done

if [[ "$1" != "" ]]; then
    echo "${ANSI_RED}Error performing '$OPERATION' operation!${ANSI_RESET}" >&2
    exit 1
fi
