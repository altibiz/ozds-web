#!/usr/bin/env sh

SCRIPT_DIR="$(dirname "$(realpath "$0")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export ASPNETCORE_ENVIRONMENT=Development;
export DOTNET_ENVIRONMENT=Development;
export ORCHARD_APP_DATA="$ROOT_DIR/App_Data";
export DOTNET_WATCH_RESTART_ON_RUDE_EDIT=1;

printf "\n[OZDS] Running 'dotnet' watcher...\n";
dotnet watch run \
  --configuration Debug \
  --property:consoleLoggerParameters=ErrorsOnly \
  --project "$ROOT_DIR/src/Ozds.Web/Ozds.Web.csproj";
