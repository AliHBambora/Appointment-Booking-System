$(document).ready(() => {
    $("#LoginButton").click(() => {
        var formdata = new FormData();
        formdata.append("Email", $("#Email").val());
        formdata.append("Password", $("#Password").val());
        $.ajax({
            type: "POST",
            url: "/Login/Login",
            data: formdata,
            contentType: false,
            processData: false,
            cache: false,
            success: function (data) {
                console.log(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    })
})