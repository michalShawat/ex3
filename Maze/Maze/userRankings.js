window.onload = function getAllUsers() {
    $.getJSON("../api/Users").done(function(usersList) {
        //usersList = sortBykey(usersList, "Rank");

        var tr;
        for (var i = 0; i < usersList.length; ++i) {
            tr = $("<tr/>");
            tr.append("<th/>" + usersList[i].Rank + "<th/>");
            tr.append("<th/>" + usersList[i].UserName + "<th/>");
            tr.append("<th/>" + usersList[i].Wins + "<th/>");
            tr.append("<th/>" + usersList[i].Losses + "<th/>");
        }
    })
}