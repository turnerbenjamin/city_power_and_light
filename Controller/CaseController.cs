using CityPowerAndLight.Model;
using CityPowerAndLight.Service;
using Microsoft.Xrm.Sdk;

namespace CityPowerAndLight.Controller;

internal class CaseController(EntityService<Incident> caseController)
{
    private readonly EntityService<Incident> _caseService = caseController;

    internal void ReadAll()
    {
        var incidents = _caseService.GetAll();
        foreach (Incident incident in incidents)
        {
            Console.WriteLine(incident.Title);
            Console.WriteLine(incident.CustomerId.Name);
        }
    }

    internal Guid Create(Guid customerId)
    {

        Incident newIncident = new()
        {
            Title = "Some awfullness",
            Description = "Oh no there has been some awfullness",
            CustomerId = new EntityReference(Contact.EntityLogicalName, customerId),
            StatusCode = incident_statuscode.InProgress,
            PriorityCode = incident_prioritycode.High,
        };
        return _caseService.Create(newIncident);
    }

    internal void Delete(Guid caseId)

    {
        _caseService.Delete(caseId);
    }

}