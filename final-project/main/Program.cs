namespace main;

using System.Reflection.Metadata.Ecma335;
using System.IO;
using Spectre.Console;
using System.Collections;
using System.Linq;
using Microsoft.VisualBasic;

class Program
{

    static void Main(string[] args)
    {


    AppointmentLog appointmentLog = DataManager.ReadAppointmentsFromFile(); 


    MedicationLog medicationLog = DataManager.ReadMedicationsFromFile();
    
        string choice;
        do {

            //This is the main menu for selection by presenting choices
            choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("What would you like to do?")
            .AddChoices("Manage Supply Inventory - COMING SOON!","Track Medical Information","Track Exercise - COMING SOON!","Control Meal Records - COMING SOON!","Track Dog Info - COMING SOON!","Exit")
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
                Appointment appointmentRemovalChoice;
                Appointment appointmentCompleted;    
                medModeChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Which would you like to manage?")
                .AddChoices("Appointments","Medications","Vaccinations - COMING SOON!","Exit to Main Menu")
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
                            int appointmentNumber = 1;
                            Console.WriteLine(Environment.NewLine);
                            Console.WriteLine("Here are the appointments in the appointment log:");
                            Console.WriteLine(Environment.NewLine);
                            foreach(Appointment appointment in appointmentLog.Appointments)
                                {
                                    Console.WriteLine("Appointment " + appointmentNumber + " ");
                                    Console.WriteLine(appointment);
                                    Console.WriteLine(Environment.NewLine);
                                    appointmentNumber += 1; 
                                }
                            }

                        
                            else if (AppointmentChoice == "Add Appointment")
                            {
                            Appointment appointment;
                            appointment = new Appointment("", new(1,1,1), new(0,0));
                            string appointmentReason = AnsiConsole.Prompt(new TextPrompt<string>("What is the reason for the appointment?"));
                            int year = AnsiConsole.Prompt(new TextPrompt<int>("What year is the appointment?"));
                            int month = AnsiConsole.Prompt(new TextPrompt<int>("What month is the appointment? Please enter the month number."));
                            int day = AnsiConsole.Prompt(new TextPrompt<int>("What day is the appointment? Please enter the day number."));
                            int hour = AnsiConsole.Prompt(new TextPrompt<int>("What time is the appointment? Enter the hour here using 24 hour time, then hit enter to put in the minutes."));
                            int minutes = AnsiConsole.Prompt(new TextPrompt<int>("The hour of the appointment is " + hour + "o'clock. What minute of that hour is the appointment?"));
                            appointment.VisitReason = appointmentReason;
                            appointment.Date = new(year,month,day);
                            appointment.Time = new(hour,minutes);
                            appointmentLog.AddAppointment(appointment);
                            Console.WriteLine("Added Appointment to the list.");
                            }

                            else if (AppointmentChoice == "Mark Appointment Complete")
                            {
                            appointmentCompleted = AnsiConsole.Prompt(new SelectionPrompt<Appointment>()
                            .Title("Please select the appointment you want to mark as complete.")
                            .AddChoices(appointmentLog.Appointments)
                            );
                            appointmentCompleted.Status = "COMPLETED";
                            appointmentLog.SynchronizeAppointments();
                            Console.WriteLine("Appointment Marked as Completed");
                            }

                            else if (AppointmentChoice == "Remove Appointment")
                            {
                            appointmentRemovalChoice = AnsiConsole.Prompt(new SelectionPrompt<Appointment>()
                            .Title("Please select the appointment you want to remove.")
                            .AddChoices(appointmentLog.Appointments)
                            );

                            appointmentLog.RemoveAppointment(appointmentRemovalChoice);
                            
                            Console.WriteLine(appointmentRemovalChoice + " has been removed.");    
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
