///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>
import * as $ from "jquery";
import * as signalr from "@aspnet/signalr";

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

chatHub.on("GetMessagesFromDb",
    (msgList: Array<Message>) => {
        var msgListLength = msgList.length;
        for (var i = 0; i < msgListLength; i++) {
            $("#discussion")
                .append(
                    `<li class="messagebox"><strong>${htmlEncode(msgList[i].user.userName)}:</strong><br> ${
                    htmlEncode(msgList[i].text)}</li>`);
        }
    });


chatHub.on("GetServerList",
    (serverList: Array<ServerStub>) => {

    });

chatHub.on("GetChannelList",
    (channelList: Array<ChannelStub>): void => {
        var channelListLength = channelList.length;
        for (var i = 0; i < channelListLength; i++) {
            $("#channellist")
                .append('<li class="channellist"><strong><br></li>');
        }
    });

chatHub.on("AddNewMessage",
    (msg: Message): void => {
        $("#discussion")
            .append(`<li><strong>${htmlEncode(msg.user.userName)}</strong>: ${htmlEncode(msg.text)}</li>`);
    });

chatHub.on("ShowSearchResult",
    (msgList: Array<Message>): void => {
        var msgListLength = msgList.length;
        for (var i = 0; i < msgListLength; i++) {
            var message = msgList[i];
            $("#discussion")
                .append(`<li class="messagebox"><strong>${htmlEncode(message.user.userName)}:</strong><br> ${htmlEncode(message.text)}</li>`);
        }
    });

chatHub.on("ShowUserAutocomplete",
    (userList: Array<UserStub>): void => {

    });

document.getElementById("message").focus();

$("#server_add_btn").click(() =>
{
    var url = $("#server_modal").data("url");
    $.get(url,
        (data: string) => {
            $("#server_modal").html(data);
            $("#server_modal").modal("show");
        });
});

$("#channel_add_button").click(() => {
    var url = $("#channel_modal").data("url");
    $.get(url,
        (data: string) => {
            $("#channel_modal").html(data);
            $("#channel_modal").modal("show");
        });
});