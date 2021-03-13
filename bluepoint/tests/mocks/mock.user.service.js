/* eslint-disable no-unused-vars */

export default {
	login,
	logout,
	register
};

function logout() {
	localStorage.removeItem("userToken");
}

function login(username, password) {
	return "TestUserToken"; 
}

function register(username, password, email) {
	return "TestUserToken";
}
