function OnBookingReportBegin() {
    SetHtmlBlank("divMessage");
  
    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnBookingReportSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divReportTable", data.message);
            Enablebutton("btnSubmit");
            break;
        case -1:
            SetHtml("divReportTable", data.message);
            Enablebutton("btnSubmit");
            break;
        case -2:
            SetHtml("divReportTable", "SomeThing Goes Wrong");
            Enablebutton("btnSubmit");
            break;
        case -99:
            LogoutOperation();
            break;
    }
}

function GetBookingDetailsById(id) {
    SetCssToBody();
    var url = GetDomain(_DOMAINDIVID) + "DetailReporting/GetBookingByBookingId?bookingId=" + id;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divBookingPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divBookingPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divBookingPopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divBookingPopUp').html('<p>An Error Has Occurred</p>');
        }
    });

}