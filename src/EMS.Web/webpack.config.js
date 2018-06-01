const path = require('path');
const merge = require('webpack-merge');
const webpack = require('webpack');
const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;

const isDevelopment = process.env.ASPNETCORE_ENVIRONMENT === 'see3slamdev';

module.exports = merge({
    stats: { modules: false },
    context: __dirname,
    resolve: {
        extensions: ['.js', '.ts'],
        alias: {
            vue: 'vue/dist/vue.js'
        }
    },
    module: {
        loaders: [
            { test: /\.ts(x?)$/, exclude: /node_modules/, loader: 'awesome-typescript-loader?silent' }
        ]
    },
    entry: {
        teamManagement: ['./Scripts/Utils.ts', './Scripts/TeamComponent.ts', './Scripts/PlayerComponent.ts', './Scripts/TeamManagement.ts'],
        checkout: ['./Scripts/Utils.ts', './Scripts/Checkout.ts']
    },
    output: {
        path: path.join(__dirname, 'wwwroot', 'js'),
        filename: '[name].js',
        publicPath: '/js/'
    },
    plugins: [
        new CheckerPlugin(),
        new webpack.DefinePlugin({
            'process.env': {
                NODE_ENV: JSON.stringify(isDevelopment ? 'development' : 'production')
            }
        }),
        new webpack.DllReferencePlugin({
            context: __dirname,
            manifest: require('./wwwroot/lib/vendor-manifest.json')
        })
    ].concat(isDevelopment ? [
        new webpack.SourceMapDevToolPlugin({
            filename: '[file].map',
            moduleFilenameTemplate: path.relative(path.join('wwwroot', 'dist'), '[resourcePath]')
        })
    ] : [
            new webpack.optimize.UglifyJsPlugin()
    ])
});