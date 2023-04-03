function togglePopup(popupId, backdropIsClear = false) {
    let popupClassList = document.getElementById(popupId).classList;

    if (popupClassList.contains("popup-inactive")) {
        popupClassList.remove("popup-inactive");
        popupClassList.add("popup-active");
    } else {
        popupClassList.add("popup-inactive");
        popupClassList.remove("popup-active");
    }

    let backdropClassList = document.querySelector("div#popup-backdrop").classList;

    if (backdropClassList.contains("popup-inactive")) {
        backdropClassList.remove("popup-inactive");
        backdropClassList.add("popup-active");
        if (backdropIsClear) backdropClassList.add("backdrop-clear");
    } else {
        backdropClassList.add("popup-inactive");
        backdropClassList.remove("popup-active");
        backdropClassList.remove("backdrop-clear");
    }

    document.querySelector("div#popup-backdrop").addEventListener("click", function () {
        togglePopup(popupId);
    });
}


const MORE_BUTTONS = document.querySelectorAll("img.more-button");

MORE_BUTTONS.forEach(element =>
    element.addEventListener("click", () => 
        togglePopup(element.nextElementSibling.id, true)));