const SEARCH_PARAMS = searchParams = new URLSearchParams(window.location.search);
function ApplySearch(resetPage = true) {
    if(resetPage) SEARCH_PARAMS.set(PAGE_NUMBER_PARAM, 1);
    window.location.search = searchParams.toString();
}

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