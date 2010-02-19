function ShowStreetDialog(value, url) {
    if (value == 0)
        ShowURLDialog(url, "dialog_2", 300, 400);
}

function SubmitStreets(url) {
    var name = $("#Name").val();
    var tag = $('#Tag').val();
    jQuery.ajax({
        type: "POST",
        url: url,
        data: { name: name, tag: tag },
        success: function(data) {
            setTimeout(function() {

            $("#dialog_2").dialog("close");
            $("#dialog_2").dialog("destroy");
            
            if ($("#Street_StreetId").length > 0 )
                populateDropdown($("#Street_StreetId"), data);
            
            }, 500);
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