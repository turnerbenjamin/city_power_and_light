using System.Net;
using CityPowerAndLight.App;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CityPowerAndLight.Service;

internal class EntityService<T>(
    IOrganizationService organisationService,
    string entityLogicalName)
    : IEntityService<T>
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


    public T GetById(Guid entityId)
    {
        if (_organisationService.Retrieve(
            _entityLogicalName,
            entityId,
            new ColumnSet(true)) is not T entity)
        {
            throw new HttpRequestException(
                "Entity with id {entityId} was not found", null,
                HttpStatusCode.NotFound);
        }

        return entity;
    }

    public void Update(T updatedEntity)
    {
        _organisationService.Update(updatedEntity);
    }


    public void DeleteById(Guid entityId)
    {
        _organisationService.Delete(_entityLogicalName, entityId);
    }

}