﻿$(document).ready(function () {
    $("#StartMultiLink").click(function (event) {
        var name = $("#mazeName").val();
        var rows = $("#mazeRows").val();
        var cols = $("#mazeCols").val();
        $("#player1").startConnection(name, rows, cols);
    });


    $.fn.startConnection = function(mazeName, mazeRows, mazeCols) {
// Declare a proxy to reference the hub
        var game = $.connection.GameHub;
// Create a function that the hub can call to broadcast messages
        game. = function(name, message) {
            // Add the message to the page
            alert("hihihihihi");
        };
// Get the user name and store it to prepend to messages
        var username = prompt('Enter your name:');
// Set initial focus to message input box
        $('#message').focus();
// Start the connection
        $.connection.hub.start().done(function() {
            $('#btnSendMessage').click(function() {
                // Call the Send method on the hub
                game.server.send(username, $('#message').val());
                // Clear text box and reset focus for next comment
                $('#message').val('').focus();
            });
        });
    }
});