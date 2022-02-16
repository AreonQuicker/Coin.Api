# Coin.Api
Sync and Get cryptocurrencies

## Descrption
This API application will be used to sync all rates from *CoinMarketCap* in a local DB that has been favorited. By doing that we can get the rates directly from the local database

The rates will be synced in a frequency of 10 min (Can be configured)

The following systems will be synced
- Coins
- Currencies
- Cryptocurrencies (Only if it has been favorited)

## How to configure

Following config values can be configured from appsettings.json
- **CoinGeneralConfig.UseInMemoryDatabase**: Specify if inmemory database need to be used (Currently is true)
- **ConnectionStrings.LOCAL**: Sql connection string
- **CoinApiConfig.Key**: Api Key
- **CoinApiConfig.BaseUrl**: Base Url to *CoinMarketCap* (Currently is _"https://pro-api.coinmarketcap.com/"_)
- **CoinApiCronJobsConfig.SyncCoinsCronJob**: Cron job frequency for Coinds (Currently is 10 min)
- **CoinApiCronJobsConfig.SyncCurrenciesCronJob**: Cron job frequency for Currencies (Currently is 10 min)
- **CoinApiCronJobsConfig.SyncCryptoCurrenciesCronJob**: Cron job frequency for cryptocurrencies (Currently is 10 min)

## How to use (Run)

After pulling the branch locally, configure the app settings values and then Run.

When the application is running ,2 URLs will be available:
- **Hangfire**: https://localhost:port/hangfire
- **swagger**: https://localhost:port/swagger/index.html

## Available APIs

![image](https://user-images.githubusercontent.com/3213398/154169643-f3bcea46-3db6-4c1f-b75c-d9ff9a5794d5.png)

## Architecture

![image](https://user-images.githubusercontent.com/3213398/154169701-7e3ab5ff-918f-42f0-8570-dadd3f6e9a39.png)

## Folder and Project layout

![image](https://user-images.githubusercontent.com/3213398/154169774-6baa775a-6a8d-4918-a6c9-d85933173785.png)
