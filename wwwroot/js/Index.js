

$(document).ready(function () {
    $("#loader").hide();
    $("#login").click(() => {
        $("#loader").show();
        window.location.replace("/Login");
    });
    $("#register").click(() => {
        $("#loader").show();
        window.location.replace("/Register");
    });
});

