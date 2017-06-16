$(document).ready(function () {
    $("#solveLink").click(function (event) {

        
        $.getJSON("api/solve/" + 1,
            function (data) {
               
          
               
         
            });
    });

    document.onkeydown = function (e) {
        $("#mazeCanvas").move(e);
    }
});