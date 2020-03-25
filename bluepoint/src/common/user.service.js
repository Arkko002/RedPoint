import API_BASE_URL from "./api.config";

export const userService = {
  login,
  logout,
  register
};

function login(username, password) {
  axios({
    method: "post",
    url: `${API_BASE_URL}/account/login`,

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
    })
    .catch(error => {
      handleError(error);
    });
}

function logout() {
  localStorage.removeItem("user");
}

function register(username, password, email) {
  axios({
    method: "post",
    url: `${API_BASE_URL}/account/register`,

    data: {
      username: username,
      password: password,
      email: email
    }
  })
    .then(response => {}) //TODO
    .catch(error => {
      handleError(error);
    });
}

function handleError(error) {
  if (error.response) {
    handleResponseError(error);
  } else if (error.request) {
    //TODO
  } else {
    Promise.reject(error);
  }
}

function handleResponseError(error) {
  // TODO Handle error return codes
  Promise.reject(error);
}
