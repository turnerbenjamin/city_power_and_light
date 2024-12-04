using CityPowerAndLight.Model;

namespace CityPowerAndLight.View;

/// <summary>
/// The <c>ConsoleInterface</c> class provides methods to interact with the user 
/// via the console.
/// </summary>
internal class ConsoleInterface : IUserInterface
{
    /// <summary>
    /// Prints a title with an astrix border to the console.
    /// </summary>
    /// <param name="title">The title to print.</param>
    public void PrintTitle(string title)
    {
        PrintWithBorder('*', title, 5);
    }

    /// <summary>
    /// Prints a heading with a dash border.
    /// </summary>
    /// <param name="title">The heading to print.</param>
    public void PrintHeading(string title)
    {
        PrintWithBorder('-', title);
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

    //Overload of print with border with padding defaulted to 0
    private static void PrintWithBorder(char borderChar, string message)
    {
        PrintWithBorder(borderChar, message, 0);
    }

    //Prints a message with borders above and below the message. The message 
    //will be centered withing the borders and padded by the specified value.
    private static void PrintWithBorder(char borderChar, string message, int padding)
    {
        var border = new string(borderChar, message.Length + padding * 2);
        var messagePadding = new string(' ', padding);

        Console.WriteLine(Environment.NewLine + border);
        Console.WriteLine($"{messagePadding}{message}");
        Console.WriteLine(border + Environment.NewLine);
    }
}