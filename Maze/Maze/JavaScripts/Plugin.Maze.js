﻿(function ($) {
    var rows, cols;
    var curCol, curRow;
    var startCol, startRow;
    var endCol, endRow;
    var mazeCanvas, context;
    var cellWidth, cellHeight;
    var playerImg, mazeStr;

    $.fn.drawMaze = function(mazeData, // the matrix containing the maze cells
        myRows, myCols,
        myStartRow, myStartCol, // initial position of the player
        exitRow, exitCol, // the exit position
        playerImage, // player's icon (of type Image)
        exitImage // exit's icon (of type Image)
    ) {
        startRow = myStartRow;
        startCol = myStartCol;
        cols = myCols;
        rows = myRows;
        endCol = exitCol;
        endRow = exitRow;
        mazeStr = mazeData;
    
        playerImg = playerImage;
        curCol = startCol;
        curRow = startRow;
        mazeCanvas = this.get(0);
        context = mazeCanvas.getContext("2d");
        cellWidth = mazeCanvas.width / cols;
        cellHeight = mazeCanvas.height / rows;
        for (var i = 0; i < rows; i++) {
            for (var j = 0; j < cols; j++) {
                if (mazeData[i * cols + j] == 1) {
                    context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                }
            }
        };
        // draw the player and exit
        context.drawImage(playerImage, cellWidth * startCol, cellHeight * startRow, cellWidth, cellHeight);
        context.drawImage(exitImage, cellWidth * exitCol, cellHeight * exitRow, cellWidth, cellHeight);
        return this;
    };

    $.fn.move = function (direction) {
        switch (direction) {
        case "l":
            //left
                if (mazeStr[(curRow * cols) + curCol - 1] != 1 && (curCol - 1) >= 0) {
                context.clearRect(cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);
                curCol = curCol - 1;
                context.drawImage(playerImg, cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);
                    if (curCol == endCol && curRow == endRow) {
                        alert("you won!");
                    }
            }
            break;
        case "u":
            //up
                if (mazeStr[((curRow - 1) * cols) + curCol] != 1 && (curRow - 1) >= 0) {

                    context.clearRect(cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);
                curRow = curRow - 1;
                context.drawImage(playerImg, cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);
                if (curCol == endCol && curRow == endRow) {
                        alert("you won!");
                    }
            }
            break;
        case "r":
            //right
                if (mazeStr[(curRow * cols) + curCol + 1] != 1 && (curCol + 1) < cols) {

                    context.clearRect(cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);
                curCol = curCol + 1;
                context.drawImage(playerImg, cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);
                    if (curCol == endCol && curRow == endRow) {
                        alert("you won!");
                    }
            }
            break;
        case "d":
            //down
                if (mazeStr[((curRow + 1) * cols) + curCol] != 1 && (curRow + 1) < rows) {

                    context.clearRect(cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);
                curRow = curRow + 1;
                context.drawImage(playerImg, cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);
                    if (curCol == endCol && curRow == endRow) {
                        alert("you won!");
                    }
            }
            break;
        }
    };
    
    $.fn.solveMaze = function(data) {
        var length = data.length;
        context.clearRect(cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);
        curCol = startCol;
        curRow = startRow;
        context.drawImage(playerImg, cellWidth * curCol, cellHeight * curRow, cellWidth, cellHeight);
        (function myfunc(i) {
            switch ((data[i])) {
                case "0":
                //left
                    {
                        $("#mazeCanvas").move("l");
                        break;
                    }
            case "1":
                //right
                {
                    $("#mazeCanvas").move("r");
                    break;
                }
            case "2":
                //up
                {
                    $("#mazeCanvas").move("u");
                    break;
                }
            case "3":
                //down
                {
                    $("#mazeCanvas").move("d");
                    break;
                }
            default:{}
            }
            if (i >= 0) setTimeout(function () { myfunc(--i); }, 500);
        }(length));
};
})(jQuery);
