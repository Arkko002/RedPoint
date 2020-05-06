import userService from "../../common/api/user.service"
import router from "../../router/index"

//TODO Add mapGetters for loggedIn etc.
//Set user status as logged in if JWT token found in local storage
const user = JSON.parse(localStorage.getItem("user"));
const initialState = user
  ? { status: { loggedIn: true }, user }
  : { status: {}, user: null };

export const authentication = {
  namespaced: true,
  state: initialState,

  actions: {
    login({ dispatch, commit }, { username, password }) {
      commit("loginRequest", { username });

      userService.login(username, password).then(
        user => {
          commit("loginSuccess", user);
          router.push("/chat");
        },
        error => {
          commit("loginFailure", error);
          dispatch("alert/error", error, { root: true });
        }
      );
    },

    logout({ commit }) {
      userService.logout();
      commit("logout");
    },

    register({ dispatch, commit }, { username, password, email }) {
      commit("registerREquest");

      userService.register(username, password).then(
        success => {
          commit("registerSuccess");
          router.push("/login");
        },
        error => {
          commit("registerFailure");
          dispatch("alert/error", error, { root: true });
        }
      );
    }
  },

  mutations: {
    loginRequest(state, user) {
      state.status = { loggingIn: true };
      state.user = user;
    },
    loginSuccess(state, user) {
      state.status = { loggedIn: true };
      state.user = user;
    },
    loginFailure(state) {
      state.status = {};
      state.user = null;
    },
    logout(state) {
      state.status = {};
      state.user = null;
    },
    registerRequest(state) {
      state.status = { registering: true };
    },
    registerSuccess(state) {
      state.status = { registered: true };
    },
    registerFailure(state) {
      state.status = {};
    }
  }
};
