/// <reference path="jquery-ui-1.7.2/ui/ui.dialog.js" />

function ShowDialog(url) {
    ShowLoading()
    jQuery.ajax({
        type: "GET",
        url: url,
        data: {},
        success: function(html) {
            setTimeout(function() {
                HideLoading();
                ShowPopup("dialog", html, 450, 600);
            }, 500);
        },

        error: function(request, textStatus, errorThrown) {
            ShowMessage("AJAX error: " + request.statusText);
        }
    });
}

function ShowURLDialog(url, element, height, width) {
    ShowLoading()
    if (element.length <= 0)
        element = "dialog"

    jQuery.ajax({
        type: "GET",
        url: url,
        data: {},
        success: function(html) {
            setTimeout(function() {
                HideLoading();
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

function ShowLoading() {
    var load = GetElement("LoadingDialog");
    load.appendTo(document.body);
    $("#LoadingDialog").show();
}

function HideLoading() {
    var load = GetElement("LoadingDialog");
    $("#LoadingDialog").hide();
}

function GetElement(name) {
    var load = $("#" + name);
    if (load.length <= 0) {
        load = $('<div id="' + name + '"><div class=\'ui-loading\'></div></div>').addClass('ui-widget-overlay ');
        
    }
    
    return load;
}