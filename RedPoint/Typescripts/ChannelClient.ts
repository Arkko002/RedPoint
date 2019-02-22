///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>
import * as signalr from "@aspnet/signalr";

const channelHub = new signalr.HubConnectionBuilder()
    .withUrl("/channelhub")
    .build();

channelHub.start().then(() => {
    document.getElementById("channeladd_button").addEventListener("click",
        event => {
            var channelName = document.getElementById("channeladd_name").textContent;
            var channelDescription = document.getElementById("channeladd_description").textContent;
            channelHub.invoke("AddChannel", channelName, channelDescription);
        });
});

//Appends created channel to channellist
channelHub.on("AddChannel",
    (channel: ChannelStub): void => {
        var channelList = document.getElementById("channellist");

        var channelBtn = document.createElement("button");
        channelBtn.textContent = htmlEncode(channel.name);
        channelBtn.id = htmlEncode(channel.name + "_" + channel.id)

        channelList.appendChild(channelBtn);
    });

channelHub.on("RemoveChannel",
    (channel: ChannelStub): void => {
        var listItem = document.getElementById(htmlEncode(channel.name) +
            "_" +
            channel.id.toString());

        listItem.parentNode.removeChild(listItem);
    });
