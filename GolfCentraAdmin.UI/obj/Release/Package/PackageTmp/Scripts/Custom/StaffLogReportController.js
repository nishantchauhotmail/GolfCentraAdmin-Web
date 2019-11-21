
function OnStaffReportBegin() {
    SetHtmlBlank("divMessage");

    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnStaffReportSuccess(data) {
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