function OnSearchGolferReady() {

}




function OnGolferSearchBegin() {
    SetHtmlBlank("divMessage");



    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OngolferSearchSuccess(data) {
    HideLoader(_LOADERDIVID);
    data = eval(data);
    switch (data.code) {
       

        case 0:
            SetHtml("divSearchGolferTable", data.message);
           
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

function userDetailsByUserId(id) {
    var url = GetDomain(_DOMAINDIVID) + "SearchGolfer/GetUserDetailByGolferId?golferId=" + id;

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


function UserReport(id) {
    var url = GetDomain(_DOMAINDIVID) + "SearchGolfer/GetUserReportDetailByGolferId?golferId=" + id;

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
    SetHtml("divMessagePopUp", "Please Wait...");
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
                    SetHtml("divBlocked" + id + "", "Status Updated");

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

function UpdateGolferPasswordPopUp(id) {
    var url = GetDomain(_DOMAINDIVID) + "SearchGolfer/UpdateGolferPasswordPopUp?golferId=" + id ;

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

function UpdateGolferPassword(id) {


    var password = $("#txtPassword").val();
    SetHtml("divMessagePopUp", "Please Wait...");
    var url = GetDomain(_DOMAINDIVID) + "SearchGolfer/UpdateGolferPassword?GolferId=" + id + "&Password=" + password;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", data.message);
                   
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
