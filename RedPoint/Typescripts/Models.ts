class UserStub {
    userId: string;
    userName: string;
}

class ServerStub {
    id: number;
    name: string;
    image: HTMLImageElement;
}

class ChannelStub {
    id: number;
    name: string;
    description: string;
}

class Message {
    id: number;
    dateTimePosted: Date;
    text: string;
    user: UserStub;
}
