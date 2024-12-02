namespace CityPowerAndLight;

using CityPowerAndLight.Model;
using CityPowerAndLight.Service;
using Microsoft.Xrm.Sdk;
using DotNetEnv;
using CityPowerAndLight.App;

class Program
{
    static void Main()
    {
        try
        {
            //Load environment variables
            Env.Load();
            string? serviceUrl = Environment.GetEnvironmentVariable("SERVICE_URL");
            string? appId = Environment.GetEnvironmentVariable("APP_ID");
            string? clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
            string? entityIdentifierPrefix = Environment.GetEnvironmentVariable("ENTITY_IDENTIFIER_PREFIX") ?? "";

            //Initialise Dynamics API connection
            IOrganizationService service = OrganisationServiceConnector.Connect(serviceUrl, appId, clientSecret);

            //Initialise service dependencies
            EntityService<Account> accountService = new(service, Account.EntityLogicalName);
            EntityService<Contact> contactService = new(service, Contact.EntityLogicalName);
            EntityService<Incident> incidentService = new(service, Incident.EntityLogicalName);

            //Intialise and Execute App
            var app = new CustomerServiceAPIExplorationApp(
                accountService, contactService, incidentService, entityIdentifierPrefix
            );

            app.Run();
        }
        catch (Exception ex)
        {
            //Global try catch
            Console.WriteLine("Sorry, There has been an unexpected error:");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }
}
