tinymce.baseURL = '/bower_components/tinymce';

//https://www.tinymce.com/docs/demo/full-featured/

var init_obj_textareas = {
    mode: "specific_textareas",
    editor_selector: "mceEditor",
    plugins: [
        'advlist autolink lists link image imagetools charmap print preview anchor',
        'searchreplace visualblocks code fullscreen',
        'insertdatetime media table contextmenu paste code codesample wordcount autoresize'
    ],
    extended_valid_elements: "a[class|name|href|target|title|onclick|rel],script[type|src|async|defer|charset|language],iframe[src|style|width|height|scrolling|marginwidth|marginheight|frameborder],img[class|src|border=0|alt|title|hspace|vspace|width|height|align|onmouseover|onmouseout|name]",
    image_advtab: true,
    autoresize_bottom_margin: 20,
    content_css: '/css/site-xs.css',
    toolbar: 'preview fullscreen | undo redo | insert | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image media | codesample ',
    file_browser_callback: OpenFileManager
};

function OpenFileManager(field_name, url, type, win) {
    var roxyFileman = '/FileManager/index.html?integration=tinymce4';
    if (roxyFileman.indexOf("?") < 0) {
        roxyFileman += "?type=" + type;
    }
    else {
        roxyFileman += "&type=" + type;
    }
    roxyFileman += '&input=' + field_name + '&value=' + win.document.getElementById(field_name).value;
    if (tinyMCE.activeEditor.settings.language) {
        roxyFileman += '&langCode=' + tinyMCE.activeEditor.settings.language;
    }
    tinyMCE.activeEditor.windowManager.open({
        file: roxyFileman,
        title: 'File Manager',
        width: 850,
        height: 650,
        resizable: "yes",
        plugins: "media",
        inline: "yes",
        close_previous: "no"
    }, { window: win, input: field_name });
    return false;
}

tinyMCE.init(init_obj_textareas);