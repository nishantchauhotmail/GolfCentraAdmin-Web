var txtFreeCaddieCount = 0;
var txtFreeCartCount = 0;
var memberTypeArry = [];
var courseTaxMappingViewModels = [];
var equipmentViewModels = [];
var SessionTimeArray = [];
var couponData = {};

function OnSpotBookingReady() {


    $("#divBucketType").hide();
    OnBookingTypeChange();
    OnSessionMasterChange();
    OnPlayerMasterChange();
    OnCaddieChange();
    OnCartChange();
    $("#ddlSessionTypeBDR").hide();
    $("#ddlSessionType").show();
    GetAllMemberTypeTable();
    //  GetAllEquipmentTable();
    OnHoleTypeChange();
    OnStartCourseChange();
    $('#ddlStartCourse').append(new Option("Ridge 9", 4));
    $('#ddlStartCourse').append(new Option("Valley 9", 5));
    $('#ddlStartCourse').append(new Option("Canyon 9", 6));

    var cPID = $('#hdnCoursePairingId').val();
    var sSID = $('#hdnSlotSessionId').val();
    var SID = $('#hdnSessionId').val();



    if (cPID != null && cPID != 0) {
        $('#ddlStartCourse').val(cPID).change();
        $('#ddlSessionType').val(SID).change();
    }
}

function OnBookingTypeChange() {

    $("#ddlBookingType").on("change", function () {
        var value = $("#ddlBookingType option:selected").val();
        if (value == "") { return; }

        if (value == 2) {
            $("#divHoleType").hide();
            $("#divBucketType").show();
            $("#ddlSessionTypeBDR").show();
            $("#ddlSessionType").hide();
            $("#divCourseType").hide();

        } else {

            $("#divHoleType").show();
            $("#divBucketType").hide();
            $("#ddlSessionTypeBDR").hide();
            $("#ddlSessionType").show();
            $("#divCourseType").show();

        }
        GetSessionTimeDetail();
    });

}

function OnSessionMasterChange() {

    $("#ddlSessionType").on("change", function () {

        GetSessionTimeDetail();
    });

    $("#ddlSessionTypeBDR").on("change", function () {

        GetSessionTimeDetail();
    });
}

function OnPlayerMasterChange() {

    $("#ddlPlayerMaster").on("change", function () {
        $.each(memberTypeArry, function (i, item) {
            var str = memberTypeArry[i].MemberTypeId;
            $("#" + str).val(0);

        });

    });

}

function OnPlayerBoxEdit() {
    var value = $("#ddlPlayerMaster option:selected").val();
    if (value == "") { return; }
    var total = 0;

    $.each(memberTypeArry, function (i, item) {

        //  var str = memberTypeArry[i].MemberTypeId;

        total = parseInt(total) + parseInt($("#" + memberTypeArry[i].MemberTypeId).val());

    });

    if (value < (parseInt(total))) {

        SetHtml("divMessage1", "Invalid Player Details")

    }

}

function GetSessionTimeDetail() {
    $("#ddlSessionTime").empty();
    var sessionTypeId = "";
    var coursePairingId = null;
    var bookingTypeId = $("#ddlBookingType option:selected").val();
    if (bookingTypeId == "") { return; }
    if (bookingTypeId == 1) {
        var sessionTypeId = $("#ddlSessionType option:selected").val();

        coursePairingId = $("#ddlStartCourse option:selected").val();
    } else {
        var sessionTypeId = $("#ddlSessionTypeBDR option:selected").val();
    }
    if (sessionTypeId == "") { return; }
    var date = $("#txtdate").val();
    if (date == "") { return; }

    var url = GetDomain(_DOMAINDIVID) + "OnSpotBooking/GetSlotDetails?bookingTypeId=" + bookingTypeId + "&date=" + date + "&sessionId=" + sessionTypeId + "&coursePairingId=" + coursePairingId;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    $.each(data.message, function (index, item) {
                        timedata = eval(item.Time);
                        $('#ddlSessionTime').append(new Option(timedata.Hours + ":" + timedata.Minutes + " - " + item.PlayerLeftCount, item.SlotId));
                    });
                    SessionTimeArray = data.message;

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
            // $('#divMessage').html('<p>An Error Has Occurred</p>');
        }
    });
    GetAllBucketTypeTable();
}

