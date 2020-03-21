import API_BASE_URL from "./api.config";

export const userService = {
  login,
  logout
};

function login(username, password) {
  axios({
    method: "post",
    url: `${API_BASE_URL}/users/authenticate`,

    data: {
      username: username,
      password: password
    }
  })
    .then(response => {
      if ("token" in response.data) {
        localStorage.setItem("user", JSON.stringify(response.data));
        return response.data;
      }

      return;
    })
    .catch(error => {
      if (error.response) {
        handleResponseError(error);
      } else if (error.request) {
        //TODO
      } else {
        Promise.reject(error);
      }
    });
}

function logout() {
  localStorage.removeItem("user");
}

function handleResponseError(error) {
  // TODO Handle error return codes
  Promise.reject(error);
}
