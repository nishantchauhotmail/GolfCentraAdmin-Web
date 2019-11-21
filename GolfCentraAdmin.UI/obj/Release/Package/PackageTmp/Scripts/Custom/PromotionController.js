function OnPromotionAddBegin() {
    SetHtmlBlank("divMessage");

    if (Validate.StringValueValidate("txtName", "divMessage", "Please Enter  Name.")) { }
    else { return false; }
    if (Validate.StringValueValidate("txtStartDate", "divMessage", "Please Enter  Start date.")) { }
    else { return false; }
    if (Validate.StringValueValidate("txtEndDate", "divMessage", "Please Enter  End Date.")) { }
    else { return false; }

    if ($("#txtStartTime").val() != "") {
        
        var endTime = $("#txtEndTime").val(); 
        if (endTime == "") {
            SetHtml("divMessage", "Select End Time");
            return false;
        }
    }
    if (Validate.StringValueValidate("txtAmount", "divMessage", "Please Enter  Price.")) { }
    else { return false; }
    

    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnPromotionAddSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessage", "Saved Successfully");
            GetAllCouponTable();
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
