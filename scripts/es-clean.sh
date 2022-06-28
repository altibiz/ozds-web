#!/usr/bin/env bash

SCRIPT_DIR="$(dirname "$(realpath "${BASH_SOURCE[0]}")")";
ROOT_DIR="$(dirname "$SCRIPT_DIR")";

if [[ ! -f "$ROOT_DIR/certs/ca/ca.crt" ]]; then
  echo "Make sure that a CA exists at 'certs/ca/ca.crt'";
  exit 1;
fi;

if [[ ! -f "$ROOT_DIR/.env" ]]; then
  echo "Please update your '.env' file";
  exit 1;
fi;
source .env;

"$SCRIPT_DIR/es-cat.sh" 'indices/ozds.debug.*.test.*' | \
  awk '{ print "https://localhost:9200/" $3; }' | \
  xargs \
    curl -X DELETE \
      --cacert certs/ca/ca.crt \
      -u "elastic:${ELASTIC_PASSWORD}"
