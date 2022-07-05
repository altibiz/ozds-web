#!/usr/bin/env sh

SCRIPT_DIR="$(dirname "$(realpath "$0")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

for project in \
    "$ROOT_DIR/test/Ozds.Elasticsearch.Test/Ozds.Elasticsearch.Test.csproj" \
    "$ROOT_DIR/src/Ozds.Web/Ozds.Web.csproj" \
    ; do
  printf "\n[OZDS] Secrets for '%s':\n" "$project";
  dotnet user-secrets list --project "$project";
done
