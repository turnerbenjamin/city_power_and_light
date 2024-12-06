# City Power and Light

## Summary

This console application demonstrates interaction with the dataverse, in a
customer service environment, using the dataverse SDK.

## Usage

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

## The Demonstration

This application demonstrates the performance of CRUD operations on the
following tables:

- Account
- Contact
- Incident/Case
