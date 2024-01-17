using System;

public class Line
{
    private int id;
    private static int count = 0;
    private string state;

    public Line()
    {
        id = count;
        count++;
        this.state = "idle";
    }

    public string GetState()
    {
        return this.state;
    }

    public int GetID()
    {
        return this.id;
    }

    public void SetState(string state)
    {
        this.state = state;
    }

    public override string ToString()
    {
        return "Line ID: " + id + ", in state: " + state;
    }
}
