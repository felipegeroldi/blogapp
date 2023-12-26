using BlogApp.DataAccess.Repositories.RepositoryInterfaces;
using BlogApp.Models;
using BlogApp.WebApp.Events;
using BlogApp.WebApp.Exceptions;
using BlogApp.WebApp.Extensions;
using BlogApp.WebApp.Handlers;
using BlogApp.WebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogApp.WebApp.Controllers;

[Route("Login")]
public class LoginController : Controller
{
    private IUserRepository _userRepository;
    private event EventHandler<RegistryNotifyEventArgs> OnRegisterNotify;

    public LoginController(IUserRepository userRepository,
        EmailHandler emailHandler)
    {
        _userRepository = userRepository;
        OnRegisterNotify += emailHandler.SendRegistryEmail;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet("Register")]
    public ActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> ValidateAsync(
        [FromForm] LoginValidateViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await AuthenticateAsync(model);
                return RedirectToAction("Index", "Home");
            } catch (InvalidEmailOrPasswordException)
            {
                ModelState.AddModelError("Email", "Invalid email or password");
            }
        }

        return View("Index", model);
    }


    [HttpPost("Register")]
    public async Task<IActionResult> CompleteRegistrationAsync(
        [FromForm] LoginRegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                bool emailAlreadyRegistered = await _userRepository
                                .Users.AnyAsync(x => x.Email == model.Email);

                if (emailAlreadyRegistered)
                    throw new EmailAlreadyRegisteredException(string.Format("Email {0} already registered", model.Email));

                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    PasswordHash = model.Password,
                    UserRole = EUserRole.User,
                    CreatedAt = DateTime.UtcNow,
                };

                await _userRepository.AddUserAsync(user);
                OnRegisterNotify(this, new RegistryNotifyEventArgs(user.Email, user.Name));
                return RedirectToAction("Index", "Home");
            } catch (EmailAlreadyRegisteredException)
            {
                ModelState.AddModelError("Email", "Email already registered");
            }
        }

        return View("Register", model);
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await DestroySessionAsync();
        return RedirectToAction("Index", "Home");
    }

    [NonAction]
    public async Task AuthenticateAsync(LoginValidateViewModel model)
    {
        var user = await _userRepository.GetUserByEmailAsync(model.Email) ??
            throw new InvalidEmailOrPasswordException(string.Format("Invalid email {0}", model.Email));

        if (!user.VerifyPassword(model.Password))
            throw new InvalidEmailOrPasswordException(string.Format("Invalid password"));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.UserRole.ToRoleDescriptionString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
    }

    [NonAction]
    public async Task DestroySessionAsync() =>
        await HttpContext.SignOutAsync();
}
