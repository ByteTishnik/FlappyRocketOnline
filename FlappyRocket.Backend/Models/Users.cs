public class User
{
    public int UserID {get; set;}
    public string PasswordHash {get; set;} = string.Empty;
    public string Username  {get; set;} = string.Empty;
    public List<ScoreEntry> Scores {get; set;} = new();
}