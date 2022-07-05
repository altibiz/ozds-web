#!/usr/bin/env sh

SCRIPT_DIR="$(dirname "$(realpath "$0")")";

"$SCRIPT_DIR/clean-web.sh";
"$SCRIPT_DIR/watch-web.sh";
