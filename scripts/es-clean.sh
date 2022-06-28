#!/usr/bin/env bash

if [[ ! -f certs/ca/ca.crt ]]; then
  echo "Make sure that a CA exists at 'certs/ca/ca.crt'";
  exit 1;
fi;

if [[ ! -f .env ]]; then
  echo "Please update your '.env' file";
  exit 1;
fi;
source .env;

es-cat 'indices/ozds.debug.*' | \
  awk '{ print "https://localhost:9200/" $3; }' | \
  xargs \
    curl -X DELETE \
      --cacert certs/ca/ca.crt \
      -u "elastic:${ELASTIC_PASSWORD}"
