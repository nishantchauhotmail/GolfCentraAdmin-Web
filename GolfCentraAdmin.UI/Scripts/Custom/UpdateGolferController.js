function OnUpdateGolferReady() {

}




function OnGolferUpdateBegin() {
    SetHtmlBlank("divMessage");

    if (Validate.StringValueValidate("txtFirstName", "divMessage", "Please Enter First Name.")) { }
    else { return false; }




    if (Validate.StringValueValidate("txtMemberShipId", "divMessage", "Please Enter MemberShipId")) { }
    else { return false; }

           




    if (Validate.StringValueValidate("txtMobileNo", "divMessage", "Please Enter Mobile No.")) { }
    else { return false; }

    if (Validate.StringValueValidate("txtEmail", "divMessage", "Please Enter EmailId.")) { }
    else { return false; }
    if (!isEmail($("#txtEmail").val())) {
        $("#divMessage").html("Email Is Not Correct.");
        return false;
    }
    

    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OngolferUpdateSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessage", "Updatated Successfully");
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