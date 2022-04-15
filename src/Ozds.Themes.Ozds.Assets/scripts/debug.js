"use strict";

const concurrently = require("concurrently");

concurrently(
    [
      {
        command : "yarn node --inspect scripts/watch.js",
        name : "scripts/watch.js",
        prefixColor : "magenta",
      },
      {
        command : "yarn browser-sync " +
                      "dist --watch --no-online --no-open",
        name : "browser-sync",
        prefixColor : "yellow",
      },
    ],
    {
      prefix : "name",
      killOthers : [ "failure", "success" ],
    })
    .then(success, failure);

function success() { console.log("Success"); }

function failure() { console.log("Failure"); }