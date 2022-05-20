/*
** NOTE: This file is generated by Gulp and should not be edited directly!
** Any changes made directly to this file will be overwritten next time its asset group is processed by Gulp.
*/

window.addEventListener("DOMContentLoaded", function () {
  var scrollPos = 0;
  var navRoot = document.getElementById("nav-root");
  var headerHeight = navRoot.clientHeight;
  window.addEventListener("scroll", function () {
    var currentTop = document.body.getBoundingClientRect().top * -1;

    if (currentTop < scrollPos) {
      // Scrolling Up
      if (currentTop > 0 && navRoot.classList.contains("is-fixed")) {
        navRoot.classList.add("is-visible");
      } else {
        console.log(123);
        navRoot.classList.remove("is-visible", "is-fixed");
      }
    } else {
      // Scrolling Down
      navRoot.classList.remove(["is-visible"]);

      if (currentTop > headerHeight && !navRoot.classList.contains("is-fixed")) {
        navRoot.classList.add("is-fixed");
      }
    }

    scrollPos = currentTop;
  });
});