#!/usr/bin/env sh

SCRIPTS="$(dirname "$(realpath "$0")")";

"$SCRIPTS/clean-web.sh";
# NOTE: build before because the dotnet watcher needs to realize there are
# NOTE: files to be watched in wwwroot
"$SCRIPTS/build.sh";
"$SCRIPTS/debug.sh";
