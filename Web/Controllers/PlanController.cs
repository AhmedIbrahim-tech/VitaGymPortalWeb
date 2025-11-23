namespace Web.Controllers;

[Authorize(Roles = "SuperAdmin")]
public class PlanController(IPlanService _planService, IToastNotification _toastNotification) : Controller
{
    #region Get Plans

    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var plans = await _planService.GetAllPlansAsync(cancellationToken);

        ViewData["Title"] = "Plans";

        if (plans == null || !plans.Any())
        {
            ViewData["EmptyIcon"] = "inbox";
            ViewData["EmptyTitle"] = "No Plans Available";
            ViewData["EmptyMessage"] = "Create your first membership plan to get started";
        }

        return View(plans);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            _toastNotification.AddErrorToastMessage("Plan ID can Not be Zero or Negative!");
            return RedirectToAction(nameof(Index));
        }

        var plan = await _planService.GetPlanByIdAsync(id, cancellationToken);
        if (plan == null)
        {
            _toastNotification.AddErrorToastMessage("Plan Not Found!");
            return RedirectToAction(nameof(Index));
        }

        return View(plan);
    }

    #endregion

    #region Update Plan

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken = default)
    {
        var plan = await _planService.GetPlanToUpdateAsync(id, cancellationToken);
        if (plan == null)
        {
            _toastNotification.AddErrorToastMessage("Plan Can not be Edited!");
            return RedirectToAction(nameof(Index));
        }

        return View(plan);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromRoute] int id, UpdatePlanViewModel input, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return View(input);
        }

        bool updatePlan = await _planService.UpdatePlanAsync(id, input, cancellationToken);

        if (updatePlan)
            _toastNotification.AddSuccessToastMessage("Plan Updated Successfully!");
        else
            _toastNotification.AddErrorToastMessage("Plan Failed To Update!");

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Activate/Deactivate Plan

    [HttpPost]
    public async Task<IActionResult> Activate(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            _toastNotification.AddErrorToastMessage("Plan ID can Not be Zero or Negative!");
            return RedirectToAction(nameof(Index));
        }

        var activate = await _planService.ActivateAsync(id, cancellationToken);

        if (activate)
            _toastNotification.AddSuccessToastMessage("Plan Activated Successfully!");
        else
            _toastNotification.AddErrorToastMessage("Plan Failed To Activate!");

        return RedirectToAction(nameof(Index));
    }

    #endregion
}
