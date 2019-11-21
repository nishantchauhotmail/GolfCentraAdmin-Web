
function OnStandardPricingReady() {
    $('#ddlHoleType').select2({ placeholder: "Select A Hole Type" });
    $('#ddlMemberType').select2({ placeholder: "Select a Member Type" });
    $('#ddlTimeFormat').select2({ placeholder: "Select a Time Format" });
    $("#ddlHoleType").hide();
    $("#pricingBTT").hide();
    $("#pricingBDR").hide();
    $("#divHoleType").hide();
    OnBookingTypeChange();
}


function OnBookingTypeChange() {
    $("#ddlBookingType").on("change", function () {
        var value = $("#ddlBookingType option:selected").val();
        if (value == "") { return; }

        if (value == 1) {
            $("#ddlHoleType").show();
            $("#divHoleType").show();

        } else {

            $("#ddlHoleType").hide();
            $("#divHoleType").hide();
        }

    });

}

function OnNextButtonClick() {

    var holeTypeId = "";
    var bookingTypeId = $("#ddlBookingType option:selected").val();
    if (bookingTypeId == "") {
        SetHtml("divMessage", "Please Select Booking Type");
        return;
    }
    var memberTypeId = $("#ddlMemberType").val();
    if (memberTypeId == "") {
        SetHtml("divMessage", "Please Select Member Type");
        return;
    }

    if (bookingTypeId == 1) {
        var holeTypeId = $("#ddlHoleType").val();
        if (holeTypeId == "") {
            SetHtml("divMessage", "Please Select Hole Type");
            return;
        }
    }


    var txtStartDate = $("#txtStartDate").val();
    if (txtStartDate == "") {
        SetHtml("divMessage", "Please Select Start Date");
        return;
    }
    var txtEndDate = $("#txtEndDate").val();
    if (txtEndDate == "") {
        SetHtml("divMessage", "Please Select End Date");
        return;
    }


    $("#firstPage").hide();
    if (bookingTypeId == 1) {
        $("#pricingBTT").show();
        setTimeout(function () {
            $('#ddlGreenFeeTax').select2({ placeholder: "Select a Tax" });
            $('#ddlRangeFeeTax').select2({ placeholder: "Select a Tax" });
            $('#ddlAddOnCaddieTax').select2({ placeholder: "Select a Tax" });
            $('#ddlAddOnCartTax').select2({ placeholder: "Select a Tax" });
        }, 100);
    }
    else {
        $("#pricingBDR").show();
        setTimeout(function () {
            $('#ddlGreenFeeTax').select2({ placeholder: "Select a Tax" });
            $('#ddlRangeFeeTax').select2({ placeholder: "Select a Tax" });
            $('#ddlAddOnCaddieTax').select2({ placeholder: "Select a Tax" });
            $('#ddlAddOnCartTax').select2({ placeholder: "Select a Tax" });
        }, 100);
    }

}

function OnSaveButtonClick() {

    var holeTypeId = "";
    var bookingTypeId = $("#ddlBookingType option:selected").val();
    if (bookingTypeId == "") { return; }
    if (bookingTypeId == 1) {
        var holeTypeId = $("#ddlHoleType").val();
        if (holeTypeId == "") { return; }
    }

    var memberTypeId = $("#ddlMemberType").val();
    if (memberTypeId == "") { return; }
    var startDate = $("#txtStartDate").val();
    var endDate = $("#txtEndDate").val();

    var slotId = $("#ddlSlotTime").val();


    var greenFee = $("#txtGreenFee").val();
    var addOnCaddie = $("#txtAddonCaddie").val();
    var addOnCart = $("#txtAddOnCart").val();
    var rangeFee = $("#txtRangeFee").val();
    var greenFeeTax = $("#ddlGreenFeeTax").val();
    var rangeFeeTax = $("#ddlRangeFeeTax").val();
    var addOnCaddieTax = $("#ddlAddOnCaddieTax").val();
    var addOnCartTax = $("#ddlAddOnCartTax").val();


    var url = GetDomain(_DOMAINDIVID) + "AddSpecialPricingMultiple/SavePricing";
    var model = {
        "BookingTypeId": bookingTypeId,
        "HoleTypeArray": holeTypeId,
        "MemberTypeArray": memberTypeId,
        "GreenFee": greenFee,
        "RangeFee": rangeFee,
        "AddOnCaddie": addOnCaddie,
        "AddOnCart": addOnCart,
        "GreenFeeTax": greenFeeTax,
        "RangeFeeTax": rangeFeeTax,
        "AddOnCaddieTax": addOnCaddieTax,
        "AddOnCartTax": addOnCartTax,
        "StartDate": startDate,
        "EndDate": endDate,
        "SlotArray": slotId
    };

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
                    SetHtml("divMessage1", data.message);
                    break;
                case -1:
                    SetHtml("divMessage1", data.message);

                    break;
                case -2:
                    SetHtml("divMessage1", "SomeThing Goes Wrong");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divMessage1').html('<p>An Error Has Occurred</p>');
        }
    });

}