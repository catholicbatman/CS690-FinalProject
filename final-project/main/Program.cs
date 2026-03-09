namespace main;

using Spectre.Console;
class Program
{
    static void Main(string[] args)
    {
    
        string choice;
        do {

            //This is the main menu for selection by presenting choices
            choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("What would you like to do?")
            .AddChoices("Manage Supply Inventory","Track Medical Information","Track Exercise","Control Meal Records","Track Dog Info","Exit")
            );


            if(choice == "Manage Supply Inventory")
            {
                Console.WriteLine("Manage Supply");
            }
        } while (choice != "Exit");
    }
}
