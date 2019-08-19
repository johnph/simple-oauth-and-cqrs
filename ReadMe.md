## Simple OAuth2 and CQRS sample

Two sample applications created using .Net Core 2.2 web api project. One of the application is a authentication Server implemented using IdentityServer4 as Open-Id Connect/OAuth2 Provider and the other application is a resource server implemented using CQRS pattern. Below shown diagram is a high level picture of what's in the sample.

![](https://i.imgur.com/mMGK8IE.png)


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

### Where is it hosted?

Both Web Api projects are published and hosted in Azure App Service - shared infrastructure at the below given urls. Resource server has swagger for API documentation.

#### Auth Server & Access Token URL:
https://johndev-authserver.azurewebsites.net/connect/token

#### *Client Configuration to request token from Identity Server*

grant_type:client_credentials<br/>
client_id:oauthClient<br/>
client_secret:superSecretPassword<br/>
scope:api.execute<br/>

#### Resource Server & Api documentation:
https://johndev-resourceserver.azurewebsites.net/swagger

### How to run the application?

Just in case if the above given url doesn't work and you want to publish the code in Azure App Service or run it in local, here is how you can do.

1. Create a empty Database and Publish the db script to create the sql table. Script is in the Database project.
2. configure the SQL connection string and Auth Server Url in Resource server appsettings.json. That's it. You are good to go.

```json
  "ConnectionStrings": {
    "DefaultConnection": "<Your sql database connection>"
  },
  "AuthServer": "<your Auth Server base Url>",
```

This sample doesn't have MVC App in the front-end with UI. It's just only the back-end part. Will add ASP.NET Core MVC sample app soon. For now, consider using Postman or any REST API client tool to connect to Auth and Resource Server to see this sample working working.

---

### References

1. [User Authentication and Identity with Angular, Asp.Net Core and IdentityServer](https://fullstackmark.com/post/21/user-authentication-and-identity-with-angular-aspnet-core-and-identityserver)
2. [Simple CQRS implementation with raw SQL and DDD](http://www.kamilgrzybek.com/design/simple-cqrs-implementation-with-raw-sql-and-ddd/)