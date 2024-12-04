using CityPowerAndLight.App;
using CityPowerAndLight.Model.DemoTemplates;
using CityPowerAndLight.Service;
using CityPowerAndLight.View;
using DotNetEnv;
using Microsoft.Xrm.Sdk;

namespace CityPowerAndLight;

public class Program
{
    public static void Main()
    {
        //TODO: Write a simple README
        var userInterface = new ConsoleInterface();
        try
        {
            //Load environment variables
            Env.Load();
            string? serviceUrl = Environment.GetEnvironmentVariable("SERVICE_URL");
            string? appId = Environment.GetEnvironmentVariable("APP_ID");
            string? clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

            //Initialise Dynamics API connection
            IOrganizationService organisationService = OrganisationServiceConnector.Connect(serviceUrl, appId, clientSecret);

            //Intitalise entity templates
            var accountTemplate = DemoEntityTemplateFactory.GetAccountTemplate();
            var contactTemplate = DemoEntityTemplateFactory.GetContactTemplate();
            var incidentTemplate = DemoEntityTemplateFactory.GetIncidentTemplate();

            //Intialise and Execute App
            var app = new CustomerServiceAPIExplorationApp(
                userInterface,
                organisationService,
                accountTemplate,
                contactTemplate,
                incidentTemplate
                );
            app.Run();
        }
        catch (Exception ex)
        {
            userInterface.PrintMessage("Sorry, There has been an unexpected error:");
            userInterface.PrintMessage(ex.Message);
        }
    }
}
