namespace Web.Controllers;

[AllowAnonymous]
public class ErrorController : Controller
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    #region Error Pages

    [Route("/Error/404")]
    public IActionResult NotFound()
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        var model = new ErrorViewModel
        {
            StatusCode = 404,
            Title = "Page Not Found",
            Message = "The page you are looking for doesn't exist or has been moved.",
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(model);
    }

    [Route("/Error/401")]
    public IActionResult Unauthorized()
    {
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        var model = new ErrorViewModel
        {
            StatusCode = 401,
            Title = "Unauthorized Access",
            Message = "You are not authorized to access this resource. Please log in to continue.",
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(model);
    }

    [Route("/Error/403")]
    public IActionResult Forbidden()
    {
        Response.StatusCode = StatusCodes.Status403Forbidden;
        var model = new ErrorViewModel
        {
            StatusCode = 403,
            Title = "Access Forbidden",
            Message = "You don't have permission to access this resource.",
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(model);
    }

    [Route("/Error/400")]
    public IActionResult BadRequest()
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        var model = new ErrorViewModel
        {
            StatusCode = 400,
            Title = "Bad Request",
            Message = "The request is invalid or malformed.",
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(model);
    }

    [Route("/Error/500")]
    public IActionResult Error()
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        
        var exception = HttpContext.Items["Exception"] as Exception;
        var statusCode = HttpContext.Items["StatusCode"] as int? ?? 500;

        if (exception != null)
        {
            _logger.LogError(exception, "Error occurred. Request ID: {RequestId}", HttpContext.TraceIdentifier);
        }

        var model = new ErrorViewModel
        {
            StatusCode = statusCode,
            Title = "Internal Server Error",
            Message = "An error occurred while processing your request. We apologize for the inconvenience.",
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            Exception = _logger.IsEnabled(LogLevel.Debug) ? exception : null
        };

        return View(model);
    }

    #endregion
}

