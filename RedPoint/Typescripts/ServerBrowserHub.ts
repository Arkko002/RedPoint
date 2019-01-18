///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>
import * as signalr from "@aspnet/signalr";

const serverBrowserHub = new signalr.HubConnectionBuilder()
    .withUrl("/serverbrowser")
    .build();

serverBrowserHub.start().then(() => {
    serverBrowserHub.invoke("GetServerStubsList");
});

serverBrowserHub.on("GetServerStubList",
    (serverStubList: Array<ServerStub>) => {
        //TODO Add servers to the list in BrowseServers.cshtml 
    });