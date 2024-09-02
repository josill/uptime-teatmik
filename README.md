# Uptime Teatmik Kloon

## Notes

appsettings.json and appsettings.Development.json files are removed because of confidental information. Here are the templates to add them:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "DefaultConnection": {
    "Host": "localhost",
    "Port": "5432",
    "Username": "josill",
    "Password": "uptime_teatmik",
    "Database": "uptime-teatmik"
  },
  "BusinessRegisterSettings": {
    "Username": "username_here",
    "Password": "password_here",
    "ChangesUrl": "https://ariregxmlv6.rik.ee/ettevotjaMuudatusedTasuline_v1" 
  },
  "AllowedHosts": "*"
}
```
