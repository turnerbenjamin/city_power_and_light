using CityPowerAndLight.Model;
using CityPowerAndLight.Utilities;
using CityPowerAndLight.View;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CityPowerAndLight.App;

/// <summary>
/// The <c>CustomerServiceAPIExplorationApp</c> class provides methods to 
/// demonstrate CRUD operations using an instance of IOrganisationService. 
/// </summary>
/// <param name="userInterface">The user interface for displaying messages</param>
/// <param name="organizationService">Service for performing CRUD operations on entities.</param>
/// <param name="demoAccountTemplate">Template for creating a demonstration account.</param>
/// <param name="demoContactTemplate">Template for creating a demonstration contact.</param>
/// <param name="demoIncidentTemplate">The template for creating a demonstration incident.</param>
internal class CustomerServiceAPIExplorationApp(
    IUserInterface userInterface,
    IOrganizationService organizationService,
    Account demoAccountTemplate,
    Contact demoContactTemplate,
    Incident demoIncidentTemplate
    )
{
    private readonly IUserInterface _userInterface = userInterface;
    private readonly IOrganizationService _organisationService = organizationService;
    private readonly Account _demoAccountTemplate = demoAccountTemplate;
    private readonly Contact _demoContactTemplate = demoContactTemplate;
    private readonly Incident _demoIncidentTemplate = demoIncidentTemplate;


    /// <summary>
    /// Executes the main workflow of the Customer Service API Exploration application.
    /// </summary>
    /// <remarks>
    /// This method demonstrates the following operations:
    /// <list type="bullet">
    /// <item><description>Creation of entities (Account, Contact, Incident)</description></item>
    /// <item><description>Retrieval of an Incident entity</description></item>
    /// <item><description>Update of an Incident entity</description></item>
    /// <item><description>Deletion of entities (Account, Contact, Incident)</description></item>
    /// </list>
    /// </remarks>
    public void Run()
    {
        _userInterface.PrintTitle("Running Customer Service API Exploration");

        _userInterface.PrintHeading("Demonstrate Creation of Entities");
        var newAccountId = DemonstrateAccountCreation();
        var newContactId = DemonstrateContactCreation(newAccountId);
        var newIncidentId = DemonstrateIncidentCreation(newAccountId, newContactId);

        _userInterface.PrintHeading("Demonstrate Retrieval of an Incident");
        var newIncident = DemonstrateFetchingAnIncident(newIncidentId);

        _userInterface.PrintHeading("Demonstrate Update of an Incident");
        DemonstrateUpdatingAnIncident(newIncident);

        _userInterface.PrintHeading("Demonstrate Deletion of Entities");
        DemonstrateDeletionOfEntities(newAccountId, newContactId, newIncidentId);

        _userInterface.PrintHeading("Validate Deletion of Incident");
        ValidateDeletionOfIncident();

    }


    //Demonstrate the creation of a demo account
    private Guid DemonstrateAccountCreation()
    {
        _userInterface.PrintMessage(
            $"Creating demonstration account ({_demoAccountTemplate.Name})");

        return _organisationService.Create(_demoAccountTemplate);
    }

    //Demonstrate the creation of a demo contact. Associsates the contact with
    //the account entity referenced by the associatedAccountId
    private Guid DemonstrateContactCreation(Guid associatedAccountId)
    {
        var demoContactFullName =
            $"{_demoContactTemplate.FirstName} {_demoContactTemplate.LastName}";
        _userInterface.PrintMessage(
            $"Creating demonstration contact ({demoContactFullName})");

        _demoContactTemplate.ParentCustomerId =
            new EntityReference("account", associatedAccountId);

        return _organisationService.Create(_demoContactTemplate);
    }

    //Demonstrate the creation of a demo incident. Associates the incident with 
    //the account and contact entities referenced by associatedAccountId and 
    //associated contact Id respectively
    private Guid DemonstrateIncidentCreation(
        Guid associatedAccountId,
        Guid associatedContactId)
    {
        _userInterface.PrintMessage(
            $"Creating demonstration case ({_demoIncidentTemplate.Title})");

        _demoIncidentTemplate.CustomerId =
            new EntityReference(Account.EntityLogicalName, associatedAccountId);
        _demoIncidentTemplate.PrimaryContactId =
            new EntityReference(Contact.EntityLogicalName, associatedContactId);

        return _organisationService.Create(_demoIncidentTemplate);
    }



    //Demonstrate fetching an incident. Gets the incident and the prints it 
    //using the IUserInterface dependency
    private Incident DemonstrateFetchingAnIncident(Guid incidentToFetchId)
    {
        _userInterface.PrintMessage("Fetching newly created incident...");

        var incident = GetIncidentById(incidentToFetchId);
        _userInterface.PrintIncident(incident);

        return incident;
    }

    //Demonstrate updating an incident. Updates the incident by updating its
    //service stage. Refetches the incident and then prints the result using
    //the IUserInterface dependency
    private void DemonstrateUpdatingAnIncident(Incident incidentToUpdate)
    {
        _userInterface.PrintMessage("Updating service stage...");

        var maxServiceStage = servicestage.Resolve;
        if (incidentToUpdate.ServiceStage != maxServiceStage)
        {
            incidentToUpdate.ServiceStage++;
        }
        else
        {
            incidentToUpdate.ServiceStage = servicestage.Identify;
        }

        _organisationService.Update(incidentToUpdate);

        var updatedIncident = GetIncidentById(incidentToUpdate.Id);
        _userInterface.PrintIncident(updatedIncident);

    }

    //Demonstrates the deletion of entities by removing all demo entities 
    //created in the demonstration so far
    private void DemonstrateDeletionOfEntities(
        Guid accountIdToDelete,
        Guid contactIdToDelete,
        Guid incidentIdToDelete)
    {
        _organisationService.Delete(Incident.EntityLogicalName, incidentIdToDelete);
        _userInterface.PrintMessage("Demo incident deleted");

        _organisationService.Delete(Contact.EntityLogicalName, contactIdToDelete);
        _userInterface.PrintMessage("Demo contact deleted");

        _organisationService.Delete(Account.EntityLogicalName, accountIdToDelete);
        _userInterface.PrintMessage("Demo account deleted");

    }

    //Fetches all active incidents and prints their titles to the console to
    //demonstrate that the demo incident is no longer in the DB.
    private void ValidateDeletionOfIncident()
    {
        _userInterface.PrintMessage("Fetching all active incidents...");

        var allActiveIncidentsFilter = new ConditionExpression(
                "statecode",
                ConditionOperator.Equal,
                (int)incident_statecode.Active
        );

        var allActiveIncidents = _organisationService.GetAll<Incident>(
            Incident.EntityLogicalName,
            new ColumnSet("title"),
            [allActiveIncidentsFilter]
        );

        _userInterface.PrintMessage("Current active incident titles:");
        _userInterface.PrintMessage("");

        foreach (var incident in allActiveIncidents)
        {
            Console.WriteLine($"- {incident.Title}");
        }
    }


    //Helper method to get an incident by id. Specifies the column set to 
    //reduce the size of the response payload. Note, statuscode is needed with
    //servicestage to allow for the service stage to be updated.
    private Incident GetIncidentById(Guid incidentToFetchId)
    {
        var columnSet = new ColumnSet(
            "title",
            "description",
            "customerid",
            "primarycontactid",
            "servicestage",
            "statuscode",
            "statecode",
            "casetypecode"
        );

        var incident = _organisationService.GetById<Incident>(
            Incident.EntityLogicalName, incidentToFetchId, columnSet);
        return incident;
    }
}
