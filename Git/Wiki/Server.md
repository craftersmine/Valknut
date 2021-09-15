# Server
All server endpoints are as close as possible to original authentication services
## Authentication
###### Valknut doesn't support legacy authentication. Only 1.6.x and later versions of Minecraft that uses AuthLib is supported
---
##### `/authenticate` endpoint
Authenticates user using e-mail and password and returns access token

Type: `POST`

Request `application/json`:

```json
{
	"agent": {
		"name": "Minecraft",
		"version": 1
	},
	"username": "<user_email>",
	"password": "<user_password>",
	"clientToken": "<user_client_token>",
	"requestUser": true/false
}
```
Request parameters description

Parameter     | Type           | Description
--------------|----------------|------------
`agent`       | `Agent Object` | Defines what type of launcher or game tries to authenticate. Valknut doesn't uses this parameter and ignores it
`username`    | `string`       | User E-mail used to authenticate
`password`    | `string`       | User password
`clientToken` | `string`       | Hexademical string that contains GUID of last client token when user was authenticated. If it is new authentication location, it must be null or ommited
`requestUser` | `boolean`      | Valknut currently ignores this parameter

Response `application/json`:
```json
{
	"accessToken": "<user_access_token>",
	"clientToken": "<client_token>",
	"availableProfiles": [
		{
			"id": "<profile_uuid>",
			"name": "<username>"
		}
	],
	"selectedProfile":
	{
		"id": "<profile_uuid>",
		"name": "<username>"
	}
}
```

Response parameters description

Parameter           | Type            | Description
--------------------|-----------------|------------
`accessToken`       | `string`        | Access token used to authenticate user when connecting to Minecraft server or using some API endpoints
`clientToken`       | `string`        | Client token. Only added or not null when no client token provided on request, server will generate new and client should keep it and send it along with access token when making certain requests
`availableProfiles` | `Array`         | Available user profiles, Valknut doesn't support multiple profiles, so it will contain only one, selected profile
`selectedProfile`   | `Profile object`| Currently selected profile. Contains UUID of profile and username