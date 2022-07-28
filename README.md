# `OZDS` Web

System for managing closed electricity distribution systems.

The system acts as a middleman between the open distribution system, owners
of closed distribution systems, and their members.

Owners of closed distribution systems can use the system to manage their
systems. The primary concern of owners is their consumption from open
distribution systems and the consumption of members of their system.

Members of closed distribution systems can use the system to view their
consumption in the system.

## Dependencies

- [`bash`](https://www.gnu.org/software/bash/)
- [`dotnet@6.0.202`](https://dotnet.microsoft.com/en-us/)
- [`yarn@3.2.0`](https://yarnpkg.com/)
- [`docker-compose@^2.6.x`](https://docker.com/)

## Development

The development process mostly involves pulling changes,
running [scripts](scripts), changing source and test files, committing, and
pushing changes.

To start the development process locally, follow these steps:

1. Copy all secrets.
2. Run the [prepare script](scripts/prepare) to setup `git hooks` and install
   dependencies.
3. Run the [set-secrets script](scripts/set-secrets) to set
   `dotnet user-secrets`. You can run the
   [list-secrets script](scripts/list-secrets.sh) to make sure that your
   secrets are properly stored.
4. Run the [watch-debug script](scripts/watch-debug.sh) to start
   [the development server](https://localhost:5001),
   [the browser-sync server](http://localhost:3000), and to start file
   watchers for hot reload. Open [the site](https://localhost:5001) if it
   didn't automatically open.
