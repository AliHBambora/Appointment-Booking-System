$(document).ready(() => {
    $("#loader").hide();
    $("#LoginButton").click(() => {
        $("#loader").show();
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
                if (data.status == "success") {
                    toastr.success('Login successful');
                    //sessionStorage.setItem("token", data.data);
                    sessionStorage.setItem("userID", data.data);
                    window.location.replace("/Appointment");
                }
                else {
                    toastr.error(data.errorMessage);
                    $("#loader").hide();
                }
                
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(jqXHR, textStatus, errorThrown);
            }
        });
    })
})