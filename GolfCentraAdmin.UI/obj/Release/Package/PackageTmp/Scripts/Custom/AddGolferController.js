function OnAddGolferReady() {
    
}



function OnGolferAddBegin() {
    SetHtmlBlank("divMessage");

    if (Validate.StringValueValidate("txtFirstName", "divMessage", "Please Enter First Name.")) { }
    else { return false; }

    var memberCatId = $("#ddlMemberCat option:selected").val();
    if (memberCatId == "") {
        SetHtml("divMessage", "Please Select Member Type")

        return false;
    }
    

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
    if (Validate.StringValueValidate("txtPassword", "divMessage", "Please Enter Password")) { }
    else { return false; }
    var passValue = PasswordStrengthVaildation($("#txtPassword").val());
    if (passValue == 0) {
        SetHtml("divMessage", "Password Should Contain Minimum 8 Character.");
        return false;
    }
    if (passValue == 1) {
        SetHtml("divMessage", "Password Should Contain One Capital Character,Small Character,Number,Special Character.");
        return false;
    }
 
    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnGolferAddSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessage", "Saved Successfully");
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
