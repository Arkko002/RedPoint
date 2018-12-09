///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>
$(function () {
    var chatHub = $.connection.chatHub;
    chatHub.client.getMessagesFromDb = function (msgList) {
        var msgListLength = msgList.length;
        for (var i = 0; i < msgListLength; i++) {
            $("#discussion").append("<li class=\"messagebox\"><strong>" + htmlEncode(msgList[i].user.userName) + ":</strong><br> " + htmlEncode(msgList[i].text) + "</li>");
        }
    };
    chatHub.client.getServerList = function (serverList) {
    };
    chatHub.client.getChannelList = function (channelList) {
        var channelListLength = channelList.length;
        for (var i = 0; i < channelListLength; i++) {
            $("#channellist").append('<li class="channellist"><strong><br></li>');
        }
    };
    chatHub.client.addNewMessage = function (msg) {
        $('#discussion').append('<li><strong>' + htmlEncode(msg.user.userName)
            + "</strong>: " + htmlEncode(msg.text) + "</li>");
    };
    chatHub.client.showSearchResult = function (msgList) {
        var msgListLength = msgList.length;
        for (var i = 0; i < msgListLength; i++) {
            var message = msgList[i];
            $("#discussion").append('<li class="messagebox"><strong>' + htmlEncode(message.user.userName)
                + ":</strong><br> " + htmlEncode(message.text) + "</li>");
        }
    };
    chatHub.client.showUserAutocomplete = function (userList) {
    };
    $('#message').focus();
    $.connection.hub.start().done(function () {
        chatHub.server.checkIfUserLoggedIn();
        $("#sendmessage").click(function () {
            chatHub.server.send($("#message").val().toString(), 1);
            $("#message").val("").focus();
        });
        $("#search").click(function () {
            chatHub.server.search($("#searchText").val().toString());
        });
    });
});
$("#serveraddbtn").click(function () {
    var url = $("#servermodal").data("url");
    $.get(url, function (data) {
        $("#servermodal").html(data);
        $("#servermodal").modal("show");
    });
});
//# sourceMappingURL=ChatHub.js.map