"use strict";

const concurrently = require("concurrently");

concurrently(
    [
      {
        command : "yarn node scripts/watch.js",
        name : "scripts/watch.js",
        prefixColor : "magenta",
      },
      {
        command :
            "yarn browser-sync --reload-delay 2000 --reload-debounce 2000 " +
                "dist --watch --no-online --no-open",
        name : "browser-sync",
        prefixColor : "magenta",
      },
    ],
    {
      prefix : "name",
      killOthers : [ "failure", "success" ],
    })
    .then(success, failure);

function success() { console.log("Success"); }

function failure() { console.log("Failure"); }