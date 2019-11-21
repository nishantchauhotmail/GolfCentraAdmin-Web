
function OnStandardPricingReady() {
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
    if (bookingTypeId == 1) {
        var holeTypeId = $("#ddlHoleType option:selected").val();
        if (holeTypeId == "") {
            SetHtml("divMessage", "Please Select Hole Type");
            return;
        }
    }
    var dayTypeId = $("#ddlDayType option:selected").val();
    if (dayTypeId == "") {
        SetHtml("divMessage", "Please Select Day Type");
        return;
    }
    var memberTypeId = $("#ddlMemberType option:selected").val();
    if (memberTypeId == "") {
        SetHtml("divMessage", "Please Select Member Type");
        return;
    }
    var timeFormatId = $("#ddlTimeFormat option:selected").val();
    if (timeFormatId == "") {
        SetHtml("divMessage", "Please Select Time Type");
        return;
    }

    var url = GetDomain(_DOMAINDIVID) + "StandardPricing/GetPricingDetails";
    var model = {
        "BookingTypeId": bookingTypeId,
        "HoleTypeId": holeTypeId,
        "DayTypeId": dayTypeId,
        "MemberTypeId": memberTypeId,
        "TimeFormatId": timeFormatId
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
                    $("#firstPage").hide();

                    var pricing = eval(data.message);
                    $("#hdnPricingId").val(pricing.PricingId);

                    if (bookingTypeId == 1) {
                        $("#pricingBTT").show();
                        $("#txtGreenFee").val(pricing.GreenFee);
                        $("#txtAddonCaddie").val(pricing.AddOnCaddie);
                        $("#txtAddOnCart").val(pricing.AddOnCart);
                        $("#ddlGreenFeeTax").val(pricing.GreenFeeTax);
                        $("#ddlRangeFeeTax").val(pricing.RangeFeeTax);
                        $("#ddlAddOnCaddieTax").val(pricing.AddOnCaddieTax);
                        $("#ddlAddOnCartTax").val(pricing.AddOnCartTax);
                       
                        setTimeout(function () {
                            $('#ddlGreenFeeTax').select2();
                            $('#ddlRangeFeeTax').select2();
                            $('#ddlAddOnCaddieTax').select2();
                            $('#ddlAddOnCartTax').select2();
                        }, 100);
                    }
                    else {
                        $("#pricingBDR").show();
                        $("#txtRangeFee").val(pricing.RangeFee);
                    }

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

function OnSaveButtonClick() {

    var holeTypeId = "";
    var bookingTypeId = $("#ddlBookingType option:selected").val();
    if (bookingTypeId == "") { return; }
    if (bookingTypeId == 1) {
        var holeTypeId = $("#ddlHoleType option:selected").val();
        if (holeTypeId == "") { return; }
    }
    var dayTypeId = $("#ddlDayType option:selected").val();
    if (dayTypeId == "") { return; }
    var memberTypeId = $("#ddlMemberType option:selected").val();
    if (memberTypeId == "") { return; }
    var timeFormatId = $("#ddlTimeFormat option:selected").val();
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


    var url = GetDomain(_DOMAINDIVID) + "StandardPricing/SavePricing";
    var model = {
        "BookingTypeId": bookingTypeId,
        "HoleTypeId": holeTypeId,
        "DayTypeId": dayTypeId,
        "MemberTypeId": memberTypeId,
        "GreenFee": greenFee,
        "RangeFee": rangeFee,
        "AddOnCaddie": addOnCaddie,
        "AddOnCart": addOnCart,
        "PricingId": pricingId,
        "GreenFeeTax": greenFeeTax,
        "RangeFeeTax": rangeFeeTax,
        "AddOnCaddieTax": addOnCaddieTax,
        "AddOnCartTax": addOnCartTax,
        "TimeFormatId": timeFormatId
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