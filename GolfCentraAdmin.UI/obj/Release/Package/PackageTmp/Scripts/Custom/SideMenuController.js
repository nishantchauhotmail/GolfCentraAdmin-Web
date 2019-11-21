
function OnSideMenuReady() {
    GetAllSideMenuTable()

}

function GetAllSideMenuTable() {
    var url = GetDomain(_DOMAINDIVID) + "SideMenu/GetAllPageDetail";

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divSideMenuTable", data.message);
                    break;
                case -1:
                    SetHtml("divSideMenuTable", data.message);
                    Enablebutton("btnSubmit");
                    break;
                case -2:
                    SetHtml("divSideMenuTable", "SomeThing Goes Wrong");
                    Enablebutton("btnSubmit");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divSideMenuTable').html('<p>An Error Has Occurred</p>');
        }
    });
}

function EditPageOrdering(id) {
    var ordering = $("#txtOrdering").val();
    var url = GetDomain(_DOMAINDIVID) + "SideMenu/UpdatePageOrdering?id=" + id + "&ordering=" + ordering;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            switch (data.code) {
                case 0:
                    SetHtml("divMessagePopUp", "Page Order Updated Successfully");
                    GetAllSideMenuTable();
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
function UpdateUserPopUp(id, ordering) {
    var url = GetDomain(_DOMAINDIVID) + "SideMenu/UpdatePopUp?id=" + id + "&ordering=" + ordering;

    $.ajax({
        method: "POST",
        url: url,
        success: function (data) {

            data = eval(data);

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divSideMenuPopUp", data.message);
                    break;
                case -1:
                    SetHtml("divSideMenuPopUp", data.message);

                    break;
                case -2:
                    SetHtml("divSideMenuPopUp", "SomeThing Goes Wrong");

                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divSideMenuPopUp').html('<p>An Error Has Occurred</p>');
        }
    });
}