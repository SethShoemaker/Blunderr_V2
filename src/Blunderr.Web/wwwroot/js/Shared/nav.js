const NAV_TOGGLE_BUTTON = document.querySelector("button#sidebar-toggle");
const DASHBOARD_CONTENT = document.querySelector("div#dashboard-content");
const DASHBOARD_CONTENT_EXPANDED_ATTRIBUTE_NAME = "data-sidebar-hidden";
const SIDEBAR = document.querySelector("div#sidebar");
const SIDEBAR_HIDDEN_ATTRIBUTE_NAME = "data-hidden";

NAV_TOGGLE_BUTTON.addEventListener("click", function () {
    let sidebarIsHidden = SIDEBAR.getAttribute(SIDEBAR_HIDDEN_ATTRIBUTE_NAME);

    if (sidebarIsHidden == "true") {
        SIDEBAR.setAttribute(SIDEBAR_HIDDEN_ATTRIBUTE_NAME, "false");
        DASHBOARD_CONTENT.setAttribute(DASHBOARD_CONTENT_EXPANDED_ATTRIBUTE_NAME, "false");
    } else {
        SIDEBAR.setAttribute(SIDEBAR_HIDDEN_ATTRIBUTE_NAME, "true");
        DASHBOARD_CONTENT.setAttribute(DASHBOARD_CONTENT_EXPANDED_ATTRIBUTE_NAME, "true");
    }
});

const PATH = window.location.pathname;
const SIDEBAR_TABS_LIST = document.querySelector("div#sidebar ul").children;
for (let i = 0; i < SIDEBAR_TABS_LIST.length; i++) {
    let tab = SIDEBAR_TABS_LIST[i];
    let tabRoute = tab.getAttribute("data-route");
    if (tabRoute == PATH)
        tab.classList.add("current");
}