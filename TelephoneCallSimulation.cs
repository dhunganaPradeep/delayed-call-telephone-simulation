using System;
using System.Collections.Generic;
using System.Timers;

public class TelephoneCallSimulation
{
    private static int numOfLines = 8;
    public static int NUMBER_OF_LINKS = 3;
    private static int maxTalkTime = 20;

    static CallsOnProgressList callsInProgress;
    public static List<Call> delayedCallList;

    static Line[] line;
    static Timer timer = new Timer();

public class GenerateRandomCall
{
    private static Random random = new Random();
    private Timer timer;

    public GenerateRandomCall()
    {
        timer = new Timer();
        timer.Elapsed += (sender, e) => GenerateCall();
    }

    public void Run()
    {
        // Create a random delay between 5 and 20 seconds
        int delaySeconds = random.Next(5, 20);
        int delayMilliseconds = delaySeconds * 1000;

        timer.Interval = delayMilliseconds;
        timer.Start();
    }

    private void GenerateCall()
    {
        timer.Stop(); // Stop the timer while generating the call

        // Create a random call
        Call call;
        do
        {
            call = new Call(line, maxTalkTime);
        } while (call.GetFromLine().GetState() == "busy");

        Console.WriteLine("\n\n\n\nCURRENT TIME: " + DateTime.Now);
        Console.WriteLine("\n>> A call came from: " + call.GetFromLine().GetID() + " TO " + call.GetToLine().GetID() + " at " + DateTime.Now + " which has duration " + call.GetDuration() / 1000);

        // Connect to a call
        call.Connect(callsInProgress, delayedCallList);
        PrintLists();

        // Schedule the next call after processing the current one
        Run();
    }
}

    static void PrintLists()
    {
        Console.WriteLine("\nIn progress List: ");
        foreach (Call c in callsInProgress)
        {
            Console.WriteLine((Call)c);
        }
        Console.WriteLine("\nDelayed List: ");
        foreach (Call c in delayedCallList)
        {
            Console.WriteLine(c);
        }

        int delay = (1 + new Random().Next(10)) * 1000;
        timer.Interval = delay;
        timer.Elapsed += (sender, e) => new GenerateRandomCall().Run();
        timer.Start();
    }

    public static void Main(string[] args)
    {
        // Create 8 idle lines
        line = new Line[numOfLines];
        for (int i = 0; i < numOfLines; i++)
        {
            line[i] = new Line();
        }

        // Creating a list of calls as 'Call in Progress'
        callsInProgress = new CallsOnProgressList();

        // Creating a list for delayed calls
        delayedCallList = new List<Call>();

        new GenerateRandomCall().Run();
    }
}
