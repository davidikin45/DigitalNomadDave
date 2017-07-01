var lang = document.getElementsByName("Language")[0].value;

var langLocaleMap = {
    "English": 'en',
    "German": 'de',
    "French": 'fr',
    "Spanish": 'es',
    "Italian": 'it',
    "Chinese": 'zh-cn',
    "Brazilian": 'pt-BR',
    "Korean": 'ko'
};

moment.updateLocale(langLocaleMap[lang], {
    monthsShort: monthNames
});
