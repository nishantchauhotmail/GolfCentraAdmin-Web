
function OnStandardPricingReady() {
    $('#ddlHoleType').select2({ placeholder: "Select A Hole Type" });
    $('#ddlDayType').select2({ placeholder: "Select a Day Type" });
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
   
    var dayTypeId = $("#ddlDayType").val();
    if (dayTypeId == "") {
        SetHtml("divMessage", "Please Select Day Type");
        return;
    }
    var memberTypeId = $("#ddlMemberType").val();
    if (memberTypeId == "") {
        SetHtml("divMessage", "Please Select Member Type");
        return;
    }
    var timeFormatId = $("#ddlTimeFormat option:selected").val();
    if (timeFormatId == "") {
        SetHtml("divMessage", "Please Select Time Type");
        return;
    }
    if (bookingTypeId == 1) {
        var holeTypeId = $("#ddlHoleType").val();
        if (holeTypeId == "") {
            SetHtml("divMessage", "Please Select Hole Type");
            return;
        }
    }
    $("#firstPage").hide();
    if (bookingTypeId == 1) {
        $("#pricingBTT").show();     
        setTimeout(function () {
            $('#ddlGreenFeeTax').select2({ placeholder: "Select a Tax"});
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
        var holeTypeId = $("#ddlHoleType option:selected").val();
        if (holeTypeId == "") { return; }
    }
    var dayTypeId = $("#ddlDayType").val();
    if (dayTypeId == "") { return; }
    var memberTypeId = $("#ddlMemberType").val();
    if (memberTypeId == "") { return; }
    var timeFormatId = $("#ddlTimeFormat").val();
    if (timeFormatId == "") {
        SetHtml("divMessage", "Please Select Time Type");
        return;
    }


    var greenFee = $("#txtGreenFee").val();
    var addOnCaddie = $("#txtAddonCaddie").val();
    var addOnCart = $("#txtAddOnCart").val();
    var rangeFee = $("#txtRangeFee").val();
    var pricingId = $("#hdnPricingId").val();
    var greenFeeTax = $("#ddlGreenFeeTax").val();
    var rangeFeeTax = $("#ddlRangeFeeTax").val();
    var addOnCaddieTax = $("#ddlAddOnCaddieTax").val();
    var addOnCartTax = $("#ddlAddOnCartTax").val();


    var url = GetDomain(_DOMAINDIVID) + "StandardPricingMultiple/SavePricing";
    var model = {
        "BookingTypeId": bookingTypeId,
        "HoleTypeArray": holeTypeId,
        "DayArray": dayTypeId,
        "MemberTypeArray": memberTypeId,
        "GreenFee": greenFee,
        "RangeFee": rangeFee,
        "AddOnCaddie": addOnCaddie,
        "AddOnCart": addOnCart,
        "PricingId": pricingId,
        "GreenFeeTax": greenFeeTax,
        "RangeFeeTax": rangeFeeTax,
        "AddOnCaddieTax": addOnCaddieTax,
        "AddOnCartTax": addOnCartTax,
        "TimeFormatArray": timeFormatId
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