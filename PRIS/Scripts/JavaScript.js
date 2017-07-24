function showLangDiv() {
    document.getElementById('LanguageDiv').style.display = "block";
}

function showWEDiv() {
    document.getElementById('divWE').style.display = "block";
}

function showSkillDiv() {
    document.getElementById('divSkills').style.display = "block";
}

function showFilterDiv() {
    document.getElementById('divFilter').style.display = "block";
}

//multiple select (not really can function)
$.getScript('http://davidstutz.github.io/bootstrap-multiselect/dist/js/bootstrap-multiselect.js', function () {

    $("#mySel3").multiselect({
        buttonContainer: '<div class="dropdown"/>',
        buttonClass: 'btn btn-primary btn-lg',
        onChange: function (option, checked) {
            var values = [];
            //            var optGroup = $(option).parent().attr('label');

            alert($("#mySel3").val());

        }

    });


});

//Fail
function directToPageTwo() {
    location.href = "~/Views/Main/MainPage.cshtml";
}