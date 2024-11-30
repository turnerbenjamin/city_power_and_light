using System.Text.Json;
using Microsoft.Identity.Client;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace CityPowerAndLight.Service;

internal static class OrganisationServiceConnector
{
    public static IOrganizationService Connect(string serviceUrl, string appId, string clientSecret)
    {





        string connectionString = $@"AuthType=ClientSecret;
                        SkipDiscovery=true;url={serviceUrl};
                        Secret={clientSecret};
                        ClientId={appId};
                        RequireNewInstance=true";

        IOrganizationService service = new ServiceClient(connectionString);
        return service;
    }

    private static async Task<string> GetAccessToken(string tenantId, string clientId, string clientSecret, string username, string password, string scope)
    {
        Console.WriteLine($"Client Id: {clientId}");
        Console.WriteLine($"TenantId: {tenantId}");
        Console.WriteLine($"SecretValue: {clientSecret}");
        Console.WriteLine($"UserName: {username}");
        Console.WriteLine($"Password: {password}");
        Console.WriteLine($"Scope: {scope}");
        var url = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";
        var client = new HttpClient();
        var payload = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", clientId },
            { "client_secret", clientSecret },
            { "username", username },
            { "password", password },
            { "scope", scope }
        };
        Console.WriteLine("$$$$");
        var response = await client.PostAsync(url, new FormUrlEncodedContent(payload));

        Console.WriteLine($"STATUS CODE: {response.StatusCode}");
        response.EnsureSuccessStatusCode();
        Console.WriteLine("$$$$");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine("$$$$");
        var tokenResponse = JsonSerializer.Deserialize<AuthResponse>(jsonResponse);
        Console.WriteLine("$$$$");
        return tokenResponse.access_token;
    }

    private class AuthResponse
    {
        public string token_type { get; set; }
        public int expires_in { get; set; } // Numeric field
        public int ext_expires_in { get; set; } // Numeric field
        public string access_token { get; set; }
    }
}