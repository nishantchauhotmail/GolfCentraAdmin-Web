
function OnRoleManagementReady() {
    OnEmployeeChangeDropdown();
}

function OnEmployeeChangeDropdown() {
    try {
        $("#ddlEmployee").on("change", function () {
            var value = $("#ddlEmployee option:selected").val();
            if (value == "") {
                SetHtml("divEmployeeInformation", "");
                return;
            }
            SetHtml("divEmployeeInformation", "");
            var url = GetDomain(_DOMAINDIVID) + "RoleManagement/GetPermissionInformationByEmployeeId?employeeId=" + value;
            $.ajax({
                method: "POST",
                url: url,
                success: function (data) {

                    data = eval(data);
                    SetHtml("divEmployeeInformation", data.message);

                },
                error: function () {
                    SetHtml("divEmployeeInformation", "An Error Has Occurred");
                }
            });
        });
    }
    catch (e) {
        MyAlert("OnEmployeeChangeDropdown :" + e);
        return false;
    }
}


function OnEmployeeManagementSaveBegin() {
    SetHtmlBlank("divMessage");

    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnEmployeeManagementSaveSuccess(data) {

    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessage", "Updated Successfully");
            Enablebutton("btnSubmit");
            break;
        case -1:
            SetHtml("divMessage", "SomeThing Goes Wrong");
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