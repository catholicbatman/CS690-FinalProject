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

    MedicationLog medicationLog;
    AppointmentLog appointmentLog;
    SupplyLog supplyLog;
    WalkRecord walkRecord; 

    string[] unsortedSupplyData = File.ReadAllLines("Supply_List.txt");
    supplyLog = new SupplyLog();
    string[] supplyInfoSplit;
    foreach(string supplyInfo in unsortedSupplyData)
        {
        Supply supply;
        supply = new Supply("Unnamend",0,"Untyped");
        supplyInfoSplit = supplyInfo.Split(',');
        supply.Name = supplyInfoSplit[0];
        supply.Amount = Convert.ToInt32(supplyInfoSplit[1]);
        supply.Type= supplyInfoSplit[2];
        supplyLog.Supplies.Add(supply);
        }

    
    string[] unsortedAppointmentData = File.ReadAllLines("Appointment_List.txt");
    appointmentLog = new AppointmentLog();
    string[] appointmentInfoSplit;
    string[] dateInfo;
    string[] timeInfo;
    //DateOnly appointmentDate;
    DateOnly temporaryDate;
    TimeOnly temporaryTime;
    temporaryDate = DateOnly.MinValue;
    temporaryTime = TimeOnly.MinValue;

    foreach(string appointmentInfo in unsortedAppointmentData)
        {
        Appointment appointment;
        appointment = new Appointment("",temporaryDate,temporaryTime);
        appointmentInfoSplit =  appointmentInfo.Split(',');
        appointment.VisitReason = appointmentInfoSplit[0];
        dateInfo = appointmentInfoSplit[1].Split('/');
        DateOnly appointmentDate = new(Convert.ToInt32(dateInfo[2]),Convert.ToInt32(dateInfo[0]), Convert.ToInt32(dateInfo[1]));
        appointment.Date = appointmentDate;
        timeInfo = appointmentInfoSplit[2].Split(':');
        appointment.Time = new(Convert.ToInt32(timeInfo[0]),Convert.ToInt32(timeInfo[1]));
        appointment.Status = appointmentInfoSplit[3];
        appointmentLog.Appointments.Add(appointment);
        }

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


    //Creating a new walkRecord and reading the walk_Record file in.
    string[] unsortedWalkData = File.ReadAllLines("Walk_Record.txt");
    walkRecord = new WalkRecord();
    string[] walkInfoSplit;
    string[] dateParts;
    string[] timeParts;
    string[] walkTimeParts;
    foreach(string walkInfo in unsortedWalkData)
        {
        Walk walk;
        //Splits the data into date, time of day, and time walked from the Walk Record File
        walkInfoSplit = walkInfo.Split(' ');

        //gathers the day, month, and year for creating a DateTime type
        int day, month, year;
        dateParts = walkInfoSplit[0].Split('/');
        month = Convert.ToInt32(dateParts[0]);
        day = Convert.ToInt32(dateParts[1]);
        year = Convert.ToInt32(dateParts[2]);

        //gathers the hours, minutes, and seconds of the day for creating a DateTime type
        int hours, minutes, seconds;
        timeParts = walkInfoSplit[1].Split(':');
        hours = Convert.ToInt32(timeParts[0]);
        minutes = Convert.ToInt32(timeParts[1]);
        seconds = Convert.ToInt32(timeParts[2]);

        //Gets the duration of the walk from the file
        int walkHours,walkMinutes,walkSeconds;
        walkTimeParts = walkInfoSplit[2].Split(':');
        walkHours = Convert.ToInt32(walkTimeParts[0]);
        walkMinutes = Convert.ToInt32(walkTimeParts[1]);
        walkSeconds = Convert.ToInt32(walkTimeParts[2]);

        //combines all of the info from the file to create a walk with the date and time walked
        //and then adds that walk to the walk record
        var thisWalk = new DateTime(year,month,day,hours,minutes,seconds);
        var thisWalkTime = new TimeSpan(walkHours,walkMinutes,walkSeconds); 
        walk = new Walk(thisWalk,thisWalkTime);
        walkRecord.Walks.Add(walk);
        }
    
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
                            string medName = AnsiConsole.Prompt(new TextPrompt<string>("What is the name of the medication?"));
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
            if(choice == "Track Exercise")
            {
                string exerciseChoice;

                do {
                    //sub submenu selection for exercise options
                    exerciseChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices("Record A Walk","View Today's Walk Record","View This Week's Walk Record","Exit to Main Menu")
                    );

                        if (exerciseChoice == "Record A Walk")
                        {
                            Walk walk;
                            DateTime now = DateTime.Now;
                            TimeSpan walkTime = TimeSpan.FromMinutes(AnsiConsole.Prompt(new TextPrompt<double>("How long did you walk the dog in minutes?")));
                            walk = new Walk(now, walkTime);
                            walkRecord.AddWalk(walk);
                            Console.WriteLine("Added this walk to the list.");
                        }

                        else if (exerciseChoice == "View Today's Walk Record")
                        {
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

                        /*else if (supplyChoice == "Edit inventory for a selected item")
                        {
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
                        foreach(Supply supply in supplyLog.Supplies)
                            {
                                AnsiConsole.MarkupLine("[green]Supply " + supplyNumber + " in the supply list[/]");
                                Console.WriteLine(supply);
                                supplyNumber += 1; 
                            }
                        }*/
                } while(exerciseChoice != "Exit to Main Menu");
            }
        } while (choice != "Exit"); 
    }
}
