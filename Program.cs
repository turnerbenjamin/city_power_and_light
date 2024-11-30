using CityPowerAndLight.Config;
using CityPowerAndLight.Service;
using Microsoft.Xrm.Sdk;

class Program
{
    static void Main()
    {
        AppConfig.ParseAndSetEnvironmentVariables();

        string serviceUrl = Environment.GetEnvironmentVariable("SERVICE_URL") ?? "";
        string appId = Environment.GetEnvironmentVariable("APP_ID") ?? "";
        string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET") ?? "";

        IOrganizationService service = OrganisationServiceConnector.Connect(serviceUrl, appId, clientSecret);

        CreateAccount(service);
        Console.ReadKey();
    }

    static void CreateAccount(IOrganizationService service)
    {
        Entity lateBoundAccount = new("account");
        // string primary name
        lateBoundAccount["name"] = "Contoso (Late Bound)";
        // Boolean (Two option)
        lateBoundAccount["creditonhold"] = false;
        // DateTime
        lateBoundAccount["lastonholdtime"] = new DateTime(2023, 1, 1);
        // Double
        lateBoundAccount["address1_latitude"] = 47.642311;
        lateBoundAccount["address1_longitude"] = -122.136841;
        // Int
        lateBoundAccount["numberofemployees"] = 500;
        // Money
        lateBoundAccount["revenue"] = new Money(new decimal(5000000.00));
        // Choice (Option set)
        lateBoundAccount["accountcategorycode"] = new OptionSetValue(1);

        //Create the account
        Guid lateBoundAccountId = service.Create(lateBoundAccount);
        Console.WriteLine(lateBoundAccountId);

        //Delete the accounts
        service.Delete("account", lateBoundAccountId);
    }
}