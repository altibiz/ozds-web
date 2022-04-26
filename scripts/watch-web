#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export ASPNETCORE_ENVIRONMENT=Development;
export DOTNET_ENVIRONMENT=Development;
export ORCHARD_APP_DATA="$ROOT_DIR/App_Data";
export DOTNET_WATCH_RESTART_ON_RUDE_EDIT=1;

echo -e "
[OZDS] Running 'dotnet' watcher...
";
dotnet watch run \
  --configuration Debug \
  --property:consoleLoggerParameters=ErrorsOnly \
  --project "$ROOT_DIR/src/Ozds.Web/Ozds.Web.csproj";
