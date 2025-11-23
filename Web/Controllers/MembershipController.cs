namespace Web.Controllers;

[Authorize]
public class MembershipController(IMembershipService _membershipService, IToastNotification _toastNotification) : Controller
{
    #region Get Memberships

    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var memberships = await _membershipService.GetAllMemberShipsAsync(cancellationToken);

        ViewData["Title"] = "Memberships";
        ViewData["PageTitle"] = "Active Memberships";
        ViewData["PageSubtitle"] = "Members that have active plans";
        ViewData["PageIcon"] = "card-checklist";
        ViewData["ShowActionButton"] = true;
        ViewData["ActionButtonText"] = "New Membership";
        ViewData["ActionButtonUrl"] = Url.Action("Create");

        if (memberships == null || !memberships.Any())
        {
            ViewData["EmptyIcon"] = "card-checklist";
            ViewData["EmptyTitle"] = "No Active Memberships Available";
            ViewData["EmptyMessage"] = "No active memberships found";
        }

        return View(memberships);
    }

    #endregion

    #region Create Membership

    public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
    {
        await LoadDropdownsAsync(cancellationToken);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberShipViewModel model, CancellationToken cancellationToken = default)
    {
        if (ModelState.IsValid)
        {
            var result = await _membershipService.CreateMembershipAsync(model, cancellationToken);

            if (result)
            {
                _toastNotification.AddSuccessToastMessage("Membership created successfully!");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Failed to create membership. Member already has an active membership.");
            }
        }

        await LoadDropdownsAsync(cancellationToken);
        return View(model);
    }

    #endregion

    #region Cancel Membership

    [HttpPost]
    public async Task<IActionResult> Cancel(int id, CancellationToken cancellationToken = default)
    {
        var result = await _membershipService.DeleteMemberShipAsync(id, cancellationToken);

        if (result)
        {
            _toastNotification.AddSuccessToastMessage("Membership cancelled successfully!");
        }
        else
        {
            _toastNotification.AddErrorToastMessage("Failed to cancel membership.");
        }

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Helper Methods

    private async Task LoadDropdownsAsync(CancellationToken cancellationToken = default)
    {
        var members = await _membershipService.GetMembersForDropDownAsync(cancellationToken);
        var plans = await _membershipService.GetPlansForDropDownAsync(cancellationToken);

        ViewBag.members = new SelectList(members, "Id", "Name");
        ViewBag.plans = new SelectList(plans, "Id", "Name");
    }

    #endregion
}