function OnNextButtonClick() {

    var bookingTypeId = $("#ddlBookingType option:selected").val();
    if (bookingTypeId == "") {
        SetHtml("divMessage1", "Please Select Booking Type")

        return;
    }
    var sessionTypeId = "";
    if (bookingTypeId == 1) {
        sessionTypeId = $("#ddlSessionType option:selected").val();
    } else {
        sessionTypeId = $("#ddlSessionTypeBDR option:selected").val();

    }
    if (sessionTypeId == "") {
        SetHtml("divMessage1", "Please Select Session")

        return;
    }
    var date = $("#txtdate").val();
    if (date == "") {
        SetHtml("divMessage1", "Please Select A Date")

        return;
    }
    var bucketTypeId;
    if (bookingTypeId == 2) {
        bucketTypeId = $("#ddlBucketType option:selected").val();
        if (bucketTypeId == "" || bucketTypeId == undefined || bucketTypeId == 0 || bucketTypeId == "0") {
            SetHtml("divMessage1", "Please Select Bucket ")

            return;
        }
        else {
            var coursePairingId = $("#ddlStartCourse option:selected").val();
            if (coursePairingId == "" || coursePairingId == undefined || coursePairingId == 0 || coursePairingId == "0") {
                SetHtml("divMessage1", "Please Select Course ")

                return;
            }
        }
    }


    var holeTypeId = $("#ddlHoleType option:selected").val();
    var sessionTimeId = $("#ddlSessionTime option:selected").val();

    if (sessionTimeId == "" || sessionTimeId == undefined) {
        SetHtml("divMessage1", "Please Select Session Time ")

        return;
    }

    var memberPlayer = $("#memberPlayer").val();
    var nonMemberPlayer = $("#nonMemberPlayer").val();
    var playerId = $("#ddlPlayerMaster option:selected").val();
    if (playerId == "") {
        SetHtml("divMessage1", "Please Select Player ")
        return;
    }
    var status = false;
    $.each(SessionTimeArray, function (i, item) {

        if (SessionTimeArray[i].SlotId == sessionTimeId) {
            var str = SessionTimeArray[i].PlayerLeftCount;

            if (playerId > str) {
                SetHtml("divMessage1", "Please Select Less Player ")
                status = true;
                return false;
            }
        }


    });
    if (status == true) {
        SetHtml("divMessage1", "Please Select Less Player ")

        return false;
    }

    var total = 0;

    $.each(memberTypeArry, function (i, item) {

        var str = memberTypeArry[i].MemberTypeId;

        total = parseInt(total) + parseInt($("#" + str).val());

    });
    if (playerId != (parseInt(total))) {
        SetHtml("divMessage1", "Invalid Player Details")
        return;
    }
    var memberTypeId = $("#ddlMemberType option:selected").val();
    var memberTypeArryDB = [];
    var memberCount = 0;
    $.each(memberTypeArry, function (i, item) {

        var str = memberTypeArry[i].MemberTypeId;
        if (str == 1) {
            memberCount=  parseInt($("#" + str).val())
        }
        memberTypeArryDB.push({
            MemberTypeId: memberTypeArry[i].MemberTypeId,
            Name: memberTypeArry[i].Name,
            PlayerCount: parseInt($("#" + str).val()),
        })
    });


    //  GetFreeCaddieAndCart(memberPlayer, nonMemberPlayer);
    var model = {
        "BookingTypeId": bookingTypeId,
        "HoleTypeId": holeTypeId,
        "SearchDate": date,
        "SlotId": sessionTimeId,
        "NoOfMemberPlayer": memberPlayer,
        "NoOfNonMemberPlayer": nonMemberPlayer,
        "NoOfPlayer": playerId,
        "BucketTypeId": bucketTypeId,
        "MemberTypeViewModels": memberTypeArryDB,
        "MemberTypeId": memberTypeId
    };

    var url = GetDomain(_DOMAINDIVID) + "OnSpotBooking/GetPriceCalculation";

    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(model),
        contentType: "application/json",

        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divMessage1", "")
                    $("#firstDiv").hide();

                    $("#nextButtonDiv").show();
                    if (bookingTypeId == 1) {
                        $("#divGreenFee").show();
                        $("#divRangeFee").hide();
                        $("#divBallFee").hide();
                        //$("#divCartCaddie").hide();
                        //$("#divCartCaddiePrice").hide();
                    } else {
                        $("#divCartCaddie").hide();
                        $("#divCartCaddiePrice").hide();
                        $("#equipmentDiv").hide();

                        $("#divRangeFee").show();
                        $("#divGreenFee").hide();
                        $("#divBallFee").show();

                    }

                    for (var i = 0; i < 4; i++) {
                        $("#player" + i.toString()).hide();
                        $("#divSearchString" + i.toString()).show();
                        $("#email" + i.toString()).prop('disabled', false);
                        $("#contact" + i.toString()).prop('disabled', false);
                        $("#name" + i.toString()).prop('disabled', false);
                        $("#divemail" + i.toString()).show();
                    }

                    var playerId = $("#ddlPlayerMaster option:selected").val();
                    var numberOfPlayer = parseInt(playerId);
                    memberCount = parseInt(memberCount);
                    for (var i = 0; i < numberOfPlayer; i++) {

                        $("#player" + i.toString()).show();
                        if (i >= memberCount && i != 0) {
                            $("#divemail" + i.toString()).hide();
                            $("#divSearchString" + i.toString()).hide();
                        }
                        if (i < memberCount) {
                            $("#email" + i.toString()).prop('disabled', true);

                            $("#contact" + i.toString()).prop('disabled', true);

                            $("#name" + i.toString()).prop('disabled', true);
                        }
                         

                    }

                    var pricing = eval(data.message);
                    var greenfee = parseFloat(pricing.GreenFee)
                    var rangeFee = parseFloat(pricing.RangeFee)
                    courseTaxMappingViewModels = pricing.CourseTaxMappingViewModels;
                    $("#Ballfee").val(parseFloat(pricing.BallPrice));
                    if (holeTypeId == 1) {
                        $("#hiddenCartCharge").html(pricing.Cart9HolePrice);
                        $("#hiddenCaddieCharge").html(pricing.Caddie9HolePrice);

                    } else if (holeTypeId == 2) {
                        $("#hiddenCartCharge").html(pricing.Cart18HolePrice);
                        $("#hiddenCaddieCharge").html(pricing.Caddie18HolePrice);
                    }
                    else {
                        $("#divCartCaddie").hide();
                        $("#divCartCaddiePrice").hide();


                    }
                    $("#Greenfee").val(greenfee);
                    $("#Rangefee").val(rangeFee);
                    //  SetHtml("divSessionPopUp", data.message);
                    equipmentViewModels = pricing.BookingEquipmentMappingViewModels;
                    SumAmount();
                    break;
                case -1:
                    // SetHtml("divSessionPopUp", data.message);
                    SetHtml("divMessage1", data.message)

                    break;
                case -2:
                    SetHtml("divMessage1", data.message)

                    //  SetHtml("divSessionPopUp", "SomeThing Goes Wrong");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            SetHtml("divMessage1", '<p>An Error Has Occurred</p>')
        }
    });


}

