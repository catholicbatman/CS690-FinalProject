using System.Diagnostics;

namespace main;

public class Medication
{
    public string Name { get; }
    public int AdministrationTimes { get; } 

    public Medication(string name, int administrationTimes)
    {
        this.Name = name;
        this.AdminstrationTimes = administrationTimes;
    }

    /*public override string ToString()
    {
        return this.Name;
    }*/
}

public class MedicationLog
{
    public List<Medication> Meds { get; }

    public MedicationLog()
    {
    this.Meds = new List<Medication>();     
    }

}