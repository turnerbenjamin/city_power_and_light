namespace CityPowerAndLight.Model.DemoTemplates;

/// <summary>
/// The <c>DemoEntityTemplateFactory</c> class provides static methods to create 
/// template instances of various entities for demonstration purposes.
/// </summary>
internal static class DemoEntityTemplateFactory
{
    /// <summary>
    /// Creates a template instance of the <see cref="Account"/> entity.
    /// </summary>
    /// <returns>A new <see cref="Account"/> instance with predefined values.
    /// </returns>
    public static Account GetAccountTemplate()
    {
        Account newAccount = new()
        {
            Name = $"Relecloud",
            StatusCode = account_statuscode.Active
        };
        return newAccount;
    }

    /// <summary>
    /// Creates a template instance of the <see cref="Contact"/> entity.
    /// </summary>
    /// <returns>A new <see cref="Contact"/> instance with predefined values.
    /// </returns>
    public static Contact GetContactTemplate()
    {
        Contact newContact = new()
        {
            FirstName = $"Jane",
            LastName = "Smith",
        };
        return newContact;
    }

    /// <summary>
    /// Creates a template instance of the <see cref="Incident"/> entity.
    /// </summary>
    /// <returns>A new <see cref="Incident"/> instance with predefined values.
    /// </returns>
    public static Incident GetIncidentTemplate()
    {
        Incident newIncident = new()
        {
            Title = $"Defective Screen",
            Description = "Laptop display is too bright",
            StatusCode = incident_statuscode.InProgress,
            CaseTypeCode = incident_casetypecode.Problem,
            ServiceStage = servicestage.Identify,
        };
        return newIncident;
    }
}