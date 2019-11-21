function OnChanegePasswordBegin() {
    SetHtmlBlank("divMessage");
    if (Validate.StringValueValidate("txtOldPassword", "divMessage", "Please Enter Old Password.")) { }
    else { return false; }
    if (Validate.StringValueValidate("txtPassword", "divMessage", "Please Enter New Password.")) { }
    else { return false; }
    if (Validate.IntValueValidate("txtConfirmPassword", "divMessage", "Please Enter Confirm Password.")) { }
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
    if ($("#txtPassword").val() != $("#txtConfirmPassword").val()) {
        SetHtml("divMessage", "Confirm Password Does Not Match New Password.");
        return false;
    }
   


    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnChangePasswordSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessage", "Password Has Been Updated, Successfully. You Will Be Redirect To Login In 2 Seconds.");
            setTimeout(function () {
                window.location = GetDomain(_DOMAINDIVID) + "Logout/index";
            }, 2000);

        
            break;
        case -1:
            SetHtml("divMessage", data.message);
            Enablebutton("btnSubmit");
            break;
        case -2:
            SetHtml("divMessage", "SomeThing Goes Wrong");
            Enablebutton("btnSubmit");
            break;
        case -3:
            SetHtml("divMessage", "Invalid Try. Please Go To Login Page");
            Enablebutton("btnSubmit");
            break;
        case -99:
            LogoutOperation();
            break;
    }
}
