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
    public static T GetById<T>(this IOrganizationService organizationService, string entityLogicalName, Guid entityId, ColumnSet columnSet)
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
}