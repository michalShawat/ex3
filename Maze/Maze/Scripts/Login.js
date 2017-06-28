$("#btnLogin").click(function () {
    var name = $("#Username").val();
    var password = $("#Password").val();
    var usersUri = "../api/Users/GetUser/" + name + "/" + password;
    $.getJSON(usersUri, name, password).done(function (data) {
        alert("ahoi!");
    });


});