const path = require("path");

module.exports = {
    entry: {
        app: path.resolve(__dirname, "Client/Boot/app.jsx")
    },
    output: {
        filename: "[name].bundle.js",
        path: path.resolve(__dirname, "Scripts/Dist")
    },
    resolve: {
        extensions: [".js", ".jsx"]
    },
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader"
                }
            }
        ]
    }
};