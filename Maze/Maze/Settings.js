$(document).ready(function() {
    localStorage.setItem("rows", "6");
    localStorage.setItem("cols", "6");
    localStorage.setItem("algoType", 0);
    
    $("#saveLink").click(function (event) {
        if ((localStorage.getItem("rows").localeCompare($("#mazeRows").val()))) {
            localStorage.clear("rows");
            localStorage.setIte
m("rows", $("#mazeRows").val());
        }

        if ((localStorage.getItem("cols").localeCompare($("#mazeRows").val()))) {
            localStorage.setItem("cols", document.getElementById("mazeCols"));
        }

        if (document.getElementById("algoSelect").selectedIndex == 1) {
            localStorage.setItem("rows", 1);
        }
        
        alert("rows: "+localStorage.getItem("rows"));
        alert("cols: "+localStorage.getItem("cols"));
        alert("algo: "+localStorage.getItem("algoType"));
    });
});