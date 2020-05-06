import Vue from "vue";
import Vuex from "vuex";

import { authentication } from "./modules/authentication.module";
import { alert } from "./modules/alert.module";

Vue.use(Vuex);

export default new Vuex.store({
  modules: {
    authentication,
    alert
  },
  
  getters: {
    loggedIn(state) {
      return !!state.user
    }
  }
});
