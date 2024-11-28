using Microsoft.AspNetCore.Mvc;
using SafePass.Data;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("validate")]
    public async Task<IActionResult> Validate([FromBody] LoginRequest loginRequest)
    {
        var user = await _userService.ValidateUserAsync(loginRequest.UserName, loginRequest.Password);
        if (user != null)
        {
            return Ok(user); // Return user details or a token
        }
        else
        {
            return Unauthorized("Invalid username or password.");
        }
    }
}

public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
