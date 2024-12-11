using CityPowerAndLight.Model;

namespace CityPowerAndLight.App;


/// <summary>
/// Interface for demonstrating various account table operations.
/// </summary>
internal interface IAccountTableExploration
{
    /// <summary>
    /// Demonstrates the creation of an account.
    /// </summary>
    /// <returns>The GUID of the created account.</returns>
    public Task<Guid> DemonstrateAccountCreation();


    /// <summary>
    /// Demonstrates fetching an account by its ID.
    /// </summary>
    /// <param name="accountToFetchId">The ID of the account to fetch.</param>
    /// <returns>The fetched account.</returns>
    public Task<Account> DemonstrateFetchingAnAccount(Guid accountToFetchId);


    /// <summary>
    /// Demonstrates updating an account.
    /// </summary>
    /// <param name="accountToUpdate">The account to update.</param>
    /// <param name="primaryContactId">The primary contact ID associated with 
    /// the account.</param>
    public Task DemonstrateUpdatingAnAccount(
            Account accountToUpdate, Guid primaryContactId);


    /// <summary>
    /// Demonstrates deleting an account by its ID.
    /// </summary>
    /// <param name="accountToDeleteId">The ID of the account to delete.</param>
    public Task DemonstrateDeletingAnAccount(Guid accountToDeleteId);


    /// <summary>
    /// Demonstrates fetching all active accounts.
    /// </summary>
    /// <returns>A collection of active accounts.</returns>
    public Task<IEnumerable<Account>> DemonstrateFetchingAllActiveAccounts();


    /// <summary>
    /// Displays the details of a single account.
    /// </summary>
    /// <param name="accountToDisplay">The account to display.</param>
    public void DisplayAccount(Account accountToDisplay);


    /// <summary>
    /// Displays the details of multiple accounts.
    /// </summary>
    /// <param name="accountsToDisplay">The accounts to display.</param>
    public void DisplayAccounts(IEnumerable<Account> accountsToDisplay);


}