function OnCartChange() {

    $("#ddlCart").on("change", function () {
        var value = $("#ddlCart option:selected").val();
        if (value == "") { return; }
        if ((value - txtFreeCartCount) > 0) {

            var amount = $("#hiddenCartCharge").html();
            $("#cartCharges").val(parseFloat(amount) * (value - txtFreeCartCount));

        }
        else {
            $("#cartCharges").val(0);
        }

        SumAmount();
    });

}

function OnCaddieChange() {

    $("#ddlCaddie").on("change", function () {
        var value = $("#ddlCaddie option:selected").val();
        if (value == "") { return; }

        if (value - txtFreeCaddieCount > 0) {
            var amount = $("#hiddenCaddieCharge").html();
            $("#caddieCharges").val(parseFloat(amount) * (value - txtFreeCaddieCount));

        }
        else {
            $("#caddieCharges").val(0);
        }

        SumAmount();
    });

}

function SumAmount() {
    var greenFee = GetValueOrZero($("#Greenfee").val());
    var rangeFee = GetValueOrZero($("#Rangefee").val());
    var caddieCharges = GetValueOrZero($("#caddieCharges").val());
    var cartCharges = GetValueOrZero($("#cartCharges").val());
    var discount = GetValueOrZero($("#discount").val());
    var ballFee = GetValueOrZero($("#Ballfee").val());
    var equipmentPrice = 0;
    var totalAmount = ((greenFee + rangeFee + caddieCharges + cartCharges + ballFee));

    $.each(equipmentViewModels, function (i, item) {
        var str = equipmentViewModels[i].EquipmentId;

        var count = parseInt($("#equ" + str).val());

        if (count > 0) {
            var price = equipmentViewModels[i].EquipmentPrice;
            equipmentPrice = parseFloat(equipmentPrice) + parseFloat(price * count)
            totalAmount = parseFloat(totalAmount) + parseFloat(price * count);
        }
    });
    var percentage = 0;
    $.each(courseTaxMappingViewModels, function (i, item) {
        percentage = parseFloat(percentage) + parseFloat(courseTaxMappingViewModels[i].Percentage);


    });
    totalAmount = parseFloat(totalAmount) + parseFloat((parseFloat(totalAmount) * parseFloat(percentage)) / 100);
    $("#equipmentPrice").val(equipmentPrice.toFixed(2));

    if (jQuery.isEmptyObject(couponData) != true) {

        if (couponData.IsFlat == true) {

            $("#discount").val(couponData.Value);
            discount = couponData.Value;
        }
        else {
            am = parseFloat((parseFloat(totalAmount) * parseFloat(couponData.Value)) / 100);
            $("#discount").val(am);
            discount = am;
        }

    }
    else {
        $("#discount").val(0);
        discount = 0;
    }

    totalAmount = parseFloat(totalAmount) - parseFloat(discount);
    if (totalAmount > 0) {
        $("#amount").val(totalAmount.toFixed(2));
    } else {
        $("#amount").val(0);
    }

}

