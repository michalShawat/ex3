(function ($) {
    var rows, cols;
    var curCol, curRow;
    var mazeCanvas, context;
    var cellWidth, cellHeight;
    var playerImg, mazeStr;

    $.fn.drawMaze = function(mazeData, // the matrix containing the maze cells
        myRows, myCols,
        startRow, startCol, // initial position of the player
        exitRow, exitCol, // the exit position
        playerImage, // player's icon (of type Image)
        exitImage // exit's icon (of type Image)
        ///isEnabled // is the board enabled (i.e., player can move)
        //,function (direction, playerRow, playerCol)
    ) {
        cols = myCols;
        rows = myRows;
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
                if (mazeData[i * rows + j] == 1) {
                    context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                }
            }
            context.drawImage(playerImage, cellWidth * startCol, cellHeight * startRow, cellHeight, cellWidth);
            context.drawImage(exitImage, cellWidth * exitCol, cellHeight * exitRow, cellHeight, cellWidth);
        };
        return this;
    };

    $.fn.move = function(e) {
        switch (e.keyCode) {
        case 37:
            //left
                if (mazeStr[(curRow * rows) + curCol - 1] != 1 && (curCol - 1) >= 0) {
                context.clearRect(cellWidth * curCol, cellHeight * curRow, cellHeight, cellWidth);
                curCol = curCol - 1;
                context.drawImage(playerImg, cellWidth * curCol, cellHeight * curRow, cellHeight, cellWidth);
            }
            break;
        case 38:
            //up
                if (mazeStr[((curRow - 1) * rows) + curCol] != 1 && (curRow - 1) >= 0) {

                context.clearRect(cellWidth * curCol, cellHeight * curRow, cellHeight, cellWidth);
                curRow = curRow - 1;
                context.drawImage(playerImg, cellWidth * curCol, cellHeight * curRow, cellHeight, cellWidth);
            }
            break;
        case 39:
            //right
                if (mazeStr[(curRow * rows) + curCol + 1] != 1 && (curCol + 1) < cols) {

                context.clearRect(cellWidth * curCol, cellHeight * curRow, cellHeight, cellWidth);
                curCol = curCol + 1;
                context.drawImage(playerImg, cellWidth * curCol, cellHeight * curRow, cellHeight, cellWidth);
            }
            break;
        case 40:
            //down
                if (mazeStr[((curRow + 1) * rows) + curCol] != 1 && (curRow + 1) < rows) {

                context.clearRect(cellWidth * curCol, cellHeight * curRow, cellHeight, cellWidth);
                curRow = curRow + 1;
                context.drawImage(playerImg, cellWidth * curCol, cellHeight * curRow, cellHeight, cellWidth);
            }
            break;
        }
    };

})(jQuery);
