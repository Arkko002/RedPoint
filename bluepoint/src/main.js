import Vue from "vue";
import App from "./App.vue";
import router from "./router/router";
import store from "./store/store";

const DEV = process.env.NODE_ENV;

Vue.config.productionTip = false;
Vue.config.errorHandler = function (err, vm, info){
	if (DEV) {
		console.log(`Error: ${err}  ----  Info: ${info}`);
	}
};

new Vue({
	router,
	store,
	render: (h) => h(App)
}).$mount("#app");
