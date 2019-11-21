

function SearchScore(id) {

    var txtEmail = $("#txtEmail").val();
    var txtDate = $("#txtDate").val();

    var txtSDate = $("#txtSDate").val();
    var txtStartDate = $("#txtStartDate").val();
    var txtEndDate = $("#txtEndDate").val();

    var txtArrayEmail = $("#txtArrayEmail").val();
    var txtArrayEmail1 = $("#txtArrayEmail1").val();
    var txtArrayEmail2 = $("#txtArrayEmail2").val();
    var txtArrayEmail3 = $("#txtArrayEmail3").val();
    var txtArrayEmail4 = $("#txtArrayEmail4").val();
    var date = "";
    if (id == 2) {
        date = txtDate
    } else {
        date = txtSDate
    }
    if (id == 1) {
        if (txtSDate == "") {
            SetHtml("divMessage1", "Date Can't Be Blank");
            return;
        }
    }
    if (id == 2) {
        var txtEmail = $("#txtEmail").val();
        var txtDate = $("#txtDate").val();
        if (txtEmail == "") {
            SetHtml("divMessage", "Email Can't Be Blank");
            return;
        }
        if (txtDate == "") {
            SetHtml("divMessage", "Date Can't Be Blank");
            return;
        }
    }
    if (id == 3) {

        if (txtStartDate == "") {
            SetHtml("divMessage2", "Start Date Can't Be Blank");
            return;
        }
        if (txtEndDate == "") {
            SetHtml("divMessage2", "End Date Can't Be Blank");
            return;
        }
    }

    SetHtml("divMessage", "Please Wait..");
    SetHtml("divMessage1", "Please Wait..");
    SetHtml("divMessage2", "Please Wait..");


    var scoreSearchViewModel = {
        Date: date,
        Email: txtEmail,
        StartDate: txtStartDate,
        EndDate: txtEndDate,
        SearchTypeId: id,
        EmailList: [txtArrayEmail, txtArrayEmail1, txtArrayEmail2, txtArrayEmail3, txtArrayEmail4]
    };
    var url = GetDomain(_DOMAINDIVID) + "ScoreDetail/GetScoreDetailByAdvanceSearch";

    $.ajax({
        method: "POST",
        url: url,
        data: scoreSearchViewModel,
        success: function (data) {

            data = eval(data);

            SetHtml("divMessage", "");
            SetHtml("divMessage1", "");
            SetHtml("divMessage2", "");

            HideLoader(_LOADERDIVID);
            switch (data.code) {
                case 0:
                    SetHtml("divScoreTable", data.message);
                    break;
                case -1:
                    SetHtml("divScoreTable", data.message);
                    Enablebutton("btnSubmit");
                    break;
                case -2:
                    SetHtml("divScoreTable", "SomeThing Goes Wrong");
                    Enablebutton("btnSubmit");
                    break;
                case -99:
                    LogoutOperation();
                    break;
            }
        },
        error: function () {
            $('#divScoreTable').html('<p>An Error Has Occurred</p>');
        }
    });
}

function SetTabValue() {

    $("#txtEmail").val();
    $("#txtDate").val();

    $("#txtSDate").val();
    $("#txtStartDate").val();
    $("#txtEndDate").val();

    $("#txtArrayEmail").val();

    SetHtml("divMessage", "");
    SetHtml("divMessage1", "");
    SetHtml("divMessage2", "");

}

function AddMoreOption() {


    var value = parseInt($("#emailCount").html());

    if (value <= 4) {
        var count = parseInt(value) + 1
        $("#divEmail" + count.toString()).show();
    }
    else {
        $("#btnAddMore").hide();
    }
    if (value == 4) { $("#btnAddMore").hide(); }
    $("#emailCount").html(parseInt(value) + 1);

}