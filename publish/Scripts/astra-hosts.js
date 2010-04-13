function ShowHostsDialog(url) {
    ShowURLDialog(url, "dialog_hosts", 400, 600);
};

function ShowHostDialog(url) {
    ShowURLDialog(url, "dialog_host", 350, 400);
};

function SubmitHost(url) {
    var Address = $("#Address").val();
    var UserName = $('#UserName').val();
    var UserPassword = $('#UserPassword').val();
    jQuery.ajax({
        type: "POST",
        url: url,
        data: { Address: Address, UserName: UserName, UserPassword: UserPassword },
        success: function(data) {
            setTimeout(function() {

                $("#dialog_host").dialog("close");
                $("#dialog_hosts").dialog("close");
                ShowHostsDialog("/ContactManager/Hosts/Host/Index");
            }, 500);
        },

        error: function(request, textStatus, errorThrown) {
            alert(request.statusText);
        }
    });
};

function ChangeHost(url) {

    jQuery.ajax({
    type: "POST",
        url: url,
        data: {},
        success: function(data) {
            setTimeout(function() {
                $("#dialog_hosts").dialog("close");
                ShowHostsDialog("/ContactManager/Hosts/Host/Index");
            }, 500);
        },

        error: function(request, textStatus, errorThrown) {
            alert(request.statusText);
        }
    });
};