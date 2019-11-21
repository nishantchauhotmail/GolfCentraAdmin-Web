function OnUpdatePricingReady() {
   
}

function OnUpdatePricingBegin() {
    SetHtmlBlank("divMessage");
    if (Validate.StringValueValidate("txtNoOfAllowedSession", "divMessage", "Please Enter No Of Session.")) { }
    else { return false; }
    if (Validate.StringValueValidate("txtStartDate", "divMessage", "Please Enter Start Date.")) { }
    else { return false; }
    if (Validate.StringValueValidate("txtEndDate", "divMessage", "Please Enter End Date.")) { }
    else { return false; }

    var value = $("#ddlSlot option:selected").val();
    if (value == "") {
        SetHtml("divMessage", "Please Select Slot ");
        return;
    }
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
