namespace main;

public class Walk
{
    public DateTime Date { get; set; }
    public TimeSpan WalkTime { get; set; }

    public Walk(DateTime date, TimeSpan walkTime)
    {
        this.Date = date;
        this.WalkTime = walkTime;
    }

    public override string ToString()
    {
        return this.Date + Environment.NewLine + "Time walked: " + this.WalkTime.Hours + " hours and " + this.WalkTime.Minutes;
    }
}

public class WalkRecord
{
    public List<Walk> Walks { get; }

    public WalkRecord()
    {
    this.Walks = new List<Walk>();     
    }

    public void AddWalk(Walk newWalk)
    {
        this.Walks.Add(newWalk);
        SynchronizeWalks();
    }

    /*public void RemoveAppointment(Appointment appointmentToRemove)
    {
        this.Appointments.Remove(appointmentToRemove);
        SynchronizeAppointments();
    }*/

    public void SynchronizeWalks()
    {
        if(File.Exists("Walk_Record.txt")){
            File.Delete("Walk_Record.txt");
        }

        foreach (Walk walk in this.Walks)
        {
            File.AppendAllText("Walk_Record.txt", walk.Date +Environment.NewLine + walk.WalkTime + Environment.NewLine);
        }
    }

    
}