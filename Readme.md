# Amazing BeerApp for displaying beers made by BrewDog brewery
## Prerequisites
.net 8.0 SDK installed
___
## Assumptions and shortcuts
`https://api.punkapi.com/` is always available

I chose only one query string just for presenting the data. In real case scenario all query string parameters should be handle.

I have not attached precompiled files because every email client I have used refused to send exec in the zip folder.
___
## How to run the app
1. Extract content of the .zip folder provider wherever suits you.
2. Build BeerApp solution.
3. Navigate in the console to folder containing BeerApp.csproj.
4. Run the command:

```
dotnet run abv
```
abv is dot separated double value of alcohol percentage below which api will search beers
