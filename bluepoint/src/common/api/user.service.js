import api from "./api";


function AddUserIfInResponse(response) {
  if ("token" in response.data) {
    localStorage.setItem("user", JSON.stringify(response.data));
  }
}

function logout() {
  localStorage.removeItem("user");
}

function login(username, password) {
  return api.post("/account/login", {
    username,
    password
  }).then((response) => {
    AddUserIfInResponse(response);
  });
}

function register(username, password, email) {
  return api.post("/account/register", {
    username,
    password,
    email
  }).then((response) => {
    AddUserIfInResponse(response);
  });
}

export default {
  login,
  logout,
  register
};
