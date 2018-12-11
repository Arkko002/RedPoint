///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>

$(() =>
{
    var chatHub = new SignalR.HubConnectionBuilder()

    chatHub.client.getMessagesFromDb = (msgList) =>
    {
        var msgListLength = msgList.length;
        for (var i = 0; i < msgListLength; i++) {
            $("#discussion").append(`<li class="messagebox"><strong>${htmlEncode(msgList[i].user.userName)}:</strong><br> ${htmlEncode(msgList[i].text)}</li>`);
        }
    };

    chatHub.client.getServerList = (serverList) =>
    {

    }

    chatHub.client.getChannelList = (channelList): void =>
    {
        var channelListLength = channelList.length;
        for (var i = 0; i < channelListLength; i++) {
            $("#channellist").append('<li class="channellist"><strong><br></li>');
        }
    }

    chatHub.client.addNewMessage = (msg): void =>
    {
        $('#discussion').append('<li><strong>' + htmlEncode(msg.user.userName)
            + "</strong>: " + htmlEncode(msg.text) + "</li>");
    };

    chatHub.client.showSearchResult = (msgList): void =>
    {
        var msgListLength = msgList.length;
        for (var i = 0; i < msgListLength; i++) {
            var message = msgList[i];
            $("#discussion").append('<li class="messagebox"><strong>' + htmlEncode(message.user.userName)
                + ":</strong><br> " + htmlEncode(message.text) + "</li>");
        }
    };

    chatHub.client.showUserAutocomplete = (userList): void =>
    {

    };

    $('#message').focus();

    $.connection.hub.start().done(() =>
    {
        chatHub.server.checkIfUserLoggedIn();

        $("#sendmessage").click(() =>
        {
            chatHub.server.send($("#message").val().toString(), 1);
            $("#message").val("").focus();
        });

        $("#search").click(() =>
        {
            chatHub.server.search($("#searchText").val().toString());
        });
    });
});

$("#serveraddbtn").click(() =>
{
    var url = $("#servermodal").data("url");
    $.get(url,
        function (data) {
            $("#servermodal").html(data);
            $("#servermodal").modal("show");
        });
});