function OnDiscountAdd() {

    SumAmount();
}

function GetValueOrZero(value) {
    if (value == "") { return 0; }
    if (isNaN(value)) {
        return 0;
    }
    else { return parseFloat(value); }
}

function SaveBooking() {
    SetHtml("divMessage", "Please Wait..");


    var caddieCount = $("#ddlCaddie option:selected").val();

    var cartCount = $("#ddlCart option:selected").val();

    var bookingTypeId = $("#ddlBookingType option:selected").val();
    if (bookingTypeId == "") { return; }
    var sessionTypeId = "";
    if (bookingTypeId == 1) {
        sessionTypeId = $("#ddlSessionType option:selected").val();
    } else {

        sessionTypeId = $("#ddlSessionTypeBDR option:selected").val();
    }
    if (sessionTypeId == "") { return; }
    var date = $("#txtdate").val();

    if (date == "") { return; }
    var holeTypeId = $("#ddlHoleType option:selected").val();
    var sessionTimeId = $("#ddlSessionTime option:selected").val();
    var memberPlayer = $("#memberPlayer").val();
    var nonMemberPlayer = $("#nonMemberPlayer").val();
    var bucketTypeId = $("#ddlBucketType option:selected").val();
    if (bucketTypeId == "") { return; }
    var playerId = $("#ddlPlayerMaster option:selected").val();
    if (playerId == "") { return; }
    var greenFee = GetValueOrZero($("#Greenfee").val());
    var rangeFee = GetValueOrZero($("#Rangefee").val());
    var caddieCharges = GetValueOrZero($("#caddieCharges").val());
    var cartCharges = GetValueOrZero($("#cartCharges").val());
    var discount = GetValueOrZero($("#discount").val());
    var ballFee = GetValueOrZero($("#Ballfee").val());
    var amount = $("#amount").val();
    var err = false;
    var coursePairingId = $("#ddlStartCourse option:selected").val();
    $.each(equipmentViewModels, function (i, item) {
        var str = equipmentViewModels[i].EquipmentId;

        var count = parseInt(GetValueOrZero($("#equ" + str).val()));
        if (count <= equipmentViewModels[i].EquipmentLeft) {
            if (count > playerId) {

                SetHtml("divMessage", "" + equipmentViewModels[i].EquipmentName + " equipment cannot be more than number of players.");
                err = true;
            }
        } else {
            SetHtml("divMessage", "" + equipmentViewModels[i].EquipmentName + " equipment cannot be more than " + equipmentViewModels[i].EquipmentLeft );
            err = true;
        }
    });
    if (err == true) { return; }

    var emails = [];
    for (var i = 0; i < playerId; i++) {
        var name = $("#name" + i.toString()).val();
        if (name == "") {
            SetHtml("divMessage", 'Please Enter Name For Player ' + (i + 1).toString())

            return;
        }
        if (i == 0) {
            var email = $("#email" + i.toString()).val();
            if (email == "") {
                SetHtml("divMessage", 'Please Enter Email For Player ' + (i + 1).toString())

                return;
            } else {
                if (emails.includes(email)) {
                    SetHtml("divMessage", 'Please Enter Different Player For Player ' + (i + 1).toString())
                    return;
                }
                emails.push(email);
            }
            var contact = $("#contact" + i.toString()).val();
        }
    }
    var golferDetailsArray = [];
    for (var i = 0; i < playerId; i++) {
        var name = $("#name" + i.toString()).val();

        var email = $("#email" + i.toString()).val();

        var contact = $("#contact" + i.toString()).val();
        var memberShipId = $("#SearchString" + i.toString()).val();

        golferDetailsArray.push({
            Name: name,
            EmailAddress: email,
            Contact: contact,
            MemberShipId: memberShipId,
            PlayerSerialNumber: "Player " + (i + 1).toString()
        })
    }

    var memberTypeArryDB = [];
    $.each(memberTypeArry, function (i, item) {

        var str = memberTypeArry[i].MemberTypeId;
        memberTypeArryDB.push({
            MemberTypeId: memberTypeArry[i].MemberTypeId,
            Name: memberTypeArry[i].Name,
            PlayerCount: parseInt($("#" + str).val()),
        })
    });

    var equipmentArryDB = [];
    $.each(equipmentViewModels, function (i, item) {

        var str = equipmentViewModels[i].EquipmentId;
        equipmentArryDB.push({
            EquipmentId: equipmentViewModels[i].EquipmentId,
            EquipmentName: equipmentViewModels[i].EquipmentName,
            EquipmentCount: parseInt($("#equ" + str).val()),
            EquipmentPrice: equipmentViewModels[i].EquipmentPrice,
        })
    });
    var model = {
        "BookingTypeId": bookingTypeId,
        "HoleTypeId": holeTypeId,
        "TeeOffDate": date,
        "SlotId": sessionTimeId,
        "NoOfMemberPlayer": memberPlayer,
        "NoOfNonMemberPlayer": nonMemberPlayer,
        "NoOfPlayer": playerId,
        "BucketId": bucketTypeId,
        "CartCount": cartCount,
        "CartFee": cartCharges,
        "CaddieFee": caddieCharges,
        "CaddieCount": caddieCount,
        "GreenFee": greenFee,
        "RangeFee": rangeFee,
        "BallFee": ballFee,
        "Discount": discount,
        "Amount": amount,
        "Name": name,
        "Email": email,
        "MobileNumber": contact,
        "MemberTypeViewModels": memberTypeArryDB,
        "BookingEquipmentMappingViewModels": equipmentArryDB,
        "CoursePairingId": coursePairingId,
        "BookingPlayerDetailViewModels": golferDetailsArray
    };


    var url = GetDomain(_DOMAINDIVID) + "OnSpotBooking/SaveBooking";

    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(model),
        contentType: "application/json",

        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divMessage", "Saved Successfully. Please Refresh For New Booking.");

                    break;
                case -1:
                    SetHtml("divMessage", data.message);

                    break;
                case -2:
                    SetHtml("divMessage", data.message);
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

function GetFreeCaddieAndCart(member, nonmember) {
    txtFreeCaddieCount = parseInt(nonmember) + parseInt(member);
    if ((nonmember) > 2) {
        txtFreeCartCount = 2;
    } else if (nonmember <= 2 && nonmember > 0) {
        txtFreeCartCount = 1;
    } else {
        txtFreeCartCount = 0;
    }

}

function GetAllMemberTypeTable() {
    var url = GetDomain(_DOMAINDIVID) + "OnSpotBooking/GetMemberType";
    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    memberTypeArry = eval(data.message);
                    $.each(memberTypeArry, function (i, item) {

                        var str = memberTypeArry[i].MemberTypeId;
                        $("#" + str).val(0);
                        //    $('#ddlState').append(new Option(data.states[i].StateName, data.states[i].StateId));
                    });
                    // SetHtml("divTaxDetailTable", data.message);
                    break;
                case -1:
                    //SetHtml("divTaxDetailTable", data.message);
                    //Enablebutton("btnSubmit");
                    break;
                case -2:
                    //SetHtml("divdivTaxDetailTable", "SomeThing Goes Wrong");
                    //Enablebutton("btnSubmit");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            // $('#divTaxDetailTable').html('<p>An Error Has Occurred</p>');
        }
    });
}

