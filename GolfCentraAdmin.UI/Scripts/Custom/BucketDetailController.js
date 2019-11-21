function OnBucketDetailsReady() {
    GetAllBucketTypeTable()

}
function GetAllBucketTypeTable() {
    var url = GetDomain(_DOMAINDIVID) + "BucketDetail/GetAllBucketType";
    SetHtml("divBucketDetailTable", "Please Wait..");
    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divBucketDetailTable", data.message);
                    break;
                case -1:
                    SetHtml("divBucketDetailTable", data.message);
                    Enablebutton("btnSubmit");
                    break;
                case -2:
                    SetHtml("divBucketDetailTable", "SomeThing Goes Wrong");
                    Enablebutton("btnSubmit");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divBucketDetailTable').html('<p>An Error Has Occurred</p>');
        }
    });
}

function DeleteUserPopUp(value) {
    var url = GetDomain(_DOMAINDIVID) + "BucketDetail/DeletePopUp?id=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divBucketPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divBucketPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divBucketPopUp", "SomeThing Goes Wrong");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divBucketPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}

function DeleteBucketType(value) {

    var url = GetDomain(_DOMAINDIVID) + "BucketDetail/DeleteBucketType?id=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", "Bucket Type Deleted Successfully");
                    GetAllMemberTypeTable();
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

function TaxDetailsPopUp(value) {
    var data1 = value;
    alert(data1)

    var url = GetDomain(_DOMAINDIVID) + "BucketDetail/TaxDetailsPopUp";

    $.ajax({
        method: "POST",
        url: url,
        contentType: 'application/json; charset=utf-8',
        data: data1,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divBucketPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divBucketPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divBucketPopUp", "SomeThing Goes Wrong");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divBucketPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}


function SaveBucketType() {
    var txtCount = $("#txtCount").val();
    var txtPrice = $("#txtPrice").val();

    var txtTax = $("#ddlTax").val();
    
    
    

    if (txtCount == "") {
        SetHtml("divMessage", "Balls Can't  Be Blank");
        return;
    }
    if (txtPrice == "") {
        SetHtml("divMessage", "Price Can't  Be Blank");
        return;
    }
    var dayTypeId = $("#ddlDayType option:selected").val();
    if (dayTypeId == "") { SetHtml("divMessage", "Day Type Can't  Be Blank");return; }
    var memberTypeId = $("#ddlMemberType option:selected").val();
    if (memberTypeId == "") { SetHtml("divMessage", "Member Type Can't  Be Blank"); return; }
    
    var url = GetDomain(_DOMAINDIVID) + "BucketDetail/SaveBucketType"
    var model = {
        "Balls": txtCount,
        "Price": txtPrice,
        "DayTypeId": dayTypeId,
        "MemberTypeId": memberTypeId,
        "Taxs": txtTax,
        "DayTypeId": dayTypeId,
        "MemberTypeId": memberTypeId,

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
                    SetHtml("divMessage", "Bucket Saved Successfully");
                    GetAllEquipmentTypeTable();
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
        },
        error: function () {
            $('#divMessage').html('<p>An Error Has Occurred</p>');
        }
    });
}

function UpdateUserPopUp(id, dayTypeId, memberTypeId, price, tax, balls) {
    var taxs = tax.split(",");

    var model = {
        "Balls": balls,
        "Price": price,


        "Taxs": taxs,
        "DayTypeId": dayTypeId,
        "MemberTypeId": memberTypeId,
        "BucketDetailId":id

    };
    var url = GetDomain(_DOMAINDIVID) + "BucketDetail/UpdatePopUp"
 
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
                    SetHtml("divBucketPopUp", data.message);
                    var hdnTax = $("#hdnTax").html();
            
                    var taxs = hdnTax.split(",");
               
                    $("#ddlTax1").val(taxs);
                    break;
                case -1:
                    SetHtml("divBucketPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divBucketPopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divBucketPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}

function UpdateBucketType(value) {
    var count = $("#txtCountPopUp").val();
    var price = $("#txtPricePopUp").val();
    var tax = $("#ddlTax1").val();
    if (count == "") {
        SetHtml("divMessagePopUp", "Count Can't Be Blank");
        return;
    }
    if (price == "") {
        SetHtml("divMessagePopUp", "Price Can't  Be Blank");
        return;
    }
    var dayTypeId = $("#ddlDayTypePopUp option:selected").val();
    if (dayTypeId == "") { SetHtml("divMessagePopUp", "Day Type Can't  Be Blank"); return; }
    var memberTypeId = $("#ddlMemberTypePopUp option:selected").val();
    if (memberTypeId == "") { SetHtml("divMessagePopUp", "Member Type Can't  Be Blank"); return; }

    var url = GetDomain(_DOMAINDIVID) + "BucketDetail/UpdateBucketType?"
    var model = {
        "Balls": count,
        "Price": price,
        
        
        "Taxs": tax,
        "DayTypeId": dayTypeId,
        "MemberTypeId": memberTypeId,
        "BucketDetailId": value

    };

    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(model),
        contentType: "application/json",
        success: function (data) {

            data = eval(data);

            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", "Bucket Updated Successfully");
                    GetAllEquipmentTypeTable();
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