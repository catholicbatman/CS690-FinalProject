using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace main;

public class Medication
{
    public string Name { get; }
    public int AdministrationTimes { get; } 

    public Medication(string name, int administrationTimes)
    {
        this.Name = name;
        this.AdministrationTimes = administrationTimes;
    }

    public override string ToString()
    {
        return this.Name;
    }
}

public class MedicationLog
{
    public List<Medication> Meds { get; }

    public MedicationLog()
    {
    this.Meds = new List<Medication>();     
    }

    public void AddMedication(Medication medication)
    {
        MedicationLog.Add(medication);
        SynchronizeMedications;
    }

    public void RemoveMedication(Medication medication)
    {
        MedicationLog.Remove(medication);
        SynchronizeMedications();
    }


    public void SynchronizeMedications()
    {
        if(File.Exists("Medication_List.txt")){
            File.Delete("Medication_List.txt");
        }

        foreach (Medication medication in medicationLog.Meds)
        {
            File.AppendAllText("Medication_List.txt", medication.Name+','+ medication.AdministrationTimes);
        }
    }
    /*public override string ToString()
    {
        return this.Meds;
    }*/

}




/*public class MedData
{
    public string name
}*/