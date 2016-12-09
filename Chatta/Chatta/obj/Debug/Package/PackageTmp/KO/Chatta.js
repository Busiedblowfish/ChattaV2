//Namespace in camerlCase
var chatta = {};

//Use Chatta.Models.Chat

chatta.chatMessage = function (sender, message, time) {
    var self = this;
    self.username = sender;
    self.message = message;
    if (time !== null) {
        self.timestamp = time;
    }
};

//Connected User
chatta.chatUser = function (username, userId) {
    var self = this;
    self.username = username;
    self.userId = userId;
};

// Chat ViewModel
chatta.chatViewModel = function () {
    var self = this;
    self.messages = ko.observableArray();
};

// Connected User ViewModel
//Remove connected user from webpage upon disconnect
chatta.connectedUsersViewModel = function () {
    var self = this;
    self.contacts = ko.observableArray();
    self.customRemove = function (userToRemove) {
        var userIdToRemove = userToRemove.userId;
        self.contacts.remove(function (item) {
            return item.userId === userIdToRemove;
        });
    };
};