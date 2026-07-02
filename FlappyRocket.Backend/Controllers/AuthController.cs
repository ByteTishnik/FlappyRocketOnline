using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly JwtService _jwtservice;

    public AuthController(UserService userService , JwtService jwtService)
    {
        _userService = userService;
        _jwtservice = jwtService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        bool success = _userService.Register(request.Username , request.Password);

        if (!success)
        {
            return BadRequest("User already exists");
        }

        return Ok("User registered succssesfully");

    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var user = _userService.Login(request.Username , request.Password);

        if(user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        string token = _jwtservice.GenerateToken(user);

        return Ok(new
        {
            accessToken = token
        });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var deletedUser = _userService.DeleteUser(id);

        if (!deletedUser)
        {
            return NotFound();
        }

        return NoContent();
    }
}