function OnSlotReservationReady() {
    GetAllBlockSlotRangeTable()
    OnDateChange()
}

function GetAllBlockSlotRangeTable() {
    var url = GetDomain(_DOMAINDIVID) + "SlotReservation/GetAllBlockSlotRange";
    SetHtml("divBlockSlotRangeTable", "Please Wait..");
    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divBlockSlotRangeTable", data.message);
                    break;
                case -1:
                    SetHtml("divBlockSlotRangeTable", data.message);
                  
                    break;
                case -2:
                    SetHtml("divBlockSlotRangeTable", "SomeThing Goes Wrong");
                   
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divBlockSlotRangeTable').html('<p>An Error Has Occurred</p>');
        }
    });
}

function DeleteBlockSlotRangePopUp(value) {
    var url = GetDomain(_DOMAINDIVID) + "SlotReservation/DeletePopUp?blockSlotRangeId=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divBlockSlotPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divBlockSlotPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divBlockSlotPopUp", "SomeThing Goes Wrong");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divBlockSlotPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}

function DeleteBlockSlotRange(value){

    var url = GetDomain(_DOMAINDIVID) + "SlotReservation/DeleteBlockSlotRange?blockSlotRangeId=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", " Deleted Successfully");
                    GetAllBlockSlotRangeTable();
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

function OnBlockSlotRangeAddBegin() {
    $("#divMessage").html("");
    var startdate = $("#txtStartDate").val();
    if (startdate == "") {
        $("#divMessage").html("Please Select A Start Date");
        return false;
    }
    var enddate = $("#txtEndDate").val();
    if (enddate == "") {
        $("#divMessage").html("Please Select A End Date");
        return false;
    }
    var txtReason = $("#txtReason").val();
    if (txtReason == "") {
        $("#divMessage").html("Please Enter A Reason");
        return false;
    }
    var txtCourse = $("#ddlCourseNameId").val();
    if (txtCourse == "") {
        $("#divMessage").html("Please Select A Course");
        return false;
    }

    var ddlSlot = $("#ddlSlot").val();
    if (ddlSlot == "") {
        $("#divMessage").html("Please Select A Slot");
        return false;
    }
    
    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
    $("#divMessage").html("");
}

function OnBlockSlotRangeAddSuccess(data) {
   
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
          
            $("#divMessage").html("Saved Successfully");
            $("#divMessage").show();
            GetAllBlockSlotRangeTable();
            break;

        case -1:
            $("#divMessage").html("Something Goes Wrong. Please Try Again.");
            $("#divMessage").show();
            break;

        default:
            break;
    }
}

function OnDateChange() {
    var startdate = $("#txtStartDate").val();
    if (startdate == "") { return; }
    var enddate = $("#txtEndDate").val();
    if (enddate == "") { return; }

    GetALLSlotForBlockRange(startdate,enddate);
}


function GetALLSlotForBlockRange(startDate, endDate) {
    $("#ddlSlot").empty();
    $("#ddlAlreadyBlocked").empty();
    var url = GetDomain(_DOMAINDIVID) + "SlotReservation/GetALLSlotForBlockRange?startDate=" + startDate +  "&endDate=" + endDate;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    var lockSlot ="";
                    $.each(data.message, function (index, item) {
                        //   var  timedata = eval(item.SlotTime);
                        $('#ddlSlot').append(new Option(item.SlotTime, item.SlotSessionWiseId));
                       if (!item.IsAlreadyDisabled ) {
                          
                        } else {
                            $('#ddlAlreadyBlocked').append(new Option(item.SlotTime, item.SlotSessionWiseId));
                            if (lockSlot !="") {
                                lockSlot += " , " + item.SlotTime;
                            } else {
                                lockSlot += item.SlotTime;
                            }
                        }
                    });
                
                    $('#txtADS').val(lockSlot);
                    $(".select2").select2();
                    //  SetHtml("divSessionPopUp", data.message);
                    break;
                case -1:
                    // SetHtml("divSessionPopUp", data.message);

                    break;
                case -2:
                    //  SetHtml("divSessionPopUp", "SomeThing Goes Wrong");
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