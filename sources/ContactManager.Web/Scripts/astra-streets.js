function ShowStreetDialog(streets, url) {
    if (streets.value == 0)
        ShowURLDialog(url, "dialog_2", 300, 400);
}

function SubmitStreets(url, name, tag) {
    jQuery.ajax({
        type: "POST",
        url: url,
        data: { name: name.val(), tag: tag.val() },
        success: function(data) {
            ///setTimeout(function() {

            $("#dialog_2").dialog("close");
            populateDropdown($("#Street_StreetId"), data);

            //}, 500);
        },

        error: function(request, textStatus, errorThrown) {
            alert(request.statusText);
            //ShowMessage("AJAX error: " + request.statusText);
        }
    });
}

function populateDropdown(select, data) {
    select.append($('<option></option>').val(data.value).html(data.name).attr('selected', 'yes'));
}