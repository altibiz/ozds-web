const concurrently = require("concurrently");

concurrently(
    [
      {
        command : "yarn node scripts/watch.js",
        name : "ozds-themes-ozds-assets scripts/watch.js",
        prefixColor : "bgBlue.bold",
      },
      {
        command :
            `yarn browser-sync --reload-delay 2000 --reload-debounce 2000 dist -w --no-online`,
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