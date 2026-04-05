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
    
    //creating the necessary different logs of data
    MedicationLog medicationLog;
    AppointmentLog appointmentLog;
    SupplyLog supplyLog;
    WalkRecord walkRecord;
    VaccinationLog vaccinationLog; 

    //Creating a new vaccine log and reading the Vaccine_Log file in to add vaccines to the list.
    string[] unsortedVaccinationData = File.ReadAllLines("Vaccination_Log.txt");
    vaccinationLog = new VaccinationLog();
    string[] vaccinationInfoSplit;
    DateOnly initialDate;
    string[] dateData;
    foreach(string vaccinationInfo in unsortedVaccinationData)
        {
        Vaccination vaccination;
        initialDate = DateOnly.MinValue;
        vaccination = new Vaccination("Untyped",initialDate,false,"0");
        vaccinationInfoSplit = vaccinationInfo.Split(',');
        vaccination.Type = vaccinationInfoSplit[0];
        dateData = vaccinationInfoSplit[1].Split('/');
        DateOnly vaccinationDate = new(Convert.ToInt32(dateData[2]),Convert.ToInt32(dateData[0]), Convert.ToInt32(dateData[1]));
        vaccination.Date = vaccinationDate;
        vaccination.Recurrance = Convert.ToBoolean(vaccinationInfoSplit[2]);
        vaccination.RecurranceTime = vaccinationInfoSplit[3];
        }

    //Creating a new supply log and reading the Supply_List file in to add supplies to the list.
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

    //Creating a new appointment log and reading the Appointment_List file in to add appointments to the list.
    string[] unsortedAppointmentData = File.ReadAllLines("Appointment_List.txt");
    appointmentLog = new AppointmentLog();
    string[] appointmentInfoSplit;
    string[] dateInfo;
    string[] timeInfo;
    //DateOnly is a type that only keeps a date (year, month, day)
    DateOnly temporaryDate;
    //TimeOnly is a type that only keeps a time (hours, minutes, seconds)
    TimeOnly temporaryTime;
    temporaryDate = DateOnly.MinValue;
    temporaryTime = TimeOnly.MinValue;

    //reads the log info and splits it with delimeters
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


    //Creating a new medication log and reading the Medication_List file in to add medications to the list.
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


    //Creating a new walkRecord and reading the walk_Record file in to add walks to the list.
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
                            int walkNumber = 1;
                            foreach(Walk walk in walkRecord.Walks)
                            {

                                //This sets the day of the week today (from Sunday to Saturday) as an integer
                                int dayOfWeekToday = Convert.ToInt32(DateTime.Now.DayOfWeek);
                                //This variable is the exact number of the day of the month (from 1-31)
                                int dateToday = DateTime.Now.Day;
                                //Creating integer variables of the past 6 days (what numbered day of the month) from the date today.
                                DateTime oneDayAgo = DateTime.Now.AddDays(-1);
                                int dateOneDayAgo = oneDayAgo.Day;
                                DateTime twoDaysAgo = DateTime.Now.AddDays(-2);
                                int dateTwoDaysAgo = twoDaysAgo.Day;
                                DateTime threeDaysAgo = DateTime.Now.AddDays(-3);
                                int dateThreeDaysAgo = threeDaysAgo.Day;
                                DateTime fourDaysAgo = DateTime.Now.AddDays(-4);
                                int dateFourDaysAgo = fourDaysAgo.Day;
                                DateTime fiveDaysAgo = DateTime.Now.AddDays(-5);
                                int dateFiveDaysAgo = fiveDaysAgo.Day;
                                DateTime sixDaysAgo = DateTime.Now.AddDays(-6);
                                int dateSixDaysAgo = sixDaysAgo.Day;
                                //finds the last month, which is necessary because sometimes the date early in the month
                                //will have a week that began the prior month and needs to be taken into account for producing
                                //a weekly list of walks
                                DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);
                                int dateOneMonthAgo = oneMonthAgo.Month;
                                
                                //This conditional checks to see if they year and month of each walk are aligned with
                                //the current year and month.
                                if (walk.Date.Year == DateTime.Now.Year && (walk.Date.Month == DateTime.Now.Month 
                                | (walk.Date.Month == dateOneMonthAgo && walk.Date.Day > 23)))
                                {
                                    //All of the conditionals here check if the current day is a certain day of the week and then
                                    //checks to see if the walk date day matches that or any day that is a part of the week (possibly
                                    //one to six days prior. Together the conditionals ultimately print the walks of the current week)
                                    //starting on Sunday.
                                    if(dayOfWeekToday == 0 && walk.Date.Day == dateToday)
                                    {
                                        AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week[/]");
                                        Console.WriteLine(walk +Environment.NewLine);
                                        walkNumber += 1;
                                    }

                                    if(dayOfWeekToday == 1 && (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo))
                                    {
                                        AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week[/]");
                                        Console.WriteLine(walk +Environment.NewLine);
                                        walkNumber += 1;
                                    }

                                    if(dayOfWeekToday == 2 && 
                                    (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo))
                                    {
                                        AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week (which started on Sunday)[/]");
                                        Console.WriteLine(walk +Environment.NewLine);
                                        walkNumber += 1;
                                    }

                                    if(dayOfWeekToday == 3 && 
                                    (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo | walk.Date.Day == dateThreeDaysAgo))
                                    {
                                        AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week (which started on Sunday)[/]");
                                        Console.WriteLine(walk +Environment.NewLine);
                                        walkNumber += 1;
                                    }

                                    if(dayOfWeekToday == 4 && 
                                    (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo | walk.Date.Day == dateThreeDaysAgo
                                    | walk.Date.Day == dateFourDaysAgo))
                                    {
                                        AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week (which started on Sunday)[/]");
                                        Console.WriteLine(walk +Environment.NewLine);
                                        walkNumber += 1;
                                    }

                                    if(dayOfWeekToday == 5 && 
                                    (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo | walk.Date.Day == dateThreeDaysAgo
                                    | walk.Date.Day == dateFourDaysAgo | walk.Date.Day == dateFiveDaysAgo))
                                    {
                                        AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week (which started on Sunday)[/]");
                                        Console.WriteLine(walk +Environment.NewLine);
                                        walkNumber += 1;
                                    }

                                    if(dayOfWeekToday == 6 && 
                                    (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo | walk.Date.Day == dateThreeDaysAgo
                                    | walk.Date.Day == dateFourDaysAgo | walk.Date.Day == dateFiveDaysAgo | walk.Date.Day == dateSixDaysAgo))
                                    {
                                        AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week (which started on Sunday)[/]");
                                        Console.WriteLine(walk +Environment.NewLine);
                                        walkNumber += 1;
                                    }
                                }
                                
                            }   
                        }
                } while(exerciseChoice != "Exit to Main Menu");
            }
        } while (choice != "Exit"); 
    }
}
