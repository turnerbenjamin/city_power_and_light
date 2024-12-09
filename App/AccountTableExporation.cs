using System.Text;
using CityPowerAndLight.Model;
using CityPowerAndLight.Utilities;
using CityPowerAndLight.View;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CityPowerAndLight.App;


/// <summary>
/// Provides methods to demonstrate CRUD operations on the Account table.
/// </summary>
/// <param name="organisationService">The organization service instance.</param>
/// <param name="userInterface">The user interface instance.</param>
/// <param name="demoAccountTemplate">The demo account template.</param>
internal class AccountTableExploration(
    IOrganizationService organisationService,
    IUserInterface userInterface,
    Account demoAccountTemplate
    ) : IAccountTableExploration
{
    private readonly IOrganizationService _organisationService
        = organisationService;
    private readonly IUserInterface _userInterface = userInterface;
    private readonly Account _demoAccountTemplate = demoAccountTemplate;


    /// <summary>
    /// Demonstrates the creation of an account.
    /// </summary>
    /// <returns>The ID of the newly created account.</returns>
    public async Task<Guid> DemonstrateAccountCreation()
    {
        _userInterface.PrintMessage(
            $"Creating demonstration account ({_demoAccountTemplate.Name})");

        var newAccountId = await Task.Run(() =>
            _organisationService.Create(_demoAccountTemplate));

        return newAccountId;
    }


    /// <summary>
    /// Demonstrates fetching an account by its ID.
    /// </summary>
    /// <param name="accountToFetchId">The ID of the account to fetch.</param>
    /// <returns>The fetched account.</returns>
    public async Task<Account> DemonstrateFetchingAnAccount(
        Guid accountToFetchId)
    {
        _userInterface.PrintMessage("Fetching demo account...");
        return await GetAccountById(accountToFetchId);
    }


    /// <summary>
    /// Demonstrates updating an account by associating it with a primary 
    /// contact.
    /// </summary>
    /// <param name="accountToUpdate">The account to update.</param>
    /// <param name="primaryContactId">The ID of the primary contact to set.
    /// </param>
    public async Task DemonstrateUpdatingAnAccount(
        Account accountToUpdate, Guid primaryContactId)
    {
        _userInterface.PrintMessage("Updating account primary contact...");
        accountToUpdate.PrimaryContactId =
            new EntityReference(Contact.EntityLogicalName, primaryContactId);

        await Task.Run(() =>
            _organisationService.Update(accountToUpdate));
    }


    /// <summary>
    /// Demonstrates deleting an account by its ID.
    /// </summary>
    /// <param name="accountToDeleteId">The ID of the account to delete.</param>
    public async Task DemonstrateDeletingAnAccount(Guid accountToDeleteId)
    {
        await Task.Run(() => _organisationService.Delete(
            Account.EntityLogicalName, accountToDeleteId));

        _userInterface.PrintMessage("Demo account deleted");
    }


    /// <summary>
    /// Demonstrates fetching all active accounts.
    /// </summary>
    /// <returns>An IEnumerable of all active accounts.</returns>
    public Task<IEnumerable<Account>> DemonstrateFetchingAllActiveAccounts()
    {
        _userInterface.PrintMessage("Fetching all active accounts...");

        var allActiveAccountsFilter = new ConditionExpression(
                "statecode",
                ConditionOperator.Equal,
                (int)account_statecode.Active
        );

        return _organisationService.GetAll<Account>(
            Account.EntityLogicalName,
            new ColumnSet("name"), [allActiveAccountsFilter]
        );
    }


    /// <summary>
    /// Displays the details of an account.
    /// </summary>
    /// <param name="accountToDisplay">The account to display.</param>
    public void DisplayAccount(Account accountToDisplay)
    {
        var accountDisplayString = GetAccountDisplayString(accountToDisplay);
        _userInterface.PrintMessage("Printing account:");
        _userInterface.PrintMessage(accountDisplayString);
    }


    /// <summary>
    /// Displays the details of multiple accounts.
    /// </summary>
    /// <param name="accountsToDisplay">The list of accounts to display.</param>
    public void DisplayAccounts(IEnumerable<Account> accountsToDisplay)
    {
        _userInterface.PrintMessage("Printing accounts:");
        _userInterface.PrintSpacer();

        _userInterface.PrintEntityList(
            accountsToDisplay, account => account.Name);
    }


    //Helper method to get an account by id. Specifies the column set to 
    //reduce the size of the response payload. 
    private Task<Account> GetAccountById(Guid accountToFetchId)
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


    // Private helper method to get a display string for an account
    private static string GetAccountDisplayString(Account account)
    {
        var accountDisplayStringBuilder = new StringBuilder();
        accountDisplayStringBuilder.AppendLine(
            $"Name: {account.Name}");
        accountDisplayStringBuilder.AppendLine(
            $"Main Phone: {account.Telephone1}");
        accountDisplayStringBuilder.AppendLine(
            $"Address1 (City): {account.Address1_City}");
        accountDisplayStringBuilder.AppendLine(
            $"Primary Contact: {account.PrimaryContactId?.Name
                                ?? "null"}");
        return accountDisplayStringBuilder.ToString();
    }
}