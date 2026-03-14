namespace main;

using System.Reflection.Metadata.Ecma335;
using System.IO;
using Spectre.Console;
using System.Collections;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {

    MedicationLog medicationLog;

    string[] unsortedMedData = File.ReadAllLines("Medication_List.txt");
    medicationLog = new MedicationLog();
    string[] medicationInfoSplit;
    foreach(string medicationInfo in unsortedMedData)
        {
        Medication medication;
        medication = new Medication("Tested",0);
        medicationInfoSplit = medicationInfo.Split(',');
        medication.Name = medicationInfoSplit[0];
        medication.AdministrationTimes = Convert.ToInt32(medicationInfoSplit[1]);
        medicationLog.Meds.Add(medication);
        }
    
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
                        Medication medRemovalChoice;
                        MedicationChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("View Current Medications","Add Medication","Remove Medication","Show Medication Reminders", "Return to Medical Menu")
                        );
                        
                        if (MedicationChoice == "View Current Medications")
                            {
    
                            Console.WriteLine(Environment.NewLine);
                            Console.WriteLine("Here are the current medications the dog is taking:");
                            foreach(Medication medication in medicationLog.Meds)
                                {
                                    Console.WriteLine(medication);
                                }
                            
                            Console.WriteLine(Environment.NewLine);
                            }

                            else if (MedicationChoice == "Add Medication")
                            {
                            Console.WriteLine("What medication do you want to add?");
                            
                            Medication medication;
                            medication = new Medication("Test Data",0);
                            string medName = Console.ReadLine();
                            int times = AnsiConsole.Prompt(new TextPrompt<int>("How many times is it administered a day?"));
                            medication.Name = medName;
                            medication.AdministrationTimes = times;
                            medicationLog.AddMedication(medication);

                            Console.WriteLine(medication.Name +" has been added to the list.");
                            }

                            else if (MedicationChoice == "Remove Medication")

                            {
                            medRemovalChoice = AnsiConsole.Prompt(new SelectionPrompt<Medication>()
                            .Title("Please select the medication you want to remove.")
                            .AddChoices(medicationLog.Meds)
                            );

                            medicationLog.RemoveMedication(medRemovalChoice);
                            
                            Console.WriteLine(medRemovalChoice + " has been removed.");    
                            }

                            else if (MedicationChoice == "Show Medication Reminders")
                            {
                            Console.WriteLine("Here are the medication reminders:");
                            foreach(Medication medication in medicationLog.Meds)
                                {
                                    Console.WriteLine("Administer " + medication + " " + medication.AdministrationTimes + " times a day.");
                                }
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
