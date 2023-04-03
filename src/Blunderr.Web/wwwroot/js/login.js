const EMAIL_INPUT = document.querySelector("input#email");
const PASSWORD_INPUT = document.querySelector("input#password");
const POPUP_DEMO_ACCOUNTS_ID = "popup-demo-accounts";

document.querySelector("a#demo-btn").addEventListener("click", function () {
    togglePopup(POPUP_DEMO_ACCOUNTS_ID);
});

document.querySelector("li#client").addEventListener("click", function () {
    EMAIL_INPUT.value = "Sally@Client.com";
    PASSWORD_INPUT.value = "Client1";
    togglePopup(POPUP_DEMO_ACCOUNTS_ID);
});

document.querySelector("li#dev").addEventListener("click", function () {
    EMAIL_INPUT.value = "Jamie@LoremIpsum.com";
    PASSWORD_INPUT.value = "Dev1";
    togglePopup(POPUP_DEMO_ACCOUNTS_ID);
});

document.querySelector("li#manager").addEventListener("click", function () {
    EMAIL_INPUT.value = "Jonathan@LoremIpsum.com";
    PASSWORD_INPUT.value = "Manager1";
    togglePopup(POPUP_DEMO_ACCOUNTS_ID);
});