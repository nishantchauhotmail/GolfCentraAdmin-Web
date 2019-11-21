function OnEmployeeManagementReady() {
    try {
        FillEmployeeManagementTable();

    }
    catch (e) {
        MyAlert("OnEmployeeManagementReady :" + e);
        return false;
    }
}

function FillEmployeeManagementTable() {
    var url = GetDomain(_DOMAINDIVID) + "EmployeeManagement/GetEmployeeManagementDetails";

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divResult", data.message);
                    break;
                case -1:
                    SetHtml("divResult", data.message);

                    break;
                case -2:
                    SetHtml("divResult", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divResult').html('<p>An Error Has Occurred</p>');
        }
    });
}


function OnEmployeeAddBegin() {
    SetHtmlBlank("divMessage");
    if (Validate.StringValueValidate("txtName", "divMessage", "Please Enter Name.")) { }
    else { return false; }
    if (Validate.StringValueValidate("txtEmail", "divMessage", "Please Enter Email.")) { }
    else { return false; }
    if (!isEmail($("#txtEmail").val())) {
        $("#divMessage").html("Email Is Not Correct.");
        return false;
    }
    if (Validate.StringValueValidate("txtPassword", "divMessage", "Please Enter Password.")) { }
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
  
    if (Validate.StringValueValidate("txtChangePassword", "divMessage", "Please Enter Confirm Password.")) { }
    else { return false; }

    if ($("#txtPassword").val() != $("#txtChangePassword").val()) {
        SetHtml("divMessage", "Password Does Not Match")
        return false;
    }



    if (Validate.IntValueValidate("ddlGenderType", "divMessage", "Please Select Gender Type.")) { }
    else { return false; }
    if (Validate.IntValueValidate("ddlEmployeeType", "divMessage", "Please Select Employee Type.")) { }
    else { return false; }
    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnEmployeeAddSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessage", "Saved Successfully");
            FillEmployeeManagementTable();
            Enablebutton("btnSubmit");

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


function EditUserPopUp(id) {
    SetCssToBody();
    var url = GetDomain(_DOMAINDIVID) + "EmployeeManagement/UpdateEmployeePopUp?employeeId=" + id;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divTimePopUp", data.message);
                    break;
                case -1:
                    SetHtml("divTimePopUp", data.message);

                    break;
                case -2:
                    SetHtml("divTimePopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divTimePopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}



function UpdateUserStatusPopUP(id, status) {
    var url = GetDomain(_DOMAINDIVID) + "EmployeeManagement/UpdatePopUp?employeeId=" + id + "&status=" + status;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divTimePopUp", data.message);
                  
                    break;
                case -1:
                    SetHtml("divTimePopUp", data.message);

                    break;
                case -2:
                    SetHtml("divTimePopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divTimePopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}

function UpdateUserStatus(id, status) {
    var url = GetDomain(_DOMAINDIVID) + "EmployeeManagement/UpdateEmployeeStatus?employeeId=" + id + "&IsActive=" + status;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", data.message);
                    FillEmployeeManagementTable();
                    break;
                case -1:
                    SetHtml("divMessagePopUp", data.message);

                    break;
                case -2:
                    SetHtml("divMessagePopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divMessagePopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}



function OnEmployeeEditBegin() {
    SetHtmlBlank("divMessagePopUp");

    if (Validate.StringValueValidate("txtNamePopUp", "divMessagePopUp", "Please Enter Name.")) { }
    else { return false; }
    if (Validate.StringValueValidate("txtEmailPopUp", "divMessagePopUp", "Please Enter Email.")) { }
    else { return false; }
    if (!isEmail($("#txtEmailPopUp").val())) {
        $("#divMessagePopUp").val("Email Is Not Correct.");
        return false;
    }
   
    if (Validate.StringValueValidate("txtMobileNumberPopUp", "divMessagePopUp", "Please Enter Mobile Number.")) { }
    else { return false; }

    // DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmitPopUp");
}
function OnEmployeeEditSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessagePopUp", "Updated Successfully");
            FillEmployeeManagementTable();
            Enablebutton("btnSubmitPopUp");
            break;
        case -1:
            SetHtml("divMessagePopUp", "SomeThing Goes Wrong");
            Enablebutton("btnSubmitPopUp");
            break;
        case -2:
            SetHtml("divMessagePopUp", "SomeThing Goes Wrong");
            Enablebutton("btnSubmitPopUp");
            break;
        case -99:
            LogoutOperation();
            break;
    }
}

