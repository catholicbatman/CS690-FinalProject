namespace main;

using System.Reflection.Metadata.Ecma335;
using System.IO;
using Spectre.Console;
using System.Collections;
using System.Linq;
using Microsoft.VisualBasic;

class DataManager {
    public static AppointmentLog ReadAppointmentsFromFile() {
        DateOnly temporaryDate;
        TimeOnly temporaryTime;
        temporaryDate = DateOnly.MinValue;
        temporaryTime = TimeOnly.MinValue;
        string[] appointmentInfoSplit;
        string[] dateInfo;
        string[] timeInfo;
        string[] unsortedAppointmentData = File.ReadAllLines("Appointment_List.txt");
        AppointmentLog appointmentLog; 
        appointmentLog = new AppointmentLog();
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

    public static MedicationLog ReadMedicationsFromFile() {
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
}