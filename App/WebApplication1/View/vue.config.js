const fs = require('fs')
const path = require('path')

const baseFolder =
    process.env.APPDATA !== undefined && process.env.APPDATA !== ''
        ? `${process.env.APPDATA}/ASP.NET/https`
        : `${process.env.HOME}/.aspnet/https`;

const certificateArg = process.argv.map(arg => arg.match(/--name=(?<value>.+)/i)).filter(Boolean)[0];
const certificateName = certificateArg ? certificateArg.groups.value : "wwwroot";
const { createProxyMiddleware } = require('http-proxy-middleware');
if (!certificateName) {
    console.error('Invalid certificate name. Run this script in the context of an npm/yarn script or pass --name=<<app>> explicitly.')
    process.exit(-1);
}

const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

module.exports = {
    // 將編譯後的檔案輸出到指定的目錄
    outputDir: '../wwwroot',
    configureWebpack: {
        //以 @ 代替 src
        resolve: {
            alias: {
                '@': path.resolve(__dirname, 'src'),
            },
        },
    //在網頁url後面加入版本號，使其更新後可以即時刷新，不受到 cache 影響。
            output: {
            filename: '[name].[hash].js',
            chunkFilename: '[name].[hash].js'
        },
    },
    devServer: {
        https: {
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        },
        proxy: process.env.NODE_ENV === 'production' ?
            {
                '^': {
                    target: 'https://localhost:7135/',
                    ws: true,
                    changeOrigin: true
                }
            }
            : {},
        port: 5002
    }
}