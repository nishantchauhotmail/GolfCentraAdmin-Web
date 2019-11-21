
function OnRoleMasterReady() {
    GetAllRoleMasterTable()

}

function GetAllRoleMasterTable() {
    var url = GetDomain(_DOMAINDIVID) + "RoleMaster/GetAllEmployeeType";

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divRoleTable", data.message);
                    break;
                case -1:
                    SetHtml("divRoleTable", data.message);
                    Enablebutton("btnSubmit");
                    break;
                case -2:
                    SetHtml("divRoleTable", "SomeThing Goes Wrong");
                    Enablebutton("btnSubmit");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divRoleTable').html('<p>An Error Has Occurred</p>');
        }
    });
}

function SaveRoleType() {
    var value = $("#role").val();
    if (value == "") {
        SetHtml("divMessage", "Role Can't Be Blank");
        return;

}
    var url = GetDomain(_DOMAINDIVID) + "RoleMaster/SaveEmployeeType?value=" + value;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divMessage", "Role Type Saved Successfully");

                    GetAllRoleMasterTable();
                    Enablebutton("btnSubmit");
                    $("#role").val("")
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