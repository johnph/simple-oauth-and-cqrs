## Simple OAuth2 and CQRS sample

Two sample applications created using .Net Core 2.2 web api project. One of the application is a authentication Server implemented using IdentityServer4 as Open-Id Connect/OAuth2 Provider and the other application is a resource server implemented using CQRS pattern. Below shown diagram is a high level picture of what's in the sample. 

![High Level Design](https://i.imgur.com/O7uOyvF.png)

A .Net Core 2.2 MVC Web app added to the front-end that communicates with the Auth and Resource server. This web app supports login via google and login via registering the user through ASP.Net Core Identity membership system. For both the login methods (google or by providing registered username/password), Auth server is the one that issues security token to access the protected resources.

## Development Environment

1. Visual Studio 2017
2. .Net Core SDK 2.2
3. Identity Server4
4. Azure SQL Database 

## Tools, Technologies

1. Entity Framework Core for Database connectivity (Write model)
2. Dapper for executing raw SQL (Read model)
3. MediatR for domain events implementation
4. Swashbucke for API documentation
5. XUnit for Unit test
6. Postman as API Client

### CRUD Operation

A simple "Employee" entity/object is used at the resource server api to implement 4 basic CRUD operation (Create, Read, Update, Delete). The application follows CQRS pattern. Commands such as Create, Update and Delete uses Entity Framework Core for database connectivity and Query Operation such as Read (Get) uses Dapper for database access.

![CQRS](https://i.imgur.com/wTLgmfX.png)

### Where is it hosted?

Both Web Api projects are published and hosted in Azure App Service - shared infrastructure at the below given urls. Resource server has swagger for API documentation. Auth Server is configured with in Memory clients, resources and users

#### Auth Server & Access Token URL:
https://johndev-authserver.azurewebsites.net/connect/token

#### *Client Configuration to request token from Identity Server*

grant_type:client_credentials<br/>
client_id:oauthClient<br/>
client_secret:superSecretPassword<br/>
scope:api.execute<br/>

#### Resource Server & Api documentation:
https://johndev-resourceserver.azurewebsites.net/swagger

#### Web App:
https://johndev-webapp.azurewebsites.net

### How to run the application?

1. Create a empty Database and Publish the db script to create the sql table. Script is in the Database project.
2. Configure the SQL connection string and Auth Server Url in Resource server appsettings.json to run/publish the Resource Server Api. 

```json
  "ConnectionStrings": {
    "DefaultConnection": "<sql database connection>"
  },
  "AuthServer": "<Auth Server base Url>",
```
3. Configure the below app settings to run/publish the web app

```json
  "ConnectionStrings": {
    "DefaultConnection": "<SQL connection string>"
  },
  "ServiceHost": {
    "ResourceServer": "<Resource Server base url>"
  },
  "Authentication": {
    "Google": {
      "ClientId": "<google oauth 2.0 client id>",
      "ClientSecret": "<google oauth 2.0 client secret>"
    }
  },
  "AuthToken": {
    "Authority": "<Auth server base url>",
    "ClientId": "oauthClient",
    "ClientSecret": "superSecretPassword",
    "Scope": "api.execute"
  }
```
---

### Some Screen shots

#### Login Page

![Login page](https://i.imgur.com/vdpxC2w.png)

#### Inner page

![](https://i.imgur.com/hB27hqi.png)

### Try out

https://johndev-webapp.azurewebsites.net

---

### References

1. [User Authentication and Identity with Angular, Asp.Net Core and IdentityServer](https://fullstackmark.com/post/21/user-authentication-and-identity-with-angular-aspnet-core-and-identityserver)
2. [Simple CQRS implementation with raw SQL and DDD](http://www.kamilgrzybek.com/design/simple-cqrs-implementation-with-raw-sql-and-ddd/)
