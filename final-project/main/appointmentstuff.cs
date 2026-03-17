namespace main;

public class Appointment
{
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string VisitReason { get; set; }
    public string Status { get; set; }

    public Appointment(string visitReason, DateOnly date, TimeOnly time, string status = "SCHEDULED")
    {
        this.VisitReason = visitReason;
        this.Date = date;
        this.Time = time;
        this.Status = status;
    }

    public override string ToString()
    {
        return this.Status + Environment.NewLine + "Reason: " + this.VisitReason + Environment.NewLine + "Date: " + this.Date + " Time: " + this.Time;
    }
}

public class AppointmentLog
{
    public List<Appointment> Appointments { get; }

    public AppointmentLog()
    {
    this.Appointments = new List<Appointment>();     
    }

    public void AddAppointment(Appointment newAppointment)
    {
        this.Appointments.Add(newAppointment);
        SynchronizeAppointments();
    }

    public void RemoveAppointment(Appointment appointmentToRemove)
    {
        this.Appointments.Remove(appointmentToRemove);
        SynchronizeAppointments();
    }

    public void SynchronizeAppointments()
    {
        if(File.Exists("Appointment_List.txt")){
            File.Delete("Appointment_List.txt");
        }

        foreach (Appointment appointment in this.Appointments)
        {
            File.AppendAllText("Appointment_List.txt", appointment.VisitReason+','+ appointment.Date + ',' + appointment.Time + ',' + appointment.Status +Environment.NewLine);
        }
    }

    
}