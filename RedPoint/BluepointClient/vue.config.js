module.exports = {
    configureWebpack: {
        devServer: {
            proxy: process.env.ASPNET_URL || 'http://localhost:54332'
        },
        devtool: 'source-map',
        output: {
            devtoolModuleFilenameTemplate: info => {
                const resourcePath = info.resourcePath.replace('./src', './ClientApp/src')
                return `webpack:///${resourcePath}?${info.loaders}`
            }
        }
    }
}