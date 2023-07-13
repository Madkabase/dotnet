using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.Controllers;

// controller that serves the UI

[Route("/ui")]
[Controller]
public class Uicontroller : ControllerBase
{
    public Uicontroller()
    {
    }

    // serves reset password page with image and css
    [HttpGet]
    [Route("reset-password")]
    public IActionResult ResetPassword()
    {
        return File("reset-password.html", "text/html");
    }
}