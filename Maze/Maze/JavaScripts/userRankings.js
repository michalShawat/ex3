window.onload = function getAllUsers() {
    $.getJSON("api/Users").done(function(usersList) {
        //usersList = sortBykey(usersList, "Rank");
        alert("hi");
   //     usersList = GetUsers();
        var tr;
        for (var i = 0; i < usersList.length; ++i) {
            tr = $("<tr/>");
            tr.append("<th>" + i + "</th>");
            tr.append("<th>" + usersList[i].username + "</th>");
            tr.append("<th>" + usersList[i].wins + "</th>");
            tr.append("<th>" + usersList[i].losses + "</th>");

            var dt = new Date(usersList[i].Date);
            tr.append("<th>" + (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear() + "</th>");
            $("#table").append(tr);
        }
    });
}