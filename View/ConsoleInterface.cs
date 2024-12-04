using CityPowerAndLight.Model;

namespace CityPowerAndLight.View;

/// <summary>
/// The <c>ConsoleInterface</c> class provides methods to interact with the user 
/// via the console.
/// </summary>
internal class ConsoleInterface : IUserInterface
{
    /// <summary>
    /// Prints a title with a border to the console.
    /// </summary>
    /// <param name="title">The title to print.</param>
    public void PrintTitle(string title)
    {
        var titlePaddingLength = 5;
        var border = new string('*', title.Length + titlePaddingLength * 2);
        var titlePadding = new string(' ', titlePaddingLength);

        Console.WriteLine(Environment.NewLine + border);
        Console.WriteLine($"{titlePadding}{title}");
        Console.WriteLine(border + Environment.NewLine);
    }

    /// <summary>
    /// Prints a heading with an underline to the console.
    /// </summary>
    /// <param name="title">The heading to print.</param>
    public void PrintHeading(string title)
    {
        Console.WriteLine(Environment.NewLine + title);
        Console.WriteLine(new string('-', title.Length) + Environment.NewLine);

    }

    /// <summary>
    /// Prints a message to the console.
    /// </summary>
    /// <param name="message">The message to print.</param>
    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    /// <summary>
    /// Prints the details of an incident to the console.
    /// </summary>
    /// <param name="incidentToPrint">The incident to print.</param>
    public void PrintIncident(Incident incidentToPrint)
    {
        Console.WriteLine();
        Console.WriteLine($"Title: {incidentToPrint.Title}");
        Console.WriteLine($"Description: {incidentToPrint.Description}");
        Console.WriteLine($"Account: {incidentToPrint.CustomerId.Name}");
        Console.WriteLine($"Contact: {incidentToPrint.PrimaryContactId.Name}");
        Console.WriteLine($"Stage: {incidentToPrint.ServiceStage}");
        Console.WriteLine($"Status: {incidentToPrint.StateCode}");
        Console.WriteLine($"Type: {incidentToPrint.CaseTypeCode}");
        Console.WriteLine();
    }
}