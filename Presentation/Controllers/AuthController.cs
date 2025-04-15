using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { accessToken = "sample-token", role = "admin" });
    }
}