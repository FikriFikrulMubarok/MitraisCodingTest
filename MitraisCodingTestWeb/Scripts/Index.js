
function initializeControls() {

    $('#txtDOB').datetimepicker();

    $('#chxMale').change(function () {
        if ($('#chxMale').is(':checked')) {
            $('#chxFemale').prop('checked', false);
            $('#chxMale').prop('checked', true);
        }
    });

    $('#chxFemale').change(function () {
        if ($('#chxFemale').is(':checked')) {
            $('#chxFemale').prop('checked', true);
            $('#chxMale').prop('checked', false);
        }
    });

    $('#btnSubmit').unbind('click').click(function () {
        if (document.getElementById('form1').checkValidity()) {
            var valMobileNumber = $('#txtMobileNumber').val();
            var valFirstName = $('#txtFirstName').val();
            var valLastName = $('#txtLastName').val();
            var valDOB = $('#txtDOB').val();
            var valGender;
            if ($('#chxMale').is(':checked'))
            { valGender = 'Male' }
            else if ($('#chxFemale').is(':checked'))
            { valGender = 'Female' }
            var valEmail = $('#txtEmail').val();

            var dataPost = {
                MobileNumber: valMobileNumber,
                FirstName: valFirstName,
                LastName: valLastName,
                DateOfBirth: valDOB,
                Gender: valGender,
                Email: valEmail
            };



            $.ajax({
                type: "POST",
                url: "http://localhost:59941/api/Registration",
                data: dataPost,
                success: function (msg) {
                    alert("Data Saved: " + msg);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("some error");
                }
            }).always(function () {
                //alert("finished");
                $('#btnLogin').show();
            });
        }
    });
}

$(function () {
    initializeControls();
    $('form1').on('submit', function (e) {
        e.preventDefault();
    });
});