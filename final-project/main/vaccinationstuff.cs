namespace main;

public class Vaccination
{
    public string Type { get; set; }
    public DateOnly Date { get; set; }
    public bool Recurrance { get; set; }
    public string RecurranceTime { get; set; }

    public Vaccination(string type, DateOnly date, bool recurrance, string recurranceTime = "0")
    {
        this.Type = type;
        this.Date = date;
        this.Recurrance = recurrance;
        this.RecurranceTime = recurranceTime;
    }

    public override string ToString()
    {
        return (this.Type + " Date Administered: " + this.Date);
    }
}

public class VaccinationLog
{
    public List<Vaccination> Vaccines { get; }

    public VaccinationLog()
    {
    this.Vaccines = new List<Vaccination>();     
    }

    public void AddVaccination(Vaccination vaccination)
    {
        this.Vaccines.Add(vaccination);
        SynchronizeVaccinations();
    }

    public void SynchronizeVaccinations()
    {
        if(File.Exists("Vaccination_Log.txt")){
            File.Delete("Vaccination_Log.txt");
        }

        foreach (Vaccination vaccination in this.Vaccines)
        {
            File.AppendAllText("Vaccination_Log.txt", vaccination.Type + ',' + vaccination.Date + ',' 
            + vaccination.Recurrance + ',' + vaccination.RecurranceTime + Environment.NewLine);
        }
    }

}



