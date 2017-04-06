// Write your Javascript code.

(function () {

    $("#toggle-sidebar").on("click", function () {
        var sidebarWrapperAndButton = $("#sidebar, #wrapper, #toggle-sidebar");
        var icon = $("#toggle-sidebar i.fa");


        sidebarWrapperAndButton.toggleClass("hide-sidebar");
        if (icon.hasClass("fa-chevron-left")) {
            icon.removeClass("fa-chevron-left");
            icon.addClass("fa-chevron-right");
        }
        else {
            icon.removeClass("fa-chevron-right");
            icon.addClass("fa-chevron-left");
        }

    });



}());