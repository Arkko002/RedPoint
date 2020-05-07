import api from "./api"

export default {
  fetchServers,
  fetchServerData,
  fetchChannelData,
  sendMessage,
  sendClosingData
};


function fetchServers() {
  api.get("chat/servers")
      .then(response => { return response})
}

function fetchServerData(serverId){
  api.get(`chat/server/${serverId}`)
      .then(response => { return response})
}

function fetchChannelData(channelId, serverId){
  api.get(`chat/server/${serverId}/${channelId}`)
      .then(response => { return response})
}

function sendMessage(message) {
  
}

function sendClosingData(data) {
  api.post("chat/close", data)
      .then(response => { return response})
}

