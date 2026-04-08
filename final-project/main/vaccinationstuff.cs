namespace main;

using System.IO;

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
        return (this.Type + " vaccination given on " + this.Date);
    }
}

public class VaccinationLog
{
    FileSaver vacFileSaver = new FileSaver("Vaccination_Log.txt");

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
        vacFileSaver.DeleteFile();

        foreach (Vaccination vaccination in this.Vaccines)
        {
            vacFileSaver.AppendLine(vaccination.Type + ',' + vaccination.Date + ',' 
            + vaccination.Recurrance + ',' + vaccination.RecurranceTime);
        }

    }

}



