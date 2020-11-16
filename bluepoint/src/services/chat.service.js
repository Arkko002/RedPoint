import api from "./api";

/**
 * Retrieves current user's server list from back end.
 */
function fetchServers() {
	api.get("chat/servers")
		.then((response) => { return response;});
}

/**
 * Retrieves data of server with provided ID from back end.
 * @param serverId
 */
function fetchServerData(serverId){
	api.get(`chat/server/${serverId}`)
		.then((response) => { return response;});
}

/**
 * Retrieves data of channel with provided ID from back end.
 * @param channelId ID of the requested channel
 * @param serverId ID of the server that contains the channel
 */
function fetchChannelData(channelId, serverId){
	api.get(`chat/server/${serverId}/${channelId}`)
		.then((response) => { return response;});
}

/**
 * Stores information about currently selected server and channel in back end.
 * Should be only called when user closes his session.
 * @param data
 */
function sendClosingData(data) {
	api.post("chat/close", data)
		.then((response) => { return response;});
}

export default {
	fetchServers,
	fetchServerData,
	fetchChannelData,
	sendClosingData
};
