#!/usr/bin/env sh

SCRIPTS="$(dirname "$(realpath "$0")")";
ROOT="$(dirname "$SCRIPTS")";

for project in \
    "$ROOT/test/Ozds.Elasticsearch.Test/Ozds.Elasticsearch.Test.csproj" \
    "$ROOT/src/Ozds.Web/Ozds.Web.csproj" \
    ; do
  printf "\n[OZDS] Secrets for '%s':\n" "$project";
  dotnet user-secrets list --project "$project";
done
