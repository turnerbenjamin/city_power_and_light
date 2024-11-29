namespace CityPowerAndLight;

using CityPowerAndLight.Config;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

class Program
{
    static void Main()
    {

        AppConfig.InitEnvironment();

        string SecretID = Environment.GetEnvironmentVariable("AppSecretId") ?? "";
        string AppId = Environment.GetEnvironmentVariable("AppId") ?? "";
        string InstanceUri = Environment.GetEnvironmentVariable("InstanceUri") ?? "";

        string connectionString = $@"AuthType=ClientSecret;
                        SkipDiscovery=true;url={InstanceUri};
                        Secret={SecretID};
                        ClientId={AppId};
                        RequireNewInstance=true";

        IOrganizationService service = new ServiceClient(connectionString);
        var response = (WhoAmIResponse)service.Execute(new WhoAmIRequest());

        foreach (var r in response.Results)
        {
            Console.WriteLine(r);
        }

        CreateCase(service);
        Console.ReadKey();
    }

    static Guid CreateCase(IOrganizationService service)

    {


        Entity newCase = new("incident");

        newCase["title"] = "Sample Case - API Test";
        newCase["description"] = "This is a test case created via the Dataverse API.";
        newCase["customerid"] = new EntityReference("account", Guid.NewGuid()); // Use an existing Account or Contact ID

        Guid caseId = service.Create(newCase);

        return caseId;

    }

    //prvCreateIncident
    //prvReadIncident



}