function GetAllBucketTypeTable() {
    $("#ddlBucketType").empty();
    var memberTypeId = $("#ddlMemberType option:selected").val();
    var date1 = $("#txtdate").val();
    if (date1 == "") { return; }

    var url = GetDomain(_DOMAINDIVID) + "OnSpotBooking/GetAllBucketList?memberTypeId=" + memberTypeId + "&date=" + date1;
    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    var da = eval(data.message);

                    $('#ddlBucketType').append(new Option("Please Select A Bucket", 0));
                    $.each(da, function (i, item) {

                        $('#ddlBucketType').append(new Option(da[i].Balls, da[i].BucketDetailId));
                    });

                    // SetHtml("divTaxDetailTable", data.message);
                    break;
                case -1:
                    //SetHtml("divTaxDetailTable", data.message);
                    //Enablebutton("btnSubmit");
                    break;
                case -2:
                    //SetHtml("divdivTaxDetailTable", "SomeThing Goes Wrong");
                    //Enablebutton("btnSubmit");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            // $('#divTaxDetailTable').html('<p>An Error Has Occurred</p>');
        }
    });
}

function OnPlayerMasterChange() {

    $("#ddlMemberType").on("change", function () {
        GetAllBucketTypeTable();

    });

}

function GetAllEquipmentTable() {
    var url = GetDomain(_DOMAINDIVID) + "OnSpotBooking/GetAllEquipment";
    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    equipmentViewModels = eval(data.message);
                    $.each(equipmentViewModels, function (i, item) {

                        var str = equipmentViewModels[i].EquipmetId;
                        $("#equ" + str).val(0);
                        //    $('#ddlState').append(new Option(data.states[i].StateName, data.states[i].StateId));
                    });
                    // SetHtml("divTaxDetailTable", data.message);
                    break;
                case -1:
                    //SetHtml("divTaxDetailTable", data.message);
                    //Enablebutton("btnSubmit");
                    break;
                case -2:
                    //SetHtml("divdivTaxDetailTable", "SomeThing Goes Wrong");
                    //Enablebutton("btnSubmit");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            // $('#divTaxDetailTable').html('<p>An Error Has Occurred</p>');
        }
    });
}

