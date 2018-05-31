const autoprefixer = require('autoprefixer');
const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = {
    entry: {
        main: './Scripts/main.ts',
        teamManagement: './Scripts/teamManagement.ts',
        checkout: './Scripts/checkout.ts',
        index: './Scripts/index.ts'
    },
    output: {
        path: path.join(__dirname, 'wwwroot', 'js'),
        filename: '[name].js',
        publicPath: '/js'
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: '../css/[name].css',
            chunkFilename: '[id].css',
            path: path.join(__dirname, 'wwwroot', 'css')
        })
    ],
    module: {
        rules: [
            {
                test: /\.s?[ac]ss$/,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader
                    },
                    {
                        loader: 'css-loader',
                        options: {minimize: true},
                    },
                    {
                        loader: 'postcss-loader',
                        options: {
                            ident: 'postcss',
                            plugins: () => [autoprefixer()],
                        }
                    },
                    {
                        loader: 'sass-loader'
                    }
                ],
            },
            {
                test: /\.vue$/,
                loader: 'vue-loader',
                options: {
                    loaders: {
                        'scss': 'vue-style-loader!css-loader!sass-loader',
                        'sass': 'vue-style-loader!css-loader!sass-loader?indentedSyntax',
                    }
                }
            },
            {
                test: /\.tsx?/,
                loader: 'ts-loader',
                exclude: /node_modules/,
                options: {
                    appendTsSuffixTo: [/\.vue$/],
                }
            },
            {
                test: /\.(png|jpg|gif|svg)$/,
                loader: 'file-loader',
                options: {
                    name: '[name].[ext]?[hash]'
                }
            }
        ]
    },
    resolve: {
        extensions: ['.ts', '.js', '.vue', '.json'],
        alias: {
            'vue$': 'vue/dist/vue.js'
        }
    },
    devtool: 'eval-source-map',
}