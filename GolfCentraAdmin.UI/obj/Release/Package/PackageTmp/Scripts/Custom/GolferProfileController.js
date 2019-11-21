
function userDetailsByUserId(id) {
    var url = GetDomain(_DOMAINDIVID) + "GolferProfile/GetUserDetailByGolferId?golferId=" + id;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divEditUserPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divEditUserPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divEditUserPopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divEditUserPopUp').html('<p>An Error Has Occurred</p>');
        }
    });

}


function OnGolferEditBegin() {
    SetHtmlBlank("divMessage");

    SetHtmlBlank("divMessage");
    if (Validate.StringValueValidate("txtName", "divMessage", "Please Enter Name.")) { }
    else { return false; }
    if (Validate.StringValueValidate("txtEmail", "divMessage", "Please Enter Email.")) { }
    else { return false; }
    if (!isEmail($("#txtEmail").val())) {
        $("#divMessage").val("Email Is Not Correct.");
        return false;
    }


    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnGolferEditSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessage", "Updated Successfully");
            Enablebutton("btnSubmit");
            break;
        case -1:
            SetHtml("divMessage", "SomeThing Goes Wrong");
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

function UpdateUserStatusPopUP(id, status) {
    var url = GetDomain(_DOMAINDIVID) + "SearchGolfer/UpdatePopUp?golferId=" + id + "&status=" + status;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divGolferPopUp", data.message);

                    break;
                case -1:
                    SetHtml("divGolferPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divGolferPopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divGolferPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}

function UpdateUserStatus(id, status) {
    var url = GetDomain(_DOMAINDIVID) + "SearchGolfer/UpdateGolferStatus?GolferId=" + id + "&IsBlocked=" + status;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", data.message);
                    SetHtml("divBlocked"+id+"", "Status Updated");

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

