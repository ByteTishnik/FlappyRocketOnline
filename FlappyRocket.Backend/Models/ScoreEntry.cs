public class ScoreEntry
{
    public int ScoreEntryID {get; set;}
    public int UserID {get; set;}
    public int Score {get; set;}
    public DateTime Date {get; set; }
    public User? User {get; set;}
}