﻿$("#btnLogin").click(function () {
    var name = $("#Username").val();
    var password = $("#Password").val();
    var usersUri = "../api/Users/GetUser/" + name + "/" + password;

    $.getJSON(usersUri, name, password).done(function (data) {

        alert("welcome!");
        // update the session storage
        sessionStorage.UserName = name;
         window.location.replace("HomePage.html");
    })
        .fail(function (data) {
            alert("user not found or wrong password");
        });
});

