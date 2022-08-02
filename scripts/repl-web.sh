#!/usr/bin/env sh

SCRIPTS="$(dirname "$(realpath "$0")")";

"$SCRIPTS/clean-web.sh";
"$SCRIPTS/debug-web.sh";
