import api from "./api";

/**
 * Sets "user" in local storage if response contains authentication token.
 * @param response
 * @constructor
 */
function AddUserIfInResponse(response) {
	if ("token" in response.data) {
		localStorage.setItem("user", JSON.stringify(response.data));
	}
}

/**
 * Removes "user" field from local storage.
 */
function logout() {
	localStorage.removeItem("user");
}

/**
 * Sends POST login request to back end and logs user in on positive response.
 * @param username
 * @param password
 * @returns {Promise<void>}
 */
function login(username, password) {
	return api.post("/account/login", {
		username,
		password
	}).then((response) => {
		AddUserIfInResponse(response);
	});
}

/**
 * Sends POST register request to back end and logs user in in on positive response.
 * @param username
 * @param password
 * @param email
 * @returns {Promise<void>}
 */
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
