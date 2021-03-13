import userService from "../../services/authentication.service";
import router from "../../router/index";

//TODO Add mapGetters for loggedIn etc.
//Set user status as logged in if JWT token found in local storage
const user = localStorage.getItem("userToken");
const initialState = user
	? { status: { loggedIn: true }, user }
	: { status: { loggedIn: false }, user: null };

export const authentication = {
	namespaced: true,
	state: initialState,

	actions: {
		login({ dispatch, commit }, { username, password }) {
			commit("loginRequest", { username });
			
			userService.login(username, password).then(
				() => {
					commit("loginSuccess", username);
					router.push("/chat");
				},
				(error) => {
					commit("loginFailure", error);
					dispatch("alert/error", error, { root: true });
				}
			);
		},

		logout({ commit }) {
			userService.logout();
			commit("logout");
			router.push("/");
		},

		register({ dispatch, commit }, { username, password, email }) {
			commit("registerRequest");

			userService.register(username, password, email).then(
				() => {
					commit("registerSuccess");
					router.push("/login");
				},
				(error) => {
					commit("registerFailure");
					dispatch("alert/error", error, { root: true });
				}
			);
		},
	},

	mutations: {
		loginRequest(state, user) {
			state.status = { loggingIn: true, loggedIn: false };
			state.user = user;
		},
		loginSuccess(state, user) {
			state.status = { loggedIn: true, loggingIn: false };
			state.user = user;
		},
		loginFailure(state) {
			state.status = { loggedIn: false, loggingIn: false };
			state.user = null;
		},
		logout(state) {
			state.status = { loggedIn: false, loggingIn: false };
			state.user = null;
		},
		registerRequest(state) {
			state.status = { registering: true, registered: false };
		},
		registerSuccess(state) {
			state.status = { registered: true, registering: false };
		},
		registerFailure(state) {
			state.status = { registered: false, registering: false };
		},
	},
};
