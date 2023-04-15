const FORM = document.querySelector("div#dashboard-content form");
const FORM_TYPE = FORM.getAttribute("data-form-type");

const SELECTS = document.querySelectorAll("form div.input-container select");

if (FORM_TYPE == "create")
    for (let i = 0; i < SELECTS.length; i++){
        SELECTS[i].value = "";
        SELECTS[i].setAttribute("placeholder", "on");
    }

for (let i = 0; i < SELECTS.length; i++)
    SELECTS[i].addEventListener("change", () => 
        SELECTS[i].setAttribute("placeholder", "off"));