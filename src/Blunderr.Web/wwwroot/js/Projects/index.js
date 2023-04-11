const CLIENT_SELECTOR = document.querySelector("select#client-filter");
const CLIENT_PARAM = "ClientId";

CLIENT_SELECTOR.value = SEARCH_PARAMS.get(CLIENT_PARAM) ?? "";
CLIENT_SELECTOR.addEventListener("change", () => {
    searchParams.set(CLIENT_PARAM, CLIENT_SELECTOR.value);
    ApplySearch();
});