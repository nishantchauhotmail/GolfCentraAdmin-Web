function OnBookingDetailReady() {
    try {
        FillBookingTable();
     
    }
    catch (e) {
        MyAlert("OnBookingDetailReady :" + e);
        return false;
    }
}

function FillBookingTable(value) {
    SetHtml("divSearchBooking","Please Wait..");
    var url = GetDomain(_DOMAINDIVID) + "BookingDetail/GetBookingDetails" ;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divSearchBooking", data.message);
                    break;
                case -1:
                    SetHtml("divSearchBooking", data.message);
                   
                    break;
                case -2:
                    SetHtml("divSearchBooking", "SomeThing Goes Wrong");
                    
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divSearchBooking').html('<p>An Error Has Occurred</p>');
        }
    });
}


function GetBookingDetailsById(id) {
    var url = GetDomain(_DOMAINDIVID) + "BookingDetail/GetBookingByBookingId?bookingId=" + id;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("bookingPopUp", data.message);
                    break;
                case -1:
                    SetHtml("bookingPopUp", data.message);

                    break;
                case -2:
                    SetHtml("bookingPopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#bookingPopUp').html('<p>An Error Has Occurred</p>');
        }
    });

}

function CancelBookingPopUP(id) {
    var url = GetDomain(_DOMAINDIVID) + "BookingDetail/CancelBookingPopUP?bookingId=" + id ;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("bookingPopUp", data.message);

                    break;
                case -1:
                    SetHtml("bookingPopUp", data.message);

                    break;
                case -2:
                    SetHtml("bookingPopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#bookingPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}

function CancelBooking(id) {
    var url = GetDomain(_DOMAINDIVID) + "BookingDetail/CancelBooking?BookingId=" + id ;
    DisplayLoader('divLoaderPopUp');
    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader('divLoaderPopUp');
            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", data.message);
                    SetHtml("divCancelBooking", 'N/A');
                    
                    break;
                case -1:
                    SetHtml("divMessagePopUp", data.message);

                    break;
                case -2:
                    SetHtml("divMessagePopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divMessagePopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}