function OnEquipmetBoxEdit() {
    SetHtml("divMessage", "");
    var playerId = $("#ddlPlayerMaster option:selected").val();
    if (playerId == "") { return; }
    var err = false;

    $.each(equipmentViewModels, function (i, item) {
        var str = equipmentViewModels[i].EquipmentId;

        var count = parseInt(GetValueOrZero($("#equ" + str).val()));

        if (count > playerId) {

            SetHtml("divMessage", "" + equipmentViewModels[i].EquipmentName + " equipment cannot be more than number of players.");
            err = true;

        }
    });
    if (err == true) { return; }
    SumAmount();

}

function OnHoleTypeChange() {

    $("#ddlHoleType").on("change", function () {
        $('#ddlStartCourse').empty();
        var value = $("#ddlHoleType option:selected").val();
        if (value == 2) {
            $('#ddlStartCourse').append(new Option("Ridge 9 - Valley 9", 1));
            $('#ddlStartCourse').append(new Option("Valley 9 - Canyon 9", 2));
            $('#ddlStartCourse').append(new Option("Canyon 9 - Ridge 9", 3));
        } else {
            $('#ddlStartCourse').append(new Option("Ridge 9", 4));
            $('#ddlStartCourse').append(new Option("Valley 9", 5));
            $('#ddlStartCourse').append(new Option("Canyon 9 ", 6));
        }
    });

}

