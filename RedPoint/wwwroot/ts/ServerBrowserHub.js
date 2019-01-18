"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>
const signalr = require("@aspnet/signalr");
const serverBrowserHub = new signalr.HubConnectionBuilder()
    .withUrl("/serverbrowser")
    .build();
serverBrowserHub.start().then(() => {
    serverBrowserHub.invoke("GetServerStubsList");
});
serverBrowserHub.on("GetServerStubList", (serverStubList) => {
    //TODO Add servers to the list in BrowseServers.cshtml 
});
//# sourceMappingURL=ServerBrowserHub.js.map