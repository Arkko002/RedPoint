///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>
$(function () {
    var channelHub = $.connection.channelHub;
    //Appends to list with id formatted like ChannelName_1 
    channelHub.client.addChannel = function (channel) {
        $("#channellist").append('<li class="channellist" id="' + htmlEncode(channel.name) + '_' +
            htmlEncode(channel.id.toString()) + '">' + htmlEncode(channel.name) + '</li>');
    };
    channelHub.client.removeChannel = function (channel) {
        var listItem = document.getElementById(+htmlEncode(channel.name) +
            "_" +
            htmlEncode(channel.id.toString()));
        listItem.parentNode.removeChild(listItem);
    };
    $.connection.hub.start().done(function () {
        $("#channel_add_button").click(function () {
            //TODO server id
            channelHub.server.addChannel($("#channel_add_name").val().toString(), 1, $("#channel_add_description").val().toString());
        });
    });
});
//# sourceMappingURL=ChannelHub.js.map