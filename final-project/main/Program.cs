namespace main;

using Spectre.Console;
class Program
{
    static void Main(string[] args)
    {
    
        string choice;
        do {
            choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("What would you like to do?")
            .AddChoices("Manage Supply Inventory","Track Medical Information","Track Exercise","Control Meal Records","Track Dog Info","Exit")
            );
            Console.WriteLine(choice);
        } while (choice != "Exit");
    }
}
