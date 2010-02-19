function ShowDialog(url) {
    jQuery.ajax({
        type: "GET",
        url: url,
        data: {},
        success: function(html) {
            setTimeout(function() {
                ShowPopup("dialog", html, 450, 600);
            }, 500);
        },

        error: function(request, textStatus, errorThrown) {
            ShowMessage("AJAX error: " + request.statusText);
        }
    });
}

function ShowURLDialog(url, element, height, width) {
    if (element.length <= 0)
        element = "dialog"

    jQuery.ajax({
        type: "GET",
        url: url,
        data: {},
        success: function(html) {
            setTimeout(function() {
                ShowPopup(element, html, height, width);
            }, 500);
        },

        error: function(request, textStatus, errorThrown) {
            ShowMessage("AJAX error: " + request.statusText);
        }
    });
}

function ShowPopup(elementId, html, height, width) {
    var popup = jQuery("#" + elementId).dialog();
    if (popup.length <= 0) {
        popup = jQuery("<div id='" + elementId + "' />");
        popup.dialog({ autoOpen: false });
    }

    popup.empty()
    .append(html)
    .dialog('option', 'modal', true)
    .dialog('option', 'resizable', false)
    .dialog('option', 'position', 'top')
    .dialog('option', 'height', height)
    .dialog('option', 'width', width)
    //.dialog('option', 'show', 'slide')
    //.dialog('option', 'title', src.attr('alt'));

    popup.dialog('option', 'buttons',
        {
            "Cancel":
                function() { $(this).dialog("close"); }
        });


    popup.dialog('open');
}