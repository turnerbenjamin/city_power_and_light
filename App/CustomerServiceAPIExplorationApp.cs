using CityPowerAndLight.Model;
using Microsoft.Xrm.Sdk;

namespace CityPowerAndLight.App;

internal class CustomerServiceAPIExplorationApp(
    IEntityService<Account> accountService,
    IEntityService<Contact> contactService,
    IEntityService<Incident> incidentService,
    string entityIdentifierPrefix
    )
{

    private readonly IEntityService<Account> _accountService = accountService;
    private readonly IEntityService<Contact> _contactService = contactService;
    private readonly IEntityService<Incident> _incidentService = incidentService;
    private readonly string _entityIdentifierPrefix = entityIdentifierPrefix;

    public void Run()
    {
        string newLine = Environment.NewLine;

        Console.WriteLine($"{newLine}***** Running Customer Service API Exploration{newLine}");
        //Demonstrate creation of a case
        Console.WriteLine("Creating demonstration case/incident...");
        var newAccountId = CreateAccount();
        var newContactId = CreateContact(newAccountId);
        var newIncidentId = CreateIncident(newAccountId, newContactId);

        //Demonstrate reading a case
        Console.WriteLine($"Case created successfully:{newLine}");
        Incident newIncident = _incidentService.GetById(newIncidentId);
        PrintIncident(newIncident);

        //Demonstrate updating a case
        Console.WriteLine($"Updating case by moving it to the next stage:{newLine}");
        MoveIncidentToNextServiceStage(newIncident);

        Incident updatedIncident = _incidentService.GetById(newIncidentId);
        Console.WriteLine($"Case successfully updated");
        PrintIncident(updatedIncident);

        //Demonstrate deleting a case by cleaning-up demo entities
        Console.WriteLine($"Cleaning up entities created for demonstration purposes{newLine}");
        CleanUp(newAccountId, newContactId, newIncidentId);
        Console.WriteLine("Demo entities deleted successfully");
        Console.WriteLine();
    }


    private Guid CreateAccount()
    {
        Account newAccount = new()
        {
            Name = $"{_entityIdentifierPrefix}Relecloud",
            StatusCode = account_statuscode.Active,
        };
        return _accountService.Create(newAccount);
    }

    private Guid CreateContact(Guid associatedAccountId)
    {
        Contact newContact = new()
        {
            FirstName = $"{_entityIdentifierPrefix}Jane",
            LastName = "Smith",
            ParentCustomerId = new EntityReference("account", associatedAccountId)
        };

        return _contactService.Create(newContact);
    }

    private Guid CreateIncident(Guid associatedAccountId, Guid associatedContactId)
    {
        Incident newIncident = new()
        {
            Title = $"{_entityIdentifierPrefix}Defective Screen",
            Description = "Laptop display is too bright",
            CustomerId = new EntityReference(Account.EntityLogicalName, associatedAccountId),
            PrimaryContactId = new EntityReference(Contact.EntityLogicalName, associatedContactId),
            ServiceStage = servicestage.Identify,
            StatusCode = incident_statuscode.InProgress,
            CaseTypeCode = incident_casetypecode.Problem
        };
        return _incidentService.Create(newIncident);
    }

    private void MoveIncidentToNextServiceStage(Incident incidentToUpdate)
    {
        if (incidentToUpdate.ServiceStage == servicestage.Resolve)
        {
            throw new ArgumentException($"Incident with id {incidentToUpdate.Id} has already been resolved");
        }

        incidentToUpdate.ServiceStage++;
        _incidentService.Update(incidentToUpdate);

    }

    private void CleanUp(
        Guid accountIdToDelete,
        Guid contactIdToDelete,
        Guid incidentIdToDelete)
    {
        _incidentService.DeleteById(incidentIdToDelete);
        _contactService.DeleteById(contactIdToDelete);
        _accountService.DeleteById(accountIdToDelete);
    }

    private void PrintIncident(Incident incidentToPrint)
    {
        Console.WriteLine($"*****{incidentToPrint.Title}*****");
        Console.WriteLine();

        Console.WriteLine($"Description: {incidentToPrint.Description}");
        Console.WriteLine($"Account: {incidentToPrint.CustomerId.Name}");
        Console.WriteLine($"Contact: {incidentToPrint.PrimaryContactId.Name}");
        Console.WriteLine($"Stage: {incidentToPrint.ServiceStage}");
        Console.WriteLine($"Status: {incidentToPrint.StateCode}");
        Console.WriteLine($"Type: {incidentToPrint.CaseTypeCode}");

        Console.WriteLine();
        Console.WriteLine($"***********************************");
        Console.WriteLine();
    }
}