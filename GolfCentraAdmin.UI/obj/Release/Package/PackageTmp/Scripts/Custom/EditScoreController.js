
function increaseValue(id, count) {
    $("#divMessage").html("");
    if (id == "putt" || id == "sand" || id == "drive") {
        var putt = $("#putt" + count.toString()).val();
        var drive = $("#drive" + count.toString()).val();
        var sand = $("#sand" + count.toString()).val();
        var stroke = GetValueOrZero($("#stroke" + count.toString()).val());
        if (stroke > (parseInt(putt) + parseInt(drive) + parseInt(sand))) {
            var value = parseInt(document.getElementById(id + count).value, 10);
            value = isNaN(value) ? 0 : value;
            value++;
            document.getElementById(id + count).value = value;
        } else {
            $("#divMessage").html("Invalid Score Value");

        }

    } else {
        var value = parseInt(document.getElementById(id + count).value, 10);
        value = isNaN(value) ? 0 : value;
        value++;
        document.getElementById(id + count).value = value;
        CalculateScore();
    }
}

function decreaseValue(id, count) {
    $("#divMessage").html("");
    var value = parseInt(document.getElementById(id + count).value, 10);
    value = isNaN(value) ? 0 : value;
    value < 1 ? value = 1 : '';
    value--;
    document.getElementById(id + count).value = value;
    if (id == "putt" || id == "sand" || id == "drive") { } else {
        $("#putt" + count).val(0);
        $("#drive" + count).val(0);
        $("#sand" + count).val(0);
    }

    CalculateScore();
}

function RefreshCurrentPageData() {
    currentIndex = $('#activeIndex').html();
    $("#putt" + currentIndex).val(0);
    $("#drive" + currentIndex).val(0);
    $("#sand" + currentIndex).val(0);
    $("#stroke" + currentIndex.toString()).val(0);
    CalculateScore();
}

function CalculateScore() {
    var totalCountValue = $("#totalCountValue").html();
    var total = 0;
    var grossScore = 0;
    var par = 0;
    var strokes = 0;
    for (var i = 1; i <= totalCountValue; i++) {
        var p = parseInt($("#par" + i.toString()).html());
        var s = GetValueOrZero($("#stroke" + i.toString()).val());
        if (s != 0) {
            par = parseInt(par) + parseInt(p);
            strokes = parseInt(strokes) + parseInt(s);
        }
    }

    $("#divTotal").html(strokes);
    $("#divRoundTotal").html(parseInt(strokes) - parseInt(par));
}

function MoveToNextHole(count) {
    var s = GetValueOrZero($("#stroke" + count.toString()).val());
    if (s != 0) {
        $("#divMessage").html("");
        $("#carouselExampleControls").carousel("next");
        $("#carouselExampleControls").carousel("pause");
        $('#activeIndex').html(count+1);
    }
    else {
        $("#divMessage").html("Invalid Strokes Value");

    }
}

function MoveToPreviousHole(count) {

    $("#divMessage").html("");
    if (CheckNextHoleValue()) {
        $("#carouselExampleControls").carousel("prev");
        $("#carouselExampleControls").carousel("pause");
        $('#activeIndex').html(count-1);
    }
}

function OnAddScoreCardBegin() {
    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}



function OnAddScoreCardSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divMessage", "Updated Successfully");

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


function CheckNextHoleValue() {
    var currentIndex = $('div.active').index() + 1;
    var currentStroke = $("#stroke" + currentIndex.toString()).val();
    var nextStroke = $("#stroke" + (currentIndex + 1).toString()).val();

    if (currentStroke == 0) {
        if (nextStroke != 0) {
            $("#divMessage").html("Invalid Stroke Value");
            return false;
        }
    }
    CalculateScore();
    return true;
}