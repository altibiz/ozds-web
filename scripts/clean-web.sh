#!/usr/bin/env sh

SCRIPT_DIR="$(dirname "$(realpath "$0")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export ORCHARD_APP_DATA="$ROOT_DIR/App_Data";

printf "\n[OZDS] Cleaning '%s'...\n" "$ORCHARD_APP_DATA";
rm -rf "$ORCHARD_APP_DATA";
