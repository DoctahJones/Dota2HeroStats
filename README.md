# Dota2HeroStats
Web site for tracking stats related to ability draft matches in Dota2

### To build for development:
* #### Server: 
	..* Add steam API key to Startup.Auth.SteamSettings.cs
	..* Add steam account ID to Dota2HeroStatsDBInitializer.Admin.cs to be able to use controller actions requiring authorization or disable the attribute from those methods.
	..* You may also need to change the location for cross site origin requests in WebApiConfig.cs.
* #### Client: 
	..* Run `npm install` from client directory to download dependencies (requires node package manager.
	..* Run `ng serve` to serve the client in web browser.
	