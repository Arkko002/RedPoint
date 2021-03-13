import api from "@/services/api";

export default {
	fetchServers,
	fetchServerData,
	fetchChannelData,
	fetchChatUser,
	sendClosingData
};

function fetchServers() {
	api.get("chat/servers")
		.then((response) => {
			return JSON.parse(response);
		});
}

function fetchServerData(serverId){
	api.get(`chat/server/${serverId}`)
		.then((response) => {
			return JSON.parse(response);
		});
}

function fetchChannelData(channelId, serverId){
	api.get(`chat/server/${serverId}/${channelId}`)
		.then((response) => {
			return JSON.parse(response);
		});
}

function fetchChatUser(){
	api.get("chat/user")
		.then((response) => {
			return JSON.parse(response);
		});
}

function sendClosingData(data) {
	api.post("chat/close", data)
		.then((response) => {
			return JSON.parse(response);
		});
}

