const concurrently = require("concurrently");

concurrently(
    [
      {
        command : "yarn node --inspect scripts/watch.js",
        name : "ozds-themes-ozds-assets scripts/watch.js",
        prefixColor : "bgBlue.bold",
      },
      {
        command : `yarn browser-sync dist -w --no-online`,
        name : "ozds-themes-ozds-assets browser-sync",
        prefixColor : "bgMagenta.bold",
      },
    ],
    {
      prefix : "name",
      killOthers : [ "failure", "success" ],
    })
    .then(success, failure);

function success() { console.log("Success"); }

function failure() { console.log("Failure"); }