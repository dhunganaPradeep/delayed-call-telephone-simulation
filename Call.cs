using System;
using System.Collections.Generic;

public class Call
{
    private Line fromLine = null;
    private Line toLine = null;
    private Random random;
    private long duration;
    private long admittedTimestamp; //timestamp when the call is connected with the linea
    private long arrivalTimestamp;

    public long GetArrivalTimestamp()
    {
        return arrivalTimestamp;
    }

    public long GetDuration()
    {
        return duration;
    }

    public Line GetFromLine()
    {
        return fromLine;
    }

    public void SetFromLine(Line fromLine)
    {
        this.fromLine = fromLine;
    }

    public Line GetToLine()
    {
        return this.toLine;
    }

    public void SetToLine(Line toLine)
    {
        this.toLine = toLine;
    }

    public void SetAdmittedTimestamp(long a)
    {
        this.admittedTimestamp = a;
    }

    public long GetAdmittedTimestamp()
    {
        return this.admittedTimestamp;
    }

    //Constructor definition
    public Call(Line[] line, int maxTalkTime)
    {
        random = new Random();
        do
        {
            this.fromLine = line[random.Next(line.Length)]; //random available line selected for the call
            this.toLine = line[random.Next(line.Length)];
        } while (this.toLine.Equals(this.fromLine));
        DateTime date = DateTime.Now;
        arrivalTimestamp = date.Ticks; //This gives current system timestamp
        this.duration = 1000 * (long)random.Next(maxTalkTime); //select random duration for the call
        this.admittedTimestamp = 0;
    }

    //This method will be called by a call in order to connect to some lines
    public int Connect(List<Call> callsInProgress, List<Call> delayedCalls)
    {
        if (fromLine.GetState().Equals("busy") || toLine.GetState().Equals("busy") || callsInProgress.Count >= TelephoneCallSimulation.NUMBER_OF_LINKS)
        {
            Console.WriteLine(">>" + DateTime.Now + "Added to delay: " + this); //printing the log
            delayedCalls.Add(this);
            return -1;
        }
        else
        {
            this.admittedTimestamp = DateTime.Now.Ticks;
            callsInProgress.Add(this);
            Console.WriteLine(">>" + DateTime.Now + "Added to Progress: " + this);
            fromLine.SetState("busy");
            toLine.SetState("busy");
            return 1;
        }
    }

    public override string ToString()
    {
        if (this.fromLine.Equals(null) || this.toLine.Equals(null))
        {
            return "Call can't be connected";
        }
        else
        {
            return "Call From " + this.fromLine.GetID() + " To " + this.toLine.GetID() + " Arrived at " + new DateTime(this.arrivalTimestamp) + " for duration " + (duration / 1000) + ((this.admittedTimestamp != 0) ? " Admitted at " + (new DateTime(this.admittedTimestamp)) : " not admitted ");
        }
    }
}
