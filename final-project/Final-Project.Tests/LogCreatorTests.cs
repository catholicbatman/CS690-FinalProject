namespace Final_Project.Tests;

using main;
using System.Linq;

public class LogCreatorTests
{
    LogCreator logCreator;
    FileSaver? fileSaver;

    public LogCreatorTests()
    {
        logCreator = new LogCreator();
    }

    [Fact]
    public void MedicationsFileTest1()
    {
        string testFileName = "testMeds.txt";
        fileSaver = new FileSaver(testFileName);
        fileSaver.DeleteFile();
        fileSaver = new FileSaver(testFileName);
        fileSaver.AppendLine("Nail Fungus Cream,3");
        fileSaver.AppendLine("Vitamin,1");
        fileSaver.AppendLine("Benadryl,2");
        MedicationLog testMedLog = logCreator.readMedicationInfoFromFile(testFileName);
        Medication testMed = new Medication("Nail Fungus Cream",3);
        Assert.Contains(testMed.Name,testMedLog.Meds[0].Name);
        int medCount = testMedLog.Meds.Count();

        Assert.Equal(3,medCount);
    }

    [Fact]
    public void WalkFileTest()
    {
        string testFileName = "testWalks.txt";
        fileSaver = new FileSaver(testFileName);
        fileSaver.AppendLine("04/05/2026 20:17:32 01:10:00");
        WalkRecord testWalkRecord = logCreator.readWalkInfoFromFile(testFileName);
        Walk testWalk = new Walk(new DateTime(2026,4,5,20,17,32),new TimeSpan(1,10,0));
        Assert.True(testWalkRecord.Walks.Any(result => result.Date == testWalk.Date && result.WalkTime == testWalk.WalkTime));
    }

}
