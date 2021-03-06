﻿$(document).ready(function() {
    var rows, cols;
    var curCol, curRow;
    var secCurCol, secCurRow;
    var startCol, startRow;
    var endCol, endRow;
    var mazeCanvas, context, secondContext;
    var cellWidth, cellHeight;
    var playerImg, mazeStr, name;

    // Declare a proxy to reference the hub
    var game = $.connection.gameHub;

    game.client.parseList = function(data) {
        $("#ListLink").html("");
        for (var i = 0; i < data.length; i++) {
            $("#ListLink").append($("<option>" + data[i] + "</option>"));
        }
    }

    game.client.drawTheMaze = function(data) {
        mazeStr = data.Maze;
        rows = data.Rows;
        cols = data.Cols;
        startRow = data.Start.Row;
        startCol = data.Start.Col;
        endRow = data.End.Row;
        endCol = data.End.Col; // the exit position
        playerImg = document.getElementById("marco"); // player's icon (of type Image)
        var exitImage = document.getElementById("mother");
        cellWidth = document.getElementById("mazeCanvasPlayer1").width / cols;
        cellHeight = document.getElementById("mazeCanvasPlayer1").height / rows;
        $("#mazeCanvasPlayer1").drawMaze(
            mazeStr, // the matrix containing the maze cells
            rows,
            cols,
            startRow,
            startCol, // initial position of the player
            endRow,
            endCol, // the exit position
            playerImg, // player's icon (of type Image)
            exitImage); // exit's icon (of type Image)
        $("#myLoader").hide();
    }

    document.onkeydown = function(e) {
        switch (e.keyCode) {
        case 37:
            //left
            $("#mazeCanvasPlayer1").movePlayer("l");
            break;
        case 38:
            //up
            $("#mazeCanvasPlayer1").movePlayer("u");
            break;
        case 39:
            //right
            $("#mazeCanvasPlayer1").movePlayer("r");
            break;
        case 40:
            //down
            $("#mazeCanvasPlayer1").movePlayer("d");
            break;
        }
    }


    game.client.drawTheOtherMaze = function(data) {
        secCurCol = data.Start.Col;
        secCurRow = data.Start.Row;
        curRow = data.Start.Row;
        curCol = data.Start.Col;
        var mazeData = data.Maze;
        rows = data.Rows;
        cols = data.Cols;
        startRow = data.Start.Row;
        startCol = data.Start.Col;
        var exitRow = data.End.Row;
        var exitCol = data.End.Col; // the exit position
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

    game.client.updateSecondMaze = function(data) {
        secondContext = document.getElementById("mazeCanvasPlayer1").getContext("2d");
        switch (data) {
        case "l":
            //left
            secondContext.clearRect(cellWidth * secCurCol, cellHeight * secCurRow, cellWidth, cellHeight);
            secCurCol = secCurCol - 1;
            secondContext.drawImage(playerImg, cellWidth * secCurCol, cellHeight * secCurRow, cellWidth, cellHeight);

            // if lost
            if (secCurCol == endCol && secCurRow == endRow) {
                alert("you lost!");
                name = sessionStorage.UserName;
                $.getJSON("../api/Users/UpdateLose/" + name).done(function(data) {
                });
            }

            break;
        case "u":
            //up
            secondContext.clearRect(cellWidth * secCurCol, cellHeight * secCurRow, cellWidth, cellHeight);
            secCurRow = secCurRow - 1;
            secondContext.drawImage(playerImg, cellWidth * secCurCol, cellHeight * secCurRow, cellWidth, cellHeight);

            // if lost
            if (secCurCol == endCol && secCurRow == endRow) {
                alert("you lost!");
                name = sessionStorage.UserName;
                $.getJSON("../api/Users/UpdateLose/" + name).done(function(data) {
                });
            }

            break;
        case "r":
            //right

            secondContext.clearRect(cellWidth * secCurCol, cellHeight * secCurRow, cellWidth, cellHeight);
            secCurCol = secCurCol + 1;
            secondContext.drawImage(playerImg, cellWidth * secCurCol, cellHeight * secCurRow, cellWidth, cellHeight);
            // if lost
            if (secCurCol == endCol && secCurRow == endRow) {
                alert("you lost!");
                name = sessionStorage.UserName;
                $.getJSON("../api/Users/UpdateLose/" + name).done(function(data) {
                });
            }

            break;
        case "d":
            //down

            secondContext.clearRect(cellWidth * secCurCol, cellHeight * secCurRow, cellWidth, cellHeight);
            secCurRow = secCurRow + 1;
            secondContext.drawImage(playerImg, cellWidth * secCurCol, cellHeight * secCurRow, cellWidth, cellHeight);
            // if lost
            if (secCurCol == endCol && secCurRow == endRow) {
                alert("you lost!");
                name = sessionStorage.UserName;
                $.getJSON("../api/Users/UpdateLose/" + name).done(function(data) {
                });
            }


            break;
        }
    }

    // Start the connection
    $.connection.hub.start().done(function() {
        $("#StartMultiLink").click(function() {
            if (sessionStorage.getItem("UserName")) {
                $("#myLoader").show();
                var name = $("#mazeName").val();
                var rows = $("#mazeRows").val();
                var cols = $("#mazeCols").val();
                // Call the Start method on the hub
                game.server.start(name, rows, cols);
                window.document.title = name;
            } else {
                alert("you have to Register or Log in first!");
            }

        });

        $("#ListLink").click(function(event) {
            // Call the Start method on the hub
            game.server.list();
        });

        $("#JoinLink").click(function(event) {
            if (sessionStorage.getItem("UserName")) {
                $('#myLoader').show();
                var index = document.getElementById("ListLink").selectedIndex;
                var option = document.getElementById("ListLink").options;
                game.server.join(option[index].text);
                $('#myLoader').hide();
                window.document.title = option[index].text;
            } else {
                alert("you have to Register or Log in first!");
            }
        });

        $.fn.movePlayer = function(direction) {
            mazeCanvas = document.getElementById("mazeCanvasPlayer2");
            context = mazeCanvas.getContext("2d");
            switch (direction) {
            case "l":
                //left
                if (mazeStr[(curRow * cols) + curCol - 1] != 1 && (curCol - 1) >= 0) {
                    context.clearRect(cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);
                    curCol = curCol - 1;
                    context.drawImage(playerImg, cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);

                    // send to hub
                    game.server.play("l");

                    // if won
                    if (curCol == endCol && curRow == endRow) {
                        alert("you won!");
                        name = sessionStorage.UserName;
                        $.getJSON("../api/Users/UpdateWin/" + name).done(function (data) {
                        });
                    }
                }
                break;
            case "u":
                //up
                if (mazeStr[((curRow - 1) * cols) + curCol] != 1 && (curRow - 1) >= 0) {

                    context.clearRect(cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);
                    curRow = curRow - 1;
                    context.drawImage(playerImg, cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);

                    // send to hub
                    game.server.play("u");

                    if (curCol == endCol && curRow == endRow) {
                        alert("you won!");
                        name = sessionStorage.UserName;
                        $.getJSON("../api/Users/UpdateWin/" + name).done(function (data) {
                        });
                    }
                }
                break;
            case "r":
                //right
                if (mazeStr[(curRow * cols) + curCol + 1] != 1 && (curCol + 1) < cols) {

                    context.clearRect(cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);
                    curCol = curCol + 1;
                    context.drawImage(playerImg, cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);

                    // send to hub
                    game.server.play("r");

                    if (curCol == endCol && curRow == endRow) {
                        alert("you won!");
                        name = sessionStorage.UserName;
                        $.getJSON("../api/Users/UpdateWin/" + name).done(function (data) {
                        });
                    }
                }
                break;
            case "d":
                //down
                if (mazeStr[((curRow + 1) * cols) + curCol] != 1 && (curRow + 1) < rows) {

                    context.clearRect(cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);
                    curRow = curRow + 1;
                    context.drawImage(playerImg, cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);

                    // send to hub
                    game.server.play("d");

                    if (curCol == endCol && curRow == endRow) {
                        alert("you won!");
                        name = sessionStorage.UserName;
                        $.getJSON("../api/Users/UpdateWin/" + name).done(function (data) {
                        });
                    }
                }
                break;
            }
        };
    });
});