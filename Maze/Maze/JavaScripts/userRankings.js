window.onload = function getAllUsers() {
    $.get("../api/Users").done(function(usersList) {
        var tr;
        for (var i = 0; i < usersList.length; ++i) {
            tr = $("<tr/>");
            tr.append("<th>" + (i+1) + "</th>");
            tr.append("<th>" + usersList[i].username + "</th>");
            tr.append("<th>" + usersList[i].wins + "</th>");
            tr.append("<th>" + usersList[i].losses + "</th>");
            $("#table").append(tr);
        }
    });
}