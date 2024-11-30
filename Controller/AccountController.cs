using CityPowerAndLight.Model;
using CityPowerAndLight.Service;
using Microsoft.Xrm.Sdk;

namespace CityPowerAndLight.Controller;

internal class AccountController(EntityService<Account> accountService)
{
    private readonly EntityService<Account> _accountService = accountService;

    internal void ReadAll()
    {
        var accounts = _accountService.GetAll();
        foreach (Account account in accounts)
        {
            Console.WriteLine(account.Name);
        }
    }

    internal Guid Create()
    {
        Account newAccount = new()
        {
            Name = "My Test Account",
            CreditOnHold = false,
            LastOnHoldTime = new DateTime(2023, 1, 1),
            Address1_Latitude = 47.642311,
            Address1_Longitude = -122.136841,
            NumberOfEmployees = 500,
            Revenue = new Money(new decimal(5000000.00)),
            AccountCategoryCode = account_accountcategorycode.PreferredCustomer,
            StatusCode = account_statuscode.Active,
        };
        return _accountService.Create(newAccount);
    }

    internal void Delete(Guid accountId)
    {
        _accountService.Delete(accountId);
    }

}