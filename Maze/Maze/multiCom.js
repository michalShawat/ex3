$(document).ready(function () {
    // Declare a proxy to reference the hub
    var game = $.connection.gameHub;
    // Start the connection
    $.connection.hub.start().done(function() {
        $("#StartMultiLink").click(function() {
            var name = $("#mazeName").val();
            var rows = $("#mazeRows").val();
            var cols = $("#mazeCols").val();
            // Call the Start method on the hub
            game.server.start(name, rows, cols);
        });

        $("#ListLink").click(function (event) {
            // Call the Start method on the hub
            var data = game.server.list();
            for (var i = 0; i < data.length; i++){
                var a = data[i].text;
                $("#ListLink").append($("<option>", a));
            }
        });

    });
});
