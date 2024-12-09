using System.Text;
using CityPowerAndLight.Model;
using CityPowerAndLight.Utilities;
using CityPowerAndLight.View;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CityPowerAndLight.App;


/// <summary>
/// Provides methods to demonstrate CRUD operations on the contact table
/// </summary>
/// <param name="organisationService">The organization service instance.</param>
/// <param name="userInterface">The user interface instance.</param>
/// <param name="demoContactTemplate">The demo contact template.</param>
internal class ContactTableExploration(
    IOrganizationService organisationService,
    IUserInterface userInterface,
    Contact demoContactTemplate
    ) : IContactTableExploration
{
    private readonly IOrganizationService _organisationService
        = organisationService;
    private readonly IUserInterface _userInterface = userInterface;
    private readonly Contact _demoContactTemplate = demoContactTemplate;


    /// <summary>
    /// Demonstrates the creation of a contact.
    /// </summary>
    /// <param name="associatedAccountId">The ID of the associated account.
    /// </param>
    /// <returns>The ID of the newly created contact.</returns>
    public async Task<Guid> DemonstrateContactCreation(Guid associatedAccountId)
    {
        _userInterface.PrintMessage(
            $"Creating demonstration contact (" +
            $"{_demoContactTemplate.FirstName} {_demoContactTemplate.LastName})");

        _demoContactTemplate.ParentCustomerId =
            new EntityReference("account", associatedAccountId);

        var newContactId = await Task.Run(() =>
            _organisationService.Create(_demoContactTemplate));

        return newContactId;
    }


    /// <summary>
    /// Demonstrates fetching a contact.
    /// </summary>
    /// <param name="contactToFetchId">The ID of the contact to fetch.</param>
    /// <returns>The fetched contact.</returns>
    public async Task<Contact> DemonstrateFetchingAContact(Guid contactToFetchId)
    {
        _userInterface.PrintMessage("Fetching demo contact...");
        return await GetContactById(contactToFetchId);
    }


    /// <summary>
    /// Demonstrates updating a contact by updating the contact's first name.
    /// </summary>
    /// <param name="contactToUpdate">The contact to update.</param>
    /// <param name="updatedFirstName">The new first name for the contact.
    /// </param>
    public async Task DemonstrateUpdatingAContact(
        Contact contactToUpdate, string updatedFirstName)
    {
        _userInterface.PrintMessage("Updating contact first name...");
        contactToUpdate.FirstName = updatedFirstName;

        await Task.Run(() =>
            _organisationService.Update(contactToUpdate));
    }


    /// <summary>
    /// Demonstrates deleting a contact.
    /// </summary>
    /// <param name="contactToDeleteId">The ID of the contact to delete.</param>
    public async Task DemonstrateDeletingAContact(Guid contactToDeleteId)
    {
        await Task.Run(() => _organisationService.Delete(
            Contact.EntityLogicalName, contactToDeleteId));

        _userInterface.PrintMessage("Demo contact deleted");
    }

    /// <summary>
    /// Demonstrates fetching all active contacts.
    /// </summary>
    /// <returns>An enumerable of active contacts.</returns>
    public Task<IEnumerable<Contact>> DemonstrateFetchingAllActiveContacts()
    {
        _userInterface.PrintMessage("Fetching all active contacts...");

        var allActiveContactsFilter = new ConditionExpression(
                "statecode",
                ConditionOperator.Equal,
                (int)contact_statecode.Active
        );

        return _organisationService.GetAll<Contact>(
            Contact.EntityLogicalName,
            new ColumnSet("fullname"), [allActiveContactsFilter]
        );
    }


    /// <summary>
    /// Displays a single contact.
    /// </summary>
    /// <param name="contactToDisplay">The contact to display.</param>
    public void DisplayContact(Contact contactToDisplay)
    {
        var contactDisplayString = GetContactDisplayString(contactToDisplay);
        _userInterface.PrintMessage("Printing contact:");
        _userInterface.PrintMessage(contactDisplayString);
    }


    /// <summary>
    /// Displays a list of contacts.
    /// </summary>
    /// <param name="contactsToDisplay">The contacts to display.</param>
    public void DisplayContacts(IEnumerable<Contact> contactsToDisplay)
    {
        _userInterface.PrintMessage("Printing contacts:");
        _userInterface.PrintSpacer();
        _userInterface.PrintEntityList(
            contactsToDisplay, contact => contact.FullName);
    }


    //Helper method to get a contact by id. Specifies the column set to 
    //reduce the size of the response payload.
    private Task<Contact> GetContactById(Guid contactToFetchId)
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

    // Private helper method to get a display string for a contact
    private static string GetContactDisplayString(Contact contact)
    {
        var contactDisplayStringBuilder = new StringBuilder();
        contactDisplayStringBuilder.AppendLine(
           $"Full Name: {contact.FullName}");
        contactDisplayStringBuilder.AppendLine(
            $"Email: {contact.EMailAddress1}");
        contactDisplayStringBuilder.AppendLine(
            $"Company Name: {contact.ParentCustomerId?.Name ?? "n/a"}");
        contactDisplayStringBuilder.AppendLine(
            $"Business Phone: {contact.Telephone1}");
        return contactDisplayStringBuilder.ToString();
    }
}