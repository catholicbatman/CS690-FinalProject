namespace Final_Project.Tests;

using main;

public class LogCreatorTests
{
    LogCreator logCreator;
    FileSaver fileSaver;
    string testFileName;

    public LogCreatorTests(){
        testFileName = "test.txt";
        logCreator = new LogCreator();
        fileSaver = new FileSaver(testFileName);
        fileSaver.AppendLine("Nail Fungus Cream,4");
        fileSaver.AppendLine("Boogers,3");
        MedicationLog testMedLog = new MedicationLog();    
    }

    [Fact]
    public void MedicationsFileTest()
    {
        MedicationLog testMedLog = logCreator.readMedicationInfoFromFile(testFileName);
        Medication testMed = new Medication("Nail Fungus Cream",4);
        Assert.Contains(testMed,testMedLog.Meds);
    }

    [Fact]
    public void Test2()
    {
        
    }


}
