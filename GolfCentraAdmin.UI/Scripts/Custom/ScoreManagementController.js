function OnSearchScoreAddBegin() {
    SetHtmlBlank("divMessage");
    SetHtmlBlank("divScoreTable");

    
  
    DisplayLoader(_LOADERDIVID);
    Disablebutton("btnSubmit");
}

function OnSearchScoreSuccess(data) {
    HideLoader(_LOADERDIVID);
    switch (data.code) {
        case 0:
            SetHtml("divScoreTable", data.message);
            Enablebutton("btnSubmit");
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