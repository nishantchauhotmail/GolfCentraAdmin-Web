
function OnUpdatePricingReady() {
    FillUpdatePricingTable();
}


function OnUpdatePricingBegin() {
    SetHtmlBlank("divMessage");
    
    if (Validate.StringValueValidate("txtNoOfAllowedSession", "divMessage", "Please Enter No Of Session.")) { }
    else { return false; }


    if (Validate.StringValueValidate("txtStartDate", "divMessage", "Please Enter Start Date.")) { }
    else { return false; }


    if (Validate.StringValueValidate("txtEndDate", "divMessage", "Please Enter End Date.")) { }
    else { return false; }
    //if (Validate.StringValueValidate("txtEmail", "divMessage", "Please Enter Email.")) { }
    //else { return false; }
    //if (Validate.IntValueValidate("txtPassword", "divMessage", "Please Enter Password.")) { }
    //else { return false; }
    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnUpdatePricingSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessage", "Saved Successfully");
            FillUpdatePricingTable();
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



function FillUpdatePricingTable() {
    var url = GetDomain(_DOMAINDIVID) + "UpdatePricing/GetAllPricing";

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divSearchResult", data.message);
                    break;
                case -1:
                    SetHtml("divSearchResult", data.message);

                    break;
                case -2:
                    SetHtml("divSearchResult", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divSearchResult').html('<p>An Error Has Occurred</p>');
        }
    });
}