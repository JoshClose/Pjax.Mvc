const path = require("path");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const fs = require("fs");

const pagesPath = "./Client/js/pages/";
const files = fs.readdirSync(pagesPath);
const filesEntry = files.reduce((accumulator, file) => {
	console.log(file);
	const key = file.replace(/(.*)\..*/, "$1");
	const value = pagesPath + file;
	accumulator[key] = value;
	return accumulator;
}, {});

module.exports = (env) => {
	const isDevBuild = !(env && env.prod);
	const outputDir = "./wwwroot/dist";

	const config = {
		mode: isDevBuild ? "development" : "production",
		stats: { modules: false },
		resolve: { extensions: [".js"] },
		devtool: "eval-source-map",
		entry: {
			//"site": "./Client/css/site.scss",
			...filesEntry
		},
		output: {
			filename: "js/[name].js",
			publicPath: "js/",
			path: path.join(__dirname, outputDir)
		},
		module: {
			rules: [{
				test: /\.jsx?$/,
				include: /Client/,
				use: {
					loader: "babel-loader",
					options: {
						presets: [
							[
								"@babel/preset-env",
								{
									"useBuiltIns": "usage"
								}
							],
							"@babel/preset-react"
						],
						plugins: [
							"transform-class-properties"
						]
					}
				}
			}, {
				test: /\.(png|jpg|jpeg|gif|svg)$/,
				use: "url-loader?limit=25000"
			}, {
				test: /\.s[ca]ss$/,
				use: ExtractTextPlugin.extract({
					use: [
						isDevBuild ? "css-loader" : "css-loader?minimize",
						"sass-loader"
					]
				})
			}, {
				test: /\.(png|jpg|jpeg|gif|svg)$/,
				use: "url-loader?limit=25000"
			}]
		},
		plugins: [
			new ExtractTextPlugin({
				filename: (getPath) => {
					console.log("getPath", getPath("css/[name].css"));
					return getPath("css/[name].css").replace("css/js", "css");
				},
				allChunks: true
			})
		]
	};

	return config;
};