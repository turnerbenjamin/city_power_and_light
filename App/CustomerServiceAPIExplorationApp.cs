using CityPowerAndLight.Config;
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
    Incident demoIncidentTemplate,
    DemoValuesConfig demoValues
    )
{
    private readonly IUserInterface _userInterface = userInterface;
    private readonly IOrganizationService _organisationService = organizationService;
    private readonly Account _demoAccountTemplate = demoAccountTemplate;
    private readonly Contact _demoContactTemplate = demoContactTemplate;
    private readonly Incident _demoIncidentTemplate = demoIncidentTemplate;
    private readonly DemoValuesConfig _demoValues = demoValues;


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

        _userInterface.PrintHeading("Demonstrate Retrieval of entities");
        var newAccount = DemonstrateFetchingAnAccount(newAccountId);
        var newContact = DemonstrateFetchingAContact(newContactId);
        var newIncident = DemonstrateFetchingAnIncident(newIncidentId);

        _userInterface.PrintHeading("Demonstrate Update of Entities");
        DemonstrateUpdatingAnAccount(newAccount, newContactId);
        DemonstrateUpdatingAContact(newContact);
        DemonstrateUpdatingAnIncident(newIncident);

        _userInterface.PrintHeading("Demonstrate Deletion of Entities");
        DemonstrateDeletionOfEntities(newAccountId, newContactId, newIncidentId);

        _userInterface.PrintHeading("Validate Deletion of Entitites");
        ValidateDeletionOfAnEntity<Account>(
            Account.EntityLogicalName, "name", account => account.Name);
        ValidateDeletionOfAnEntity<Contact>(
            Contact.EntityLogicalName, "fullname", contact => contact.FullName);
        ValidateDeletionOfAnEntity<Incident>(
            Incident.EntityLogicalName, "title", incident => incident.Title);
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
        _userInterface.PrintMessage(
            $"Creating demonstration contact (" +
            $"{_demoContactTemplate.FirstName} {_demoContactTemplate.LastName})");

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
            $"Creating demonstration incident ({_demoIncidentTemplate.Title})");

        _demoIncidentTemplate.CustomerId =
            new EntityReference(Account.EntityLogicalName, associatedAccountId);
        _demoIncidentTemplate.PrimaryContactId =
            new EntityReference(Contact.EntityLogicalName, associatedContactId);

        return _organisationService.Create(_demoIncidentTemplate);
    }

    //Demonstrate fetching an account. Gets the account and the prints it 
    //using the IUserInterface dependency
    private Account DemonstrateFetchingAnAccount(Guid accountToFetchId)
    {
        _userInterface.PrintMessage("Fetching newly created account...");

        var account = GetAccountById(accountToFetchId);
        _userInterface.PrintEntity(account);

        return account;
    }

    //Demonstrate fetching a contact. Gets the contact and the prints it 
    //using the IUserInterface dependency
    private Contact DemonstrateFetchingAContact(Guid contactToFetchId)
    {
        _userInterface.PrintMessage("Fetching newly created contact...");

        var contact = GetContactById(contactToFetchId);
        _userInterface.PrintEntity(contact);

        return contact;
    }


    //Demonstrate fetching an incident. Gets the incident and the prints it 
    //using the IUserInterface dependency
    private Incident DemonstrateFetchingAnIncident(Guid incidentToFetchId)
    {
        _userInterface.PrintMessage("Fetching newly created incident...");

        var incident = GetIncidentById(incidentToFetchId);

        _userInterface.PrintEntity(incident);

        return incident;
    }

    //Demonstrate updating an account. Updates the account by updating its
    //primary contact value. Refetches the account and then prints the result 
    //using the IUserInterface dependency
    private void DemonstrateUpdatingAnAccount(Account accountToUpdate, Guid primaryContactId)
    {
        _userInterface.PrintMessage("Updating account primary contact...");
        accountToUpdate.PrimaryContactId =
            new EntityReference(Contact.EntityLogicalName, primaryContactId);

        _organisationService.Update(accountToUpdate);

        var updatedAccount = GetAccountById(accountToUpdate.Id);
        _userInterface.PrintEntity(updatedAccount);
    }

    //Demonstrate updating an contact. Updates the contact by updating its
    //primary first name value. Refetches the contact and then prints the result 
    //using the IUserInterface dependency
    private void DemonstrateUpdatingAContact(Contact contactToUpdate)
    {
        _userInterface.PrintMessage("Updating contact first name...");
        contactToUpdate.FirstName = _demoValues.ContactUpdatedFirstName;

        _organisationService.Update(contactToUpdate);

        var updatedContact = GetContactById(contactToUpdate.Id);
        _userInterface.PrintEntity(updatedContact);
    }

    //Demonstrate updating an incident. Updates the incident by updating its
    //service stage. Refetches the incident and then prints the result using
    //the IUserInterface dependency
    private void DemonstrateUpdatingAnIncident(Incident incidentToUpdate)
    {
        _userInterface.PrintMessage("Updating incident service stage...");
        incidentToUpdate.ServiceStage = _demoValues.IncidentUpdatedServiceStage;
        _organisationService.Update(incidentToUpdate);

        var updatedIncident = GetIncidentById(incidentToUpdate.Id);
        _userInterface.PrintEntity(updatedIncident);
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

    //Validate deletion of an entity of type T by printing all active entities
    //of type T using the IUserInterface dependency
    private void ValidateDeletionOfAnEntity<T>(
        string entityLogicalName,
        string fieldToPrint,
        Func<T, string> getFieldToPrintValue)
        where T : Entity
    {
        _userInterface.PrintMessage($"Fetching all {entityLogicalName}s...");

        var allActiveEntitiesFilter = new ConditionExpression(
                "statecode",
                ConditionOperator.Equal,
                0
        );

        var allActiveEntites = _organisationService.GetAll<T>(
            entityLogicalName,
            new ColumnSet(fieldToPrint),
            [allActiveEntitiesFilter]
        );

        _userInterface.PrintMessage($"Current active {entityLogicalName}s:");
        _userInterface.PrintMessage("");
        _userInterface.PrintEntityList(allActiveEntites, getFieldToPrintValue);

    }

    //Helper method to get an account by id. Specifies the column set to 
    //reduce the size of the response payload.
    private Account GetAccountById(Guid accountToFetchId)
    {
        var columnSet = new ColumnSet(
            "name",
            "telephone1",
            "address1_city",
            "primarycontactid"
        );

        return _organisationService.GetById<Account>(
            Account.EntityLogicalName, accountToFetchId, columnSet);
    }

    //Helper method to get a contact by id. Specifies the column set to 
    //reduce the size of the response payload.
    private Contact GetContactById(Guid contactToFetchId)
    {
        var columnSet = new ColumnSet(
            "fullname",
            "emailaddress1",
            "parentcustomerid",
            "telephone1"
        );

        return _organisationService.GetById<Contact>(
            Contact.EntityLogicalName, contactToFetchId, columnSet);
    }


    //Helper method to get an incident by id. Specifies the column set to 
    //reduce the size of the response payload. Note, statuscode is needed with
    //servicestage to allow for the service stage to be updated.
    private Incident GetIncidentById(Guid incidentToFetchId)
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
}
