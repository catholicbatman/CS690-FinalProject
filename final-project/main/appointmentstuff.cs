namespace main;

public class Appointment
{
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string VisitReason { get; set; } 

    public Appointment(string visitReason, DateOnly date, TimeOnly time)
    {
        this.VisitReason = visitReason;
        this.Date = date;
        this.Time = time;
    }

    public override string ToString()
    {
        return "Reason: " + this.VisitReason + " Date: " + this.Date + " Time: " + this.Time;
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
            File.AppendAllText("Appointment_List.txt", appointment.VisitReason+','+ appointment.Date + ',' + appointment.Time +Environment.NewLine);
        }
    }

    
}