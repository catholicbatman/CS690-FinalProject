namespace Final_Project.Tests;

using main;

public class AnalyzerTests
{

    Analyzer analyzer;
    WalkRecord allWalkRecord;
    List<Walk>? thisWeeksWalks;
    Walk walk1,walk2,walk3,walk4,walk5,walk6,walk7,walk8,walk9,walk10;
    TimeSpan fakeTimeSpan;
    int dateTodayInteger;

    public AnalyzerTests(){
        DateTime dateToday = DateTime.Now;
        dateTodayInteger = Convert.ToInt32(dateToday.DayOfWeek);
        DateTime dateFiveDaysAgo = dateToday.AddDays(-5);
        DateTime dateTwoDaysAgo = dateToday.AddDays(-2);
        DateTime dateYesterday = dateToday.AddDays(-1);
        DateTime dateFourDaysAgo = dateToday.AddDays(-4);
        DateTime dateSixDaysAgo = dateToday.AddDays(-6);
        DateTime dateSevenDaysAgo = dateToday.AddDays(-7);
        DateTime dateThreeDaysAgo = dateToday.AddDays(-3);
        DateTime dateTenDaysAgo = dateToday.AddDays(-10);
        DateTime dateEightDaysAgo = dateToday.AddDays(-8);
        analyzer = new Analyzer();
        allWalkRecord = new WalkRecord();
        fakeTimeSpan = new TimeSpan(0,0,0);
        walk1 = new Walk(dateToday,fakeTimeSpan);
        walk2 = new Walk(dateYesterday,fakeTimeSpan);
        walk3 = new Walk(dateTwoDaysAgo,fakeTimeSpan);
        walk4 = new Walk(dateThreeDaysAgo,fakeTimeSpan);
        walk5 = new Walk(dateFourDaysAgo,fakeTimeSpan);
        walk6 = new Walk(dateFiveDaysAgo,fakeTimeSpan);
        walk7 = new Walk(dateSixDaysAgo,fakeTimeSpan);
        walk8 = new Walk(dateSevenDaysAgo,fakeTimeSpan);
        walk9 = new Walk(dateEightDaysAgo,fakeTimeSpan);
        walk10 = new Walk(dateTenDaysAgo,fakeTimeSpan);
        allWalkRecord.AddWalk(walk1);
        allWalkRecord.AddWalk(walk2);
        allWalkRecord.AddWalk(walk3);
        allWalkRecord.AddWalk(walk4);
        allWalkRecord.AddWalk(walk5);
        allWalkRecord.AddWalk(walk6);
        allWalkRecord.AddWalk(walk7);
        allWalkRecord.AddWalk(walk8);
        allWalkRecord.AddWalk(walk9);
        allWalkRecord.AddWalk(walk10);
        thisWeeksWalks = new List<Walk>();
        thisWeeksWalks = analyzer.getWeeklyWalks(allWalkRecord);     
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal(dateTodayInteger+2,thisWeeksWalks.Count+1); 
    }
}

/*if (DateTime.Now.Day == 0){
            Assert.Single(thisWeeksWalks);
        }
        if (DateTime.Now.Day == 1){
            Assert.Equal(8,thisWeeksWalks.Count());
        }
        if (DateTime.Now.Day == 2){
            Assert.Equal(8,thisWeeksWalks.Count());
        }
        if (DateTime.Now.Day == 3){
            Assert.Equal(8,thisWeeksWalks.Count());
        }
        if (DateTime.Now.Day == 4){
            Assert.Equal(8,thisWeeksWalks.Count());
        }
        if (DateTime.Now.Day == 5){
            Assert.Equal(8,thisWeeksWalks.Count());
        }
        if (DateTime.Now.Day == 6){
            Assert.Equal(8,thisWeeksWalks.Count());
        }*/