using Microsoft.Xrm.Sdk;

namespace CityPowerAndLight.View;

/// <summary>
/// The <c>ConsoleInterface</c> class provides methods to interact with the user 
/// via the console.
/// </summary>
internal class ConsoleInterface : IUserInterface
{
    /// <summary>
    /// Prints a title with an asterisk border to the console with some padding
    /// to indent the title in the border
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
    /// Prints a specified number of blank lines to the console.
    /// </summary>
    /// <param name="spacerSize">
    /// The number of blank lines to print. Default is 1.
    /// </param>
    public void PrintSpacer(int spacerSize = 1)
    {
        for (int i = 1; i <= spacerSize; i++)
        {
            Console.WriteLine();
        }
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
        PrintSpacer();
    }


    //Overload of PrintWithBorder with padding defaulted to 0
    private void PrintWithBorder(char borderChar, string message)
    {
        PrintWithBorder(borderChar, message, 0);
    }


    //Prints a message with borders above and below the message. The message 
    //will be centered withing the borders and padded by the specified value.
    private void PrintWithBorder(char borderChar, string message, int padding)
    {
        var border = new string(borderChar, message.Length + padding * 2);
        var messagePadding = new string(' ', padding);

        PrintSpacer();
        PrintMessage(border);
        PrintMessage($"{messagePadding}{message}");
        PrintMessage(border);
        PrintSpacer();
    }
}