public class UserSession
{
    public string Username {get; set;} = "";
    public string? Token {get; set;}
    public bool IsLoggedIn => !string.IsNullOrEmpty(Token);
}