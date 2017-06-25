$(document).ready(function () {
    $("#StartMultiLink").click(function (event) {
        var name = $("#mazeName").val();
        var rows = $("#mazeRows").val();
        var cols = $("#mazeCols").val();
    });

    // Declare a proxy to reference the hub
    var game = $.connection.gameHub;
    // Start the connection
    $.connection.hub.start().done(function () {
        alert("startHi");
    });
    $.fn.startConnection = function(mazeName, mazeRows, mazeCols) {
// Create a function that the hub can call to broadcast messages
        game.client.Move = function(name) {
            alert("hihihihihi");
        };

    }
});
