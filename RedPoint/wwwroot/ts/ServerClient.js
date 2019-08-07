"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>
const signalr = require("@aspnet/signalr");
const serverHub = new signalr.HubConnectionBuilder()
    .withUrl("/serverhub")
    .build();
serverHub.start().then(() => {
    document.getElementById("serveradd_btn").addEventListener("click", event => {
        var serverName = document.getElementById("serveradd_name").textContent;
        var serverDescription = document.getElementById("serveradd_description").textContent;
        //TODO Add images
        serverHub.invoke("CreateServer", serverName, serverDescription, null);
    });
});
serverHub.on("AddServer", (server) => {
    //TODO
});
serverHub.on("RemoveServer", (server) => {
});
//# sourceMappingURL=ServerClient.js.map