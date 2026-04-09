namespace Final_Project.Tests;

using main;

public class AnalyzerTests
{

    Analyzer analyzer;
    WalkRecord allWalkRecord;
    Walk walk1,walk2,walk3,walk4,walk5,walk6,walk7,walk8;
    DateTime fakeDateTime;
    TimeSpan fakeTimeSpan;

    public AnalyzerTests(){
        DateTime dateToday = DateTime.Now;
        DateTime dateYesterday = DateTime.Now.AddDays(-1);
        analyzer = new Analyzer();
        allWalkRecord = new WalkRecord();
        fakeDateTime = new DateTime(1111,1,1,1,1,1);
        fakeTimeSpan = new TimeSpan(0,0,0);
        walk1 = new Walk(dateToday.Now,fakeTimeSpan);
        walk2 = new Walk(fakeDateTime,fakeTimeSpan);
        walk3 = new Walk(fakeDateTime,fakeTimeSpan);
        walk4 = new Walk(fakeDateTime,fakeTimeSpan);
        walk5 = new Walk(fakeDateTime,fakeTimeSpan);
        walk6 = new Walk(fakeDateTime,fakeTimeSpan);
        walk7 = new Walk(fakeDateTime,fakeTimeSpan);
        walk8 = new Walk(fakeDateTime,fakeTimeSpan);
        walk1.Date = new DateTime(2026,4,9,10,0,0);
        walk1.WalkTime = new TimeSpan(0,10,0);
        walk2.Date = new DateTime(2026,4,5,10,0,0);
        walk2.WalkTime = new TimeSpan(0,10,0);
        allWalkRecord.AddWalk(walk1);
        allWalkRecord.AddWalk(walk2);
        allWalkRecord.AddWalk(walk3);
        allWalkRecord.AddWalk(walk4);
        allWalkRecord.AddWalk(walk5);
        allWalkRecord.AddWalk(walk6);
        allWalkRecord.AddWalk(walk7);
        allWalkRecord.AddWalk(walk8);     
    }

    [Fact]
    public void Test1()
    {
        
    }


}