///<reference path="ISignalR.ts"/>
///<reference path="Tools.ts"/>
$(function () {
    var serverHub = $.connection.serverHub;
    serverHub.client.addServer = function (server) {
        //TODO
        //$("#serverlist").append()
    };
    serverHub.client.removeServer = function (server) {
    };
    $.connection.hub.start().done(function () {
        $("#server_add_btn").click(function () {
            serverHub.server.createServer($("#server_add_name").val().toString(), $("#server_add_description").val().toString(), null);
        });
    });
});
//# sourceMappingURL=ServerHub.js.map