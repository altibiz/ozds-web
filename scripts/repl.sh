#!/usr/bin/env sh

SCRIPT_DIR="$(dirname "$(realpath "$0")")";

"$SCRIPT_DIR/clean.sh" -f;
# NOTE: build before because the dotnet watcher needs to realize there are
# NOTE: files to be watched in wwwroot
"$SCRIPT_DIR/build.sh";
"$SCRIPT_DIR/watch-debug.sh";
