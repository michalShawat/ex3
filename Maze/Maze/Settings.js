$(document).ready(function() {
    localStorage.setItem("rows", "7");
    localStorage.setItem("cols", "7");
    localStorage.setItem("algoType", 0);
    document.getElementById("mazeRows").value = localStorage.getItem("rows");
    document.getElementById("mazeRows").value = localStorage.getItem("rows");
    $("#saveLink").click(function(event) {
        if (localStorage.getItem("rows").localeCompare($("#mazeRows").val())) {
            localStorage.setItem("rows", $("#mazeRows").val());
        }

        if (localStorage.getItem("cols").localeCompare($("#mazeCols").val())) {
            localStorage.setItem("cols", $("#mazeCols").val());
        }

        if (document.getElementById("algoSelect").selectedIndex == 1) {
            localStorage.setItem("algoType", 1);
        }
    });
});