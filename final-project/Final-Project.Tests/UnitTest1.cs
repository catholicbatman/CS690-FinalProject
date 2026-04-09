namespace Final_Project.Tests;

using main;

public class FileSaverTests
{

    FileSaver fileSaver;
    string testFileName;

    public FileSaverTests(){
        testFileName = "test.txt";
        fileSaver = new FileSaver(testFileName);
    }

    [Fact]
    public void Test1()
    {
        fileSaver.AppendLine("This is a test.");
        string[] contents = File.ReadAllText(testFileName);
        Assert.Equal("This is a test." +Environment.NewLine, contents);
    }
}
