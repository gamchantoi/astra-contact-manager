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
                ShowPopup(element, html, 350, 500);
            }, 500);
        },

        error: function(request, textStatus, errorThrown) {
            ShowMessage("AJAX error: " + request.statusText);
        }
    });
}

function ShowPopup(elementId, html, height, width) {
    var popup = jQuery(elementId).dialog();
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

function ShowStreetDialog(streets, url) {
    if (streets.value == 0)
        ShowURLDialog(url, "dialog_2", 450, 600);
}

function SubmitStreets(url, name, tag) {
    jQuery.ajax({
        type: "POST",
        url: url,
        data: { name: name.val(), tag: tag.val() },
        success: function(data) {
            //        setTimeout(function() {

            $("#dialog_2").dialog("close");
            populateDropdown($("#Street_StreetId"), data);

            //            }, 500);
        },

        error: function(request, textStatus, errorThrown) {
            alert(request.statusText);
            //ShowMessage("AJAX error: " + request.statusText);
        }
    });
}

function populateDropdown(select, data) {
    //select.html(''); // Clear dropDown
    ////var option = $.JSON.encode(data);
    ////alert('json encoded object:' + option);
    ////$("<option/>").val(option.SelectId).text(option.Name).appendTo(select);
    select.append($('<option></option>').val(data.value).html(data.name).attr('selected', 'yes'));
    //        $.each(data, function(id, option) {
    //            select.append($('<option></option>').val(option.value).html(option.name));
    //        });
}