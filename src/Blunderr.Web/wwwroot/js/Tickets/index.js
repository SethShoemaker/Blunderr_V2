const SEARCH_PARAMS = searchParams = new URLSearchParams(window.location.search);
function ApplySearch(resetPage = true) {
    if(resetPage) SEARCH_PARAMS.set(PAGE_NUMBER_PARAM, 1);
    window.location.search = searchParams.toString();
}


const SUBMITTER_SELECTOR = document.querySelector("select#client-filter");
const SUBMITTER_PARAM = "SubmitterId";

SUBMITTER_SELECTOR.value = SEARCH_PARAMS.get(SUBMITTER_PARAM) ?? "";
SUBMITTER_SELECTOR.addEventListener("change", () => {
    searchParams.set(SUBMITTER_PARAM, SUBMITTER_SELECTOR.value);
    ApplySearch();
});


const PROJECT_SELECTOR = document.querySelector("select#project-filter");
const PROJECT_PARAM = "ProjectId";

PROJECT_SELECTOR.value = SEARCH_PARAMS.get(PROJECT_PARAM) ?? "";
PROJECT_SELECTOR.addEventListener("change", () => {
    searchParams.set(PROJECT_PARAM, PROJECT_SELECTOR.value);
    ApplySearch();
});


const DEVELOPER_SELECTOR = document.querySelector("select#developer-filter");
const DEVELOPER_PARAM = "DeveloperId";

DEVELOPER_SELECTOR.value = SEARCH_PARAMS.get(DEVELOPER_PARAM) ?? "";
DEVELOPER_SELECTOR.addEventListener("change", () => {
    searchParams.set(DEVELOPER_PARAM, DEVELOPER_SELECTOR.value);
    ApplySearch();
});


const STATUS_LINKS = document.querySelectorAll("a.status-link");
const STATUS_PARAM = "Status";
const CURRENT_STATUS = SEARCH_PARAMS.get(STATUS_PARAM);

STATUS_LINKS.forEach((element) => {

    if (element.getAttribute("data-status") == CURRENT_STATUS)
        element.classList.add("active");
    else
        element.classList.add("inactive");

    element.addEventListener("click", () => {
        let selectedStatus = element.getAttribute("data-status");
        if (selectedStatus == null)
            SEARCH_PARAMS.delete(STATUS_PARAM);
        else
            SEARCH_PARAMS.set(STATUS_PARAM, selectedStatus);
        
        ApplySearch();
    });
});


const PAGE_NUMBER_PARAM = "PageNumber";
const CURRENT_PAGE_NUMBER = parseInt(SEARCH_PARAMS.get(PAGE_NUMBER_PARAM) ?? "1");

const PAGINATION_PREV = document.querySelector("a#pagination-prev");
if(PAGINATION_PREV != null)
    PAGINATION_PREV.addEventListener("click", () => {
        SEARCH_PARAMS.set(PAGE_NUMBER_PARAM, CURRENT_PAGE_NUMBER - 1);
        ApplySearch(false);
    });

const PAGINATION_NEXT = document.querySelector("a#pagination-next");
if(PAGINATION_NEXT != null)
    PAGINATION_NEXT.addEventListener("click", () => {
        SEARCH_PARAMS.set(PAGE_NUMBER_PARAM, CURRENT_PAGE_NUMBER + 1);
        ApplySearch(false);
    });