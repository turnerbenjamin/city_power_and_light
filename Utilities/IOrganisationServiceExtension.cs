using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CityPowerAndLight.Utilities;

/// <summary>
/// The <c>IOrganisationServiceExtension</c> class provides extension methods 
/// for the <see cref="IOrganizationService"/> interface.
/// </summary>
public static class IOrganisationServiceExtension
{
    /// <summary>
    /// Retrieves an entity by its ID and casts it to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to which the entity should be cast.
    /// </typeparam>
    /// <param name="organizationService">The organization service instance.
    /// </param>
    /// <param name="entityLogicalName">The logical name of the entity.</param>
    /// <param name="entityId">The ID of the entity.</param>
    /// <param name="columnSet">The columns to retrieve.</param>
    /// <returns>The entity cast to the specified type.</returns>
    /// <exception cref="InvalidCastException">Thrown when the entity cannot be 
    /// cast to the specified type.</exception>
    public static T GetById<T>(
    this IOrganizationService organizationService,
    string entityLogicalName,
    Guid entityId,
    ColumnSet columnSet)
    where T : Entity
    {
        var entity = organizationService.Retrieve(
            entityLogicalName, entityId, columnSet);

        if (entity is not T castEntity)
        {
            throw new InvalidCastException(
                $"The entity with id {entityId} cannot be cast to " +
                $"{typeof(T).Name}");
        }
        return castEntity;
    }

    /// <summary>
    /// Retrieves all entities of the specified type that match the given 
    /// conditions.
    /// </summary>
    /// <typeparam name="T">The type to which the entities should be cast.
    /// </typeparam>
    /// <param name="organizationService">The organization service instance.
    /// </param>
    /// <param name="entityLogicalName">The logical name of the entity.</param>
    /// <param name="columnSet">The columns to retrieve.</param>
    /// <param name="conditions">The conditions to filter the entities. If empty
    /// all entities will be retrieved</param>
    /// <returns>An IEnumerable of entities cast to the specified type.</returns>
    /// <exception cref="InvalidCastException">Thrown when an entity cannot be 
    /// cast to the specified type.</exception>
    public static IEnumerable<T> GetAll<T>(
    this IOrganizationService organizationService,
    string entityLogicalName,
    ColumnSet columnSet,
    IEnumerable<ConditionExpression> conditions
    )
    where T : Entity
    {
        PagingInfo pagingInfo = new() { Count = 50, PageNumber = 1 };
        QueryExpression query = BuildGetAllQuery(
            entityLogicalName, columnSet, conditions, pagingInfo);

        List<T> entities = [];
        while (true)
        {
            var result = organizationService.RetrieveMultiple(query);
            entities.AddRange(result.Entities.Select(e => e.ToEntity<T>()));

            if (!result.MoreRecords) break;

            query.PageInfo.PageNumber++;
            query.PageInfo.PagingCookie = result.PagingCookie;
        }
        return entities;
    }

    //Builds a query to use with RetrieveMultiple
    private static QueryExpression BuildGetAllQuery(
        string entityLogicalName,
        ColumnSet columnSet,
        IEnumerable<ConditionExpression> conditions,
        PagingInfo pagingInfo
        )
    {
        QueryExpression query = new(entityLogicalName)
        {
            ColumnSet = columnSet,
            PageInfo = pagingInfo,
        };

        foreach (ConditionExpression condition in conditions)
        {
            query.Criteria.AddCondition(condition);
        }
        return query;
    }
}