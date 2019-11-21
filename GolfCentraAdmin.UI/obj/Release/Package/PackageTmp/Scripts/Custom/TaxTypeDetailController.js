function OnTaxTypeDetailsReady(){
    GetAllTaxTypeTable()


}
function GetAllTaxTypeTable() {
    var url = GetDomain(_DOMAINDIVID) + "TaxTypeDetail/GetAllTaxType";
    SetHtml("divTaxDetailTable", "Please Wait..");
    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divTaxDetailTable", data.message);
                    break;
                case -1:
                    SetHtml("divTaxDetailTable", data.message);
                    Enablebutton("btnSubmit");
                    break;
                case -2:
                    SetHtml("divdivTaxDetailTable", "SomeThing Goes Wrong");
                    Enablebutton("btnSubmit");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divTaxDetailTable').html('<p>An Error Has Occurred</p>');
        }
    });
}
function SaveTaxType() {
    var txtName = $("#txtName").val();
    var txtPercentage = $("#txtPercentage").val();
    if (txtName == "") {
        SetHtml("divMessage", "Name Can Not Be Blank");
        return;
    }
    if (txtPercentage == "") {
        SetHtml("divMessage", "Percentage Can't  be Blank");
        return;
    }
    var url = GetDomain(_DOMAINDIVID) + "TaxTypeDetail/SaveTaxType?taxTypeName=" + txtName + "&percentage=" + txtPercentage;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divMessage", "TaxType Saved Successfully");
                    GetAllTaxTypeTable();
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
function UpdateTaxType(value) {
    var name = $("#txtNamePopUp").val();
    var percentage = $("#txtPercentagePopUp").val();
    if (name == "") {
        SetHtml("divMessagePopUp", "Tax Type Can't Be Blank");
        return;
    }
    var url = GetDomain(_DOMAINDIVID) + "TaxTypeDetail/UpdateTaxType?id=" + value + "&taxTypeName=" + name + "&percentage=" + percentage;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", "Tax Updated Successfully");
                    GetAllTaxTypeTable();
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
function DeleteTaxType(value) {

    var url = GetDomain(_DOMAINDIVID) + "TaxTypeDetail/DeleteTaxType?id=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", "Tax Type Deleted Successfully");
                    GetAllTaxTypeTable();
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
function UpdateUserPopUp(id, taxTypeName, taxPercentage) {
    var url = GetDomain(_DOMAINDIVID) + "TaxTypeDetail/UpdatePopUp?id=" + id + "&taxTypeName=" + taxTypeName + "&percentage=" + taxPercentage

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divTaxPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divTaxPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divTaxPopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divTaxPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}
function DeleteUserPopUp(value) {
    var url = GetDomain(_DOMAINDIVID) + "TaxTypeDetail/DeletePopUp?id=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divTaxPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divTaxPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divTaxPopUp", "SomeThing Goes Wrong");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divTaxPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}