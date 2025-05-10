using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Patikadev_RestfulApi.Interfaces;

namespace Patikadev_RestfulApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var apiKey = _authService.GenerateApiKey(request.Username, request.Password);

        if (apiKey == null)
            return Unauthorized(new { message = "Invalid username or password" });

        return Ok(new { apiKey });
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}