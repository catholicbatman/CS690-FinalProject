namespace main;


public class Analyzer{

    
    
    public Analyzer(){



    }
    

    public List<Walk> getWeeklyWalks(WalkRecord walkRecord){

        List<Walk> weeklyWalks = new List<Walk>();

        foreach(Walk walk in walkRecord.Walks)
        {

            //This sets the day of the week today (from Sunday to Saturday) as an integer
            int dayOfWeekToday = Convert.ToInt32(DateTime.Now.DayOfWeek);
            //This variable is the exact number of the day of the month (from 1-31)
            int dateToday = DateTime.Now.Day;
            //Creating integer variables of the past 6 days (what numbered day of the month) from the date today.
            DateTime oneDayAgo = DateTime.Now.AddDays(-1);
            int dateOneDayAgo = oneDayAgo.Day;
            DateTime twoDaysAgo = DateTime.Now.AddDays(-2);
            int dateTwoDaysAgo = twoDaysAgo.Day;
            DateTime threeDaysAgo = DateTime.Now.AddDays(-3);
            int dateThreeDaysAgo = threeDaysAgo.Day;
            DateTime fourDaysAgo = DateTime.Now.AddDays(-4);
            int dateFourDaysAgo = fourDaysAgo.Day;
            DateTime fiveDaysAgo = DateTime.Now.AddDays(-5);
            int dateFiveDaysAgo = fiveDaysAgo.Day;
            DateTime sixDaysAgo = DateTime.Now.AddDays(-6);
            int dateSixDaysAgo = sixDaysAgo.Day;
            //finds the last month, which is necessary because sometimes the date early in the month
            //will have a week that began the prior month and needs to be taken into account for producing
            //a weekly list of walks
            DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);
            int dateOneMonthAgo = oneMonthAgo.Month;
            
            //This conditional checks to see if they year and month of each walk are aligned with
            //the current year and month.
            if (walk.Date.Year == DateTime.Now.Year && (walk.Date.Month == DateTime.Now.Month 
            | (walk.Date.Month == dateOneMonthAgo && walk.Date.Day > 23)))
            {
                //All of the conditionals here check if the current day is a certain day of the week and then
                //checks to see if the walk date day matches that or any day that is a part of the week (possibly
                //one to six days prior. Together the conditionals ultimately print the walks of the current week)
                //starting on Sunday.
                if(dayOfWeekToday == 0 && walk.Date.Day == dateToday)
                {
                    weeklyWalks.Add(walk);
                }

                if(dayOfWeekToday == 1 && (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo))
                {
                    weeklyWalks.Add(walk);
                }

                if(dayOfWeekToday == 2 && 
                (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo))
                {
                    weeklyWalks.Add(walk);
                }

                if(dayOfWeekToday == 3 && 
                (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo | walk.Date.Day == dateThreeDaysAgo))
                {
                    weeklyWalks.Add(walk);
                }

                if(dayOfWeekToday == 4 && 
                (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo | walk.Date.Day == dateThreeDaysAgo
                | walk.Date.Day == dateFourDaysAgo))
                {
                    weeklyWalks.Add(walk);
                }

                if(dayOfWeekToday == 5 && 
                (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo | walk.Date.Day == dateThreeDaysAgo
                | walk.Date.Day == dateFourDaysAgo | walk.Date.Day == dateFiveDaysAgo))
                {
                    weeklyWalks.Add(walk);
                }

                if(dayOfWeekToday == 6 && 
                (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo | walk.Date.Day == dateThreeDaysAgo
                | walk.Date.Day == dateFourDaysAgo | walk.Date.Day == dateFiveDaysAgo | walk.Date.Day == dateSixDaysAgo))
                {
                    weeklyWalks.Add(walk);
                }
            }
        
        }
        return weeklyWalks;
    }


}   


