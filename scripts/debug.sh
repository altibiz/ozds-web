#!/usr/bin/env sh

SCRIPTS="$(dirname "$(realpath "$0")")";
ROOT="$(dirname "$SCRIPTS")";

export ASPNETCORE_ENVIRONMENT=Development;
export DOTNET_ENVIRONMENT=Development;
export ORCHARD_APP_DATA="$ROOT/App_Data";
export DOTNET_WATCH_RESTART_ON_RUDE_EDIT=1;

export NODE_OPTIONS="--no-warnings";
export NODE_ENV="development";

stop() {
  printf n[OZDS] Giving everything 1 second to stop properly...\n";
  sleep 1s;
}
trap 'stop' INT;

printf "\n[OZDS] Running 'yarn' and 'dotnet' watchers...\n";
echo "
yarn --cwd '$ROOT' debug; \

dotnet watch run \
  --configuration Debug \
  --property:consoleLoggerParameters=ErrorsOnly \
  --project '$ROOT/src/Ozds.Web/Ozds.Web.csproj'; \

" | xargs -P2 -IR /usr/bin/env sh -c R;
