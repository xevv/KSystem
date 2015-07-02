function showModalWindow(width, height) {
    $('#container').append('<div id="containerWindowModal" class="containerWindow"><div style="width:' + width + 'px; height:' + height + 'px" id="windowModal" class="window"></div><div class="transparent"></div></div>');
}

function showValidationMessage(message, width, height) {
    $('#container').append('<div id=containerValidationMessageWindow class="containerWindow"><div style="width:' + width + 'px; height:' + height + 'px" id="validationMessageWindow" class="window"></div><div class="transparent"></div></div>');
    $('#validationMessageWindow').html("<div class='windowHeader'>\
        <span>Ошибка</span>\
    </div>\
    <div class='windowContent'>\
    <div class=row>" + message + "</div>\
    </div>\
    <div class='bottomContainer'>\
        <a id='ok' class='button blue'>ОК</a>\
    </div>");
    $("#validationMessageWindow #ok").bind('click', function () {
        $("#containerValidationMessageWindow").remove();
    })
}

function startLoadingAnimation() {
    $("#loadContainer").show();
}
   
function stopLoadingAnimation() {
    $("#loadContainer").hide();
}