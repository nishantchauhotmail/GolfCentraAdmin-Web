﻿function OnUpdatePricingBegin() {
    SetHtmlBlank("divMessage");
    if (Validate.StringValueValidate("txtNoOfAllowedSession", "divMessage", "Please Enter No Of Session.")) { }
    else { return false; }

    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnUpdatePricingSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessage", "Saved Successfully");
            FillUpdatePricingTable();
            break;
        case -1:
            SetHtml("divMessage", data.message);
            Enablebutton("btnSubmit");
            break;
        case -2:
            SetHtml("divMessage", "SomeThing Goes Wrong");
            Enablebutton("btnSubmit");
            break;
        case -99:
            LogoutOperation();
            break;
    }
}
