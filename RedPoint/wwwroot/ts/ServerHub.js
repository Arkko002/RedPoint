"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>
const signalr = require("@aspnet/signalr");
const serverHub = new signalr.HubConnectionBuilder()
    .withUrl("/server")
    .build();
serverHub.start().then(() => {
    document.getElementById("server_add_btn").addEventListener("click", event => {
        var serverName = document.getElementById("server_add_name").textContent;
        var serverDescription = document.getElementById("server_add_description").textContent;
        //TODO Add images
        serverHub.invoke("CreateServer", serverName, serverDescription, null);
    });
});
serverHub.on("AddServer", (server) => {
    //TODO
    //$("#serverlist").append()
});
serverHub.on("RemoveServer", (server) => {
});
//# sourceMappingURL=ServerHub.js.map