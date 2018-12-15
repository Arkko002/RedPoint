///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>
import * as signalr from "@aspnet/signalr";

const serverHub = new signalr.HubConnectionBuilder()
    .withUrl("/server")
    .build();

serverHub.start().then(() => {
    document.getElementById("server_add_btn").addEventListener("click",
        event => {
            var serverName = document.getElementById("server_add_name").textContent;
            var serverDescription = document.getElementById("server_add_description").textContent;

            //TODO Add images
            serverHub.invoke("CreateServer", serverName, serverDescription, null);
        });
});

serverHub.on("AddServer", (server: ServerStub): void => {
    //TODO
    //$("#serverlist").append()
});

serverHub.on("RemoveServer", (server: ServerStub): void => {

});