function OnStartCourseChange() {

    $("#ddlStartCourse").on("change", function () {

        GetSessionTimeDetail();
    });

}

function CompleteBoxV2(Counter) {
    //var var1 = $("#SearchString").val();
    //if (var1 == "" || var1 == 0 || var1 == null) {
    try {
        //var hdntxtbox = $("#SearchString");
        //if (hdntxtbox.val == "") {
        //    document.getElementById("txtUserId").value = '';
        //    //return false;


        $("#SearchString" + Counter + " ").autocomplete({
            source: function (request, response) {
                var val = request.term;

                $.ajax({
                    url: GetDomain(_DOMAINDIVID) + "OnSpotBooking/SearchGolferByMemberShipId?memberShipId=" + val,
                    type: "POST",
                    dataType: "json",
                    data: { term: request.term },
                    success: function (data) {
                        data = eval(data);
                        switch (data.code) {
                            case -5:
                                window.location = GetDomain() + "Logout/Index";
                                break;
                        }
                        if (data.code == 0) {
                            $("#divAcHtml" + Counter + "").show();
                            JSON.parse(JSON.stringify(data.message))
                            var count = $("#ddlPlayerMaster option:selected").val();
                            for (y = 0; y < count; y++) {

                                var mId = $("#SearchString" + y.toString()).val();

                                var email = $("#email" + y.toString()).val();

                                if (mId != "") {
                                    if (data.message.count != 0) {
                                        $.each(data.message, function (i, item) {
                                            var user = data.message[i];
                                            if (user != null) {
                                                if (user.EmailAddress == email) {
                                                    data.message.splice(i, 1);
                                                    //delete data.users.index[i];
                                                }
                                            }
                                        });
                                    }
                                }
                            }

                            var html = BuildACHTMLV2(data.message, Counter);
                            if (data.message == 0 || data.message == null) {
                                var format = "<button class='autocomplete-close' type='button' [onclick]><span>x</span></button>";
                                clickEvent = " onclick=showClose('" + Counter + "');";
                                format = format.replace("[C]", Counter);
                                format = format.replace("[onclick]", clickEvent);
                                var close = " <div class='autocomplete-userdata' [id] >No User Found</div>";
                                id = " id=autoCompleteboxLoader('" + Counter + "');";
                                close = close.replace("[id]", id);
                                $("#divAcHtml" + Counter + "").html(format + close)
                            }
                            else {
                                $("#divAcHtml" + Counter + "").html(html);
                                $(".MyClass" + Counter.toString()).prop("disabled", false);
                            }
                        }
                    },
                    //complete: function () { setTimeout(fn, 500); }
                })

                var close = "<button class='autocomplete-close' type='button' [onclick]><span>x</span></button>";
                clickEvent = " onclick=showClose('" + Counter + "');";
                close = close.replace("[C]", Counter);
                close = close.replace("[onclick]", clickEvent);

                var format = " <div class='autocomplete-userdata' [id] >Please Wait...</div>";
                id = " id=autoCompleteboxLoader('" + Counter + "');";
                format = format.replace("[id]", id);
                $("#divAcHtml" + Counter + "").html(close + format);
                $("#divAcHtml" + Counter + "").show();

            },
            minLength: 3
        });

        var close = "<button class='autocomplete-close' type='button' [onclick]><span>x</span></button>";
        clickEvent = " onclick=showClose('" + Counter + "');";
        close = close.replace("[C]", Counter);
        close = close.replace("[onclick]", clickEvent);

        var searchTerm = $("#SearchString" + Counter.toString()).val().trim();
        if (searchTerm.length < 3) {
            var format = " <div class='autocomplete-userdata' [id] >Please Enter Atleast 3 Characters.</div>";
            id = " id=autoCompleteboxLoader('" + Counter + "');";
            format = format.replace("[id]", id);
            $("#divAcHtml" + Counter + "").html(close + format);
            $("#divAcHtml" + Counter + "").show();
        }

    }


    catch (e) {
        MyAlert("CompleteBoxV2: " + e);
        return false;
    }
}

