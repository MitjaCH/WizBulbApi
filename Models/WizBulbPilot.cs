namespace WizBulbApi.Models;

public class WizBulbPilot
{
    public bool State { get; set; }
    public int Dimming { get; set; }
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
    public int ColorTemp { get; set; }
}