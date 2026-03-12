namespace main;

using System.Reflection.Metadata.Ecma335;
using System.IO;
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
                    string MedicationChoice;
                    do {

                        MedicationChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("View Current Medications","Add Medication","Remove Medication","Show Medication Reminders", "Return to Medical Menu")
                        );

                        if (MedicationChoice == "View Current Medications")
                            {
                            string[] medFileContents = File.ReadAllLines("Medication_List.txt");
                            //string[] medications = Split(medFileContents,",");
                            foreach(string medicationInfo in medFileContents)
                                {
                                string[] medicationInfoSplit = medicationInfo.Split(',');
                                int count = 0;
                                foreach(string medication in medicationInfoSplit)
                                
                                    {
                                        if (count % 2 == 0)
                                        {
                                        Console.WriteLine(medication);    
                                        }
                                        count += 1;
                                    }
                                //Console.WriteLine(medicationInfo);   
                                }
                            Console.WriteLine("Medications viewed");
                            }

                            else if (MedicationChoice == "Add Medication")
                            {
                            Console.WriteLine("What is the medication name?");
                            string medicationName = Console.ReadLine();
                            Console.WriteLine("How many times a day is it administered?");
                            string medicationAdministrationTimes = Console.ReadLine();
                            File.AppendAllText("Medication_List.txt", medicationName + "," + medicationAdministrationTimes + Environment.NewLine);
                            }

                            else if (MedicationChoice == "Remove Medication")
                            {
                            Console.WriteLine("Medication Removed");
                            }
                            else if (MedicationChoice == "Show Medication Reminders")
                            {
                            Console.WriteLine("Here are the medication reminders:");
                            }

                    }while(MedicationChoice != "Return to Medical Menu");        
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
