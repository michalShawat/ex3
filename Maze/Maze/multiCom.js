$(document).ready(function () {
    // Declare a proxy to reference the hub
    var game = $.connection.gameHub;

    game.client.parseList = function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#ListLink").append($("<option>" + data[i] + "</option>"));
        }
    }

    game.client.drawTheMaze = function (data) {
        var mazeData = data.Maze;
        var rows = data.Rows;
        var cols = data.Cols;
        var startRow = data.InitialPos.Row;
        var startCol = data.InitialPos.Col;
        var exitRow = data.GoalPos.Row;
        var exitCol = data.GoalPos.Col; // the exit position
        var playerImage = document.getElementById("marco"); // player's icon (of type Image)
        var exitImage = document.getElementById("mother");
        $("#mazeCanvasPlayer1").drawMaze(
            mazeData, // the matrix containing the maze cells
            rows,
            cols,
            startRow,
            startCol, // initial position of the player
            exitRow,
            exitCol, // the exit position
            playerImage, // player's icon (of type Image)
            exitImage); // exit's icon (of type Image)
    }

    game.client.drawTheOtherMaze = function (data) {
        var mazeData = data.Maze;
        var rows = data.Rows;
        var cols = data.Cols;
        var startRow = data.InitialPos.Row;
        var startCol = data.InitialPos.Col;
        var exitRow = data.GoalPos.Row;
        var exitCol = data.GoalPos.Col; // the exit position
        var playerImage = document.getElementById("marco"); // player's icon (of type Image)
        var exitImage = document.getElementById("mother");
        $("#mazeCanvasPlayer2").drawMaze(
            mazeData, // the matrix containing the maze cells
            rows,
            cols,
            startRow,
            startCol, // initial position of the player
            exitRow,
            exitCol, // the exit position
            playerImage, // player's icon (of type Image)
            exitImage); // exit's icon (of type Image)
    }

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
            game.server.list();
        });

        $("#JoinLink").click(function (event) {
            var index = document.getElementById("ListLink").selectedIndex;
            var option = document.getElementById("ListLink").options;
            game.server.join(option[index].text);
            
        });


    });
});
