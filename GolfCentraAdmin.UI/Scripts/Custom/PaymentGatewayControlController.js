function OnPaymentControlAddBegin() {
    SetHtmlBlank("divMessage");
    if (document.getElementById('txtAllMembers').checked == true) {
       
    } else {
        var ids = $("#txtSelectedGolferIds").val();
        if (ids == "" || ids == null) {
            SetHtml("divMessage", "Please Select At Least One Member");
            return false;
        }
    }
    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnPaymentControlAddSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessage", "Saved Successfully");
            GetAllCouponTable();
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


function OnPaymentCheckBox() {
    if (document.getElementById('txtPaymentGatewayEnable').checked == true) {
        $("#divPaymentCondition").show();
        $('#txtSelectedGolferIds').select2({
            placeholder: "Select a Golfer",


        });
        $('#ddlEquipment').select2({
            placeholder: "Select a Equipment",


        });
    } else {
        $("#divPaymentCondition").hide();
    }
}


function OnAllMemberCheckBox() {
    if (document.getElementById('txtAllMembers').checked == true) {
        $("#divSelectedGolferIds").hide();

    } else {
        $("#divSelectedGolferIds").show();
    }
}