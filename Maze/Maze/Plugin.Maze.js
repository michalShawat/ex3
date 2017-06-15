//(function ($) {
//    $.fn.drawMaze = function (maze) {
//    var mazeCanvas = this.get(0);
//    var context = mazeCanvas.getContext("2d");
//    var realMaze = maze.toJSON();
//    var hi = realMaze[0];
//    var rows = maze.rows;
//    var cols = maze.cols;
//    var cellWidth = mazeCanvas.width / cols;
//    var cellHeight = mazeCanvas.height / rows;
//    for (var i = 0; i < rows; i++) {
//         for (var j = 0; j < cols; j++) {
//              if (maze[i][j] == 1) {
//                   context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
//             }

//         }
//    }
//    return this;};
//})(jQuery);


(function ($) {
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
        ){
        var mazeCanvas = this.get(0);
        var context = mazeCanvas.getContext("2d");
        var cellWidth = mazeCanvas.width / cols;
        var cellHeight = mazeCanvas.height / rows;
        var start = new Image();
        start.src = playerImage;
        var end = new Image();
        end.src = exitImage;
        for (var i = 0; i < rows; i++) {
            for (var j = 0; j < cols; j++) {
                if (mazeData[i*rows+j] == 1) {
                    context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                }
            }
        }
        context.drawImage(start.src, cellWidth * startCol, cellHeight * startRow, cellWidth, cellHeight);
        context.drawImage(end.src, cellWidth * startCol, cellHeight * startRow, cellWidth, cellHeight);
        return this;
    };
})(jQuery);
