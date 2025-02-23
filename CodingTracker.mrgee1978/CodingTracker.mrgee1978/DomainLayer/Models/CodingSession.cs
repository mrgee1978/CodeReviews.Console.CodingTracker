namespace CodingTracker.mrgee1978.DomainLayer.Models;

// A simple class that will be used to store all 
// necessary information about each coding session
public class CodingSession
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan Duration { get; set; }
}