/* my first iteration of the weekly walk algorithm...
    int walkNumber = 1;
        foreach(Walk walk in walkRecord.Walks)
        {

            //This sets the day of the week today (from Sunday to Saturday) as an integer
            int dayOfWeekToday = Convert.ToInt32(DateTime.Now.DayOfWeek);
            //This variable is the exact number of the day of the month (from 1-31)
            int dateToday = DateTime.Now.Day;
            //Creating integer variables of the past 6 days (what numbered day of the month) from the date today.
            DateTime oneDayAgo = DateTime.Now.AddDays(-1);
            int dateOneDayAgo = oneDayAgo.Day;
            DateTime twoDaysAgo = DateTime.Now.AddDays(-2);
            int dateTwoDaysAgo = twoDaysAgo.Day;
            DateTime threeDaysAgo = DateTime.Now.AddDays(-3);
            int dateThreeDaysAgo = threeDaysAgo.Day;
            DateTime fourDaysAgo = DateTime.Now.AddDays(-4);
            int dateFourDaysAgo = fourDaysAgo.Day;
            DateTime fiveDaysAgo = DateTime.Now.AddDays(-5);
            int dateFiveDaysAgo = fiveDaysAgo.Day;
            DateTime sixDaysAgo = DateTime.Now.AddDays(-6);
            int dateSixDaysAgo = sixDaysAgo.Day;
            //finds the last month, which is necessary because sometimes the date early in the month
            //will have a week that began the prior month and needs to be taken into account for producing
            //a weekly list of walks
            DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);
            int dateOneMonthAgo = oneMonthAgo.Month;
            
            //This conditional checks to see if they year and month of each walk are aligned with
            //the current year and month.
            if (walk.Date.Year == DateTime.Now.Year && (walk.Date.Month == DateTime.Now.Month 
            | (walk.Date.Month == dateOneMonthAgo && walk.Date.Day > 23)))
            {
                //All of the conditionals here check if the current day is a certain day of the week and then
                //checks to see if the walk date day matches that or any day that is a part of the week (possibly
                //one to six days prior. Together the conditionals ultimately print the walks of the current week)
                //starting on Sunday.
                if(dayOfWeekToday == 0 && walk.Date.Day == dateToday)
                {
                    AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week[/]");
                    Console.WriteLine(walk +Environment.NewLine);
                    walkNumber += 1;
                }

                if(dayOfWeekToday == 1 && (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo))
                {
                    AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week[/]");
                    Console.WriteLine(walk +Environment.NewLine);
                    walkNumber += 1;
                }

                if(dayOfWeekToday == 2 && 
                (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo))
                {
                    AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week (which started on Sunday)[/]");
                    Console.WriteLine(walk +Environment.NewLine);
                    walkNumber += 1;
                }

                if(dayOfWeekToday == 3 && 
                (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo | walk.Date.Day == dateThreeDaysAgo))
                {
                    AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week (which started on Sunday)[/]");
                    Console.WriteLine(walk +Environment.NewLine);
                    walkNumber += 1;
                }

                if(dayOfWeekToday == 4 && 
                (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo | walk.Date.Day == dateThreeDaysAgo
                | walk.Date.Day == dateFourDaysAgo))
                {
                    AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week (which started on Sunday)[/]");
                    Console.WriteLine(walk +Environment.NewLine);
                    walkNumber += 1;
                }

                if(dayOfWeekToday == 5 && 
                (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo | walk.Date.Day == dateThreeDaysAgo
                | walk.Date.Day == dateFourDaysAgo | walk.Date.Day == dateFiveDaysAgo))
                {
                    AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week (which started on Sunday)[/]");
                    Console.WriteLine(walk +Environment.NewLine);
                    walkNumber += 1;
                }

                if(dayOfWeekToday == 6 && 
                (walk.Date.Day == dateToday | walk.Date.Day == dateOneDayAgo | walk.Date.Day == dateTwoDaysAgo | walk.Date.Day == dateThreeDaysAgo
                | walk.Date.Day == dateFourDaysAgo | walk.Date.Day == dateFiveDaysAgo | walk.Date.Day == dateSixDaysAgo))
                {
                    AnsiConsole.MarkupLine("[green]Walk " + walkNumber + " this week (which started on Sunday)[/]");
                    Console.WriteLine(walk +Environment.NewLine);
                    walkNumber += 1;
                }*/