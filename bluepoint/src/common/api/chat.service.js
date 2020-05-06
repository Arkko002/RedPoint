import api from "./api"

export default {
  fetchServers,
  fetchServerData,
  fetchChannelData
};


function fetchServers() {
  return api.get("/chat/servers")
}

function fetchServerData(serverId){
  return api.get(`/chat/server/${serverId}`)
}

function fetchChannelData(channelId, serverId){
  return api.get(`/chat/server/${serverId}/${channelId}`)
}
