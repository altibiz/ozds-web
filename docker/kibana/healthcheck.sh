#!/usr/bin/env sh

curl_result=$(curl -s -I \
  --cacert config/certs/ca/ca.crt \
  https://localhost:5601);

if ! echo "$curl_result" | grep -q "HTTP/1.1 302 Found"; then
  echo "Failed connecting to server.";
  exit 1;
fi;

exit 0;
