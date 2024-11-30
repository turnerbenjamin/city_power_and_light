using CityPowerAndLight.Model;
using CityPowerAndLight.Service;
using Microsoft.Xrm.Sdk;

namespace CityPowerAndLight.Controller;

internal class ContactController(EntityService<Contact> contactService)
{
    private readonly EntityService<Contact> _contactService = contactService;

    internal void ReadAll()
    {
        var contacts = _contactService.GetAll();
        foreach (Contact contact in contacts)
        {
            Console.WriteLine(contact.FullName);
        }
    }

    internal Guid Create()
    {
        Contact newContact = new()
        {
            FirstName = "Ben",
            LastName = "Turner",
        };

        return _contactService.Create(newContact);
    }

    internal void Delete(Guid contactId)
    {
        _contactService.Delete(contactId);
    }

}