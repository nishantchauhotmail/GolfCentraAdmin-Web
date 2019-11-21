function OnReportReady() {
    try {
        $("#radioAll").prop("checked", true);
        $("#hdnSearchTypeId").val(1);

    }
    catch (e) {
        MyAlert("OnBookingDetailReady :" + e);
        return false;
    }
}
function GetBookingDetailsById(id) {
    var url = GetDomain(_DOMAINDIVID) + "Report/GetBookingByBookingId?bookingId=" + id;

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

function SetSearchTypeId(id) {
    $("#hdnSearchTypeId").val(id);
   
}

function OnReportBegin() {
    SetHtml("divSearchBooking", "Please Wait..");
}

function OnReportSuccess(data) {
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
}