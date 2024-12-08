using System.Text;
using CityPowerAndLight.Model;
using CityPowerAndLight.Utilities;
using CityPowerAndLight.View;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CityPowerAndLight.App;


/// <summary>
/// Provides methods to demonstrate the CRUD operation on the incident table.
/// </summary>
/// <param name="organisationService">The organization service instance.</param>
/// <param name="userInterface">The user interface instance.</param>
/// <param name="demoIncidentTemplate">The demo incident template.</param>
internal class IncidentTableExploration(
    IOrganizationService organisationService,
    IUserInterface userInterface,
    Incident demoIncidentTemplate
    ) : IIncidentTableExploration
{
    private readonly IOrganizationService _organisationService
        = organisationService;
    private readonly IUserInterface _userInterface = userInterface;
    private readonly Incident _demoIncidentTemplate = demoIncidentTemplate;


    /// <summary>
    /// Demonstrates the creation of an incident.
    /// </summary>
    /// <param name="associatedAccountId">The ID of the associated account.
    /// </param>
    /// <param name="associatedContactId">The ID of the associated contact.
    /// </param>
    /// <returns>The ID of the newly created incident.</returns>
    public async Task<Guid> DemonstrateIncidentCreation(
        Guid associatedAccountId, Guid associatedContactId)
    {
        _userInterface.PrintMessage(
            $"Creating demonstration incident ({_demoIncidentTemplate.Title})");

        _demoIncidentTemplate.CustomerId =
            new EntityReference(Account.EntityLogicalName, associatedAccountId);
        _demoIncidentTemplate.PrimaryContactId =
            new EntityReference(Contact.EntityLogicalName, associatedContactId);

        var newIncidentId = await Task.Run(() =>
        _organisationService.Create(_demoIncidentTemplate));

        return newIncidentId;
    }


    /// <summary>
    /// Demonstrates fetching an incident by its ID.
    /// </summary>
    /// <param name="incidentToFetchId">The ID of the incident to fetch.</param>
    /// <returns>The fetched incident.</returns>
    public async Task<Incident> DemonstrateFetchingAnIncident(
        Guid incidentToFetchId)
    {
        _userInterface.PrintMessage("Fetching demo incident...");
        return await GetIncidentById(incidentToFetchId);
    }


    /// <summary>
    /// Demonstrates updating an incident's service stage.
    /// </summary>
    /// <param name="incidentToUpdate">The incident to update.</param>
    /// <param name="updatedServiceStage">The new service stage.</param>
    public async Task DemonstrateUpdatingAnIncident(
        Incident incidentToUpdate, servicestage updatedServiceStage)
    {
        _userInterface.PrintMessage("Updating incident service stage...");
        incidentToUpdate.ServiceStage = updatedServiceStage;

        await Task.Run(() =>
        _organisationService.Update(incidentToUpdate));
    }


    /// <summary>
    /// Demonstrates deleting an incident by its ID.
    /// </summary>
    /// <param name="incidentToDeleteId">The ID of the incident to delete.
    /// </param>
    public async Task DemonstrateDeletingAnIncident(Guid incidentToDeleteId)
    {
        await Task.Run(() => _organisationService.Delete(
            Incident.EntityLogicalName, incidentToDeleteId));

        _userInterface.PrintMessage("Demo incident deleted");
    }


    /// <summary>
    /// Demonstrates fetching all active incidents.
    /// </summary>
    /// <returns>A collection of active incidents.</returns>
    public Task<IEnumerable<Incident>> DemonstrateFetchingAllActiveIncidents()
    {
        _userInterface.PrintMessage("Fetching all active incidents...");
        var allActiveIncidentsFilter = new ConditionExpression(
                "statecode",
                ConditionOperator.Equal,
                (int)incident_statecode.Active
        );

        return _organisationService.GetAll<Incident>(
            Incident.EntityLogicalName,
            new ColumnSet("title"), [allActiveIncidentsFilter]
        );
    }


    /// <summary>
    /// Displays the details of a single incident.
    /// </summary>
    /// <param name="incidentToDisplay">The incident to display.</param>
    public void DisplayIncident(Incident incidentToDisplay)
    {
        var incidentDisplayString = GetIncidentDisplayString(incidentToDisplay);
        _userInterface.PrintMessage("Printing incident:");
        _userInterface.PrintMessage(incidentDisplayString);
    }


    /// <summary>
    /// Displays the details of multiple incidents.
    /// </summary>
    /// <param name="incidentsToDisplay">The incidents to display.</param>
    public void DisplayIncidents(IEnumerable<Incident> incidentsToDisplay)
    {
        _userInterface.PrintMessage("Printing incidents:");
        _userInterface.PrintSpacer();

        _userInterface.PrintEntityList(
            incidentsToDisplay, incident => incident.Title);
    }


    //Helper method to get an incident by id. Specifies the column set to 
    //reduce the size of the response payload. Note, statuscode is needed with
    //servicestage to allow for the service stage to be updated.
    private Task<Incident> GetIncidentById(Guid incidentToFetchId)
    {
        var columnSet = new ColumnSet(
            "title",
            "description",
            "ticketnumber",
            "customerid",
            "primarycontactid",
            "servicestage",
            "statuscode",
            "statecode",
            "createdon",
            "casetypecode",
            "caseorigincode",
            "prioritycode"
        );

        return _organisationService.GetById<Incident>(
            Incident.EntityLogicalName, incidentToFetchId, columnSet);
    }


    // Private helper method to get a display string for an incident
    private static string GetIncidentDisplayString(Incident incident)
    {
        var incidentDisplayStringBuilder = new StringBuilder();
        incidentDisplayStringBuilder.AppendLine(
            $"Title: {incident.Title}");
        incidentDisplayStringBuilder.AppendLine(
            $"Description: {incident.Description}");
        incidentDisplayStringBuilder.AppendLine(
            $"Case Number: {incident.TicketNumber}");
        incidentDisplayStringBuilder.AppendLine(
            $"Priority: {incident.PriorityCode}");
        incidentDisplayStringBuilder.AppendLine(
            $"Origin: {incident.CaseOriginCode}");
        incidentDisplayStringBuilder.AppendLine(
            $"Customer: {incident.CustomerId.Name}");
        incidentDisplayStringBuilder.AppendLine(
            $"Contact: {incident.PrimaryContactId.Name}");
        incidentDisplayStringBuilder.AppendLine(
            $"Status Reason: {incident.StatusCode}");
        incidentDisplayStringBuilder.AppendLine(
            $"CreatedOn: {incident.CreatedOn}");
        incidentDisplayStringBuilder.AppendLine(
            $"Service Stage: {incident.ServiceStage}");

        return incidentDisplayStringBuilder.ToString();
    }
}