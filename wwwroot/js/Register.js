$(document).ready(() => {
    $("#RegisterButton").click(() => {
        var formdata = new FormData();
        formdata.append("Name", $("#Name").val());
        formdata.append("Email", $("#Email").val());
        formdata.append("CID", $("#CID").val());
        formdata.append("Number", $("#Number").val());
        $.ajax({
            type: "POST",
            url: "/Register/Register",
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