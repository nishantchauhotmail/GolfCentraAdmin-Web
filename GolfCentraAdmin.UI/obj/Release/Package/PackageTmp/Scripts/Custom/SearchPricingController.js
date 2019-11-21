function OnSearchPricingDetailReady() {
    try {
        FillSearchPricingTable();

    }
    catch (e) {
        MyAlert("OnSearchPricingDetailReady :" + e);
        return false;
    }
}

function FillSearchPricingTable() {
    SetHtml("divSearchPricing", "Please Wait..");
    var url = GetDomain(_DOMAINDIVID) + "SearchPricing/GetPricingDetails";

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divSearchPricing", data.message);
                    break;
                case -1:
                    SetHtml("divSearchPricing", data.message);

                    break;
                case -2:
                    SetHtml("divSearchPricing", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divSearchPricing').html('<p>An Error Has Occurred</p>');
        }
    });
}

