import VueLogger from "vuejs-logger";
import Vue from "vue";

//TODO
//const isProduction = process.env.NODE_ENV === "production";

const options = {
	isEnabled: true,
	//logLevel: isProduction ? "error" : "debug",
	stringifyArguments: true,
	showLogLevel: true,
	showMethodName: true,
	separator: "|",
	showConsoleColors: true
};

Vue.use(VueLogger, options);
