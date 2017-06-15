(function ($) {
    var curCol, curRow;
    var mazeCanvas, context;
    var cellWidth, cellHeight;
    var playerImg;
    $.fn.drawMaze = function(mazeData, // the matrix containing the maze cells
        rows,
        cols,
        startRow,
        startCol, // initial position of the player
        exitRow,
        exitCol, // the exit position
        playerImage, // player's icon (of type Image)
        exitImage // exit's icon (of type Image)
       // true, // is the board enabled (i.e., player can move)
        ) {
        playerImg = playerImage;
        curCol = startCol;
        curRow = startRow;
        mazeCanvas = this.get(0);
        context = mazeCanvas.getContext("2d");
        cellWidth = mazeCanvas.width / cols;
        cellHeight = mazeCanvas.height / rows;
        for (var i = 0; i < rows; i++) {
            for (var j = 0; j < cols; j++) {
                if (mazeData[i*rows+j] == 1) {
                    context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                }
            }
        }
        context.drawImage(playerImage, cellWidth * startCol, cellHeight * startRow, cellHeight, cellWidth);
        context.drawImage(exitImage, cellWidth * exitCol, cellHeight * exitRow, cellHeight, cellWidth);
        return this;
    };

    $.fn.move = function (e) {
        switch (e.keyCode) {
        case 37:
            //left
            context.drawImage(playerImg, cellWidth * (curCol - 1), cellHeight * curRow, cellHeight, cellWidth);
            break;
        case 38:
            //up
            context.drawImage(playerImg, cellWidth * curCol, cellHeight * (curRow - 1), cellHeight, cellWidth);
            break;
        case 39:
            //right
            context.drawImage(playerImg, cellWidth * (curCol+1), cellHeight * curRow, cellHeight, cellWidth);
            break;
        case 40:
            //down
            context.drawImage(playerImg, cellWidth * curCol, cellHeight * (curRow+1), cellHeight, cellWidth);
            break;
        }
    };
})(jQuery);
