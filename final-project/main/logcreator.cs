namespace main;


public class LogCreator{
    
    public LogCreator(){
    }

    public VaccinationLog readVaccinationsFromFile(){
        //Creating a new vaccine log and reading the Vaccine_Log file in to add vaccines to the list.
        string[] unsortedVaccinationData = File.ReadAllLines("Vaccination_Log.txt");
        VaccinationLog vaccinationLog = new VaccinationLog();
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
            vaccinationLog.Vaccines.Add(vaccination);
            }
        return vaccinationLog;
    }

    public SupplyLog readSupplyInfoFromFile(){
        //Creating a new supply log and reading the Supply_List file in to add supplies to the list.
        string[] unsortedSupplyData = File.ReadAllLines("Supply_List.txt");
        SupplyLog supplyLog = new SupplyLog();
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
        return supplyLog;
    }

    public AppointmentLog readAppointmentInfoFromFile(){
        //Creating a new appointment log and reading the Appointment_List file in to add appointments to the list.
        string[] unsortedAppointmentData = File.ReadAllLines("Appointment_List.txt");
        AppointmentLog appointmentLog = new AppointmentLog();
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
        return appointmentLog;
    }

    public MedicationLog readMedicationInfoFromFile(){
        //Creating a new medication log and reading the Medication_List file in to add medications to the list.
        string[] unsortedMedData = File.ReadAllLines("Medication_List.txt");
        MedicationLog medicationLog = new MedicationLog();
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
        
        return medicationLog;
    }

    public WalkRecord readWalkInfoFromFile(){
        //Creating a new walkRecord and reading the walk_Record file in to add walks to the list.
        string[] unsortedWalkData = File.ReadAllLines("Walk_Record.txt");
        WalkRecord walkRecord = new WalkRecord();
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
        return walkRecord;
        
    }


}