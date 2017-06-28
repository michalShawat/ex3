$(document).ready(function() {
    $("#startLink").click(function (event) {
        var name = $("#mazeName").val();
        var rows = $("#mazeRows").val();
        var cols = $("#mazeCols").val();

        if (rows > 0 && cols > 0 && !(name == "")) {
        $('#myLoader').show();
        $.getJSON("../api/Single/" + name + "/" + rows + "/" + cols,
            function(data) {
                //var mazemaze = $("#mazeCanvas").drawMaze(data);
                var mazeData = data.Maze; // the matrix containing the maze cells
                var startRow = data.Start.Row;
                var startCol = data.Start.Col; // initial position of the player
                var exitRow = data.End.Row;
                var exitCol = data.End.Col; // the exit position
                var playerImage = document.getElementById("marco"); // player's icon (of type Image)
                var exitImage = document.getElementById("mother");
                $("#mazeCanvas").drawMaze(
                    mazeData, // the matrix containing the maze cells

                    rows,
                    cols,
                    startRow,
                    startCol, // initial position of the player
                    exitRow,
                    exitCol, // the exit position
                    playerImage, // player's icon (of type Image)
                    exitImage // exit's icon (of type Image)
                    //true // is the board enabled (i.e., player can move)
                    //function(direction, playerRow, playerCol) {
                        
                    //}
                    //    // a callback function which is invoked after each move
                );
                $('#myLoader').hide();
                document.title = name;
                });

        } else {
            alert("invalid input, try again");
        }
    });

    $("#solveLink").click(function (event) {
        var name = $("#mazeName").val();
        var algorithmType = document.getElementById("algoSelect").selectedIndex;
        $.getJSON("../api/Single/" + name + "/" + algorithmType,
            function (data) {
                $("#mazeCanvas").solveMaze(data);

            });
    }); 

    document.onkeydown = function (e) {
        alert("MF");
        switch (e.keyCode) {
        case 37:
            //left
            $("#mazeCanvas").move("l");
            break;
        case 38:
            //up
            $("#mazeCanvas").move("u");
            break;
        case 39:
            //right
            $("#mazeCanvas").move("r");
            break;
        case 40:
            //down
            $("#mazeCanvas").move("d");
            break;
        }
    }
});