#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export ASPNETCORE_ENVIRONMENT=Development;
export DOTNET_ENVIRONMENT=Development;
export ORCHARD_APP_DATA="$ROOT_DIR/App_Data";
export DOTNET_WATCH_RESTART_ON_RUDE_EDIT=1;

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="development";

function stop() {
  echo -e "
[OZDS] Giving everything 1 second to stop properly...
";
  sleep 1s;
}
trap 'stop' SIGINT;

echo -e "
[OZDS] Running 'yarn' and 'dotnet' test watchers concurrently in development...
";
echo "
yarn --cwd '$ROOT_DIR' start
dotnet watch test \
  --configuration Debug \
  --property:consoleLoggerParameters=ErrorsOnly \
  $@
" | xargs -P2 -IR /usr/bin/env bash -c R;