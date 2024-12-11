using Microsoft.Xrm.Sdk;

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
    /// Prints a spacer of the specified size to the user interface
    /// </summary>
    /// <param name="spacerSize">
    /// The size of the spacer. Default is 1.
    /// </param>
    public void PrintSpacer(int spacerSize = 1);


    /// <summary>]
    /// Prints a list of entities to the user interface.
    /// </summary>
    /// <typeparam name="T">The type of the entities to print.</typeparam>
    /// <param name="entitiesToPrint">The list of entities to print.</param>
    /// <param name="getRow">A function that takes an entity of type 
    /// <typeparamref name="T"/> and returns a string representation of the 
    /// entity (Should be a single line).</param>
    void PrintEntityList<T>(
        IEnumerable<T> entitiesToPrint, Func<T, string> getRow)
        where T : Entity;
}