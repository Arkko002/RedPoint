const path = require("path");
const glob = require("glob");

module.exports = {
    entry: glob.sync("./Areas/Chat/Typescripts/*.ts"),
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