using CityPowerAndLight.Config;
using CityPowerAndLight.Model;
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
        Account earlyBoundAccount = new()
        {
            // string primary name
            Name = "Contoso (Early Bound)",
            // Boolean (Two option)
            CreditOnHold = false,
            // DateTime
            LastOnHoldTime = new DateTime(2023, 1, 1),
            // Double
            Address1_Latitude = 47.642311,
            Address1_Longitude = -122.136841,
            // Int
            NumberOfEmployees = 500,
            // Money
            Revenue = new Money(new decimal(5000000.00)),
            // Choice (Option set)
            AccountCategoryCode = account_accountcategorycode.PreferredCustomer
        };
        Guid earlyBoundAccountId = service.Create(earlyBoundAccount);
        Console.WriteLine(earlyBoundAccountId);
        service.Delete(Account.EntityLogicalName, earlyBoundAccountId);
    }
}