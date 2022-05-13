#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

export ASPNETCORE_ENVIRONMENT=Development;
export DOTNET_ENVIRONMENT=Development;

echo -e "
[OZDS] Tesing with 'dotnet' in development...
";
if [[ "$1" ]]; then
  dotnet test \
    --configuration Debug \
    --filter "FullyQualifiedName~$1";
else
  dotnet test \
    --configuration Debug;
fi;
