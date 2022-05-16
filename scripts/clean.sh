#!/usr/bin/env bash

echo -e "
[OZDS] Cleaning artifacts...
";
git clean -Xd \
  -e '!.husky/' \
  -e '!.husky/_/' \
  -e '!.husky/_/*' \
  -e '!.yarn/**' \
  -e '!.pnp.cjs' \
  -e '!.pnp.loader.mjs' \
  -e '!secrets.json' \
  -e '!secrets.sh' \
  -e '!.vs/' \
  -e '!.vs/**' \
  -e '!**/*.csproj.user' \
  -e '!.vscode/**' \
  $@;
