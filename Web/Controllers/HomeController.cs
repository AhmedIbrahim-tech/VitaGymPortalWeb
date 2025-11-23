namespace Web.Controllers;

[Authorize]
public class HomeController(IAnalaticalService _analaticalService) : Controller
{
    #region Dashboard

    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var model = await _analaticalService.GetAnalaticalDataAsync(cancellationToken);
        return View(model);
    }

    #endregion
}
