using System.Security.Claims;


public class LeaderboardService
{
    private readonly AppDbContext _context;

    public LeaderboardService(AppDbContext context)
    {
        _context = context;
    }

    public List<LeaderboardEntryDto> GetScores(Difficulty difficulty)
    {
        return _context.Scores.Where(x => x.Difficulty == difficulty).OrderByDescending(x => x.Score).Select(x => new LeaderboardEntryDto
        {
            Username = x.User.Username,
            Score = x.Score,
            Date = x.Date,
            Difficulty = x.Difficulty
        }).ToList();
    }

    public void AddScore(AddScoreRequest request , int userId)
    {

       var scoreEntry = new ScoreEntry
       {
         UserID = userId,
         Score = request.Score,
         Difficulty = request.Difficulty,
         Date = DateTime.UtcNow,  
       };

       var existingRecord = _context.Scores.FirstOrDefault(x => x.UserID == userId && x.Difficulty == request.Difficulty);

       if(existingRecord == null)
        {
            _context.Scores.Add(scoreEntry);
        }
        else
        {
            if(request.Score > existingRecord.Score)
            {
                existingRecord.Score = request.Score;
                existingRecord.Date = DateTime.Now;
            }
        }

       _context.SaveChanges();

    }
}