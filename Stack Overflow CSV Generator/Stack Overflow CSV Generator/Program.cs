using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Stack_Overflow_CSV_Generator
{
    class Program
    {
        //Constants pulled from the database
        const int MEAN = 2;
        const float STDEV = 12.77f;
        const int MULT = 1; //Number of Standard Deviations above the mean to take (For Score)
        const String Mode = "accepted"; //Accepted answers or Top Answers (relative to the mean)
        const int MEDIAN = 12253720 / 4; //12253720 is the median ID for accepted answers. But I have to use 8 partitions or I run Out of Memory.
        const int ACCEPTMULT = 6; //Which of the 8 partitions (1-8) to use for Accepted Answers
        const String FILE = "AcceptedAnswers_6.csv";
        static void Main(string[] args)
        {
            AnswersDataContext dc = new AnswersDataContext();
            dc.CommandTimeout = 600;
            Console.WriteLine("Starting at " + System.DateTime.Now.ToLongTimeString());
            //We want to create AnswerList now because the contents of that are the only difference
            //Between the Top and Accepted answers
            var answerList = from post in dc.Posts
                             where post.PostTypeId == 2
                             select post;

            switch (Mode)
            {
                case "accepted":
                    var acceptedAnswerIDs = from post in dc.Posts
                                            where post.AcceptedAnswerId > 0
                                            select post.AcceptedAnswerId;
                    //Find all posts who's ID is in the list of Accepted Answer IDs
                    //Then take the ones who's ID falls in the partition we're dealing with
                    answerList = from post in dc.Posts
                                 where acceptedAnswerIDs.Contains(post.Id)
                                 where post.Id < MEDIAN * ACCEPTMULT
                                 where post.Id > MEDIAN * (ACCEPTMULT-1)
                                 select post;
                    break;
                case "top":
                    //Just take all answers where the score is MULT Standard Deviations above the Mean
                    answerList = from post in dc.Posts
                                 where post.PostTypeId == 2
                                 where post.Score > MEAN + MULT * STDEV
                                 select post;
                    break;
                default:
                    printHelpAndExit();
                    break;
            }
            Console.WriteLine("Number of Answers: " + answerList.Count());
            System.IO.StreamWriter file = new System.IO.StreamWriter(FILE);
            foreach (var post in answerList)
            {
                int wordCount = getWordCount(post.Body);
                bool hasLink = hasALink(post.Body);
                bool hasCode = hasSomeCode(post.Body);
                int answererReputation = getPosterReputation((int)post.OwnerUserId, dc);
                int responseTime = getResponseTime(post.CreationDate, post.ParentId, dc);
                //Console.WriteLine(wordCount + "," + hasLink + "," + hasCode + "," + answererReputation + "," + responseTime);
                file.WriteLine(wordCount + "," + hasLink + "," + hasCode + "," + answererReputation + "," + responseTime);    
            }
            file.Flush();
            file.Close();
            answerList = null;
            Console.WriteLine("Press Any Key to exit. Completed at " + System.DateTime.Now.ToLongTimeString());
            Console.ReadKey();
            return;
        }

        static int getWordCount(String s)
        {
            return s.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }

        static bool hasALink(String s)
        {
            return s.Contains("<a href");
        }

        static bool hasSomeCode(String s)
        {
            return s.Contains("<code>");
        }

        static int getPosterReputation(int ownerID, AnswersDataContext dc)
        {
            if (ownerID < 1)
            {
                return 0;
            }
            var answerer = from s in dc.Users
                           where s.Id == ownerID
                           select s;
            if (answerer.Count() < 1) //Answerer does not exist
            {
                return 0;
            }
            return answerer.First().Reputation;
        }

        static int getResponseTime(DateTime postTime, int? questionID, AnswersDataContext dc)
        {
            if (questionID == null || questionID < 1) //Question does not exist
            {
                return -1;
            }
            var question = from s in dc.Posts
                           where s.Id == questionID
                           select s.CreationDate;
            if (question.Count() < 1) //Question does not exist
            {
                return -1;
            }
            DateTime questionTime = question.First();
            TimeSpan offset = postTime.Subtract(questionTime);
            return (int)offset.TotalMinutes;
        }

        //Ideally I convert this program over to a Command Line Argument based program
        //And then this becomes necessary
        //For me being lazy at the moment though, just running it through Visual Studio is fine.
        static void printHelpAndExit()
        {
            Console.WriteLine("How to use:");
            Console.WriteLine("StackOverflowCSV.exe top stdevs filename");
            Console.WriteLine("StackOverflowCSV.exe accepted filename");
            Console.WriteLine("StackOverflowCSV.exe help");
            Console.WriteLine("top tells the program to take the top-scoring answers, as determined by stdevs and the mean score");
            Console.WriteLine("accepted tells the program to take all Accepted Answers");
            Console.WriteLine("help displays this help message");
            Console.WriteLine("filename is the name of the file you want to save the output to. The file will automatically be given the .csv extension");
            Console.WriteLine("This program relies on using LINQ to query a SQL Server database containing the StackOverflow dump. As a C# program, it also relies on the .NET framework. If you do not have the database, you cannot use this program. If you have SQL server, but not the database, you can use SODDI to import the dump into SQL Server");
            Console.WriteLine("WARNING: This program is CPU- and Memory-intensive. You should expect it to take at least three hours (And likely more) to run because it must run statistics on a large number of StacKOverflow answers. My advice is to let it run overnight, especially for Accepted Answers");
            Console.WriteLine("For example, in the version of the dump that I have, there are 350,981 answers that are one Standard Deviation above the average score and 4,596,596 Accepted Answers. All of these answers must be processed individually. It takes time");
            Environment.Exit(0);
        }
    }
}
