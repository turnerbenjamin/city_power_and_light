using System.Configuration;
using CityPowerAndLight.App;
using CityPowerAndLight.Config;
using CityPowerAndLight.Model.DemoTemplates;
using CityPowerAndLight.Service;
using CityPowerAndLight.View;
using DotNetEnv;
using Microsoft.Extensions.Configuration;
using Microsoft.Xrm.Sdk;

namespace CityPowerAndLight;

public class Program
{
    public static void Main()
    {
        ConsoleInterface userInterface = new();
        try
        {
            var app = SetUp(userInterface);
            app.Run();
        }
        catch (Exception ex)
        {
            userInterface.PrintMessage("Sorry, There has been an unexpected error:");
            userInterface.PrintMessage(ex.Message);
        }
    }

    //Initialises and injects application dependencies.
    private static CustomerServiceAPIExplorationApp SetUp(
        ConsoleInterface userInterface)
    {
        //Load dataverse credentials from environment variables
        Env.Load();
        string? serviceUrl = Environment.GetEnvironmentVariable("SERVICE_URL");
        string? appId = Environment.GetEnvironmentVariable("APP_ID");
        string? clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

        //Initialise Dynamics API connection
        IOrganizationService organisationService =
            OrganisationServiceConnector.Connect(serviceUrl, appId, clientSecret);

        // Load non-sensitive configuration from appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        AppConfig appConfig = new(configuration);




        //Intitalise entity templates
        var accountTemplate = DemoEntityTemplateFactory.GetAccountTemplate(
            appConfig.DemoValues
        );
        var contactTemplate = DemoEntityTemplateFactory.GetContactTemplate(
            appConfig.DemoValues
        );
        var incidentTemplate = DemoEntityTemplateFactory.GetIncidentTemplate(
            appConfig.DemoValues
        );

        return new CustomerServiceAPIExplorationApp(
            userInterface,
            organisationService,
            accountTemplate,
            contactTemplate,
            incidentTemplate,
            appConfig.DemoValues
            );
    }
}
