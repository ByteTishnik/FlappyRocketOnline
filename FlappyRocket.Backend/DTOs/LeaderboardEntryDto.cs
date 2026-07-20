public class LeaderboardEntryDto
{
    public string Username {get; set;} = "";
    public int Score {get; set;}
    public DateTime Date {get; set;}
    public Difficulty Difficulty {get; set;}
}