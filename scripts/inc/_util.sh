# shellcheck shell=bash

#
# bootstrapping code inspired by <https://dot.net/v1/dotnet-install.sh>

#
# Use in the the functions: eval $invocation
# shellcheck disable=2016,2034
invocation='say_verbose "Calling: ${yellow:-}${FUNCNAME[0]} ${green:-}$*${normal:-}"'

#
# standard output may be used as a return value in the functions
# we need a way to write text on the screen in the functions so that
# it won't interfere with the return value.
# Exposing stream 3 as a pipe to standard output of the script itself
exec 3>&1

#
# Setup some colors to use. These need to work in fairly limited shells,
# like the Ubuntu Docker container where there are only 8 colors.
# See if stdout is a terminal
if [ -t 1 ] && command -v tput > /dev/null; then
    # see if it supports colors
    ncolors=$(tput colors)
    # shellcheck disable=2086
    if [ -n "$ncolors" ] && [ $ncolors -ge 8 ]; then
        bold="$(tput bold || echo)"
        normal="$(tput sgr0 || echo)"
        black="$(tput setaf 0 || echo)"
        red="$(tput setaf 1 || echo)"
        green="$(tput setaf 2 || echo)"
        yellow="$(tput setaf 3 || echo)"
        blue="$(tput setaf 4 || echo)"
        magenta="$(tput setaf 5 || echo)"
        cyan="$(tput setaf 6 || echo)"
        white="$(tput setaf 7 || echo)"
    fi
fi

say_warning() {
    printf "%b\n" "[${yellow:-}$SCRIPTNAME warning${normal:-}]: $1" >&3
}

say_err() {
    printf "%b\n" "[${red:-}$SCRIPTNAME error${normal:-}]: $1" >&2
}

say() {
    # using stream 3 (defined in the beginning) to not interfere with stdout
    # of functions; which may be used as return value
    printf "%b\n" "[${cyan:-}$SCRIPTNAME${normal:-}]: $1" >&3
}

say_verbose() {
    if [ "$VERBOSE" = true ]; then
        say "$1"
    fi
}

#
# A general-purpose function to ask Yes/No questions in Bash, either with
# or without a default answer. It keeps repeating the question until it
# gets a valid answer.
#
# https://jonlabelle.com/snippets/view/shell/general-purpose-yesno-prompt-function
# https://gist.github.com/davejamesmiller/1965569
ask() {
    local prompt default reply

    if [[ ${2:-} = 'Y' ]]; then
        prompt='Y/n'
        default='Y'
    elif [[ ${2:-} = 'N' ]]; then
        prompt='y/N'
        default='N'
    else
        prompt='y/n'
        default=''
    fi

    while true; do

        # Ask the question (not using "read -p" as it uses stderr not stdout)
        echo -n "$1 [$prompt] "

        # Read the answer (use /dev/tty in case stdin is redirected from somewhere else)
        read -r reply < /dev/tty

        # Default?
        if [[ -z $reply ]]; then
            reply=$default
        fi

        # Check if the reply is valid
        case "$reply" in
            Y* | y*) return 0 ;;
            N* | n*) return 1 ;;
        esac

    done
}
