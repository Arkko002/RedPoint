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
    for (var i = 0; i < msgListLength; i++) {
        $("#discussion")
            .append(`<li class="messagebox"><strong>${htmlEncode(msgList[i].user.userName)}:</strong><br> ${htmlEncode(msgList[i].text)}</li>`);
    }
});
chatHub.on("GetServerList", (serverList) => {
});
chatHub.on("GetChannelList", (channelList) => {
    var channelListLength = channelList.length;
    for (var i = 0; i < channelListLength; i++) {
        $("#channellist")
            .append('<li class="channellist"><strong><br></li>');
    }
});
chatHub.on("AddNewMessage", (msg) => {
    $("#discussion")
        .append(`<li><strong>${htmlEncode(msg.user.userName)}</strong>: ${htmlEncode(msg.text)}</li>`);
});
chatHub.on("ShowSearchResult", (msgList) => {
    var msgListLength = msgList.length;
    for (var i = 0; i < msgListLength; i++) {
        var message = msgList[i];
        $("#discussion")
            .append(`<li class="messagebox"><strong>${htmlEncode(message.user.userName)}:</strong><br> ${htmlEncode(message.text)}</li>`);
    }
});
chatHub.on("ShowUserAutocomplete", (userList) => {
});
document.getElementById("message").focus();
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