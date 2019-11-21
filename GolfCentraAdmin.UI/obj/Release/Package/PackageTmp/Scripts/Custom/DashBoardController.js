function OnDashBoardReady() {
    try {
        FillRecentBookingTable(10);
        FillTopBar();
    }
    catch (e) {
        MyAlert("OnDashBoardReady :" + e);
        return false;
    }
}

function FillRecentBookingTable(value) {
    var url = GetDomain(_DOMAINDIVID) + "DashBoard/GetRecentBookingByTake?noOfRecord=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divRecentBooking", data.message);
                    break;
                case -1:
                    SetHtml("divRecentBooking", data.message);
                    Enablebutton("btnSubmit");
                    break;
                case -2:
                    SetHtml("divRecentBooking", "SomeThing Goes Wrong");
                    Enablebutton("btnSubmit");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divRecentBooking').html('<p>An Error Has Occurred</p>');
        }
    });
}

function FillTopBar() {
    var url = GetDomain(_DOMAINDIVID) + "DashBoard/GetDataForDashboardTopBar";

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divNoOfBooking", data.message.TotalBooking);
                    SetHtml("divHole9Count", data.message.Hole9Booking);
                    SetHtml("divHole18Count", data.message.Hole18Booking);
                    SetHtml("divHole27Count", data.message.Hole27Booking);
                    SetHtml("divTotalAmount", data.message.TotalAmount);
                    break;
                case -1:
                   // SetHtml("bookingPopUp", data.message);

                    break;
                case -2:
                  //  SetHtml("bookingPopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
           // $('#bookingPopUp').html('<p>An Error Has Occurred</p>');
        }
    });

}

function GetBookingDetailsById(id) {
    SetCssToBody();
    var url = GetDomain(_DOMAINDIVID) + "DashBoard/GetBookingByBookingId?bookingId=" + id;

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