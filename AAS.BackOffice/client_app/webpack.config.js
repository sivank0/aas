const path = require('path');
const webpack = require("webpack")

module.exports = (env) => {
    return {
        entry: {
            desktop: "./src/app/index.tsx",
        },
        output: {
            path: path.join(__dirname, '../wwwroot/dist'),
            filename: '[name].js'
        },
        module: {
            rules: [{
                test: /\.module\.s(a|c)ss$/,
                use: [
                    'style-loader',
                    {
                        loader: 'css-loader',
                        options: {
                            modules: {
                                localIdentName: '[name]__[local]--[hash:base64:5]'
                            }
                        }
                    },
                    {
                        loader: 'sass-loader'
                    }
                ]
            },
                {
                    test: /\.s(a|c)ss$/,
                    exclude: /\.module.(s(a|c)ss)$/,
                    use: [
                        'style-loader',
                        'css-loader',
                        {
                            loader: 'sass-loader'
                        }
                    ]
                },
                {
                    test: /\.css$/,
                    use: ['style-loader', 'css-loader']
                },
                {
                    test: /\.js$/,
                    enforce: 'pre',
                    use: ['source-map-loader'],
                    exclude: /(node-modules)/
                },
                {
                    test: /\.tsx?$/,
                    exclude: /(node_modules)/,
                    loader: "ts-loader",
                },
                {test: /\.(png|jpe?g|gif|svg)$/i, type: 'asset/resource'},
                {test: /\.(woff|woff2|eot|ttf|otf)$/i, type: "asset/resource"},
            ]
        },
        resolve: {
            extensions: ['.ts', '.tsx', '.js', '.json', 'css', '.scss']
        },
        stats: {
            hash: false,
            entrypoints: false,
            modules: false,
        },
        optimization: {
            runtimeChunk: "single",
            splitChunks: {
                cacheGroups: {
                    vendors: {
                        name: "vendors",
                        test: /[\\/]node_modules[\\/]/,
                        chunks: "all",
                        minChunks: 1,
                        priority: 1,
                    },
                    react: {
                        name: "react",
                        test: /[\\/]node_modules[\\/]react.*?[\\/]/,
                        chunks: "all",
                        minChunks: 1,
                        priority: 2,
                    },
                    material: {
                        name: "material",
                        test: /[\\/]node_modules[\\/]@mui.*?[\\/]/,
                        chunks: "all",
                        minChunks: 1,
                        priority: 3,
                    },
                }
            }
        }
    }
}