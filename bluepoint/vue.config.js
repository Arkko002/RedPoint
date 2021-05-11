module.exports = {
	publicPath:"",
	configureWebpack: {
		devtool: "source-map",
		resolve: {
			alias: {
				signalr: "./lib/signalr.js"
			}
		}
	},
};
