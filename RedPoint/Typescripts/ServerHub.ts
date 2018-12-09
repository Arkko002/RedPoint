///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>

$(() => {
    var serverHub = $.connection.serverHub;
    
    serverHub.client.addServer = (server: ServerStub): void =>
    {
        //TODO
        //$("#serverlist").append()
    }

    serverHub.client.removeServer = (server: ServerStub): void =>
    {

    }

    $.connection.hub.start().done(() =>
    {
        $("#server_add_btn").click(() =>
        {
            serverHub.server.createServer($("#server_add_name").val().toString(),
                $("#server_add_description").val().toString(),
                null);
        });
    });
});
