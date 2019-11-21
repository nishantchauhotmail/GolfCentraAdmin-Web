function OnTeeSheetDetailReady() {
    try {
       
        //$('#ddlStartCourse').append(new Option("Ridge 9", 4));
        //$('#ddlStartCourse').append(new Option("Valley 9", 5));
        //$('#ddlStartCourse').append(new Option("Canyon 9 ", 6));
        OnCourseChange();
        //$('#ddlStartCourse').val(4).change();

       
        
    }
    catch (e) {
        MyAlert("OnTeeSheetDetailReady :" + e);
        return false;
    }
}



function FillTeeSheetTable(id) {
    SetHtml("divTeeSheet", "Please Wait..");
    var date = $("#txtdate").val();
    var url = GetDomain(_DOMAINDIVID) + "TeeSheet/GetTeeSheetById?coursePairingId=" + id +"&searchDate="+date;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divTeeSheet", data.message);
                    break;
                case -1:
                    SetHtml("divTeeSheet", data.message);

                    break;
                case -2:
                    SetHtml("divTeeSheet", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divTeeSheet').html('<p>An Error Has Occurred</p>');
        }
    });
}


function OnBookBookingButtonClick(sessionSlotId, coursePairingId, sessionId) {

    window.location = GetDomain(_DOMAINDIVID) + "OnSpotBooking/Index?sessionSlotId=" + sessionSlotId + "&coursePairingId=" + coursePairingId + "&sessionId=" + sessionId;
}


function OnCourseChange() {

    $("#ddlStartCourse").on("change", function () {
        var value = $("#ddlStartCourse option:selected").val();
        if (value == "") { return; }
        FillTeeSheetTable(value);
       
    });

}

function OnDateChange() {

   var value= $("#ddlStartCourse").val();
       
        FillTeeSheetTable(value);
}