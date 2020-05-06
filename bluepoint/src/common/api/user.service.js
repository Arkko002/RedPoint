import api from "./api"

export default {
  login,
  logout,
  register
};

function login(username, password) {
  return api.post("/account/login", {
    username: username,
    password: password
    }).then(response => {
      AddUserIfInResponse(response)
    })
}

function logout() {
  localStorage.removeItem("user");
}

function register(username, password, email) {
  return api.post("/account/register", {
    username: username,
    password: password,
    email: email
  }).then(response => {
    AddUserIfInResponse(response)
  })
}

function AddUserIfInResponse(response) {
  if ("token" in response.data) {
    localStorage.setItem("user", JSON.stringify(response.data));
  }
}
