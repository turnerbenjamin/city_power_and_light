using CityPowerAndLight.Config;
using CityPowerAndLight.Model;
using CityPowerAndLight.View;

namespace CityPowerAndLight.App;


/// <summary>
/// Manages the execution flow of the Customer Service API Exploration 
/// application.
/// </summary>
/// <param name="userInterface">The user interface for displaying messages.
/// </param>
/// <param name="accountTableExploration">The account table exploration 
/// instance.</param>
/// <param name="contactTableExploration">The contact table exploration 
/// instance.</param>
/// <param name="incidentTableExploration">The incident table exploration 
/// instance.</param>
/// <param name="demoValues">The configuration values for the demo.</param>
internal class CustomerServiceAPIExplorationApp(
    IUserInterface userInterface,
    IAccountTableExploration accountTableExploration,
    IContactTableExploration contactTableExploration,
    IIncidentTableExploration incidentTableExploration,
    DemoValuesConfig demoValues
    )
{
    private readonly IUserInterface _userInterface = userInterface;
    private readonly IAccountTableExploration _accountTableExploration
        = accountTableExploration;
    private readonly IContactTableExploration _contactTableExploration
        = contactTableExploration;
    private readonly IIncidentTableExploration _incidentTableExploration
        = incidentTableExploration;
    private readonly DemoValuesConfig _demoValues = demoValues;


    /// <summary>
    /// Runs the Customer Service API Exploration application.
    /// </summary>
    public async Task Run()
    {
        _userInterface.PrintTitle("Running Customer Service API Exploration");

        var demoEntityIds = await DemonstrateEntityCreation();

        var demoEntities = DemonstrateEntityRetrieval(demoEntityIds);

        DemonstateEntityUpdate(demoEntities, demoEntityIds);
        ValidateEntityUpdate(demoEntityIds);

        await DemonstrateEntityDeletion(demoEntityIds);
        ValidateDeletionOfEntities();
    }


    //Demonstrate the creation of an account, contact and incident. Returns the
    //ids of the newly created demo entities. The order of creation matters for
    //this demonstration so entities are created sequentially.
    private async Task<DemoEntityIds> DemonstrateEntityCreation()
    {
        _userInterface.PrintHeading("Demonstrate Creation of Entities");

        var newAccountId = await _accountTableExploration
            .DemonstrateAccountCreation();
        var newContactId = await _contactTableExploration
            .DemonstrateContactCreation(newAccountId);
        var newIncidentId = await _incidentTableExploration
            .DemonstrateIncidentCreation(newAccountId, newContactId);

        return new DemoEntityIds(newAccountId, newContactId, newIncidentId);
    }


    //Demonstrate fetching an account, contact and incident. Fetches all demo
    //entities and then prints them to the console. Returns the demo entities 
    //retrieved.
    private DemoEntities DemonstrateEntityRetrieval(DemoEntityIds demoEntityIds)
    {
        _userInterface.PrintHeading("Demonstrate Retrieval of entities");
        return FetchAndDisplayDemoEntities(demoEntityIds);
    }


    //Perform an update operation on all three demo entities.
    private void DemonstateEntityUpdate(
        DemoEntities demoEntities, DemoEntityIds demoEntityIds)
    {
        _userInterface.PrintHeading("Demonstrate Update of Entities");

        var updateAccountTask = _accountTableExploration
        .DemonstrateUpdatingAnAccount(
            demoEntities.Account, demoEntityIds.ContactId);

        var updateContactTask = _contactTableExploration
        .DemonstrateUpdatingAContact(
            demoEntities.Contact, _demoValues.ContactUpdatedFirstName);

        var updateIncidentTask = _incidentTableExploration
        .DemonstrateUpdatingAnIncident(
            demoEntities.Incident, _demoValues.IncidentUpdatedServiceStage);

        Task.WaitAll(updateAccountTask, updateContactTask, updateIncidentTask);
    }


    //Refetch and print the demo entities to validate that the update was 
    //successful.
    private void ValidateEntityUpdate(DemoEntityIds demoEntityIds)
    {
        _userInterface.PrintHeading("Validate Entity Update");
        FetchAndDisplayDemoEntities(demoEntityIds);
    }


    //Demonstrates the deletion of entities by removing all demo entities 
    //created in the demonstration so far. Deletion of one entity can cascade
    //to other entities automatically. To demonstrate delete on all three
    //tables, deletion is performed synchronously, with account deleted last to 
    //avoid a cascade.
    private async Task DemonstrateEntityDeletion(DemoEntityIds demoEntityIds)
    {
        _userInterface.PrintHeading("Demonstrate Deletion of Entities");

        await _incidentTableExploration.DemonstrateDeletingAnIncident(
            demoEntityIds.IncidentId
        );

        await _contactTableExploration.DemonstrateDeletingAContact(
            demoEntityIds.ContactId);

        await _accountTableExploration.DemonstrateDeletingAnAccount(
            demoEntityIds.AccountId);
    }


    //Fetches all active entities from the account, contact and incident tables 
    //asynchonously and prints the results to the console.
    private void ValidateDeletionOfEntities()
    {
        _userInterface.PrintHeading("Validate Deletion of Entities");

        var getAllAccountsTask =
            _accountTableExploration.DemonstrateFetchingAllActiveAccounts();

        var getAllContactsTask =
            _contactTableExploration.DemonstrateFetchingAllActiveContacts();

        var getAllIncidentsTask =
            _incidentTableExploration.DemonstrateFetchingAllActiveIncidents();

        Task.WaitAll(
            getAllAccountsTask, getAllContactsTask, getAllIncidentsTask);

        DisplayEntityLists(
            getAllAccountsTask.Result,
            getAllContactsTask.Result,
            getAllIncidentsTask.Result
        );
    }


    //Display entity lists for account, contact and incidents.
    private void DisplayEntityLists(
        IEnumerable<Account> accounts,
        IEnumerable<Contact> contacts,
        IEnumerable<Incident> incidents)
    {
        _userInterface.PrintSpacer();
        _accountTableExploration.DisplayAccounts(accounts);
        _userInterface.PrintSpacer();
        _contactTableExploration.DisplayContacts(contacts);
        _userInterface.PrintSpacer();
        _incidentTableExploration.DisplayIncidents(incidents);
    }


    //Fetch the demo entities and display to the user.
    private DemoEntities FetchAndDisplayDemoEntities(
        DemoEntityIds demoEntityIds)
    {
        var demoEntities = RetrieveDemoEntities(demoEntityIds);
        _userInterface.PrintSpacer();

        _accountTableExploration.DisplayAccount(demoEntities.Account);
        _contactTableExploration.DisplayContact(demoEntities.Contact);
        _incidentTableExploration.DisplayIncident(demoEntities.Incident);

        return demoEntities;
    }


    //Retrieve demo entities concurrently and return the retrieved entities
    //once all retrieval tasks are complete.
    private DemoEntities RetrieveDemoEntities(DemoEntityIds demoEntityIds)
    {
        var fetchAccountTask = _accountTableExploration
            .DemonstrateFetchingAnAccount(demoEntityIds.AccountId);
        var fetchContactTask = _contactTableExploration
            .DemonstrateFetchingAContact(demoEntityIds.ContactId);
        var fetchIncidentTask = _incidentTableExploration
            .DemonstrateFetchingAnIncident(demoEntityIds.IncidentId);

        Task.WaitAll(fetchAccountTask, fetchContactTask, fetchIncidentTask);

        return new DemoEntities(
            fetchAccountTask.Result,
            fetchContactTask.Result,
            fetchIncidentTask.Result
        );
    }


    //Simple structure for storing demo entity ids.
    private record DemoEntityIds(
        Guid AccountId, Guid ContactId, Guid IncidentId);


    //Simple structure for storing demo entities.
    private record DemoEntities(
        Account Account, Contact Contact, Incident Incident);
}