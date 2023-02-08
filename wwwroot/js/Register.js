$(document).ready(() => {
    $("#loader").hide();
    $("#RegisterButton").click(() => {
        
        var firstName = $("#FirstName").val();
        var lastName = $("#LastName").val();
        var email = $("#Email").val();
        var CID = $("#CID").val();
        var mobileNumber = $("#Number").val();
        if (validateRegisterForm(firstName, email, CID)) {
            return;
        }
        else {
            $("#loader").show();
            var formdata = new FormData();
            formdata.append("FirstName", firstName);
            formdata.append("LastName", lastName);
            formdata.append("Email", email);
            formdata.append("CID", CID);
            formdata.append("Number", mobileNumber);

            $.ajax({
                type: "POST",
                url: "/Register/Register",
                data: formdata,
                contentType: false,
                processData: false,
                cache: false,
                success: function (data) {
                    if (data.status == "success") {
                        Swal.fire({
                            icon: "success",
                            title: "Successfully Registered",
                            text: "Use password sent to the registered email ID to login to the system",
                            showConfirmButton: true,
                            confirmButtonText: "Go to Login",
                            showCancelButton: true,
                            cancelButtonText:"Cancle"
                        }).then((result) => {
                            if (result.isConfirmed) {
                                location.replace("/Login");
                            }
                        })
                    }
                    else {
                        Swal.fire({
                            icon: "error",
                            text:data.errorMessage
                        })
                    }
                    $("#loader").hide();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(jqXHR, textStatus, errorThrown);
                }
            });
        }

    })

    function isEmail(email) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(email);
    }

    function validateRegisterForm(firstName, email, CID) {
        var result = true;
        if (firstName == "" || email == "" || CID=="") {
            Swal.fire({
                icon: "error",
                title: "Please fill all the fields"
            })
        }
        else if (CID.split('').length != 12) {
            Swal.fire({
                icon: "error",
                title: "Please enter a valid 12 digit CIVIL ID"
            })
        }
        else if (!isEmail(email)) {
            Swal.fire({
                icon: "error",
                title: "Please enter a valid email"
            })
        }
        else {
            result = false;
        }
        return result;
        
    }
})