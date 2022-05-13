#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";

"$SCRIPT_DIR/clean" -f;
# NOTE: build before because the dotnet watcher needs to realize there are
# NOTE: files to be watched in wwwroot
"$SCRIPT_DIR/build";
"$SCRIPT_DIR/watch-debug";
