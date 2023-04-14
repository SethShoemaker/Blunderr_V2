const ROLE_SELECTOR = document.querySelector("select#role-filter");
const ROLE_PARAM = "Role";

ROLE_SELECTOR.value = SEARCH_PARAMS.get(ROLE_PARAM) ?? "";
ROLE_SELECTOR.addEventListener("change", () => {
    searchParams.set(ROLE_PARAM, ROLE_SELECTOR.value);
    ApplySearch();
});