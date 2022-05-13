#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";

"$SCRIPT_DIR/clean-web";
"$SCRIPT_DIR/watch-web";
