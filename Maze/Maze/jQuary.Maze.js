(function ($) {
    $.fn.drawMaze = function (maze) {
    var mazeCanvas = this.get(0);
    var context = mazeCanvas.getContext("2d");
    var rows = maze.rows;
    var cols = maze.cols;
    var cellWidth = mazeCanvas.width / cols;
    var cellHeight = mazeCanvas.height / rows;
    for (var i = 0; i < rows; i++) {
         for (var j = 0; j < cols; j++) {
              if (maze[i][j] == 1) {
                   context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
              }
         }
    }
    return this;};
})(jQuery);
