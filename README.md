# Description

This project was made to be able to dynamically update your IP address on Dynu.com. It is a .Net Core 3.1 Background worker application.

## Configuration

Rename the `appsettings.temp.json` to `appsettings.json` and edit the following details:

 - Dynu -> Username: Your dynu.com username.
 - Dynu -> Password: Your dynu.com account username or if you have set up a separate password for the Dynamic DNS IP update, that one.
 - IpCheckInterval: This defines the interval for the ip address check (in milliseconds). The default is the minimum TLS on dynu.com (30 seconds).

## Building the application

### Requirements
 - .Net Core 3.1 SDK