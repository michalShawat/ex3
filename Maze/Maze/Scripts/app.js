var ViewModel = function () {
    var self = this; // make 'this' available to subfunctions or closures
    self.users = ko.observableArray(); // enables data binding
    var userssUri = "/api/users";
    function getAllBooks() {
        $.getJSON(usersUri).done(function (data) {
            self.users(data);
        });
    }
    // Fetch the initial data
    getAllUsers();
};
ko.applyBindings(new ViewModel()); // sets up the data bindingself.currUser = ko.observable();
self.getUserDetails = function (user) {
    $.getJSON(usersUri + "/" + user.username).done(function (data) {
        self.currUser(data);
    });
}



self.addUser = function () {
    var user = {
        Username: self.newBook.Username(),
        Password: self.newBook.Password(),
        Email: self.newBook.Email(),
        Wins: self.newBook.Wins(),
        Losses: self.newBook.Losses()
    };
    $.post(usersUri, user).done(function (item) {
        self.users.push(item);
    });
}
getUsers();