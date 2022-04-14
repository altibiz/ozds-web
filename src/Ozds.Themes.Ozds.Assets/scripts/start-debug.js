const concurrently = require("concurrently");

concurrently(
    [
      {
        command : "yarn node --inspect scripts/sb-watch.js",
        name : "SB_WATCH",
        prefixColor : "bgBlue.bold",
      },
      {
        command : `yarn browser-sync dist -w --no-online`,
        name : "SB_BROWSER_SYNC",
        prefixColor : "bgBlue.bold",
      },
    ],
    {
      prefix : "name",
      killOthers : [ "failure", "success" ],
    })
    .then(success, failure);

function success() { console.log("Success"); }

function failure() { console.log("Failure"); }