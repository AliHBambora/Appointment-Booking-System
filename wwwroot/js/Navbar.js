$(document).ready(() => {
    $("#LogOut").click(() => {
        Swal.fire({
            text: 'Are you sure you want to logout?',
            icon: 'question',
            showCancelButton: true,
            showConfirmButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: "No"
        }).then((result) => {
            if (result.isConfirmed) {
                $("#loader").show();
                $.ajax({
                    type: "GET",
                    url: "/Home/LogOut",
                    success: function (data) {
                        if (data.status == "success") {
                            toastr.success('Logged out successfully');
                            location.replace("/Login");
                        }
                        else {
                            toastr.error("Failed to log out");
                            $("#loader").hide();
                        }

                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(jqXHR, textStatus, errorThrown);
                    }
                });
            }
        })
    })
})