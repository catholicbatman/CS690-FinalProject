namespace main;

using System.Reflection.Metadata.Ecma335;
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
            
            //submenu for selection supply things
            if(choice == "Manage Supply Inventory")
            {
                Console.WriteLine("Manage Supply");
            }

            //submenu for selecting just medical things
            else if(choice == "Track Medical Information")
            {
                string medModeChoice;
            do {    
                medModeChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Which would you like to manage?")
                .AddChoices("Appointments","Medications","Vaccinations","Exit to Main Menu")
                );

                    //sub submenu selection for appointment options
                    if (medModeChoice == "Appointments")
                    {
                    string AppointmentChoice;
                    do {
                        AppointmentChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("View Appointments","Add Appointment","Mark Appointment Complete","Remove Appointment", "Return to Medical Menu")
                        );

                            if (AppointmentChoice == "View Appointments")
                            {
                            Console.WriteLine("Appointments Viewed");
                            }
                            else if (AppointmentChoice == "Add Appointment")
                            {
                            Console.WriteLine("Added Appointment");
                            }
                            else if (AppointmentChoice == "Mark Appointment Complete")
                            {
                            Console.WriteLine("Appointment Completed");
                            }
                            else if (AppointmentChoice == "Remove Appointment")
                            {
                            Console.WriteLine("Appointment Removed");
                            }
                    }while(AppointmentChoice != "Return to Medical Menu");        
                    }

                    //sub submenu selection for medication options
                    else if (medModeChoice == "Medications")
                    {
                        string MedicationChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("View Current Medications","Add Medication","Remove Medication","Show Medication Reminders", "Return to main menu")
                        );
                    }

                    else if (medModeChoice == "Vaccinations")
                    {
                        string VaccinationChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("View Vaccination History","Show Vaccination Reminders","Record A Vaccination", "Return to main menu")
                        );
                    }
            } while (medModeChoice != "Exit to Main Menu");    

            }
        } while (choice != "Exit");
    }
}
