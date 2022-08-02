#!/usr/bin/env sh

SCRIPTS="$(dirname "$(realpath "$0")")";
ROOT="$(dirname "$SCRIPTS")";

if [ ! -f "$ROOT/docker/certs/ca/ca.crt" ]; then
  printf "Make sure that a CA exists at 'certs/ca/ca.crt'\n";
  exit 1;
fi;

if [ ! -f "$ROOT/.env" ]; then
  printf "Please update your '.env' file\n";
  exit 1;
fi;
. "$ROOT/.env";

curl -s \
  --cacert "$ROOT/docker/certs/ca/ca.crt" \
  -u "elastic:${ELASTIC_PASSWORD}" \
  -H "Accept: application/json" \
  -H "Content-Type: application/json" \
  "https://localhost:9200/_security/$1/?pretty=true"
