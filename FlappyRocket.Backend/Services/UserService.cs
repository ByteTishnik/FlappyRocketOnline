public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User? GetUserByName(string username)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username);
    }

    public bool Register(string username , string password)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Username == username);

        if(existingUser != null)
        {
            return false;
        }

        var user = new User
        {
          Username = username,
          PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return true;
    }

    public User? Login(string username , string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);

        if(user == null)
        {
            return null;
        }

        bool valid = BCrypt.Net.BCrypt.Verify(password , user.PasswordHash);

        if (!valid)
        {
            return null;
        }

        return user;
    }

    public bool DeleteUser(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserID == id);

        if(user == null)
        {
            return false;
        }

        _context.Users.Remove(user);
        _context.SaveChanges();

        return true;
    }
}