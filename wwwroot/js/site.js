$("#ipAddressBtn").click(function (e) {
    var ipAddress = $('#ipAddress').val();

    if (ipAddress === 0 || ipAddress === null || ipAddress == 'undefined' || ipAddress == '') {
        alert('please enter the ip Address');
        return;
    }

    $.ajax({
        type: "GET",
        url: "/Home/GetIpGeoLocation?ip=" + ipAddress,
        data: {},
        success: function (result) {
            $('#geoLocationDiv').html(result);
        },
        error: function (result) {
            alert('error');
        }
    });
});

