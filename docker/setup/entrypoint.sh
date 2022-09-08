#!/usr/bin/env sh

if [ ! "$CA_PASSWORD" ]; then
  printf "CA_PASSWORD environment variable missing\n"
  exit 1
fi

if [ ! "$ELASTIC_PASSWORD" ]; then
  printf "ELASTIC_PASSWORD environment variable missing\n"
  exit 1
fi

if [ ! "$KIBANA_PASSWORD" ]; then
  printf "KIBANA_PASSWORD environment variable missing\n"
  exit 1
fi

if [ ! "$APP_PASSWORD" ]; then
  printf "APP_PASSWORD environment variable missing\n"
  exit 1
fi

if [ ! -f config/certs/ca.zip ]; then
  elasticsearch-certutil ca --silent --pem --out config/certs/ca.zip
  unzip config/certs/ca.zip -d config/certs
  openssl pkcs12 -export \
    -in config/certs/ca/ca.crt -inkey config/certs/ca/ca.key \
    -out config/certs/ca/ca.p12 \
    -passout "pass:$CA_PASSWORD"
  printf "Created SSL Certificate Authority\n"
fi
printf "SSL Certificate Authority is:\n"
cat config/certs/ca/ca.crt

if [ -f config/certs/ca.zip ] && [ ! -f config/certs/certs.zip ]; then
  elasticsearch-certutil cert --silent --pem \
    --in config/instances.yml \
    --ca-cert config/certs/ca/ca.crt \
    --ca-key config/certs/ca/ca.key \
    --out config/certs/certs.zip
  unzip config/certs/certs.zip -d config/certs
  printf "Created SSL Certificates\n"
fi

chown -R root:root config/certs
find . -type d -exec chmod 755 '{}' +
find . -type f -exec chmod 640 '{}' +
chmod 644 config/certs/ca/ca.crt
chmod 644 config/certs/ca/ca.p12
printf "Set SSL Certificate file permissions\n"

printf "Waiting for Elasticsearch availability...\n"
until curl -s \
  --cacert config/certs/ca/ca.crt \
  https://elasticsearch:9200 |
  grep -q "missing authentication credentials"; do sleep 1; done

printf "Setting kibana_system password...\n"
kibana_response=""
until echo "$kibana_response" | grep -Eq "^{}"; do
  echo "$kibana_response"
  if [ "$kibana_response" ]; then
    printf "Failed setting kibana password\n"
    printf "Got response '%s'.\n" "$kibana_response"
    printf "Trying again in 5 seconds...\n"
    sleep 5
  fi

  kibana_response="$(curl -s -X POST \
    --cacert config/certs/ca/ca.crt \
    -u "elastic:${ELASTIC_PASSWORD}" \
    -H "Content-Type: application/json" \
    "https://elasticsearch:9200/_security/user/kibana_system/_password" \
    -d "{\"password\":\"${KIBANA_PASSWORD}\"}")"
done

printf "Creating roles...\n"
create_role() {
  printf "Creating '%s' role...\n" "$1"

  role_response=""
  until echo "$role_response" | grep -Eq '{"created":(true|false)}}'; do
    if [ "$role_response" ]; then
      printf "Failed creating role '%s'.\n" "$1"
      printf "Got response '%s'.\n" "$role_response"
      printf "Trying again in 5 seconds...\n"
      sleep 5
    fi

    role_response=$(curl -s -X POST \
      --cacert config/certs/ca/ca.crt \
      -u "elastic:${ELASTIC_PASSWORD}" \
      -H "Content-Type: application/json" \
      "https://elasticsearch:9200/_security/role/$1" \
      -d "$2")
  done
}

create_role write_ozds '{
  "cluster": [
    "monitor",
    "manage_index_templates",
    "manage_pipeline"
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
}'

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
}'

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
}'

printf "Creating users...\n"
create_user() {
  printf "Creating '%s' user...\n" "$1"

  user_response=""
  until echo "$user_response" | grep -Eq '{"created":(true|false)}'; do
    if [ "$user_response" ]; then
      printf "Failed creating user '%s'.\n" "$1"
      printf "Got response '%s'.\n" "$user_response"
      printf "Trying again in 5 seconds...\n"
      sleep 5
    fi

    user_response=$(curl -s -X POST \
      --cacert config/certs/ca/ca.crt \
      -u "elastic:${ELASTIC_PASSWORD}" \
      -H "Content-Type: application/json" \
      "https://elasticsearch:9200/_security/user/$1" \
      -d "$2")
  done
}

create_user app "$(printf '{
  "username": "app",
  "password": "%s",
  "roles": [
    "read_ozds",
    "write_ozds",
    "debug_ozds"
  ],
  "full_name": null,
  "email": null,
  "enabled": true
}' "$APP_PASSWORD")"

printf "Creating snapshot repos...\n"
create_snapshot_repo() {
  printf "Creating '%s' snapshot repo...\n" "$1"

  if [ ! -d "/mnt/snapshots/$1" ]; then
    mkdir "/mnt/snapshots/$1"
    chown 1000:0 "/mnt/snapshots/$1"
  fi

  repo_response=""
  until echo "$repo_response" | grep -Eq '{"acknowledged":true}'; do
    if [ "$repo_response" ]; then
      printf "Failed creating snapshot repo '%s'.\n" "$1"
      printf "Got response '%s'.\n" "$repo_response"
      printf "Trying again in 5 seconds...\n"
      sleep 5
    fi

    repo_response=$(curl -s -X PUT \
      --cacert config/certs/ca/ca.crt \
      -u "elastic:${ELASTIC_PASSWORD}" \
      -H "Content-Type: application/json" \
      "https://elasticsearch:9200/_snapshot/$1?verify=true" \
      -d "$(printf '{
        "type": "fs",
        "settings": {
          "location": "%s"
        }
      }' "$1")")
  done
}

create_snapshot_repo "dev"

printf "Creating SLM policies...\n"
create_slm_policy() {
  printf "Creating '%s' SLM policy...\n" "$1"

  slm_response=""
  until echo "$slm_response" | grep -Eq '{"acknowledged":true}'; do
    if [ "$slm_response" ]; then
      printf "Failed creating SLM policy '%s'.\n" "$1"
      printf "Got response '%s'.\n" "$slm_response"
      printf "Trying again in 5 seconds...\n"
      sleep 5
    fi

    slm_response=$(curl -s -X PUT \
      --cacert config/certs/ca/ca.crt \
      -u "elastic:${ELASTIC_PASSWORD}" \
      -H "Content-Type: application/json" \
      "https://elasticsearch:9200/_slm/policy/$1" \
      -d "$2")
  done
}

create_slm_policy "dev-snapshots" '{
  "name": "<dev-snapshot-{now/d}>",
  "schedule": "0 0 0 * * ?",
  "repository": "dev",
  "config": {
    "include_global_state": false,
    "indices": "ozds.*"
  },
  "retention": {
    "expire_after": "30d",
    "min_count": 5,
    "max_count": 30
  }
}'

printf "Setup done!"
