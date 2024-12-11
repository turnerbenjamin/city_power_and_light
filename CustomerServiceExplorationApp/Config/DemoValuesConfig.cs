using System.Configuration;
using CityPowerAndLight.Model;

namespace CityPowerAndLight.Config;

/// <summary>
/// The <c>DemoValuesConfig</c> class holds configuration values for demo 
/// entities.
/// </summary>
internal class DemoValuesConfig
{
    public required string AccountName { get; set; }
    public required account_statuscode AccountStatusCode { get; set; }
    public required string AccountTelephone1 { get; set; }
    public required string AccountAddress1_City { get; set; }

    public required string ContactFirstName { get; set; }
    public required string ContactLastName { get; set; }
    public required string ContactEMailAddress1 { get; set; }
    public required string ContactTelephone1 { get; set; }
    public required string ContactUpdatedFirstName { get; set; }

    public required string IncidentTitle { get; set; }
    public required string IncidentDescription { get; set; }
    public required incident_prioritycode IncidentPriorityCode { get; set; }
    public required incident_statuscode IncidentStatusCode { get; set; }
    public required servicestage IncidentUpdatedServiceStage { get; set; }
    public required incident_casetypecode IncidentCaseTypeCode { get; set; }
    public required servicestage IncidentServiceStage { get; set; }
    public required incident_caseorigincode IncidentCaseOriginCode { get; set; }

    /// <summary>
    /// Validates the configuration values.
    /// </summary>
    /// <returns>True if all values are valid else throw an exception</returns>
    /// <exception cref="ConfigurationErrorsException">
    /// Thrown when any of the demo values are missing or empty.
    /// </exception>
    public bool Validate()
    {
        ValidateAccountValues();
        ValidateContactValues();
        ValidateIncidentValues();
        return true;
    }

    //Validate all required values for a demo account
    private void ValidateAccountValues()
    {
        EnsureStringSet(AccountName, nameof(AccountName));
        EnsureEnumSet(AccountStatusCode, nameof(AccountStatusCode));
        EnsureStringSet(AccountTelephone1, nameof(AccountTelephone1));
        EnsureStringSet(AccountAddress1_City, nameof(AccountAddress1_City));
    }

    //Validate all required values for a demo contact
    private void ValidateContactValues()
    {
        EnsureStringSet(ContactFirstName, nameof(ContactFirstName));
        EnsureStringSet(ContactLastName, nameof(ContactLastName));
        EnsureStringSet(ContactEMailAddress1, nameof(ContactEMailAddress1));
        EnsureStringSet(ContactTelephone1, nameof(ContactTelephone1));
        EnsureStringSet(ContactUpdatedFirstName, nameof(ContactUpdatedFirstName));
    }

    //Validate all required values for a demo incident
    private void ValidateIncidentValues()
    {
        EnsureStringSet(IncidentTitle, nameof(IncidentTitle));
        EnsureStringSet(IncidentDescription, nameof(IncidentDescription));
        EnsureEnumSet(IncidentPriorityCode, nameof(IncidentPriorityCode));
        EnsureEnumSet(IncidentStatusCode, nameof(IncidentStatusCode));
        EnsureEnumSet(IncidentUpdatedServiceStage, nameof(IncidentUpdatedServiceStage));
        EnsureEnumSet(IncidentCaseTypeCode, nameof(IncidentCaseTypeCode));
        EnsureEnumSet(IncidentServiceStage, nameof(IncidentServiceStage));
        EnsureEnumSet(IncidentCaseOriginCode, nameof(IncidentCaseOriginCode));
    }

    //Helper method. Throws an exception if the value is null or empty
    private static void EnsureStringSet(string value, string propertyName)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ConfigurationErrorsException(
                $"{propertyName} is missing or empty in the configuration");
        }
    }

    //Helper method. Throws an exception if the emum is undefined
    private static void EnsureEnumSet<T>(T value, string propertyName)
        where T : Enum
    {
        if (!Enum.IsDefined(typeof(T), value))
        {
            throw new ConfigurationErrorsException(
                $"{propertyName} is missing or empty in the configuration");
        }
    }
}