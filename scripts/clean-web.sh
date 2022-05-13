#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export ORCHARD_APP_DATA="$ROOT_DIR/App_Data";

echo -e "
[OZDS] Cleaning '$ORCHARD_APP_DATA'...
";
rm -rf "$ORCHARD_APP_DATA";
