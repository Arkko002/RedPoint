import api from "./api";


export default {
	login,
	logout,
	register
};

/**
 * Removes "user" field from local storage.
 */
function logout() {
	localStorage.removeItem("userToken");
}

/**
 * Sends POST login request to back end and logs user in on positive response.
 * @param username
 * @param password
 */
function login(username, password) {
	return new Promise(function (resolve, reject) {
		api.post("/account/login", {username, password}).then(
			(response) => {
				if ("token" in response.data) {
					localStorage.setItem("userToken", response.data.token);
					resolve();
				}
				//TODO Proper reject when no token in response
				reject(response);
			},
			(error) => {
				reject(error);
			}
		);
	});
}

/**
 * Sends POST register request to back end and logs user in in on positive response.
 * @param username
 * @param password
 * @param email
 */
function register(username, password, email) {
	return new Promise(function (resolve, reject) {
		api.post("/account/register", {username, password, email}).then(
			//TODO separate this into a function
			(response) => {
				if ("token" in response.data) {
					let userToken = JSON.stringify(response.data);
					localStorage.setItem("userToken", userToken);
					resolve();
				}
				//TODO Proper reject when no token in response
				reject(response);
			},
			(error) => {
				reject(error);
			}
		);
	});
}
