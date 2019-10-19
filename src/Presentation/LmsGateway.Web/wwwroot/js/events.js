$(document).ready(function () {
    GetEvent(1);

    $('.a-course').click(function (e) {
        e.preventDefault();
        GetEvent(this.href.split("/").pop());
    })
});

function GetEvent(eventId) {
    $("#span-" + eventId).show();

    $.ajax({
        type: "GET",
        dataType: 'html',
        data: { id: eventId },
        //url: "@Url.Action("GetEvent", "Events")",

        success: function (data) {
            $("#span-" + eventId).hide();
            $('#data').html(data);
        },

        error: function (request, status, error) {
            //isBusy(false);
            $("#span-" + eventId).hide();
            alert(error + " - Critical error occurred during message submission!");
        }
    })
}
