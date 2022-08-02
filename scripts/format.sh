#!/usr/bin/env sh

export NODE_OPTIONS="--no-warnings";

printf "[OZDS] Formatting with 'yarn'...\n";
yarn format;

# printf "\n[OZDS] Formatting with 'dotnet'...\n";
# dotnet format whitespace;
