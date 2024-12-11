using CityPowerAndLight.Model;

namespace CityPowerAndLight.App;


/// <summary>
/// Interface for exploring incident table operations.
/// </summary>
internal interface IIncidentTableExploration
{
    /// <summary>
    /// Demonstrates the creation of an incident.
    /// </summary>
    /// <param name="associatedAccountId">The ID of the associated account.
    /// </param>
    /// <param name="associatedContactId">The ID of the associated contact.
    /// </param>
    /// <returns>The ID of the created incident.</returns> 
    public Task<Guid> DemonstrateIncidentCreation(
        Guid associatedAccountId, Guid associatedContactId);


    /// <summary>
    /// Demonstrates fetching an incident by ID.
    /// </summary>
    /// <param name="incidentToFetchId">The ID of the incident to fetch.</param>
    /// <returns>The fetched incident.</returns>
    public Task<Incident> DemonstrateFetchingAnIncident(Guid incidentToFetchId);


    /// <summary>
    /// Demonstrates updating an incident.
    /// </summary>
    /// <param name="incidentToUpdate">The incident to update.</param>
    /// <param name="updatedServiceStage">The updated service stage.</param>
    public Task DemonstrateUpdatingAnIncident(
        Incident incidentToUpdate, servicestage updatedServiceStage);


    /// <summary>
    /// Demonstrates deleting an incident by ID.
    /// </summary>
    /// <param name="incidentToDeleteId">The ID of the incident to delete.
    /// </param>
    public Task DemonstrateDeletingAnIncident(Guid incidentToDeleteId);


    /// <summary>
    /// Demonstrates fetching all active incidents.
    /// </summary>
    /// <returns>A collection of active incidents.</returns>
    public Task<IEnumerable<Incident>> DemonstrateFetchingAllActiveIncidents();


    /// <summary>
    /// Displays the details of an incident.
    /// </summary>
    /// <param name="incidentToDisplay">The incident to display.</param>
    public void DisplayIncident(Incident incidentToDisplay);


    /// <summary>
    /// Displays the details of multiple incidents.
    /// </summary>
    /// <param name="incidentsToDisplay">The incidents to display.</param>
    public void DisplayIncidents(IEnumerable<Incident> incidentsToDisplay);

}