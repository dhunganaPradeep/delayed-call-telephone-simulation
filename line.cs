using System;

public class Line
{
    private string state;
    private int id;

    public Line(int id)
    {
        this.id = id;
        this.state = "idle";
    }

    public int GetID()
    {
        return id;
    }

    public string GetState()
    {
        return state;
    }

    public void SetState(string state)
    {
        this.state = state;
    }

    // public override string ToString()
    // {
    //     return $"Line {id}: State - {state}";
    // }
}

// public class Program
// {
//     public static void Main()
//     {
//         // Create two Line instances for testing.
//         Line line1 = new Line(1);
//         Line line2 = new Line(2);

//         // Print the initial state of the lines.
//         Console.WriteLine(line1);
//         Console.WriteLine(line2);

//         // Set the state of the lines to "busy" for testing.
//         line1.SetState("busy");
//         line2.SetState("busy");

//         // Print the updated state of the lines after setting them to "busy".
//         Console.WriteLine(line1);
//         Console.WriteLine(line2);
//     }
// }
