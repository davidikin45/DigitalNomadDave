var CountryList = [];

document.write("    <form action='http://www.hostelworld.com/affiliates/search' method='post' name='theForm' data-server='https://vsecure.bookhostels.com' \
        > \
 \
        <input type='hidden' name='UserID' value='davidikin45'> \
        <input type='hidden' name='SubID' value=''> \
        <input type=hidden name=Currency value='EUR'> \
        <input type=hidden name=Language value='English'> \
        <input type=hidden name=HostelNumber value=''> \
         \
        <script type='text/javascript'> \
            var Language = 'English'; \
            var closeText='Close'; \
            var serverPath = 'https://vsecure.bookhostels.com'; \
            var dayNames = [ 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday' ]; \
            var abbrDayNames = [ 'Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat' ]; \
            var tinyDayNames = [ 'S', 'M', 'T', 'W', 'T', 'F', 'S' ]; \
            var monthNames = [ 'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December' ]; \
            var abbrMonthNames = [ 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec' ]; \
            var useDropDowns = true; \
            var chooseACityPlaceholder = \"Choose a city\" \
        </script> \
        <script type='text/javascript' src='http://www.digitalnomaddave.com/js/hostel-world/moment.min.js'></script> \
        <script type='text/javascript' src='http://www.digitalnomaddave.com/js/hostel-world/momentLocale.js'></script> \
        <script type='text/javascript' src='http://www.digitalnomaddave.com/js/hostel-world/head.min.js'></script> \
        <script type='text/javascript' src='http://www.digitalnomaddave.com/js/hostel-world/expressSearchPaneIInit.js'></script> \
        <style type='text/css'> \
            @charset \"UTF-8\"; \
 \
/*! \
 * Pikaday \
 * Copyright Â© 2014 David Bushell | BSD & MIT license | http://dbushell.com/ \
 */ \
 \
.pika-single { \
    z-index: 9999; \
    display: block; \
    position: relative; \
    color: #333; \
    background: #fff; \
    border: 1px solid #ccc; \
    border-bottom-color: #bbb; \
    font-family: \"Helvetica Neue\", Helvetica, Arial, sans-serif; \
} \
 \
/* \
clear child float (pika-lendar), using the famous micro clearfix hack \
http://nicolasgallagher.com/micro-clearfix-hack/ \
*/ \
.pika-single:before, \
.pika-single:after { \
    content: \" \"; \
    display: table; \
} \
.pika-single:after { clear: both } \
.pika-single { *zoom: 1 } \
 \
.pika-single.is-hidden { \
    display: none; \
} \
 \
.pika-single.is-bound { \
    position: absolute; \
    box-shadow: 0 5px 15px -5px rgba(0,0,0,.5); \
} \
 \
.pika-lendar { \
    float: left; \
    width: 240px; \
    margin: 1rem; \
} \
 \
.pika-title { \
    position: relative; \
    text-align: center; \
} \
 \
.pika-label { \
    display: inline-block; \
    *display: inline; \
    position: relative; \
    z-index: 9999; \
    overflow: hidden; \
    margin: 0; \
    padding: 5px 3px; \
    font-size: 14px; \
    line-height: 20px; \
    font-weight: bold; \
    background-color: #fff; \
} \
.pika-title select { \
    cursor: pointer; \
    position: absolute; \
    z-index: 9998; \
    margin: 0; \
    left: 0; \
    top: 5px; \
    filter: alpha(opacity=0); \
    opacity: 0; \
} \
 \
.pika-prev, \
.pika-next { \
    display: block; \
    cursor: pointer; \
    position: relative; \
    outline: none; \
    border: 0; \
    padding: 0; \
    width: 20px; \
    height: 30px; \
    /* hide text using text-indent trick, using width value (it's enough) */ \
    text-indent: 20px; \
    white-space: nowrap; \
    overflow: hidden; \
    background-color: transparent; \
    background-position: center center; \
    background-repeat: no-repeat; \
    background-size: 75% 75%; \
    opacity: .5; \
    *position: absolute; \
    *top: 0; \
} \
 \
.pika-prev:hover, \
.pika-next:hover { \
    opacity: 1; \
} \
 \
.pika-prev, \
.is-rtl .pika-next { \
    float: left; \
    background-image: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAeCAYAAAAsEj5rAAAAUklEQVR42u3VMQoAIBADQf8Pgj+OD9hG2CtONJB2ymQkKe0HbwAP0xucDiQWARITIDEBEnMgMQ8S8+AqBIl6kKgHiXqQqAeJepBo/z38J/U0uAHlaBkBl9I4GwAAAABJRU5ErkJggg=='); \
    *left: 0; \
} \
 \
.pika-next, \
.is-rtl .pika-prev { \
    float: right; \
    background-image: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAeCAYAAAAsEj5rAAAAU0lEQVR42u3VOwoAMAgE0dwfAnNjU26bYkBCFGwfiL9VVWoO+BJ4Gf3gtsEKKoFBNTCoCAYVwaAiGNQGMUHMkjGbgjk2mIONuXo0nC8XnCf1JXgArVIZAQh5TKYAAAAASUVORK5CYII='); \
    *right: 0; \
} \
 \
.pika-prev.is-disabled, \
.pika-next.is-disabled { \
    cursor: default; \
    opacity: .2; \
} \
 \
.pika-select { \
    display: inline-block; \
    *display: inline; \
} \
 \
.pika-table { \
    width: 100%; \
    border-collapse: collapse; \
    border-spacing: 0; \
    border: 0; \
} \
 \
.pika-table th, \
.pika-table td { \
    width: 14.285714285714286%; \
    padding: 0; \
    border: 1px solid #e2e2e2; \
} \
 \
.pika-table th { \
    color: #999; \
    font-size: 12px; \
    line-height: 25px; \
    font-weight: bold; \
    text-align: center; \
} \
 \
.pika-button { \
    cursor: pointer; \
    display: block; \
    box-sizing: border-box; \
    -moz-box-sizing: border-box; \
    outline: none; \
    border: 0; \
    margin: 0; \
    width: 100%; \
    padding: 5px; \
    color: #222; \
    font-size: 12px; \
    line-height: 15px; \
    text-align: right; \
    background: #fff; \
} \
 \
.pika-week { \
    font-size: 11px; \
    color: #999; \
} \
 \
.is-today .pika-button { \
    color: #ff7346; \
    font-weight: bold; \
} \
 \
.is-disabled.is-today .pika-button { \
    color: #ff7346; \
} \
 \
.is-selected .pika-button { \
    color: #fff; \
    font-weight: bold; \
    background: #ff7346; \
} \
 \
.is-inrange .pika-button { \
    background: #D5E9F7; \
} \
 \
.is-startrange .pika-button { \
    color: #fff; \
    background: #6CB31D; \
    box-shadow: none; \
} \
 \
.is-endrange .pika-button { \
    color: #fff; \
    background: #ff7346; \
    box-shadow: none; \
} \
 \
.is-disabled .pika-button, \
.is-outside-current-month .pika-button { \
    pointer-events: none; \
    cursor: default; \
    color: #999; \
    background-color: #f5f5f5; \
} \
 \
.pika-button:hover { \
    color: #222; \
    background: #ffecdd; \
    box-shadow: none; \
} \
 \
/* styling for abbr */ \
.pika-table abbr { \
    border-bottom: none; \
    cursor: help; \
} \
 \
 \
            .autocomplete-suggestions { \
    text-align: left; cursor: default; border: 1px solid #ccc; border-top: 0; background: #fff; box-shadow: -1px 1px 3px rgba(0,0,0,.1); \
 \
    /* core styles should not be changed */ \
    position: absolute; display: none; z-index: 9999; max-height: 254px; overflow: hidden; overflow-y: auto; box-sizing: border-box; \
} \
.autocomplete-suggestion { position: relative; padding: 0 .6em; line-height: 23px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; font-size: 1.02em; color: #333; } \
.autocomplete-suggestion b { font-weight: normal; color: #1f8dd6; } \
.autocomplete-suggestion.selected { } \
 \
/* custom */ \
.autocomplete-suggestions  { \
    background-color: white; \
    font-family: \"Helvetica Neue\", Helvetica, Arial, sans-serif; \
    font-size: 12px; \
} \
 \
.autocomplete-suggestion { \
    background-color: white; \
    padding: .5rem; \
    cursor: pointer; \
    white-space: normal; \
    overflow: visible; \
    text-overflow: initial; \
    border-bottom: 1px dotted #ddd; \
    line-height: 1.4; \
} \
 \
.autocomplete-suggestion:last-child { \
    border-bottom: none; \
} \
 \
.autocomplete-suggestion:hover { \
    background-color: #ffecdd; \
} \
 \
.autocomplete-suggestion strong { \
    color: #ff7346; \
} \
 \
.autocomplete-category { \
    color: #222; \
    font-weight: bold; \
    text-transform: uppercase; \
    background-color: #f0f0f0; \
    padding-left: .5rem; \
    padding-bottom: .2rem; \
    padding-top: .2rem; \
    font-size: 11px; \
} \
            .CalendarImage { \
                cursor: pointer; \
                vertical-align: middle; \
                display:inline; \
                pointer-events: none; \
             } \
        </style> \
        <script type='text/javascript'> var HwTemplate = 'small';</script> \
        <style> \
/*************************** Font Lists **************************/ \
/* All font sizes will have an option extra font which can be \
/* defined As Needed \
/*****************************************************************/ \
 \
    form[name=theForm] { \
        line-height: 1; \
    } \
 \
    table.hw-search-panel { \
        padding: 15px; \
        border-radius: 3px; \
        width: 100%; \
        max-width: 320px; \
        border: 0; \
        border-collapse: separate; \
        margin: 0; \
        background-color: #313131!important; \
    } \
 \
    table.hw-search-panel td[align=\"center\"] { \
        text-align: center; \
    } \
 \
    table.hw-search-panel table { \
        border-radius: 3px; \
        border: 0; \
        margin-bottom: 0; \
        width: 100%; \
        border-collapse: separate; \
        background-color: #313131!important; \
    } \
 \
    table.hw-search-panel td, \
    table.hw-search-panel table td { \
        border: 0; \
        padding: 0; \
        background-color: #313131!important; \
    } \
 \
    #hw-search.hw-search-panel select, \
    #hw-search.hw-search-panel input { \
        font-family: arial, helvetica; \
        font-size: 12px; \
        color: #444; \
        border-radius: 3px; \
        border: 0; \
		margin-bottom: 20px; \
		margin-top: 3px; \
        max-height: 30px; \
        text-indent: 5px; \
    } \
 \
    #hw-search.hw-search-panel select, \
    #hw-search.hw-search-panel input[type=\"submit\"] { \
		width: 100%; \
        height: 30px; \
	} \
 \
    #hw-search.hw-search-panel #date_from, \
    #hw-search.hw-search-panel #date_to { \
        width: 100%!important; \
        max-width: 100%!important; \
        margin-right: 0; \
        margin-left: 0; \
        font-size: 12px!important; \
        padding: 0!important; \
        height: 30px; \
        text-indent: 13px; \
    } \
 \
    #hw-search.hw-search-panel label { \
        color: #fff; \
        text-transform: uppercase; \
        font-size: 10px; \
        font-family: arial, helvetica; \
        font-weight: bold; \
    } \
 \
    #hw-search.hw-search-panel .logo { \
        width: 70%; \
        margin: 10px 0 25px; \
    } \
 \
    #hw-search.hw-search-panel .relative { \
        position: relative; \
        background-color: #313131!important; \
    } \
 \
    #hw-search.hw-search-panel .hw-search__field-icon { \
        position: absolute; \
        right: 10px; \
        bottom: 27px; \
        width: 15px; \
        pointer-events: none; \
    } \
 \
    #hw-search.hw-search-panel .hw-submit { \
        padding: 5px 0; \
        background-color: #ff7346; \
        color: #fff; \
        margin-bottom: 0; \
        text-transform: uppercase; \
        cursor: pointer; \
    } \
 \
    #hw-search .hw-search-lg__search-field { \
        color: #000; \
        text-align: left; \
    } \
 \
    #hw-search .hw-search-lg__search-field label { \
        color: #fff; \
        text-transform: uppercase; \
        font-size: 10px; \
        font-family: arial, helvetica; \
        font-weight: bold; \
        line-height: 1.1; \
    } \
 \
    #hw-search .hw-search-lg__search-field input[type=text], \
    #hw-search .hw-search-lg__search-field select{ \
        font-family: arial, helvetica; \
        font-size: 12px!important; \
        color: #444; \
        border-radius: 3px; \
        border: 0; \
        margin-top: 3px; \
        width: 100%; \
        max-width: 100%; \
        max-height: 30px; \
        height: 30px; \
        padding: 0; \
    } \
 \
    #hw-search.hw-search-panel #number_of_guests { \
        text-indent: 5px; \
    } \
 \
    #hw-search .hw-search-lg__search-field--printed { \
        margin-bottom: 20px; \
        display: block; \
    } \
 \
    #hw-search .hw-search-lg__search-field--printed label { \
        font-size: 12px; \
    } \
 \
    ::-webkit-input-placeholder { \
        color: #444; \
    } \
 \
    :-moz-placeholder { /* Firefox 18- */ \
        color: #444; \
    } \
 \
    ::-moz-placeholder {  /* Firefox 19+ */ \
        color: #444; \
    } \
 \
    :-ms-input-placeholder { \
        color: #444; \
    } \
    #hw-search .noResults, \
    #hw-search .freeTextError { \
        display: none; \
        padding: .3rem; \
        font-size: .7rem; \
        font-family: arial, helvetica; \
        color: #9d0000; \
        line-height: 1rem; \
        background: #ffe5e5; \
        margin-top: .5rem; \
        margin-bottom: .5rem; \
        border: 1px solid #ffb8b8; \
        font-style: normal; \
        text-align: left; \
        border-radius: 3px!important; \
    } \
 \
    #hw-search .noResults { \
        background-color: #fff; \
        color: #444; \
        border: 1px solid #fff; \
    } \
 \
    #hw-search .hw-search-lg__free-text--error #free-text-search { \
        margin-bottom: 0; \
    } \
 \
/*    #hw-search .hw-search-lg__free-text--error .freeTextError { \
        display: block; \
    }*/ \
</style> \
 \
<table id=\"hw-search\" class=\"hw-search-panel\" border=0 cellpadding=0 cellspacing=0 bgcolor=#313131> \
 <tr> \
  <td> \
      <input id=\"translations\" type=\"hidden\" data-city=\"City\" data-property=\"Property\"> \
<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=#313131> \
 <tr valign=\"middle\"> \
  <td colspan=3 id=hback align=center> \
      <img class=\"logo\" src=\"http://ucd.hwstatic.com/hw/affiliate/hw-logo.svg\" /> \
  </td> \
 </tr> \
 <tr valign=\"top\"> \
  <td> \
   <table cellpadding=0 cellspacing=0 align=center width=100%> \
    <tr> \
     <td> \
      <table cellpadding=0 cellspacing=0 border=0 align=center > \
                <div class='hw-search-lg__search-field hw-search-lg__half hw-search-lg__search-country'> \
            <label>Choose A Country:</label><br> \
                    <select class='formbk' name='FABCountryChoice' \
            onChange='populateFindABedPanel(this.options[selectedIndex].value);'> \
            <option value=''>Choose a Country</OPTION> \
            <script type='text/javascript'> \
                for(var x = 0; x < 0; x++) { \
                    document.write('<option value=\"' + CountryList[x] + '\"'); \
                    document.write('>'+CountryList[x]+'</option>'); \
                } \
            </script> \
    </select> \
        </div> \
        <div class='hw-search-lg__search-field hw-search-lg__half hw-search-lg__search-city'> \
            <label>Choose A City</label><br> \
            <select class='formbk' name='FABChoice'> \
                <option value=''>Choose A City</option> \
            </select> \
        </div> \
       <tr> \
        <td class=\"relative\"> \
            <label>Check In:</label> \
            <input readonly=\"true\" type=\"text\" autocomplete=\"off\" id=\"date_from\" value=\"\" style=\"cursor:pointer;\"> \
            <img class='hw-search__field-icon' src='http://ucd.hwstatic.com/hw/affiliate/calendar.svg' alt='Calendar' > \
            <input type=\"hidden\" name=\"date_from\" id=\"date_from_submit\"/> \
        </td> \
       </tr> \
       <tr> \
        <td class=\"relative\"> \
            <label>Check Out:</label> \
            <input readonly=\"true\" type=\"text\" autocomplete=\"off\" id=\"date_to\" value=\"\"  style=\"cursor:pointer;\"> \
            <img class='hw-search__field-icon'  src='http://ucd.hwstatic.com/hw/affiliate/calendar.svg' alt='Calendar' > \
            <input type=\"hidden\" name=\"date_to\" id=\"date_to_submit\"/> \
        </td> \
       </tr> \
       <tr valign=bottom> \
        <td> \
            <label>Guests:</label> \
            <select id=\"number_of_guests\" name=\"number_of_guests\"> \
                <option value='1'>1 Guest</option> \
                <option value='2' selected=\"selected\">2 Guests</option> \
                <option value='3'>3 Guests</option> \
                <option value='4'>4 Guests</option> \
                <option value='5'>5 Guests</option> \
                <option value='6'>6 Guests</option> \
                <option value='7'>7 Guests</option> \
                <option value='8'>8 Guests</option> \
            </select> \
        </td> \
       </tr> \
       <tr> \
        <td> \
         <input class=\"hw-submit\" type=\"submit\" name=\"Submit\" data-valid=\"0\" value=\"search\"> \
        </td> \
       </tr> \
      </table> \
     </td> \
    </tr> \
   </table> \
  </td> \
 </tr> \
</table> \
  </td> \
 </tr> \
</table> \
 \
    </form> \
<!----> \
 \
");
var chosenCity = '';
var chosenCountry = '';