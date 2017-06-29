var ViewModel = function() {
    var self = this; // make 'this' available to subfunctions or closures
    self.users = ko.observableArray(); // enables data binding
    var usersUri = "/api/Users";

    self.UserName = ko.observable();
    self.Password = ko.observable();
    self.Email = ko.observable();

    self.addUser = function() {
        var user = {
            username: self.UserName(),
            password: self.Password(),
            email: self.Email()

            //Wins: self.newUser.Wins(),
            // Losses: self.newUser.Losses()
        };
        $.post(usersUri, user).done(function(item) {
            alert("welcome!");

        });
    }
};
ko.applyBindings(new ViewModel()); // sets up the data binding