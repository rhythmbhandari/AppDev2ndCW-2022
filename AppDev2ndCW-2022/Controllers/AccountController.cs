using System.Globalization;
using System.Security.Claims;
using AppDev2ndCW_2022.Models;
using AppDev2ndCW_2022.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppDev2ndCW_2022.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    public readonly DataBaseContext dataBaseContext;
    public readonly SignInManager<User> _signInManager;
    // GET
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, DataBaseContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        dataBaseContext = db;
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> Register(UserRegistrationViewModel userModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userModel);
        }

        User user = new()
        {
            Name = userModel.Name,
            Email = userModel.Email,
            UserName = userModel.Email,
        };

        var result = await _userManager.CreateAsync(user, userModel.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }

            return View(userModel);
        }
        
        TextInfo myTI = new CultureInfo("en-US",false).TextInfo;
        await _userManager.AddToRoleAsync(user, myTI.ToTitleCase(userModel.Type));
        TempData["Message"] = "User registered successfully";
        return View("Login");
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginViewModel userModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userModel);
        }

        var user = await _userManager.FindByEmailAsync(userModel.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, userModel.Password))
        {
            var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));
            TempData["Message"] = "Logged in successfully!";
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        ModelState.AddModelError("","Username or Password invalid");
        return View();

    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        TempData["Message"] = "Logged out successfully!";
        return RedirectToAction(nameof(HomeController.Index), "Home");
        
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ChangePassword(String password)
    {
        var _user = await _userManager.GetUserAsync(HttpContext.User);
        var user = await _userManager.Users.FirstAsync(u => u.Id == _user.Id);
        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, password);
        TempData["Message"] = "Password changed successfully!";
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
    
    [HttpGet]
    [Authorize]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdminPanel()
    {
        var users = await _userManager.GetUsersInRoleAsync("Manager");
        ViewBag.users = users;
        return View();
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            ViewBag.message = "User deleted successfully!";
            return View("AdminPanel");
        }
        return View("AdminPanel");
    }
}