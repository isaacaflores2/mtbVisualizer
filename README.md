# [mtbVisualizer](https://mtbvisualizer.azurewebsites.net/)
## Visualize your Strava data
ASP.NET Core Web Application that utilizes the Strava API to allow you to view your workouts and activities on a calendar, table, and a map. 
I was inspired to create this website because I wanted to be able to visualize all the cool places I have mountain biked in Washington. The site has the following features:
1) Monthly Calendar - The calendar highlights dates you have recorded a run, ride, or any workout using Strava. 
2) Weekly Table - The table lists any activities you have recorded on Strava this week. 
3) Map - The map displays number of visits per location (start coordinates) for your run, rides, or all combined activities. 
--- 
## Technologies
The web application uses the following technologies to be able to request, display, and save your data:
1. ASP.NET Core
2. OAuth
3. Azure (App Service, SQL Database, Azure KeyVault)
4. Bing Maps API
5. Strava API
6. jQuery (jQueryUI, Datatables)
7. Bootstrap
## Dependencies 
1. Nuget Packages
    * AspNet.Security.OAuth.Strava
    * Microsoft.AspNetCore.AzureKeyVault.HostingStartup
    * Microsoft.Azure.KeyVault
    * Microsoft.Azure.Services.AppAuthentication
    * Newtonsoft.Json
2. [Strava Client Code](https://developers.strava.com/docs/#client-code)
3. [jQueryUI](https://jqueryui.com/)
4. [Datatables](https://datatables.net/)
