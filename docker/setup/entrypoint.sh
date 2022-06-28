#!/usr/bin/env bash

if [[ ! $CA_PASSWORD ]]; then
  echo "CA_PASSWORD environment variable missing";
  exit 1;
fi;

if [[ ! $ELASTIC_PASSWORD ]]; then
  echo "ELASTIC_PASSWORD environment variable missing";
  exit 1;
fi;

if [[ ! $KIBANA_PASSWORD ]]; then
  echo "KIBANA_PASSWORD environment variable missing";
  exit 1;
fi;

if [[ ! $WEB_PASSWORD ]]; then
  echo "WEB_PASSWORD environment variable missing";
  exit 1;
fi;

if [[ ! -f config/certs/ca.zip ]]; then
  elasticsearch-certutil ca --silent --pem --out config/certs/ca.zip;
  unzip config/certs/ca.zip -d config/certs;
  openssl pkcs12 -export \
    -in config/certs/ca/ca.crt -inkey config/certs/ca/ca.key \
    -out config/certs/ca/ca.p12 \
    -passout "pass:$CA_PASSWORD";
  echo "Created SSL Certificate Authority";
fi;
echo "SSL Certificate Authority is:";
echo -e "$(cat config/certs/ca/ca.crt)";

if [[ -f config/certs/ca.zip && ! -f config/certs/certs.zip ]]; then
  elasticsearch-certutil cert --silent --pem \
    --in config/instances.yml \
    --ca-cert config/certs/ca/ca.crt \
    --ca-key config/certs/ca/ca.key \
    --out config/certs/certs.zip;
  unzip config/certs/certs.zip -d config/certs;
  echo "Created SSL Certificates";
fi;

chown -R root:root config/certs;
find . -type d -exec chmod 755 '{}' +;
find . -type f -exec chmod 640 '{}' +;
chmod 644 config/certs/ca/ca.crt;
chmod 644 config/certs/ca/ca.p12;
echo "Set SSL Certificate file permissions";

echo "Waiting for Elasticsearch availability...";
until curl -s \
  --cacert config/certs/ca/ca.crt \
  https://elasticsearch01:9200 | \
  grep -q "missing authentication credentials";
do sleep 1; done;

echo "Setting kibana_system password...";
until curl -s -X POST \
  --cacert config/certs/ca/ca.crt \
  -u "elastic:${ELASTIC_PASSWORD}" \
  -H "Content-Type: application/json" \
  "https://elasticsearch01:9200/_security/user/kibana_system/_password" \
  -d "{\"password\":\"${KIBANA_PASSWORD}\"}" | \
  grep -q "^{}";
do sleep 5; done;

echo "Creating roles...";
function create_role () {
  echo "Creating '$1' role...";
  local response;
  # NOTE: false means the role already exists
  until echo "$response" | grep -q -E '{"created":(true|false)}}';
  do
    if "$response"; then
      echo "\
Failed creating role '$1'. \
Got response '$response'. \
Trying again in 5 seconds...";
      sleep 5;
    fi;

    response=$(curl -s -X POST \
      --cacert config/certs/ca/ca.crt \
      -u "elastic:${ELASTIC_PASSWORD}" \
      -H "Content-Type: application/json" \
      "https://elasticsearch01:9200/_security/role/$1" \
      -d "$2");
  done;
};

create_role write_ozds '{
  "cluster": [
    "monitor",
    "manage_index_templates"
  ],
  "indices": [
    {
      "names": [
        "ozds*"
      ],
      "privileges": [
        "write",
        "create_index"
      ],
      "field_security": {
        "grant": [
          "*"
        ]
      }
    }
  ],
  "run_as": [],
  "metadata": {},
  "transient_metadata": {
    "enabled": true
  }
}';

create_role read_ozds '{
  "cluster": [
    "monitor"
  ],
  "indices": [
    {
      "names": [
        "ozds*"
      ],
      "privileges": [
        "read"
      ],
      "field_security": {
        "grant": [
          "*"
        ]
      }
    }
  ],
  "run_as": [],
  "metadata": {},
  "transient_metadata": {
    "enabled": true
  }
}';

create_role debug_ozds '{
  "cluster": [
    "monitor"
  ],
  "indices": [
    {
      "names": [
        "ozds.debug*"
      ],
      "privileges": [
        "all"
      ],
      "field_security": {
        "grant": [
          "*"
        ]
      }
    }
  ],
  "run_as": [],
  "metadata": {},
  "transient_metadata": {
    "enabled": true
  }
}';

echo "Creating users...";
function create_user() {
  echo "Creating '$1' user...";
  local response;
  # NOTE: false means the user already exists
  until echo "$response" | grep -q -E '{"created":(true|false)}';
  do
    if "$response"; then
      echo "\
Failed creating user '$1'. \
Got response '$response'. \
Trying again in 5 seconds...";
      sleep 5;
    fi;

    response=$(curl -s -X POST \
      --cacert config/certs/ca/ca.crt \
      -u "elastic:${ELASTIC_PASSWORD}" \
      -H "Content-Type: application/json" \
      "https://elasticsearch01:9200/_security/user/$1" \
      -d "$2");
  done;
};

create_user web "{
  \"username\": \"web\",
  \"password\": \"${WEB_PASSWORD}\",
  \"roles\": [
    \"read_ozds\",
    \"write_ozds\",
    \"debug_ozds\"
  ],
  \"full_name\": null,
  \"email\": null,
  \"enabled\": true
}";

echo "Setup done!";
