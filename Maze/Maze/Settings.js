    localStorage.setItem("rows", "6");
    localStorage.setItem("cols", "6");
    localStorage.setItem("algoType", 0);
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

        alert("rows: " + localStorage.getItem("rows"));
        alert("cols: " + localStorage.getItem("cols"));
        alert("algo: " + localStorage.getItem("algoType"));
    });

    $(document).before(function() {
        document.getElementById("mazeRows").value = localStorage.getItem("rows");
    });