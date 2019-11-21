function OnGolferNotificationAddBegin() {
    SetHtmlBlank("divMessage");
    if (Validate.StringValueValidate("txtTitle", "divMessage", "Please Enter Title.")) { }
    else { return false; }
    if (Validate.StringValueValidate("txtMessage", "divMessage", "Please Enter Message.")) { }
    else { return false; }
   
    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnGolferNotificationSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessage", "Sent Successfully");
            FillGolferNotificationTable();
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


function OnGolferNotificationReady() {
    try {
        FillGolferNotificationTable();

    }
    catch (e) {
        MyAlert("OnGolferNotificationReady :" + e);
        return false;
    }
}

function FillGolferNotificationTable() {
    var url = GetDomain(_DOMAINDIVID) + "NotificationGolfer/GetNotificationGolferDetails";

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divNotificationTable", data.message);
                    break;
                case -1:
                    SetHtml("divNotificationTable", data.message);

                    break;
                case -2:
                    SetHtml("divNotificationTable", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divNotificationTable').html('<p>An Error Has Occurred</p>');
        }
    });
}