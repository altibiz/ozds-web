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
- [`node@17.9.0`](https://nodejs.org/en/)
- [`yarn@3.2.0`](https://yarnpkg.com/)

## Development

The development process mostly involves pulling changes,
running [scripts](scripts), changing source and test files, committing, and
pushing changes.

To start the development process locally, follow these steps:

1. Run the [prepare script](scripts/prepare) to setup `git hooks`, install
   dependencies and generate the [secrets file](secrets.json).
2. Populate the generated [secrets file](secrets.json).
3. Run the [set-secrets script](scripts/set-secrets) to set
   `dotnet user-secrets`. You can run the
   [list-secrets script](scripts/list-secrets) to make sure that your secrets
   are properly stored.
4. Run the [watch-debug script](scripts/watch-debug) to start
   [the development server](https://localhost:5001),
   [the browser-sync server](http://localhost:3000), and to start file
   watchers for hot reload. Open [the site](https://localhost:5001) if it
   didn't automatically open.

### Test users

Two test users are added in the development environment to make testing
easier.

#### TestOwner

- Id: 1001
- Username: 'TestOwner'
- Email: 'test-owner@helb.hr'
- Password: 'wiQm8E0iXLYCWRWjpW74zRSsC3Z4YYTq'

#### TestMember

- Id: 1002
- Username: 'TestMember'
- Email: 'test-member@helb.hr'
- Password: '8JW4aIGqbTrHOSQZz1hCUgn3qeTBza9z'

## TODO

- fix indices that read bags
- fix taxonomies
