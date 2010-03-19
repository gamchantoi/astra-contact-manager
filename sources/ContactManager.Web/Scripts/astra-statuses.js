function ShowStatusesDialog(url) {
    ShowURLDialog(url, "dialog_statuses", 400, 600);
};

function ShowStatusDialog(url) {
    ShowURLDialog(url, "dialog_status", 350, 400);
};

function SubmitStatus(url) {
    var Address = $("#Address").val();
    var UserName = $('#UserName').val();
    var UserPassword = $('#UserPassword').val();
    jQuery.ajax({
        type: "POST",
        url: url,
        data: { Address: Address, UserName: UserName, UserPassword: UserPassword },
        success: function(data) {
            setTimeout(function() {

                $("#dialog_status").dialog("close");
                $("#dialog_statuses").dialog("close");
                ShowHostsDialog("/ContactManager/Users/Status/Index");
            }, 500);
        },

        error: function(request, textStatus, errorThrown) {
            alert(request.statusText);
        }
    });
};