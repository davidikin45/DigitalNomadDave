window.onload = function () {

    var scriptPath = serverPath + '/files/static/js/',
        scriptsToLoad = [
            scriptPath + 'pikaday.js',
            scriptPath + 'datepickers.js'
        ];

    if (typeof blog !== 'undefined' && blog === true) {
        scriptsToLoad.push(scriptPath + 'jquery.auto-complete.min.js', scriptPath + 'autocomplete_blog.js');
    } else if (!useDropDowns) {
        scriptsToLoad.push(scriptPath + 'jquery.auto-complete.min.js', scriptPath + 'autocomplete.js');
    }

    if (!window.jQuery) {
        scriptsToLoad.unshift(scriptPath + "jquery-2.1.1.min.js");
    }

    if (HwTemplate == 'big') {
        scriptsToLoad.push(scriptPath + "bigTemplate.js");
    }
    if (HwTemplate == 'small') {
        scriptsToLoad.push(scriptPath + "smallTemplate.js");
    }

    head.load(scriptsToLoad, function () {
        expressPanelReady();
    });
};

function populateFindABedPanel(country) {
    var newCountry, thisEntry, newOption, i;
    newCountry = country.replace(/[^0-9a-zA-Z]/g, '');
    if (country.length == 0) {
        document.theForm.FABChoice.length = 1;
        document.theForm.FABChoice.options[0] = new Option("");
        document.theForm.FABChoice.options[0].value = '';
        document.theForm.FABChoice.options[1] = new Option(chooseACityPlaceholder);
        document.theForm.FABChoice.options[1].value = '';
        return;
    }
    if (country == 'Holland') {
        newCountry = 'Netherlands';
    }
    if (country == 'Britain') {
        newCountry = 'UK';
    }
    newOptions = window[newCountry + 'Array'];
    document.theForm.FABChoice.length = 1;
    document.theForm.FABChoice.options[0] = new Option(chooseACityPlaceholder);
    document.theForm.FABChoice.options[0].value = '';
    document.theForm.FABChoice.options[0].selected = true;
    for (i = 0; i < newOptions.length; i++) {
        thisEntry = newOptions[i];
        newOption = new Option(thisEntry);
        newOption.value = thisEntry + ';' + country;
        document.theForm.FABChoice.appendChild(newOption);
    }
}

function expressPanelReady() {
    window.WRI.search.initDatePickers({
        checkinSelector: 'date_from',
        checkoutSelector: 'date_to'
    });

    if (!useDropDowns) {
        BE.autocomplete.init();
        return;
    }

    if (chosenCountry) {
        document.theForm.FABCountryChoice.options[1].selected = true;
        document.theForm.FABCountryChoice.options[0].disabled = true;
        populateFindABedPanel(chosenCountry);
    }

    if (chosenCity) {
        document.theForm.FABChoice.options[1].selected = true;
        document.theForm.FABChoice.options[0].disabled = true;
    }
}