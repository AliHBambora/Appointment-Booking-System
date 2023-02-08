$(document).ready(() => {
    $("#loader").hide();
    $('#Tooltip').tooltip();

    const availableSlots = [
        ["10:00AM", "11:30AM", "1:00PM", "2:30PM", "3:00PM", "4:00PM", "4:30PM"],
        ["9:30AM", "10:00AM", "10:30AM", "11:00AM", "11:30AM", "12:00PM", "12:30PM", "1:00PM", "1:30PM", "2:00PM"],
        ["9:45AM", "10:00AM", "10:30AM", "11:00AM", "11:30AM", "12:00PM", "12:30PM", "1:00PM", "1:30PM", "2:00PM"]
    ];
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); 
    var yyyy = today.getFullYear();

    today = dd + '/' + mm + '/' + yyyy;

    var selectedDate = today;
    var selectedTime = "";

    var calendar = new Calendar({
        id: "#color-calendar",
        calendarSize: "small",
        primaryColor: "#8b22e2",
        dateChanged: (currentDate, events) => {
            console.log(dayjs(currentDate).format('DD/MM/YYYY'));
            selectedDate = currentDate;
            var day = dayjs(currentDate).format('DD');
            var html = "";
            var arr = [];
            var bookedSlots = [];
            console.log(availableSlots);
            switch (parseInt(day) % 3) {
                case 0:
                    arr = availableSlots[2];
                    break;
                case 1:
                    arr = availableSlots[0];
                    break;
                case 2:
                    arr = availableSlots[1];
                    break;
                default:
                    break;
            };
            var formdata = new FormData();
            formdata.append("Date", dayjs(selectedDate).format('DD/MM/YYYY'));
            $.ajax({
                type: "POST",
                url: "/Appointment/Get",
                data: formdata,
                processData: false,
                contentType: false,
                cache: false,
                success: function (data) {
                    console.log(data);
                    bookedSlots = data.data;
                    arr.map((item, index) => {
                        console.log(bookedSlots);
                        console.log(item);
                        console.log(bookedSlots.includes(String(item)));
                        if (bookedSlots.includes(item)) {
                            html += `<div class="BookedSlot" id=item${index}>${item}</div>`;
                        }
                        else {
                            html += `<div class="availableSlot" id=item${index}>${item}</div>`;
                        }
                    })
                    $("#availableSlotsGrid").html(html);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(jqXHR, textStatus, errorThrown);
                }
            });
            console.log("date change", currentDate);
        },
        monthChanged: (currentDate, events) => {
            console.log("month change", dayjs(currentDate).format('DD/MM/YYYY'), events);
        }
    });

    var addclass = 'highlighted';

    $(document).on("click", ".availableSlot", function () {
        $(".availableSlot").each(function () {
            $(this).removeClass(addclass);
        });
        $(this).addClass(addclass);
        if ($("#BookingBtn").attr('class') == "DisabledButton") {
            $("#BookingBtn").attr('class', 'BookButton');
            $("#BookingBtn").prop('disabled', false);
            $('#Tooltip').tooltip('disable');

        }
        selectedTime = $(this).html();
        console.log($(this).html());
    });


    $("#BookingBtn").click(() => {
        
        Swal.fire({
            title: `Do you want to confirm your booking on ${dayjs(selectedDate).format('DD/MM/YYYY')} ${selectedTime}`,
            icon: 'question',
            showCancelButton: true,
            showConfirmButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: "No"
        }).then((result) => {
            if (result.isConfirmed) {
                $("#loader").show();
                var POSTOBJECT = {
                    BookingTime: selectedTime,
                    BookingDate: dayjs(selectedDate).format('DD/MM/YYYY')
                }
                console.log(POSTOBJECT);
                $.ajax({
                    type: "POST",
                    url: "/Appointment/Book",
                    data: POSTOBJECT,
                    success: function (data) {
                        console.log(data);
                        if (data.status == "success") {
                            toastr.success('Booking successfully made');
                            setTimeout(function () {
                                location.reload();
                            }, 3000);
                            $("#loader").hide();
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
            }
        })

    })

    $("#CancelBookButton").click(() => {
        Swal.fire({
            text: 'Do you want to cancle your booking',
            icon: 'question',
            showCancelButton: true,
            showConfirmButton: true,
            confirmButtonText: 'Ok',
            cancelButtonText: "Cancel"
        }).then((result) => {
            if (result.isConfirmed) {
                $("#loader").show();
                $.ajax({
                    type: "POST",
                    url: "/Appointment/CancleBooking",
                    success: function (data) {
                        console.log(data);
                        if (data.status == "success") {
                            toastr.success('Booking successfully cancelled');
                            setTimeout(function () {
                                location.reload();
                            }, 3000);
                            $("#loader").hide();
                        }
                        else {
                            Swal.fire({
                                icon: "error",
                                text: data.errorMessage
                            })
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

