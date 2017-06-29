var ViewModel = function () {
    var self = this; // make 'this' available to subfunctions or closures
    self.users = ko.observableArray(); // enables data binding
    var usersUri = "/api/Users";
   
    self.UserName = ko.observable();
    self.Password = ko.observable();
    self.Email = ko.observable();

    self.addUser = function () {
        //self.getUserDetails = function (user) {
        //    $.getJSON(usersUri + "/" + user.username).done(function (data) {
        //        self.currUser(data);
        //    });
        //}

        var user = {
            username: self.UserName(),
            password: self.Password(),
            email: self.Email()
            
            //Wins: self.newUser.Wins(),
           // Losses: self.newUser.Losses()
        };
        $.post(usersUri, user).done(function (item) {
            alert("ahoi!");
            // self.users.push(item);

        });
    }
    //getUsers();
    // Fetch the initial data
   // getAllUsers();



};
ko.applyBindings(new ViewModel()); // sets up the data binding
