#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";

export Ozds__Modules__Ozds__IsDemo=1;

"$SCRIPT_DIR/clean-web.sh";
"$SCRIPT_DIR/watch-web.sh";
