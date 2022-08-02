#!/usr/bin/env sh

SCRIPTS="$(dirname "$(realpath "$0")")";
ROOT="$(dirname "$SCRIPTS")";

export ORCHARD_APP_DATA="$ROOT/App_Data";

if [ -d "$ORCHARD_APP_DATA" ]; then
  printf "[OZDS] Cleaning '%s'...\n" "$ORCHARD_APP_DATA";
  rm -rf "$ORCHARD_APP_DATA";
fi;
