function OnEquipmentDetailsReady() {
    GetAllEquipmentTypeTable()

}
function GetAllEquipmentTypeTable() {
    var url = GetDomain(_DOMAINDIVID) + "EquipmentDetail/GetAllEquipmentType";
    SetHtml("divEquipmentDetailTable", "Please Wait..");
    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divEquipmentDetailTable", data.message);
                    break;
                case -1:
                    SetHtml("divEquipmentDetailTable", data.message);
                    Enablebutton("btnSubmit");
                    break;
                case -2:
                    SetHtml("divEquipmentDetailTable", "SomeThing Goes Wrong");
                    Enablebutton("btnSubmit");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divEquipmentDetailTable').html('<p>An Error Has Occurred</p>');
        }
    });
}
function DeleteEquipmentType(value) {

    var url = GetDomain(_DOMAINDIVID) + "EquipmentDetail/DeleteEquipmentType?id=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", "Equipment Type Deleted Successfully");
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
function DeleteUserPopUp(value) {
    var url = GetDomain(_DOMAINDIVID) + "EquipmentDetail/DeletePopUp?id=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divEquipmentPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divEquipmentPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divEquipmentPopUp", "SomeThing Goes Wrong");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divEquipmentPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}

function TaxDetailsPopUp(value) {
    var data1 = value;
    alert(data1)
   
    var url = GetDomain(_DOMAINDIVID) + "EquipmentDetail/TaxDetailsPopUp";

    $.ajax({
        method: "POST",
        url: url,
        contentType: 'application/json; charset=utf-8',
        data: data1 ,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divEquipmentPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divEquipmentPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divEquipmentPopUp", "SomeThing Goes Wrong");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divEquipmentPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}
function SaveEquipmentType() {
    var txtName = $("#txtName").val();
    var txtPrice = $("#txtPrice").val();
    
    var txtTax= $("#ddlTax").val();
    var txtDescription= $("#txtDescription").val();
    
    if (txtName == "") {
        SetHtml("divMessage", "Name Can Not Be Blank");
        return;
    }
    if (txtPrice== "") {
        SetHtml("divMessage", "Price Can Not Be Blank");
        return;
    }
    if (txtDescription == "") {
        SetHtml("divMessage", "Description Can Not Be Blank");
        return;
    }
    var url = GetDomain(_DOMAINDIVID) + "EquipmentDetail/SaveEquipmentType"
    var model = {
        "Name": txtName,
        "Price": txtPrice,
        "Descriptionn": txtDescription,
        "Taxs": txtTax
        
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
                    SetHtml("divMessage", "EquipmentType Saved Successfully");
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

function UpdateUserPopUp(id, tax, price, description,name,taxList) {
    var url = GetDomain(_DOMAINDIVID) + "EquipmentDetail/UpdatePopUp"
    var taxs = tax.split(",");
   
    var model = {
        "Name": name,
        "Price": price,
        "Description": description,
        "Taxs": taxs,
        "EquipmentId": id,
        "TaxManagementViewModels": taxList
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
                    SetHtml("divEquipmentPopUp", data.message);
                    var hdnTax = $("#hdnTax").html();
               
                    var taxs = hdnTax.split(",");
                   
                    $("#ddlTax1").val(taxs);
                    break;
                case -1:
                    SetHtml("divEquipmentPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divEquipmentPopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divEquipmentPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}


function UpdateEquipmentType(value) {
    var name = $("#txtNamePopUp").val();
    var price = $("#txtPricePopUp").val();
    var description = $("#txtDescriptionPopUp").val();
    var tax = $("#ddlTax1").val();
    if (name == "") {
        SetHtml("divMessagePopUp", "Equipment Name Can't Be Blank");
        return;
    }
    if (price == "") {
        SetHtml("divMessagePopUp", "Price Can't Be Blank");
        return;
    }
    var url = GetDomain(_DOMAINDIVID) + "EquipmentDetail/UpdateEquipmentType"
    var model = {
        "Name": name,
        "Price": price,
        "Description": description,
        "Taxs": tax,
        "EquipmentId": value
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
                    SetHtml("divMessagePopUp", "Equipment Updated Successfully");
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