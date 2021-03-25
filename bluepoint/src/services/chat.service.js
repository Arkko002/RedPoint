import api from "./api";

export default {
	fetchChatUser,
	fetchServerData,
	fetchChannelData,
};

/**
 * Retrieves data of server with provided ID from back end.
 * @param serverId
 */
function fetchServerData(serverId){
	return api.get(`chat/server/${serverId}`);
}

/**
 * Retrieves data of channel with provided ID from back end.
 * @param channelId ID of the requested channel
 * @param serverId ID of the server that contains the channel
 */
function fetchChannelData(channelId, serverId){
	return api.get(`chat/server/${serverId}/${channelId}`);
}

/**
 * Retrieves chat-related data of the currently logged in user
 * based on JWT token in the request header
 */
function fetchChatUser(){
	return api.get("chat/user/current");
}

