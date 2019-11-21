
function OnSessionMasterPageReady() {
    OnTagRemove();
    GetBookTeeTimeData();
}

function OnTagRemove() {

}

function OnTabChange(id) {
    if (id == 1) {
        GetBookTeeTimeData();
    } else {
        GetDrivingRangeData();
    }

}

function GetBookTeeTimeData() {
    var url = GetDomain(_DOMAINDIVID) + "SessionMaster/BookTeeTimeTab";
    SetHtml("divBookingTab", "Please Wait..");
    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {
            data = eval(data);
            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divBookingTab", data.message);
                    break;
                case -1:
                    SetHtml("divBookingTab", data.message);

                    break;
                case -2:
                    SetHtml("divBookingTab", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divBookingTab').html('<p>An Error Has Occurred</p>');
        }
    });

}

function GetDrivingRangeData() {
    var url = GetDomain(_DOMAINDIVID) + "SessionMaster/DrivingRangeTab";
    SetHtml("divDrivingRangeTab", "Please Wait..");
    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divDrivingRangeTab", data.message);
                    break;
                case -1:
                    SetHtml("divDrivingRangeTab", data.message);

                    break;
                case -2:
                    SetHtml("divDrivingRangeTab", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divDrivingRangeTab').html('<p>An Error Has Occurred</p>');
        }
    });



}