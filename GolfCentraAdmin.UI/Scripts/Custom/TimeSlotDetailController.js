
function OnTimeSlotDetailsReady() {
    GetAllTimeSlotTable()

}

function GetAllTimeSlotTable() {
    var url = GetDomain(_DOMAINDIVID) + "TimeSlotDetail/GetAllTimeSlot";
    SetHtml("divSlotDetailTable", "Please Wait..");
    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divSlotDetailTable", data.message);
                    break;
                case -1:
                    SetHtml("divSlotDetailTable", data.message);
                    Enablebutton("btnSubmit");
                    break;
                case -2:
                    SetHtml("divSlotDetailTable", "SomeThing Goes Wrong");
                    Enablebutton("btnSubmit");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divSlotDetailTable').html('<p>An Error Has Occurred</p>');
        }
    });
}

function SaveTimeSlot() {
    var value = $("#time").val();
    if (value == "") {
        SetHtml("divMessage", "Time Can't Not Be Blank");
        return;
    }

    var url = GetDomain(_DOMAINDIVID) + "TimeSlotDetail/SaveTimeSlot?timeSlot=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divMessage", "Slot Saved Successfully");
                    GetAllTimeSlotTable();
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
        },
        error: function () {
            $('#divMessage').html('<p>An Error Has Occurred</p>');
        }
    });
}

function DeleteTimeSlot(value) {

    var url = GetDomain(_DOMAINDIVID) + "TimeSlotDetail/DeleteTimeSlot?slotId=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", "Slot Deleted Successfully");
                    GetAllTimeSlotTable();
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

function UpdateTimeSlot(value) {
    var time = $("#txtTime").val();
    if (time == "") {
        SetHtml("divMessagePopUp", "Time Can't Not Be Blank");
        return;
    }
    var url = GetDomain(_DOMAINDIVID) + "TimeSlotDetail/UpdateTimeSlot?slotId=" + value + "&timeSlot=" + time;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", "Slot Updated Successfully");
                    GetAllTimeSlotTable();
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


function UpdateUserPopUp(id, timeSpan) {
    var url = GetDomain(_DOMAINDIVID) + "TimeSlotDetail/UpdatePopUp?slotId=" + id + "&timeSpan=" + timeSpan;

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

function DeleteUserPopUp(value) {
    var url = GetDomain(_DOMAINDIVID) + "TimeSlotDetail/DeletePopUp?slotId=" + value;

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



