const path = require("path");

module.exports = {
    entry: {
        chat: "./Typescripts/ChatHub.ts",
        server: "./Typescripts/ServerHub.ts",
        channel: "./Typescripts/ChannelHub.ts"
    }, 
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: "ts-loader"
            }
        ]
    },
    resolve: {
        extensions: [".tsx", ".ts", ".js"]
    },
    output: {
        filename: "[name].js",
        path: path.resolve(__dirname, "wwwroot/js"),
        publicPath: "/"
    }
};