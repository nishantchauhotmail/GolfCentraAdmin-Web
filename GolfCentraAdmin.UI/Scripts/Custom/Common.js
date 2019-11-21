var _ISTOSHOWALERT = true;
var _DOMAINDIVID = "divDomain";
var _LOADERDIVID = "divLoader";
var _POPUPLOADERDIVID = "divPopupLoader";
var _MESSAGEDIVID = "divMessage";
var _POPUPMESSAGEDIVID = "divPopupMessage";

var MESSAGES = {
    AddSuccessMessage: "SuccessFully Added",
    AddFailMessage: "Failed To Add Please Try Again Later",
    UpdateSuccessMessage: "SuccessFully Updated",
    UpdateFailMessage: "Failed To Update Please Try Again Later",
    UniversalSuccessMessage: "Successfully Done",
    UniversalFailMessage: "SomeThing Goes Wrong Please Try Again Later",
};

function MyAlert(msg) {
    try {
        if (_ISTOSHOWALERT) { alert(msg); }
    }
    catch (e) { }
}

//Get WebSite Url.
function GetDomain(domaindivid) {
    try {
        return $("#" + domaindivid).html();
    }
    catch (e) { MyAlert("GetDomain" + e); }
}

//Display Loader.
function DisplayLoader(loderdivId) {
    try { $("#" + loderdivId).show(); }
    catch (e) { MyAlert("DisplayLoader" + e); }
}

//Hide Loader.
function HideLoader(loderdivId) {
    try { $("#" + loderdivId).hide(); }
    catch (e) { MyAlert("HideLoader" + e); }
}

var Validate = {
    StringValueValidate: function validate(id, messagedivid, errormessage) {
        var value = $("#" + id).val();
        var div = "#" + messagedivid;
        if (value == "" || value == null) {
            $(div).html(errormessage);
            $(div).show();
            return false;
        }
        else {
            return true;
        }
    },

    StringHtmlValidate: function validate(id, messagedivid, errormessage) {
        var value = $("#" + id).html();
        var div = "#" + messagedivid;
        if (value == "" || value == null) {
            $(div).html(errormessage);
            $(div).show();
            return false;
        }
        else {
            return true;
        }
    },

    IntValueValidate: function validate(id, messagedivid, errormessage) {
        var value = $("#" + id).val();
        var div = "#" + messagedivid;
        if (value <= 0) {
            $(div).html(errormessage);
            $(div).show();
            return false;
        }
        else {
            return true;
        }
    },

    IntHtmlValidate: function validate(id, messagedivid, errormessage) {
        var value = $("#" + id).val();
        var div = "#" + messagedivid;
        if (value <= 0) {
            $(div).html(errormessage);
            $(div).show();
            return false;
        }
        else {
            return true;
        }
    }
}

//Email format is ok.
function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

// Return Number
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

//Disable Button.
function Disablebutton(btnId) {
    try {
        $("#" + btnId).prop("disabled", true);
    } catch (e) {
        MyAlert("Disablebutton" + e);
    }
}

//Enable Button.
function Enablebutton(btnId) {
    try {
        $("#" + btnId).prop("disabled", false);
    } catch (e) {
        MyAlert("Enablebutton" + e);
    }
}

//set HTML Blank.
function SetHtmlBlank(divId) {
    try {
        $("#" + divId).html("");
    } catch (e) {
        MyAlert("SetHtmlBlank : " + e);
    }
}


// Show Message.
function SetHtml(messagedivId, message) {
    try {
        SetHtmlBlank(messagedivId);
        $("#" + messagedivId).html(message);
        $("#" + messagedivId).show();
    }
    catch (e) { MyAlert("SetHtml : " + e); }
}

function CloseModalPopup(id) {
    $("#" + id).modal('hide');
   // $("#" + id).html('');
    $("#" + id).hide();
    //$("#" + id).removeClass("modal-open");
    //$("#" + id).removeClass("show");
    $("#" + id).html(''); 
    $(".modal-backdrop").remove();
    $("body").removeClass("modal-open");
    document.getElementById(id).setAttribute('aria-hidden', 'true');
}

function isDecimal(el, evt) {

    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    return true;
}

function PasswordStrengthVaildation(pass) {
    var strength = 0;
    var arr = [/.{8,}/, /[a-z]+/, /[0-9]+/, /[A-Z]+/, "/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/"];
    jQuery.map(arr, function (regexp) {
        if (pass.match(regexp))
            strength++;
    });
    if (pass.match(/.{8,}/)) { }
    else { return 0; }
    if (strength == 5 || strength == 4) { return 2; }
    else { return 1; }
}
function GetValueOrZero(value) {
    if (value == "") { return 0; }
    if (isNaN(value)) {
        return 0;
    }
    else { return parseFloat(value); }
}

function LogoutOperation() {
    alert("Session Expired,You redirect to login page");
    window.location = GetDomain(_DOMAINDIVID) + "Logout/index";
}

function SetCssToBody() {
    $("body").addClass("modal-open");
}
