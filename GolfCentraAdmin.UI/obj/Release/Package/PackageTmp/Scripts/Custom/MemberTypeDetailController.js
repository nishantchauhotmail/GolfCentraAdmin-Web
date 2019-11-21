function OnMemberTypeDetailsReady() {
    GetAllMemberTypeTable()

}

function GetAllMemberTypeTable() {
    var url = GetDomain(_DOMAINDIVID) + "MemberTypeDetail/GetAllMemberType";
    SetHtml("divMemberDetailTable", "Please Wait..");
    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divMemberDetailTable", data.message);
                    break;
                case -1:
                    SetHtml("divMemberDetailTable", data.message);
                    Enablebutton("btnSubmit");
                    break;
                case -2:
                    SetHtml("divMemberDetailTable", "SomeThing Goes Wrong");
                    Enablebutton("btnSubmit");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divMemberDetailTable').html('<p>An Error Has Occurred</p>');
        }
    });
}

function SaveMemberType() {
    var txtName = $("#txtName").val();
    var txtValueToShow = $("#txtValueToShow").val();
    if (txtName == "") {
        SetHtml("divMessage", "Name Can Not Be Blank");
        return;
    }
    if (txtValueToShow == "") {
        SetHtml("divMessage", "ValueToShow Can Not Be Blank");
        return;
    }
    var url = GetDomain(_DOMAINDIVID) + "MemberTypeDetail/SaveMemberType?memberTypeName=" + txtName + "&memberTypeValue=" + txtValueToShow;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divMessage", "MemberType Saved Successfully");
                    GetAllMemberTypeTable();
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

function DeleteMemberType(value) {

    var url = GetDomain(_DOMAINDIVID) + "MemberTypeDetail/DeleteMemberType?id=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", "Member Type Deleted Successfully");
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

function UpdateMemberType(value) {
    var name = $("#txtNamePopUp").val();
    var valueToShow = $("#txtValueToShowPopUp").val();
    if (name == "") {
        SetHtml("divMessagePopUp", "Member Type Can Not Be Blank");
        return;
    }
    var url = GetDomain(_DOMAINDIVID) + "MemberTypeDetail/UpdateMemberType?id=" + value + "&memberType=" + name + "&valueToShow=" + valueToShow;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", "Member Updated Successfully");
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


function UpdateUserPopUp(id, memberTypeName, memberValue) {

   
    var url = GetDomain(_DOMAINDIVID) + "MemberTypeDetail/UpdatePopUp?id=" + id + "&memberType=" + memberTypeName.replace("^", "'") + "&valueToShow=" + memberValue.replace("^", "'");

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divMemberPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divMemberPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divMemberPopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divMemberPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}

function DeleteUserPopUp(value) {
    var url = GetDomain(_DOMAINDIVID) + "MemberTypeDetail/DeletePopUp?id=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divMemberPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divMemberPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divMemberPopUp", "SomeThing Goes Wrong");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divMemberPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}



