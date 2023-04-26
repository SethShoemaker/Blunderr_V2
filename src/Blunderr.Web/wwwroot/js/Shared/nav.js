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


const CURRENT_PATH = window.location.pathname;
const CURRENT_ROOT_PATH = CURRENT_PATH.split("/")[1];

const SIDEBAR_TABS_LIST = document.querySelector("div#sidebar ul").children;
const ROOT_PATH_ATTRIBUTE = "data-root-path";

for (let i = 0; i < SIDEBAR_TABS_LIST.length; i++) {
    let tab = SIDEBAR_TABS_LIST[i];
    let tabRootPath = tab.getAttribute(ROOT_PATH_ATTRIBUTE);
    if (tabRootPath == CURRENT_ROOT_PATH)
        tab.classList.add("current");
}

const DROPDOWN_TOGGLER = document.querySelector("nav#nav header div#right button#header-dropdown-toggler");
const DROPDOWN_ID = "header-dropdown";
DROPDOWN_TOGGLER.addEventListener("click", () => {
    togglePopup(DROPDOWN_ID, true);
});