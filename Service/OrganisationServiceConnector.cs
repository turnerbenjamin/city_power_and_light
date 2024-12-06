using System.Text;
using Microsoft.Identity.Client;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Utils;
using Microsoft.Xrm.Sdk;

namespace CityPowerAndLight.Service;

/// <summary>
/// The <c>OrganisationServiceConnector</c> class provides methods to connect to 
/// the Dataverse service.
/// </summary>
internal static class OrganisationServiceConnector
{
    /// <summary>
    /// Connects to the Dataverse service using the provided service URL, 
    /// application ID, and client secret.
    /// </summary>
    /// <param name="serviceUrl">The URL of the Dataverse service.</param>
    /// <param name="appId">The application ID used for authentication.</param>
    /// <param name="clientSecret">The client secret used for authentication.
    /// </param>
    /// <returns>An instance of <see cref="IOrganizationService"/> connected to 
    /// the Dataverse service.</returns>
    /// <exception cref="ArgumentNullException">Thrown when any of the 
    /// parameters are null.</exception>
    /// <exception cref="DataverseConnectionException">Thrown when there is an 
    /// error connecting to the Dataverse service.</exception>
    public static IOrganizationService Connect(
        string? serviceUrl,
        string? appId,
        string? clientSecret)
    {
        ArgumentNullException.ThrowIfNull(serviceUrl, nameof(serviceUrl));
        ArgumentNullException.ThrowIfNull(appId, nameof(appId));
        ArgumentNullException.ThrowIfNull(clientSecret, nameof(clientSecret));

        string connectionString = $@"AuthType=ClientSecret;
                        SkipDiscovery=true;url={serviceUrl};
                        Secret={clientSecret};
                        ClientId={appId};
                        RequireNewInstance=true";
        try
        {
            IOrganizationService service = new ServiceClient(connectionString);
            return service;
        }
        catch (DataverseConnectionException ex)
        {
            var errorMessage = ParseDataverseConnectionExceptionDetails(ex, serviceUrl);
            throw new DataverseConnectionException(errorMessage, ex);
        }
    }

    //Parses the details of a DataverseConnectionException to provide a more 
    //readable error message
    private static string ParseDataverseConnectionExceptionDetails(
        DataverseConnectionException exception, string serviceUrl)
    {
        StringBuilder exceptionMessageBuilder = new();
        exceptionMessageBuilder.AppendLine("Failed to connect to dataverse");

        var innerException = exception.InnerException as MsalServiceException;
        if (innerException is not null)
        {
            exceptionMessageBuilder
                .AppendLine($"Status code: {innerException.StatusCode}");
            exceptionMessageBuilder.AppendLine(innerException.ResponseBody);
        }
        else
        {
            exceptionMessageBuilder.AppendLine($"Service url: {serviceUrl}");
        }
        return exceptionMessageBuilder.ToString();
    }
}
