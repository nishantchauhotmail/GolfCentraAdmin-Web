function OnCouponAddBegin() {
    SetHtmlBlank("divMessage");
    if (Validate.StringValueValidate("txtName", "divMessage", "Please Enter Name.")) { }
    else { return false; }
    if (Validate.StringValueValidate("txtCode", "divMessage", "Please Enter Code.")) { }
    else { return false; }
    if (Validate.IntValueValidate("txtAmount", "divMessage", "Please Enter Amount.")) { }
    else { return false; }
    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnCouponAddSuccess(data) {
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


function OnCouponDetailsReady() {
    GetAllCouponTable()

}

function GetAllCouponTable() {
    var url = GetDomain(_DOMAINDIVID) + "CouponDetail/GetAllActiveCoupon";
    SetHtml("divCouponDetailTable", "Please Wait..");
    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divCouponDetailTable", data.message);
                    break;
                case -1:
                    SetHtml("divCouponDetailTable", data.message);
                    
                    break;
                case -2:
                    SetHtml("divCouponDetailTable", "SomeThing Goes Wrong");
                 
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divCouponDetailTable').html('<p>An Error Has Occurred</p>');
        }
    });
}

function DeletePopUp(value) {
    var url = GetDomain(_DOMAINDIVID) + "CouponDetail/DeletePopUp?id=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divCouponPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divCouponPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divCouponPopUp", "SomeThing Goes Wrong");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divCouponPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}

function DeleteCoupon(value) {

    var url = GetDomain(_DOMAINDIVID) + "CouponDetail/DeleteCoupon?id=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", "Coupon Deleted Successfully");
                    GetAllCouponTable();
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