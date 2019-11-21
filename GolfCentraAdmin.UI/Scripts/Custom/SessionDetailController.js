function OnSessionDetailReady() {
    GetAllSessionDetailTable()

}

function GetAllSessionDetailTable() {
    var url = GetDomain(_DOMAINDIVID) + "SessionDetail/GetAllSessionDetail";

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divSearch", data.message);
                    break;
                case -1:
                    SetHtml("divSearch", data.message);
                    Enablebutton("btnSubmit");
                    break;
                case -2:
                    SetHtml("divSearch", "SomeThing Goes Wrong");
                    Enablebutton("btnSubmit");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divSearch').html('<p>An Error Has Occurred</p>');
        }
    });
}



function UpdateSessionPopUp(value, startTime, endTime,name) {
    var url = GetDomain(_DOMAINDIVID) + "SessionDetail/UpdateSessionPopUp?SessionId=" + value + "&StartTime=" + startTime + "&EndTime=" + endTime +"&Name="+name;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divSessionPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divSessionPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divSessionPopUp", "SomeThing Goes Wrong");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divSessionPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}

function OnSessionDetailAddBegin() {
    if (Validate.StringValueValidate("txtSessionName", "divMessage", "Please Enter Name.")) { }
    else { return false; }
    if (Validate.StringValueValidate("txtStartTime", "divMessage", "Please Select Start Time.")) { }
    else { return false; }
    if (Validate.StringValueValidate("txtStartEndTime", "divMessage", "Please Select End Time.")) { }
    else { return false; }
    Disablebutton("btnSubmit");
    
    
}

function OnSessionDetailAddSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessage", "Saved Successfully");
            GetAllSessionDetailTable();
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

function OnSessionDetailEditBegin() { }

function OnSessionDetailEditSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessagePopUp", "Updated Successfully");
            GetAllSessionDetailTable();
            Enablebutton("btnSubmitPopUp");
            break;
        case -1:
            SetHtml("divMessagePopUp", data.message);
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

function DeleteUserPopUp(value) {
    var url = GetDomain(_DOMAINDIVID) + "SessionDetail/DeleteSessionPopUp?sessionId=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divSessionPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divSessionPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divSessionPopUp", "SomeThing Goes Wrong");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divSessionPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}


function DeleteSession(value) {

    var url = GetDomain(_DOMAINDIVID) + "SessionDetail/DeleteSession?sessionId=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", "Session Deleted Successfully");
                    GetAllSessionDetailTable();
                    break;
                case -1:
                    SetHtml("divMessagePopUp", data.message);
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
        },
        error: function () {
            $('#divMessagePopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}