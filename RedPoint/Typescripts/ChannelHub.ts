///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>

$(() => {
    var channelHub = $.connection.channelHub;

    //Appends to list with id formatted like ChannelName_1 
    channelHub.client.addChannel = (channel: ChannelStub): void => {
        $("#channellist").append('<li class="channellist" id="' + htmlEncode(channel.name) + '_' +
            htmlEncode(channel.id.toString()) + '">' + htmlEncode(channel.name) + '</li>');
    }

    channelHub.client.removeChannel = (channel: ChannelStub): void => {
        var listItem = document.getElementById(+ htmlEncode(channel.name) +
            "_" +
            htmlEncode(channel.id.toString()));

        listItem.parentNode.removeChild(listItem);
    }

    $.connection.hub.start().done(() => {
        $("#channel_add_button").click(() =>
        {
            //TODO server id
            channelHub.server.addChannel($("#channel_add_name").val().toString(),
                1,
                $("#channel_add_description").val().toString());
        });
    });
})
