
function OnHoleInformationReady() {
    OnHoleNumberChangeDropdown();
}

function OnHoleNumberChangeDropdown() {
    try {
        $("#ddlHoleNumber").on("change", function () {
            var value = $("#ddlHoleNumber option:selected").val();
            if (value == "") {
                SetHtml("divHoleInformation", "");
                return;
            }
            SetHtml("divHoleInformation", "Please Wait..");
            var url = GetDomain(_DOMAINDIVID) + "HoleInformation/GetHoleInformationByHoleNumberId?holeNumberId=" + value;
            $.ajax({
                method: "POST",
                url: url,
                success: function (data) {

                    data = eval(data);
                    SetHtml("divHoleInformation", data.message);
                    
                },
                error: function () {
                    SetHtml("divHoleInformation", "An Error Has Occurred");
                }
            });
        });
    }
    catch (e) {
        MyAlert("OnHoleNumberChangeDropdown :" + e);
        return false;
    }
}


function OnHoleInfoSaveBegin() {
    SetHtmlBlank("divMessage");

    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnHoleInfoSaveSuccess(data) {

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