//Namespace in camerlCase
var chatta = {};

//Use Chatta.Models

chatta.chatMessage = function (sender, message, time) {
    var self = this;
    //self.username = sender;  for username implementation
    self.email = sender;
    self.message = message;
    if (time != null) {
        self.timestamp = time;
    }
}

//Connected User
chatta.chatUser = function (authUser, userId) //client refers to authenticated user
{
    var self = this;
    //self.username = username;
    self.email = authUser;
    self.userId = userId;
}

// ViewModels
chatta.chatViewModel = function () {
    var self = this;
    self.messages = ko.observableArray();
}

chatta.connectedUsersViewModel = function () {
    var self = this;
    self.contacts = ko.observableArray();
    self.customRemove = function (userToRemove) {
        var userIdToRemove = userToRemove.userId;
        self.contacts.remove(function (item) {
            return item.userId === userIdToRemove;
        });
    }
}