using CityPowerAndLight.App;
using CityPowerAndLight.Model;
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
            var app = InitialiseApp(userInterface);
            var runAppTask = app.Run();
            runAppTask.Wait();
        }
        catch (Exception exception)
        {
            userInterface.PrintMessage("Sorry, There has been an unexpected error:");
            userInterface.PrintMessage(exception.Message);
        }
    }


    //Initialises dependencies required for the CustomerServiceAPIExplorationApp,
    //and returns an instance of the app.
    private static CustomerServiceAPIExplorationApp InitialiseApp(
        ConsoleInterface userInterface)
    {
        Env.Load();

        IOrganizationService organisationService =
            InitialiseOrganisationService();

        var appConfiguration = InitialiseAppConfiguration();

        var demoTemplates = InitialiseDemoTemplates(appConfiguration);
        var tableExplorations = InitialiseTableExplorations(
            userInterface, organisationService, demoTemplates);

        //Initialise and return the application instance
        return new CustomerServiceAPIExplorationApp(
            userInterface,
            tableExplorations.AccountTableExploration,
            tableExplorations.ContactTableExploration,
            tableExplorations.IncidentTableExploration,
            appConfiguration.DemoValues
            );
    }


    //Initialise instance of IOrganisation service
    private static IOrganizationService InitialiseOrganisationService()
    {
        string? serviceUrl = Environment.GetEnvironmentVariable("SERVICE_URL");
        string? appId = Environment.GetEnvironmentVariable("APP_ID");
        string? clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

        //Initialise Dynamics API connection
        return OrganisationServiceConnector.Connect(
            serviceUrl, appId, clientSecret);
    }


    //Initialise app configurations settings from appsettings.json
    private static AppConfig InitialiseAppConfiguration()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        return new(configuration);
    }


    //Initialise demo templates using values from the app configuration
    private static DemoTemplates InitialiseDemoTemplates(
        AppConfig appConfiguration)
    {
        //Intitalise entity templates
        var accountTemplate = DemoEntityTemplateFactory.GetAccountTemplate(
            appConfiguration.DemoValues
        );
        var contactTemplate = DemoEntityTemplateFactory.GetContactTemplate(
            appConfiguration.DemoValues
        );
        var incidentTemplate = DemoEntityTemplateFactory.GetIncidentTemplate(
            appConfiguration.DemoValues
        );

        return new DemoTemplates(
            accountTemplate, contactTemplate, incidentTemplate);
    }


    //Initialise table exploration dependencies
    private static TableExplorations InitialiseTableExplorations(
        IUserInterface userInterface,
        IOrganizationService organisationService,
        DemoTemplates demoTemplates
        )
    {
        AccountTableExploration accountTableExploration = new(
            organisationService, userInterface, demoTemplates.Account
        );
        ContactTableExploration contactTableExploration = new(
            organisationService, userInterface, demoTemplates.Contact
        );
        IncidentTableExploration incidentTableExploration = new(
            organisationService, userInterface, demoTemplates.Incident
        );

        return new TableExplorations(
            accountTableExploration,
            contactTableExploration,
            incidentTableExploration
        );
    }


    //Structure to hold demo templates.
    private record DemoTemplates(
        Account Account,
        Contact Contact,
        Incident Incident);


    //Structure to hold exploration dependencies.
    private record TableExplorations(
        AccountTableExploration AccountTableExploration,
        IContactTableExploration ContactTableExploration,
        IIncidentTableExploration IncidentTableExploration);

}
