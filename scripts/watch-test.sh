#!/usr/bin/env sh

SCRIPT_DIR="$(dirname "$(realpath "$0")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export ASPNETCORE_ENVIRONMENT=Development;
export DOTNET_ENVIRONMENT=Development;
export ORCHARD_APP_DATA="$ROOT_DIR/App_Data";
export DOTNET_WATCH_RESTART_ON_RUDE_EDIT=1;

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="development";

stop() {
  printf "\n[OZDS] Giving everything 1 second to stop properly...\n";
  sleep 1s;
}
trap 'stop' INT;

printf "\n[OZDS] Running 'yarn' and 'dotnet' test watchers...\n";
echo "
yarn --cwd '$ROOT_DIR' start; \

dotnet watch test \
  --configuration Debug \
  --property:consoleLoggerParameters=ErrorsOnly \
  $*; \

" | xargs -P2 -IR /usr/bin/env bash -c R;
