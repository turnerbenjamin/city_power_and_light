using CityPowerAndLight.Model;

namespace CityPowerAndLight.App;


/// <summary>
/// Interface for demonstrating various contact table operations.
/// </summary>
internal interface IContactTableExploration
{
    /// <summary>
    /// Demonstrates the creation of a contact.
    /// </summary>
    /// <param name="associatedAccountId">The ID of the associated account.
    /// </param>
    /// <returns>The ID of the created contact.</returns>
    public Task<Guid> DemonstrateContactCreation(Guid associatedAccountId);


    /// <summary>
    /// Demonstrates fetching a contact by ID.
    /// </summary>
    /// <param name="contactToFetchId">The ID of the contact to fetch.</param>
    /// <returns>The fetched contact.</returns>
    public Task<Contact> DemonstrateFetchingAContact(Guid contactToFetchId);


    /// <summary>
    /// Demonstrates updating a contact's first name.
    /// </summary>
    /// <param name="contactToUpdate">The contact to update.</param>
    /// <param name="updatedFirstName">The updated first name.</param>
    public Task DemonstrateUpdatingAContact(
        Contact contactToUpdate, string updatedFirstName);


    // <summary>
    /// Demonstrates deleting a contact by ID.
    /// </summary>
    /// <param name="contactToDeleteId">The ID of the contact to delete.</param>
    public Task DemonstrateDeletingAContact(Guid contactToDeleteId);


    // <summary>
    /// Demonstrates fetching all active contacts.
    /// </summary>
    /// <returns>A collection of active contacts.</returns>
    public Task<IEnumerable<Contact>> DemonstrateFetchingAllActiveContacts();


    /// <summary>
    /// Displays a contact.
    /// </summary>
    /// <param name="contactToDisplay">The contact to display.</param>
    public void DisplayContact(Contact contactToDisplay);


    /// <summary>
    /// Displays a collection of contacts.
    /// </summary>
    /// <param name="contactsToDisplay">The contacts to display.</param>
    public void DisplayContacts(IEnumerable<Contact> contactsToDisplay);
}
