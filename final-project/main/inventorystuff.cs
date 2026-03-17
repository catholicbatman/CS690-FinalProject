namespace main;

/*public class Supply
{
    public string Name { get; set; }
    public int Amount {get; set; }
    public string Type {get; set;}

    public Supply(string name, int amount, string type)
    {
        this.Name = name;
        this.Amount = amount;
        this.Type = type;
    }

    public override string ToString()
    {
        return "Type " + this.Type + " Name: " + this.Name + " Amount: " + this.Amount;
    }
}

public class SupplyLog
{
    public List<Supply> Supplies { get; }

    public SupplyLog()
    {
    this.Supplies = new List<Supply>();     
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

    
}*/