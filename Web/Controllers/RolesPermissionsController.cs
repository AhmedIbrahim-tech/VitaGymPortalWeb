using Core.ViewModels.RoleViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Web.Controllers;

[Authorize]
public class RolesPermissionsController(
    RoleManager<IdentityRole> _roleManager,
    IToastNotification _toastNotification) : Controller
{
    public async Task<IActionResult> Index()
    {
        var roles = _roleManager.Roles.ToList();
        
        var roleViewModels = roles.Select(role => new RoleViewModel
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
            CreatedDate = DateTime.Now, // You may want to store this in the database
            IsActive = true // You may want to add this property to IdentityRole
        }).ToList();

        ViewData["Title"] = "Roles & Permissions";
        ViewData["PageTitle"] = "Roles & Permissions";
        ViewData["PageSubtitle"] = "Manage your roles";
        ViewData["PageIcon"] = "shield";
        ViewData["ShowActionButton"] = true;
        ViewData["ActionButtonText"] = "Add Role";
        ViewData["ActionButtonUrl"] = Url.Action("Create");
        ViewData["ActionButtonIcon"] = "shield-plus";

        if (!roleViewModels.Any())
        {
            ViewData["EmptyIcon"] = "shield";
            ViewData["EmptyTitle"] = "No Roles Available";
            ViewData["EmptyMessage"] = "Add your first role to get started";
        }

        return View(roleViewModels);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            _toastNotification.AddErrorToastMessage("Role name is required!");
            return View();
        }

        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (roleExists)
        {
            _toastNotification.AddErrorToastMessage("Role already exists!");
            return View();
        }

        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
        if (result.Succeeded)
        {
            _toastNotification.AddSuccessToastMessage("Role created successfully!");
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in result.Errors)
        {
            _toastNotification.AddErrorToastMessage(error.Description);
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            _toastNotification.AddErrorToastMessage("Role ID is required!");
            return RedirectToAction(nameof(Index));
        }

        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            _toastNotification.AddErrorToastMessage("Role not found!");
            return RedirectToAction(nameof(Index));
        }

        var result = await _roleManager.DeleteAsync(role);
        if (result.Succeeded)
        {
            _toastNotification.AddSuccessToastMessage("Role deleted successfully!");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                _toastNotification.AddErrorToastMessage(error.Description);
            }
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> ToggleStatus(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            _toastNotification.AddErrorToastMessage("Role ID is required!");
            return RedirectToAction(nameof(Index));
        }

        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            _toastNotification.AddErrorToastMessage("Role not found!");
            return RedirectToAction(nameof(Index));
        }

        // Note: IdentityRole doesn't have IsActive property
        // This is a placeholder for future implementation if needed
        _toastNotification.AddInfoToastMessage("Role status toggle is not implemented. Roles are always active.");

        return RedirectToAction(nameof(Index));
    }
}

