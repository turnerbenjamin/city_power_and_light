using CityPowerAndLight.Model;

namespace CityPowerAndLight.View;

/// <summary>
/// The <c>IUserInterface</c> interface defines methods for interacting with the 
/// user.
/// </summary>
internal interface IUserInterface
{
    /// <summary>
    /// Prints a title to the user interface.
    /// </summary>
    /// <param name="title">The title to print.</param>
    void PrintTitle(string title);

    /// <summary>
    /// Prints a heading to the user interface.
    /// </summary>
    /// <param name="heading">The heading to print.</param>
    void PrintHeading(string heading);

    /// <summary>
    /// Prints a message to the user interface.
    /// </summary>
    /// <param name="message">The message to print.</param>
    void PrintMessage(string message);

    /// <summary>
    /// Prints the details of an incident to the user interface.
    /// </summary>
    /// <param name="incident">The incident to print.</param>
    void PrintIncident(Incident incident);
}