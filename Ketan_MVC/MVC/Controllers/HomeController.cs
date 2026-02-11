using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using MVC.Models;
using Repositories;

namespace MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserInterface _userRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public HomeController(ILogger<HomeController> logger, IUserInterface userRepo, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _userRepo = userRepo;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(vm_Login login)
    {
        t_User UserData = await _userRepo.Login(login);
        if (ModelState.IsValid)
        {
            if (UserData.c_UserId != 0)
            {
                HttpContext.Session.SetInt32("UserId", UserData.c_UserId);
                HttpContext.Session.SetString("UserName", UserData.c_UserName);
                TempData["SuccessMessage"] = "Login Successful";
                return RedirectToAction("Index", "Contact");
            }
            else
            {
                ViewData["message"] = "Invalid Username and Password";
            }
        }
        return View(login);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(t_User user)
    {
        if (ModelState.IsValid)
        {
            if (user.ProfilePicture != null && user.ProfilePicture.Length > 0)
            {
                // Save the uploaded file
                var fileName = user.c_Email + Path.GetExtension(user.ProfilePicture.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "profile_images", fileName);
                Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "profile_images"));
                user.c_Image = fileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    user.ProfilePicture.CopyTo(stream);
                }
            }
            Console.WriteLine("user.c_fname: " + user.c_UserName);
            var status = await _userRepo.Register(user);
            if (status == 1)
            {
                TempData["SuccessMessage"] = "User Registered Successfully";
                return RedirectToAction("Login");
            }
            else if (status == 0)
            {
                ViewData["message"] = "User Already Registered";
            }
            else
            {
                ViewData["message"] = "There was some error while Registration";
            }
        }
        return View();
    }
}
