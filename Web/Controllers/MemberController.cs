namespace Web.Controllers;

[Authorize]
public class MemberController(IMemberService _memberService, IToastNotification _toastNotification) : Controller
{
    #region Get Members

    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var members = await _memberService.GetAllMembersAsync(cancellationToken);

        ViewData["Title"] = "Members";
        ViewData["PageTitle"] = "Members List";
        ViewData["PageSubtitle"] = "Manage your gym members";
        ViewData["PageIcon"] = "people";
        ViewData["ShowActionButton"] = true;
        ViewData["ActionButtonText"] = "Add Member";
        ViewData["ActionButtonUrl"] = Url.Action("Create");
        ViewData["ActionButtonIcon"] = "person-plus";

        if (members == null || !members.Any())
        {
            ViewData["EmptyIcon"] = "people";
            ViewData["EmptyTitle"] = "No Members Available";
            ViewData["EmptyMessage"] = "Add your first member to get started";
        }

        return View(members);
    }

    public async Task<IActionResult> MemberDetails(int id, CancellationToken cancellationToken = default)
    {
        var member = await _memberService.GetMemberDetailsAsync(id, cancellationToken);
        if (member == null)
        {
            _toastNotification.AddErrorToastMessage("Member Not Found!");
            return RedirectToAction(nameof(Index));
        }
        return View(member);
    }

    public async Task<IActionResult> HealthRecordDetails(int id, CancellationToken cancellationToken = default)
    {
        var healthRecord = await _memberService.GetMemberHealthRecordAsync(id, cancellationToken);
        if (healthRecord == null)
        {
            _toastNotification.AddErrorToastMessage("Health Record is Not Found!");
            return RedirectToAction(nameof(Index));
        }
        return View(healthRecord);
    }

    #endregion

    #region Create Member

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMember(CreateMemberViewModel input, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            _toastNotification.AddErrorToastMessage("Please check the form and fix any validation errors.");
            return View(nameof(Create), input);
        }

        if (input.FormFile == null || input.FormFile.Length == 0)
        {
            ModelState.AddModelError(nameof(input.FormFile), "Profile photo is required.");
            _toastNotification.AddErrorToastMessage("Profile photo is required.");
            return View(nameof(Create), input);
        }

        bool createMember = await _memberService.CreateMemberAsync(input, cancellationToken);

        if (createMember)
            _toastNotification.AddSuccessToastMessage("Member Created Successfully!");
        else
            _toastNotification.AddErrorToastMessage("Member Failed To Create. Email or Phone Number Already Exists, or Photo Upload Failed!");

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Update Member

    public async Task<IActionResult> MemberEdit(int id, CancellationToken cancellationToken = default)
    {
        var member = await _memberService.GetMemberToUpdateAsync(id, cancellationToken);
        if (member == null)
        {
            _toastNotification.AddErrorToastMessage("Member Not Found!");
            return RedirectToAction(nameof(Index));
        }

        return View(member);
    }

    [HttpPost]
    public async Task<IActionResult> MemberEdit([FromRoute] int id, MemberToUpdateViewModel input, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return View(input);
        }

        bool updateMember = await _memberService.UpdateMemberAsync(id, input, cancellationToken);

        if (updateMember)
            _toastNotification.AddSuccessToastMessage("Member Updated Successfully!");
        else
            _toastNotification.AddErrorToastMessage("Member Failed To Update, Email or Phone Number Already Exists!");

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Toggle Status

    [HttpPost]
    public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken = default)
    {
        bool result = await _memberService.ToggleMemberStatusAsync(id, cancellationToken);

        if (result)
            _toastNotification.AddSuccessToastMessage("Member status updated successfully!");
        else
            _toastNotification.AddErrorToastMessage("Failed to update member status. Member may not have an active membership.");

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Delete Member

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            _toastNotification.AddErrorToastMessage("ID cannot be Null or Negative!");
            return RedirectToAction(nameof(Index));
        }

        var member = await _memberService.GetMemberDetailsAsync(id, cancellationToken);
        if (member == null)
        {
            _toastNotification.AddErrorToastMessage("Member Not Found!");
            return RedirectToAction(nameof(Index));
        }

        ViewBag.MemberId = id;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed([FromForm] int id, CancellationToken cancellationToken = default)
    {
        var result = await _memberService.RemoveMemberAsync(id, cancellationToken);

        if (result)
            _toastNotification.AddSuccessToastMessage("Member Deleted Successfully!");
        else
            _toastNotification.AddErrorToastMessage("Member Cannot be Deleted!");

        return RedirectToAction(nameof(Index));
    }

    #endregion
}
