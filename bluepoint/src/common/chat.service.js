import API_BASE_URL from "./api.config";

export const chatService = {};

function getServerList(user) {
  axios({
    method: "get",
    url: `${API_BASE_URL}/chat/getServerList`,

    data: {
      user: user
    }
  })
    .then(response => {
      if ("servers" in response.data) return response.data;
    })
    .catch(error => {
      //TODO
    });
}

function getUserList(server) {}

function getMessageList(channel) {}
