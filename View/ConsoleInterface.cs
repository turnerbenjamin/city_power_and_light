using CityPowerAndLight.Model;
using Microsoft.Xrm.Sdk;

namespace CityPowerAndLight.View;

/// <summary>
/// The <c>ConsoleInterface</c> class provides methods to interact with the user 
/// via the console.
/// </summary>
internal class ConsoleInterface : IUserInterface
{
    /// <summary>
    /// Prints a title with an asterisk border to the console.
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
    /// Prints the details of an account to the console.
    /// </summary>
    /// <param name="accountToPrint">The account to print.</param>
    public void PrintEntity(Account accountToPrint)
    {
        Console.WriteLine();
        Console.WriteLine($"Name: {accountToPrint.Name}");
        Console.WriteLine($"Main Phone: {accountToPrint.Telephone1}");
        Console.WriteLine($"Address1 (City): {accountToPrint.Address1_City}");
        Console.WriteLine(
            $"Primary Contact: {accountToPrint.PrimaryContactId?.Name ?? "null"}");
        Console.WriteLine();
    }

    /// <summary>
    /// Prints the details of a contact to the console.
    /// </summary>
    /// <param name="contactToPrint">The contact to print.</param>
    public void PrintEntity(Contact contactToPrint)
    {
        Console.WriteLine();
        Console.WriteLine($"Full Name: {contactToPrint.FullName}");
        Console.WriteLine($"Email: {contactToPrint.EMailAddress1}");
        Console.WriteLine(
            $"Company Name: {contactToPrint.ParentCustomerId?.Name ?? "n/a"}");

        Console.WriteLine($"Business Phone: {contactToPrint.Telephone1}");
        Console.WriteLine();
    }


    /// <summary>
    /// Prints the details of an incident to the console.
    /// </summary>
    /// <param name="incidentToPrint">The incident to print.</param>
    public void PrintEntity(Incident incidentToPrint)
    {
        Console.WriteLine();
        Console.WriteLine($"Title: {incidentToPrint.Title}");
        Console.WriteLine($"Case Number: {incidentToPrint.TicketNumber}");
        Console.WriteLine($"Priority: {incidentToPrint.PriorityCode}");
        Console.WriteLine($"Origin: {incidentToPrint.CaseOriginCode}");
        Console.WriteLine($"Customer: {incidentToPrint.CustomerId.Name}");
        Console.WriteLine($"Status Reason: {incidentToPrint.StatusCode}");
        Console.WriteLine($"Status: {incidentToPrint.StateCode}");
        Console.WriteLine($"CreatedOn: {incidentToPrint.CreatedOn}");
        Console.WriteLine($"Contact: {incidentToPrint.PrimaryContactId.Name}");
        Console.WriteLine($"Description: {incidentToPrint.Description}");
        Console.WriteLine($"Type: {incidentToPrint.CaseTypeCode}");
        Console.WriteLine($"Service Stage: {incidentToPrint.ServiceStage}");
        Console.WriteLine();
    }

    /// <summary>
    /// Prints a list of entities to the console.
    /// </summary>
    /// <typeparam name="T">The type of the entities to print.</typeparam>
    /// <param name="entitiesToPrint">The list of entities to print.</param>
    /// <param name="getRow">A function that takes an entity of type 
    /// <typeparamref name="T"/> and returns a string representation of the 
    /// entity.</param>
    public void PrintEntityList<T>(
        IEnumerable<T> entitiesToPrint, Func<T, string> getRow)
        where T : Entity
    {
        if (!entitiesToPrint.Any())
        {
            PrintMessage("No entities found");
        }
        else
        {
            foreach (T entity in entitiesToPrint)
            {
                PrintMessage($"- {getRow(entity)}");
            }
            PrintMessage("------------------------------");
        }
        PrintMessage("");
    }



    //Overload of PrintWithBorder with padding defaulted to 0
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