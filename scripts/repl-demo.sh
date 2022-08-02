#!/usr/bin/env sh

SCRIPTS="$(dirname "$(realpath "$0")")";

export Ozds__Modules__Ozds__IsDemo=1;

"$SCRIPTS/clean-web.sh";
"$SCRIPTS/debug-web.sh";
