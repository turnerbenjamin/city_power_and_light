namespace CityPowerAndLight;

using CityPowerAndLight.Controller;
using CityPowerAndLight.Model;
using CityPowerAndLight.Service;
using Microsoft.Xrm.Sdk;
using DotNetEnv;

class Program
{
    static void Main()
    {
        Env.Load();

        string serviceUrl = Environment.GetEnvironmentVariable("SERVICE_URL") ?? "";
        string appId = Environment.GetEnvironmentVariable("APP_ID") ?? "";
        string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET") ?? "";

        IOrganizationService service = OrganisationServiceConnector.Connect(serviceUrl, appId, clientSecret);

        EntityService<Account> accountService = new(service, Account.EntityLogicalName);
        AccountController accountController = new(accountService);

        EntityService<Contact> contactService = new(service, Contact.EntityLogicalName);
        ContactController contactController = new(contactService);

        EntityService<Incident> caseService = new(service, Incident.EntityLogicalName);
        CaseController caseController = new(caseService);

        Console.WriteLine();
        Console.WriteLine("CREATE, READ AND DELETE ACCOUNTS");
        Guid newAccountId = accountController.Create();
        Console.WriteLine(newAccountId);
        accountController.ReadAll();


        Console.WriteLine();
        Console.WriteLine("CREATE, READ AND DELETE CONTACTS");
        Guid newContactId = contactController.Create();
        Console.WriteLine(newContactId);
        contactController.ReadAll();

        Console.WriteLine();
        Console.WriteLine("CREATE, READ AND DELETE CASES");
        Guid newCaseId = caseController.Create(newContactId);
        Console.WriteLine(newCaseId);
        caseController.ReadAll();

        //CLEAN-UP
        Console.WriteLine();
        Console.WriteLine("CLEANING UP");
        caseController.Delete(newCaseId);
        contactController.Delete(newContactId);
        accountController.Delete(newAccountId);

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }
}
