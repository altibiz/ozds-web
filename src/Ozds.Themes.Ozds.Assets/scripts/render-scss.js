"use strict";

const autoprefixer = require("autoprefixer");
const fs = require("fs");
const upath = require("upath");
const postcss = require("postcss");
const sass = require("sass");
const sh = require("shelljs");

const stylesPath = upath.resolve(
  upath.dirname(__filename),
  "../src/scss/styles.scss",
);
const destPath = upath.resolve(
  upath.dirname(__filename),
  "../dist/css/styles.css",
);
const bootstrapIncludePath = upath.resolve(
  upath.dirname(__filename),
  "../../../.yarn/unplugged/bootstrap-virtual-43b2c55e14/node_modules",
);

module.exports = function renderSCSS() {
  const results = sass.compile(stylesPath, {
    loadPaths: [bootstrapIncludePath],
    quietDeps: true,
  });

  const destPathDirname = upath.dirname(destPath);
  if (!sh.test("-e", destPathDirname)) {
    sh.mkdir("-p", destPathDirname);
  }

  console.log(`[render] INFO: Rendering ${stylesPath} to ${destPath}`);
  postcss([autoprefixer])
    .process(results.css, { from: "styles.css", to: "styles.css" })
    .then((result) => {
      result.warnings().forEach(console.warn);
      fs.writeFileSync(destPath, result.css.toString());
    });
};
