using CityPowerAndLight.Config;

namespace CityPowerAndLight.Model.DemoTemplates;

/// <summary>
/// The <c>DemoEntityTemplateFactory</c> class provides static methods to return 
/// template instances of various entities for demonstration purposes.
/// </summary>
internal static class DemoEntityTemplateFactory
{
    /// <summary>
    /// Creates a template instance of the <see cref="Account"/> entity.
    /// </summary>
    /// <returns>A new <see cref="Account"/> instance with predefined values.
    /// </returns>
    public static Account GetAccountTemplate(DemoValuesConfig demoValues)
    {
        return new Account()
        {
            Name = demoValues.AccountName,
            Telephone1 = demoValues.AccountTelephone1,
            Address1_City = demoValues.AccountAddress1_City,
            StatusCode = demoValues.AccountStatusCode,
        };
    }

    /// <summary>
    /// Creates a template instance of the <see cref="Contact"/> entity.
    /// </summary>
    /// <returns>A new <see cref="Contact"/> instance with predefined values.
    /// </returns>
    public static Contact GetContactTemplate(DemoValuesConfig demoValues)
    {
        return new Contact()
        {
            FirstName = demoValues.ContactFirstName,
            LastName = demoValues.ContactLastName,
            EMailAddress1 = demoValues.ContactEMailAddress1,
            Telephone1 = demoValues.ContactTelephone1,
        };
    }

    /// <summary>
    /// Creates a template instance of the <see cref="Incident"/> entity.
    /// </summary>
    /// <returns>A new <see cref="Incident"/> instance with predefined values.
    /// </returns>
    public static Incident GetIncidentTemplate(DemoValuesConfig demoValues)
    {
        return new Incident()
        {
            Title = demoValues.IncidentTitle,
            Description = demoValues.IncidentDescription,
            StatusCode = demoValues.IncidentStatusCode,
            CaseTypeCode = demoValues.IncidentCaseTypeCode,
            ServiceStage = demoValues.IncidentServiceStage,
            CaseOriginCode = demoValues.IncidentCaseOriginCode,
            PriorityCode = demoValues.IncidentPriorityCode,
        };
    }
}
