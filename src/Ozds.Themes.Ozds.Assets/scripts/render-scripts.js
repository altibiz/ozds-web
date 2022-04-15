"use strict";
const fs = require("fs");
const upath = require("upath");
const sh = require("shelljs");

module.exports = function renderScripts() {
  const sourcePath = upath.resolve(upath.dirname(__filename), "../src/js");
  const destPath = upath.resolve(upath.dirname(__filename), "../dist/.");

  console.log(
      `[ozds-themes-ozds-assets] INFO: Rendering ${sourcePath} to ${destPath}`);
  sh.cp("-R", sourcePath, destPath);

  const sourcePathScriptsJS =
      upath.resolve(upath.dirname(__filename), "../src/js/scripts.js");
  const destPathScriptsJS =
      upath.resolve(upath.dirname(__filename), "../dist/js/scripts.js");

  console.log(`[ozds-themes-ozds-assets] INFO: Rendering ${
      sourcePathScriptsJS} to ${destPathScriptsJS}`);
  const scriptsJS = fs.readFileSync(sourcePathScriptsJS);

  fs.writeFileSync(destPathScriptsJS, scriptsJS);
};