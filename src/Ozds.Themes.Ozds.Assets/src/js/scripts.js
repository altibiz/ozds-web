window.addEventListener("DOMContentLoaded", () => {
  let scrollPos = 0;
  const navRoot = document.getElementById("nav-root");
  const headerHeight = navRoot.clientHeight;
  window.addEventListener("scroll", function () {
    const currentTop = document.body.getBoundingClientRect().top * -1;
    if (currentTop < scrollPos) {
      // Scrolling Up
      if (currentTop > 0 && navRoot.classList.contains("is-fixed")) {
        navRoot.classList.add("is-visible");
      } else {
        navRoot.classList.remove("is-visible", "is-fixed");
      }
    } else {
      // Scrolling Down
      navRoot.classList.remove(["is-visible"]);
      if (
        currentTop > headerHeight &&
        !navRoot.classList.contains("is-fixed")
      ) {
        navRoot.classList.add("is-fixed");
      }
    }
    scrollPos = currentTop;
  });
});
