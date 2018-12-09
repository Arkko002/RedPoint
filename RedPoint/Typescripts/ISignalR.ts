///<reference path="Models.ts"/>

interface SignalR {
    chatHub: ChatHubProxy;
    serverHub: ServerHubProxy;
    channelHub: ChannelHubProxy;
}

interface ChatHubProxy {
    client: IChatClient;
    server: IChatServer;
}

interface ChannelHubProxy {
    client: IChannelClient;
    server: IChannelServer;
}

interface ServerHubProxy {
    client: IServerClient;
    server: IServerServer;
}

interface IChatClient {
    getMessagesFromDb: (msgList: Array<Message>) => void;
    getServerList: (serverList: Array<ServerStub>) => void;
    getChannelList: (channelList: Array<ChannelStub>) => void;
    addNewMessage: (message: Message) => void;
    showSearchResult: (msgList: Array<Message>) => void;
    showUserAutocomplete: (userList: Array<UserStub>) => void;
}

interface IChatServer {
    checkIfUserLoggedIn(): any;
    send(string: string, channelId: number): any;
    search(string: string): any;
}

interface IChannelClient {
    addChannel(channel: ChannelStub): void;
    removeChannel(channel: ChannelStub): void;
}

interface IChannelServer {
    addChannel(name: string, serverId: number, description?: string): void;
    removeChannel(channelId: number, serverId: number): void;
}


interface IServerClient {
    addServer(server: ServerStub): void;
    removeServer(server: ServerStub): void;
}

interface IServerServer {
    createServer(name: string, description?: string, image?: HTMLImageElement): void;
    deleteServer(id: number): void;
    joinServer(id: number): void;
    leaveServer(id: number): void;
}
