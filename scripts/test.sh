#!/usr/bin/env sh

export ASPNETCORE_ENVIRONMENT=Development;
export DOTNET_ENVIRONMENT=Development;

printf "[OZDS] Tesing with 'dotnet' in development...\n";
if [ "$1" ]; then
  dotnet test \
    --configuration Debug \
    --filter "FullyQualifiedName~$1";
else
  dotnet test \
    --configuration Debug;
fi;
