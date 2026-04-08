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
        return this.Date + Environment.NewLine + "Time walked: " + this.WalkTime.Hours + " hours and " + this.WalkTime.Minutes + " minutes";
    }
}

public class WalkRecord
{
    FileSaver walkFileSaver = new FileSaver("Walk_Record.txt");
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
        walkFileSaver.DeleteFile();

        foreach (Walk walk in this.Walks)
        {
            walkFileSaver.AppendLine(walk.Date.ToString() +' '+ walk.WalkTime);
        }
    }

    
}