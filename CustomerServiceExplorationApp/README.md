# City Power and Light

## Summary

This console application demonstrates interaction with the dataverse, in a
customer service environment, using the dataverse SDK.

## Usage

### Connection To Dataverse

This application uses Microsoft Azure Active Directory (MSAL) for
authentication.

To access the dataverse through this application, you need to provide your
credentials to the application by creating a .env file in the root of the
application conforming to the following template:

```env
SERVICE_URL=https://<your environment>.dynamics.com
APP_ID=<your app id>
CLIENT_SECRET=<secret value for your app>
```

### Entity Properties

The properties of demo entities are taken from appsettings.json. You may
customise this file if you would like different values.

## The Demonstration

This application demonstrates the performance of CRUD operations on the
following tables:

- Account
- Contact
- Incident/Case
