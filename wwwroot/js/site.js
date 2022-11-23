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
        beforeSend: function () { 
            $("#ipAddressBtn").prop("disabled", true);
        },
        success: function (result) {
            $('#geoLocationDiv').html(result);
            $("#ipAddressBtn").prop("disabled", false);
        },
        error: function (result) {
            alert('error');
            $("#ipAddressBtn").prop("disabled", false);
        }
    });
});

function initMap(lat,lon) {
    var canvas = $("#map");

    var latitude = parseFloat(lat);
    var longitude = parseFloat(lon);

    var latlng = new google.maps.LatLng(latitude, longitude);
    var options = {
        zoom: 7,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(canvas[0], options);

    var marker = new google.maps.Marker({
        position: new google.maps.LatLng(latitude, longitude),
        map: map
    });
}