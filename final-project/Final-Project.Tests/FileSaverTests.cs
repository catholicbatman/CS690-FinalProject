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
        Assert.True(File.Exists(testFileName));
    }

    [Fact]
    public void Test2()
    {
        fileSaver.DeleteFile();
        Assert.True(!File.Exists(testFileName));
    }


    [Fact]
    public void Test3()
    {
        fileSaver.AppendLine("This is a test.");
        string contents = File.ReadAllText(testFileName);
        Assert.Equal("This is a test." +Environment.NewLine, contents);
    }

}