function BuildACHTMLV2(users, Counter) {
    try {
        var i = 0;
        var user;
        var oneUserHtml;
        var sb = [];

        var format = "<button class='autocomplete-close' type='button' [onclick]><span>x</span></button>";
        clickEvent = " onclick=showClose('" + Counter + "');";
        format = format.replace("[C]", Counter);
        format = format.replace("[onclick]", clickEvent);
        sb.push("<div id='divACMain'>");
        sb.push(format);


        for (i = 0; i <= users.length - 1; i++) {
            user = users[i];
            oneUserHtml = GetACOneUserHtmlV2(user, Counter, i);
            sb.push(oneUserHtml);
        }

        sb.push("</div>");
        var html = sb.join('');

        return html;

    }
    catch (e) {
    }
}

function GetACOneUserHtmlV2(user, Counter, i) {

    var firstName, lastName, emailAddress, clubMemberId, mobile;

    firstName = user.Name;
    if (firstName.indexOf(" ") != -1) {
        firstName = firstName.replace(/ /g, '&nbsp;');
    }

    lastName = user.LastName;
    if (lastName == null) {
        lastName = '';
    }

    clubMemberId = user.ClubMemberId;
    if (clubMemberId.indexOf(" ") != -1) {
        clubMemberId = clubMemberId.replace(/ /g, '&nbsp;');
    }

    emailAddress = user.EmailAddress;
    if (emailAddress.indexOf(" ") != -1) {
        emailAddress = emailAddress.replace(/ /g, '&nbsp;');
    }
    mobile = user.Mobile;
    if (mobile.indexOf(" ") != -1) {
        mobile = Mobile.replace(/ /g, '&nbsp;');
    }


    var r = i % 2;
    if (r == 0) {
        var format = "<div class='autocomplete-background' [onclick] >[FN] [LN] ([MI])</div>";
    }
    else {
        var format = "<div class='autocomplete-graybg' [onclick] >[FN] [LN] ([MI])</div>";
    }


    clickEvent = " onclick=SelectUserV2('" + firstName + "','" + lastName + "','" + Counter + "','" + clubMemberId + "','" + emailAddress + "','" + mobile + "');";
    format = format.replace("[FN]", firstName);
    format = format.replace("[LN]", lastName);
    format = format.replace("[MI]", clubMemberId);
    format = format.replace("[EM]", emailAddress);
    format = format.replace("[MO]", mobile);
    format = format.replace("[onclick]", clickEvent);

    return format;
}

function SelectUserV2(name, lastName, Counter, clubMemberId, emailAddress, mobile) {
    try {
        $("#divAcHtml" + Counter + "").hide();

        $("#SearchString" + Counter.toString()).val(clubMemberId);

        $("#email" + Counter.toString()).val(emailAddress);
        $("#contact" + Counter.toString()).val(mobile);
        $("#name" + Counter.toString()).val(name);
        $("#email" + Counter.toString()).prop('disabled', true);

        $("#contact" + Counter.toString()).prop('disabled', true);

        $("#name" + Counter.toString()).prop('disabled', true);


    }

    catch (e) { }
}

function showClose(counter) {
    $("#divAcHtml" + counter.toString()).hide();
}

function ResetCompletebox(Counter) {
    $("#SearchString" + Counter.toString()).val("");
    $("#email" + Counter.toString()).val("");
    $("#contact" + Counter.toString()).val("");
    $("#name" + Counter.toString()).val("");
    //$("#email" + Counter.toString()).prop('disabled', false);

    //$("#contact" + Counter.toString()).prop('disabled', false);

    //$("#name" + Counter.toString()).prop('disabled', false);

}

function CouponCheck() {
    var code = $("#couponCode").val();
    if (code == "") {
        SetHtml("divCoupon", "Please Provide Coupon");
        couponData = {};
        SumAmount();
        return false;
    }
    var url = GetDomain(_DOMAINDIVID) + "OnSpotBooking/GetCouponByCode?code=" + code;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    couponData = data.message;
                    SumAmount();
                    SetHtml("divCoupon", "Coupon Applied Successfully");

                    break;
                case -1:
                    SetHtml("divCoupon", data.message);
                    couponData = {};
                    SumAmount();
                    break;
                case -2:
                    SetHtml("divCoupon", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divCoupon').html('<p>An Error Has Occurred</p>');
        }
    });
}

function BackButton() {
    $("#firstDiv").show();

    $("#nextButtonDiv").hide();
}