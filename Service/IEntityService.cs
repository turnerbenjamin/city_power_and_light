namespace CityPowerAndLight.App;

internal interface IEntityService<T>
{
    Guid Create(T entity);
    T GetById(Guid entityId);
    void Update(T updatedEntity);
    void DeleteById(Guid entityId);
}