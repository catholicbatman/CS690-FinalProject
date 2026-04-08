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
    
    Analyzer analyzer = new Analyzer();
    LogCreator logCreator = new LogCreator();

    VaccinationLog vaccinationLog = new VaccinationLog();
    vaccinationLog = logCreator.readVaccinationsFromFile();
    SupplyLog supplyLog = new SupplyLog();
    supplyLog = logCreator.readSupplyInfoFromFile();
    AppointmentLog appointmentLog = new AppointmentLog();
    appointmentLog = logCreator.readAppointmentInfoFromFile();
    MedicationLog medicationLog = new MedicationLog();
    medicationLog = logCreator.readMedicationInfoFromFile();
    WalkRecord walkRecord = new WalkRecord();
    walkRecord = logCreator.readWalkInfoFromFile();

        //The following is the UI menu, offering choices and then subsequent choices to the user in a loop.
        string choice;
        do {

            //This is the main menu for selection by presenting choices
            choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("What would you like to do?")
            .AddChoices("Manage Supply Inventory","Track Medical Information","Track Exercise","Control Meal Records - COMING SOON!","Track Dog Info - COMING SOON!","Exit")
            );
            
            //submenu for supply management
            if(choice == "Manage Supply Inventory")
            {
                string supplyChoice;

                do {
                    //sub submenu selection for supply options
                    supplyChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices("List low supply items","Add item to supply inventory","Edit inventory for a selected item","View exact inventory","Return to Supply Menu")
                    );

                        if (supplyChoice == "List low supply items")
                        {
                        int supplyNumber = 1;
                        int supplyNumberLowInSupply = 1;
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Here are the supplies that you have 3 or less of in the supply log");
                        Console.WriteLine(Environment.NewLine);
                        //This checks the supply log for supplies that have 3 or less of that supply left
                        foreach(Supply supply in supplyLog.Supplies)
                            {
                                if (supply.Amount <= 3)
                                {
                                AnsiConsole.MarkupLine("[red]Supply " + supplyNumberLowInSupply + " in low supply[/]");
                                Console.WriteLine(supply);
                                supplyNumberLowInSupply += 1;
                                }
                                supplyNumber += 1; 
                            }
                        }

                        else if (supplyChoice == "Add item to supply inventory")
                        {
                        //Creates a new supply and then fills its attributes with the user input
                        Supply supply;
                        supply = new Supply("", 1, "");
                        string supplyName = AnsiConsole.Prompt(new TextPrompt<string>("What is the name of the supply?"));
                        int supplyAmount = AnsiConsole.Prompt(new TextPrompt<int>("How many " + supplyName + " should be entered in the log?"));
                        string supplyType = AnsiConsole.Prompt(new TextPrompt<string>("What type is the supply (treat, food, etc.)?"));
                        supply.Name = supplyName;
                        supply.Amount = supplyAmount;
                        supply.Type = supplyType;
                        supplyLog.AddSupply(supply);
                        Console.WriteLine("Added " + supply.Name + " to the list.");    
                        }

                        else if (supplyChoice == "Edit inventory for a selected item")
                        {
                        //Takes the selected supply from the UI menu and then changes the amount attribute to the user given value.
                        Supply supplyInventoryToBeEdited = AnsiConsole.Prompt(new SelectionPrompt<Supply>()
                        .Title("Please select the supply you want to change the inventory of from the list")
                        .AddChoices(supplyLog.Supplies)
                        );
                        int newAmount = AnsiConsole.Prompt(new TextPrompt<int>("How many of " + supplyInventoryToBeEdited.Name + " is there now?"));
                        supplyInventoryToBeEdited.Amount = newAmount;
                        supplyLog.SynchronizeSupplies();
                        Console.WriteLine("The amount of " + supplyInventoryToBeEdited.Name + " in the supply log has been changed to " + supplyInventoryToBeEdited.Amount);
                        }

                        else if (supplyChoice == "View exact inventory")
                        {
                        int supplyNumber = 1;
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Here are the supplies in the supply log with the supply amount listed:");
                        Console.WriteLine(Environment.NewLine);
                        //Loops through and prints each supply in the supply log.
                        foreach(Supply supply in supplyLog.Supplies)
                            {
                                AnsiConsole.MarkupLine("[green]Supply " + supplyNumber + " in the supply list[/]");
                                Console.WriteLine(supply);
                                supplyNumber += 1; 
                            }
                        }
                } while(supplyChoice != "Return to Supply Menu");
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
                            
                            //Loops through the appointment log and writes each one with an appointment number
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

                            //Creates an appointment, allows the user to input values for the time and day of the appointment as well as a reason
                            //and then creates that appointment and records it in the log.
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

                            //Offers the user a selection menu and changes the appointment status attribute to complete that they chose
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

                            //Offers the user a selection menu and removes the selected appointment from the list
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
                            //Loops through the medication log and writes each one to the terminal
                            foreach(Medication medication in medicationLog.Meds)
                                {
                                    Console.WriteLine(medication);
                                }
                            
                            Console.WriteLine(Environment.NewLine);
                            }

                            else if (MedicationChoice == "Add Medication")
                            {
                            //Creates a medication, allows the user to input values for the name and administration times per day
                            //and then creates that exact medication and records it in the log.
                            Console.WriteLine("What medication do you want to add?");
                            Medication medication;
                            medication = new Medication("Test Data",0);
                            string medName = AnsiConsole.Prompt(new TextPrompt<string>("What is the name of the medication?"));
                            int times = AnsiConsole.Prompt(new TextPrompt<int>("How many times is it administered a day?"));
                            medication.Name = medName;
                            medication.AdministrationTimes = times;
                            medicationLog.AddMedication(medication);
                            Console.WriteLine(medication.Name +" has been added to the list.");
                            }


                            else if (MedicationChoice == "Remove Medication")
                            //Offers the user a selection menu and removes the selected medication from the list
                            {
                            medRemovalChoice = AnsiConsole.Prompt(new SelectionPrompt<Medication>()
                            .Title("Please select the medication you want to remove.")
                            .AddChoices(medicationLog.Meds));
                            medicationLog.RemoveMedication(medRemovalChoice);
                            Console.WriteLine(medRemovalChoice + " has been removed.");    
                            }

                            else if (MedicationChoice == "Show Medication Reminders")
                            {
                            Console.WriteLine("Here are the medication reminders:");
                                                        //Loops through the medications and lists both the name and number of times to be administered.
                            foreach(Medication medication in medicationLog.Meds)
                                {
                                    Console.WriteLine("Administer " + medication + " " + medication.AdministrationTimes + " times a day.");
                                }
                            }

                    }while(MedicationChoice != "Return to Medical Menu");        
                    }

                    else if (medModeChoice == "Vaccinations")
                    {
                    string VaccinationChoice;
                    do {
                        VaccinationChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("View Vaccination History","Show Vaccination Reminders","Record A Vaccination", "Return to Medical Menu")
                        );

                        if(VaccinationChoice == "View Vaccination History")
                        {
                            Console.WriteLine(Environment.NewLine);
                            Console.WriteLine("Here are the vaccinations the dog has received:");
                            Console.WriteLine(Environment.NewLine);
                            //Loops through the vaccination log and writes each one to the terminal
                            foreach(Vaccination vaccination in vaccinationLog.Vaccines)
                                {
                                    Console.WriteLine(vaccination);
                                }
                            
                            Console.WriteLine(Environment.NewLine);
                        }

                        if(VaccinationChoice == "Show Vaccination Reminders")
                        {
                            Console.WriteLine(Environment.NewLine);
                            Console.WriteLine("Here are the vaccination reminders:");
                            Console.WriteLine(Environment.NewLine);
                            //Loops through the vaccination log and writes the ones that are recurring to the terminal
                            foreach(Vaccination vaccination in vaccinationLog.Vaccines)
                                {   
                                    DateOnly vaccinationDueDate;
                                    vaccinationDueDate = vaccination.Date.AddMonths(Convert.ToInt32(vaccination.RecurranceTime));
                                    if (vaccination.Recurrance){
                                       Console.WriteLine(vaccination + " needs to be given again in " + vaccination.RecurranceTime + " months.");
                                       Console.WriteLine(" That means it needs to be administered again around " + vaccinationDueDate + "."); 
                                    }
                                    
                                }
                            
                            Console.WriteLine(Environment.NewLine);
                        }


                        if(VaccinationChoice == "Record A Vaccination")
                        {
                            Console.WriteLine("What vaccination do you want to add?");
                            Vaccination vaccination;
                            DateOnly initialDate;
                            initialDate = DateOnly.MinValue;
                            string initialReccuranceTime = "0"; 
                            vaccination = new Vaccination("Untyped",initialDate,false,initialReccuranceTime);
                            string vacType = AnsiConsole.Prompt(new TextPrompt<string>("What is the vaccination type?"));
                            vaccination.Type = vacType;
                            int vacYear = AnsiConsole.Prompt(new TextPrompt<int>("What year did the vaccination take place?"));
                            int vacMonth = AnsiConsole.Prompt(new TextPrompt<int>("What month did the vaccination happen? Please enter the month number."));
                            int vacDay = AnsiConsole.Prompt(new TextPrompt<int>("What day did the vaccination happen? Please enter the day number."));
                            vaccination.Date = new DateOnly(vacYear,vacMonth,vacDay);
                            bool recurranceState = AnsiConsole.Prompt(new TextPrompt<bool>("Is this a recurring vaccine? Please enter 'true' or 'false'."));
                            vaccination.Recurrance = recurranceState;
                            if (vaccination.Recurrance){
                                vaccination.RecurranceTime = AnsiConsole.Prompt(new TextPrompt<string>("How long until this vaccination should happen again (in months)?"));
                            }
                            vaccinationLog.AddVaccination(vaccination);
                            Console.WriteLine(vaccination.Type +" has been added to the vaccination log.");
                        }

                    }while(VaccinationChoice != "Return to Medical Menu");
                    }
                } while (medModeChoice != "Exit to Main Menu");   
            }


            if(choice == "Track Exercise")
            {
                string exerciseChoice;

                do {
                    //sub submenu selection for exercise options
                    exerciseChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices("Record A Walk For Today","View Today's Walk Record","View This Week's Walk Record","Exit to Main Menu")
                    );

                        if (exerciseChoice == "Record A Walk For Today")
                        {
                            //records the date/time of today and then takes input for the walk time, and creates a new walk in the log
                            //matching those specifications.
                            Walk walk;
                            DateTime now = DateTime.Now;
                            TimeSpan walkTime = TimeSpan.FromMinutes(AnsiConsole.Prompt(new TextPrompt<double>("How long did you walk the dog in minutes?")));
                            walk = new Walk(now, walkTime);
                            walkRecord.AddWalk(walk);
                            Console.WriteLine("Added this walk to the list.");
                        }

                        else if (exerciseChoice == "View Today's Walk Record")
                        {
                            //checks all the walks in the walk record and prints them if the date of the walk matches today's date.
                            int walkNumber = 1;
                            foreach(Walk walk in walkRecord.Walks)
                            {
                                if (walk.Date.Year == DateTime.Now.Year && walk.Date.Month == DateTime.Now.Month && walk.Date.Day == DateTime.Now.Day)
                                {
                                    AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " today[/]");
                                    Console.WriteLine(walk +Environment.NewLine);
                                    walkNumber += 1;
                                }
                                
                            }   
                        }

                        else if (exerciseChoice == "View This Week's Walk Record")
                        {
                            var weeklyWalks = analyzer.getWeeklyWalks(walkRecord);
                            int walkNumber = 1;
                            foreach(Walk walk in weeklyWalks){
                                AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week[/]");
                                Console.WriteLine(walk +Environment.NewLine);
                                walkNumber += 1;
                            }


                        }
                } while(exerciseChoice != "Exit to Main Menu");
            }
        } while (choice != "Exit"); 
    }
}
