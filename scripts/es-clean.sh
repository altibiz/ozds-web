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

"$SCRIPTS/es-cat.sh" 'indices/ozds.debug.*.test.*' | \
  awk '{ print "https://localhost:9200/" $3; }' | \
  xargs \
    curl -X DELETE \
      --cacert "$ROOT/docker/certs/ca/ca.crt" \
      -u "elastic:${ELASTIC_PASSWORD}"
