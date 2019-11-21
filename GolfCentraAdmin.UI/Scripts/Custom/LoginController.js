function OnLoginBegin() {
    SetHtmlBlank("divMessage");
    if (Validate.StringValueValidate("txtUserName", "divMessage", "Please Enter UserName.")) { }
    else { return false; }
    if (Validate.StringValueValidate("txtPassword", "divMessage", "Please Enter Password.")) { }
    else { return false; }
    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnLoginSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessage", "Please Wait..");
            window.location = GetDomain(_DOMAINDIVID) + "Dashboard/Index";
            break;
        case -1:
            SetHtml("divMessage", "Invalid UserName and/or Password");
            Enablebutton("btnSubmit");
            break;
        case -2:
            SetHtml("divMessage", "SomeThing Goes Wrong");
            Enablebutton("btnSubmit");
            break;
        case -22:
            SetHtml("divMessage", "Please Wait..");
            window.location = GetDomain(_DOMAINDIVID) + "PasswordUpdate/Index";
            break;
      
    }
}