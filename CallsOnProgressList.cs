using System;
using System.Collections.Generic;
using System.Timers;

public class CallsOnProgressList : List<Call>
{
    private class CheckEmptyListTask
    {
        private CallsOnProgressList _list;

        public CheckEmptyListTask(CallsOnProgressList list)
        {
            _list = list;
        }

        public void Run()
        {
            try
            {
                List<Call> callsToRemove = new List<Call>();

                if (_list.Count > 0)
                {
                    foreach (Call call in _list)
                    {
                        if (call.GetAdmittedTimestamp() != 0
                            && call.GetAdmittedTimestamp() + call.GetDuration() <= DateTime.Now.Ticks)
                        {
                            callsToRemove.Add(call);
                        }
                    }

                    // Remove the calls outside of the iteration
                    foreach (Call callToRemove in callsToRemove)
                    {
                        _list.Remove(callToRemove);
                        callToRemove.GetFromLine().SetState("idle");
                        callToRemove.GetToLine().SetState("idle");
                        Console.WriteLine("\n>>" + DateTime.Now + "Call terminated from progress " + callToRemove);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public CallsOnProgressList()
    {
        Timer timer = new Timer(100);
        timer.Elapsed += (sender, e) => new CheckEmptyListTask(this).Run();
        timer.Start();
    }
}
