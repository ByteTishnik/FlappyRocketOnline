using System.Security.Claims;


public class LeaderboardService
{
    private readonly AppDbContext _context;

    public LeaderboardService(AppDbContext context)
    {
        _context = context;
    }

    public List<ScoreEntry> GetScores()
    {
        return _context.Scores.OrderByDescending(x => x.Score).ToList();
    }

    public void AddScore(ScoreEntry score)
    {
        _context.Scores.Add(score);
        _context.SaveChanges();
    }
}