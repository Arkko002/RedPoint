"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>
const signalr = require("@aspnet/signalr");
const $ = require("jquery");
const channelHub = new signalr.HubConnectionBuilder()
    .withUrl("/channel")
    .build();
channelHub.start().then(() => {
    document.getElementById("channel_add_button").addEventListener("click", event => {
        var channelName = document.getElementById("channel_add_name").textContent;
        var channelDescription = document.getElementById("channel_add_description").textContent;
        channelHub.invoke("AddChannel", channelName, channelDescription);
    });
});
//Appends to list with id formatted like ChannelName_1 
channelHub.on("AddChannel", (channel) => {
    $("#channellist").append()
        .append(`<li class="channellist" id="${htmlEncode(channel.name)}_${htmlEncode(channel.id.toString())}">${htmlEncode(channel.name)}</li>`);
});
channelHub.on("RemoveChannel", (channel) => {
    var listItem = document.getElementById(htmlEncode(channel.name) +
        "_" +
        htmlEncode(channel.id.toString()));
    listItem.parentNode.removeChild(listItem);
});
//# sourceMappingURL=ChannelHub.js.map