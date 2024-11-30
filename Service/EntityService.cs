using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CityPowerAndLight.Service;

internal class EntityService<T>(IOrganizationService organisationService, string entityLogicalName)
    where T : Entity
{
    private readonly IOrganizationService _organisationService
        = organisationService;
    private readonly string _entityLogicalName = entityLogicalName;

    public Guid Create(T entity)
    {
        Guid earlyBoundAccountId = _organisationService.Create(entity);
        return earlyBoundAccountId;
    }

    public IEnumerable<T> GetAll()
    {
        QueryExpression query = new(_entityLogicalName)
        {
            ColumnSet = new ColumnSet(true),
            PageInfo = new PagingInfo() { Count = 5000, PageNumber = 1 }
        };

        List<T> allRecords = [];
        EntityCollection results;

        do
        {
            results = _organisationService.RetrieveMultiple(query);

            foreach (Entity record in results.Entities)
            {
                T account = (T)record;
                allRecords.Add(account);
            }
            query.PageInfo.PageNumber++;
        } while (results.MoreRecords);

        return allRecords;
    }

    public void Delete(Guid entityId)
    {
        _organisationService.Delete(_entityLogicalName, entityId);
    }
}