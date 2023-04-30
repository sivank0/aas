const {merge} = require('webpack-merge')
const common = require('./webpack.config.js')

module.exports = env => merge(common(env), {
    mode: 'development',
    cache: {
        type: 'filesystem',
        allowCollectingMemory: true,
    },
    watch: true,
    devtool: 'source-map',
    watchOptions: {
        ignored: ['src/declares/**', 'node_modules/**'],
    },
})