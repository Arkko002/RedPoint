"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>
const $ = require("jquery");
const signalr = require("@aspnet/signalr");
const chatHub = new signalr.HubConnectionBuilder()
    .withUrl("/chat")
    .configureLogging(signalr.LogLevel.Trace)
    .build();
chatHub.start().then(() => {
    document.getElementById("sendmessage").addEventListener("click", event => {
        //TODO CHANNEL ID
        var message = document.getElementById("message").textContent;
        chatHub.invoke("Send", message, 1);
    });
    document.getElementById("search").addEventListener("click", event => {
        var searchText = document.getElementById("searchText").textContent;
        chatHub.invoke("Search", searchText);
    });
});
chatHub.on("GetMessagesFromDb", (msgList) => {
    var msgListLength = msgList.length;
    var discussion = document.getElementById("discussion");
    for (var i = 0; i < msgListLength; i++) {
        var message = document.createElement("li");
        message.className = "messagebox";
        message.textContent = htmlEncode(msgList[i].user.userName + ": " + msgList[i].text);
        discussion.appendChild(message);
    }
});
//TODO Server list
chatHub.on("GetServerList", (serverList) => {
    var serverListLength = serverList.length;
    for (var i = 0; i < serverListLength; i++) {
        $("#serverlist")
            .append('<li class="serverlist"></li>');
    }
});
chatHub.on("GetChannelList", (channelList) => {
    var channelListLength = channelList.length;
    var channelListElement = document.getElementById("channellist");
    for (var i = 0; i < channelListLength; i++) {
        var btn = document.createElement("button");
        btn.textContent = htmlEncode(channelList[i].name);
        //ID format example: ChannelName_1
        btn.id = channelList[i].name + "_" + channelList[i].id;
        channelListElement.appendChild(btn);
    }
});
chatHub.on("AddNewMessage", (msg) => {
    var discussion = document.getElementById("discussion");
    var message = document.createElement("li");
    message.textContent = htmlEncode(msg.user.userName + ": " + msg.text);
    discussion.appendChild(message);
});
chatHub.on("ShowSearchResult", (msgList) => {
    var msgListLength = msgList.length;
    var searchResult = document.getElementById("searchResult");
    for (var i = 0; i < msgListLength; i++) {
        var message = document.createElement("li");
        message.className = "messagebox";
        message.textContent = htmlEncode(msgList[i].user.userName + ": " + msgList[i].text);
        searchResult.appendChild(message);
    }
});
chatHub.on("ShowUserAutocomplete", (userList) => {
});
document.getElementById("message").focus();
//TODO Move modals to DOM if possible
//document.getElementById("serveradd_btn").onclick = () => {
//};
$("#server_add_btn").click(() => {
    var url = $("#server_modal").data("url");
    $.get(url, (data) => {
        $("#server_modal").html(data);
        $("#server_modal").modal("show");
    });
});
$("#channel_add_button").click(() => {
    var url = $("#channel_modal").data("url");
    $.get(url, (data) => {
        $("#channel_modal").html(data);
        $("#channel_modal").modal("show");
    });
});
//# sourceMappingURL=ChatHub.js.map