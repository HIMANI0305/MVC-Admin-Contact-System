using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Repositories;

public class ContactSingleController : Controller
{
    /*
        IContactInterface is injected from DI container.
        Controller → Repository → Database
        Controller does not directly talk to database.
    */

    private readonly IContactInterface _contactRepo;

    public ContactSingleController(IContactInterface contactRepo)
    {
        _contactRepo = contactRepo;
    }

    // ========================= INDEX =========================
    public async Task<IActionResult> Index()
    {
        // Check if user is logged in
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Index", "Home");

        // Get all contacts of logged-in user
        List<t_Contact> contacts =
            await _contactRepo.GetAllByUser(userId.ToString());

        return View(contacts);
    }

    // ========================= LOGOUT =========================
    public IActionResult Logout()
    {
        // Remove all session values
        HttpContext.Session.Clear();

        // After logout always redirect to Home
        return RedirectToAction("Index", "Home");
    }

    // ========================= GET CONTACT BY ID =========================
    public async Task<IActionResult> GetContactById(string id)
    {
        // Call repository
        var contact = await _contactRepo.GetOne(id);

        if (contact == null)
        {
            return BadRequest(new
            {
                success = false,
                message = "There was no contact found"
            });
        }

        return Ok(contact);
    }

    // ========================= CREATE / UPDATE =========================
    [HttpPost]
    public async Task<IActionResult> Create(t_Contact contact)
    {
        // Model Validation
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors
                        .Select(err => err.ErrorMessage)
                        .ToArray()
                );

            return BadRequest(new
            {
                success = false,
                message = errors
            });
        }

        //Session Check
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return BadRequest(new
            {
                success = false,
                message = "User not logged in"
            });
        }

        //Image Upload (if provided)
        if (contact.ContactPicture != null &&
            contact.ContactPicture.Length > 0)
        {
            var folderPath =
                Path.Combine("../MVC/wwwroot/contact_images");

            Directory.CreateDirectory(folderPath);

            var fileName =
                contact.c_Email +
                Path.GetExtension(contact.ContactPicture.FileName);

            var filePath = Path.Combine(folderPath, fileName);

            contact.c_Image = fileName;

            // Delete existing file (if exists)
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            using (var stream =
                new FileStream(filePath, FileMode.Create))
            {
                contact.ContactPicture.CopyTo(stream);
            }
        }




        // Assign UserId
        contact.c_UserId = userId.Value;

        // Insert or Update
        int result;

        if (contact.c_ContactId == 0)
        {
            result = await _contactRepo.Add(contact);
        }
        else
        {
            result = await _contactRepo.Update(contact);
        }

        //Return Result
        if (result == 0)
        {
            return BadRequest(new
            {
                success = false,
                message = "There was some error while saving the contact"
            });
        }

        return Ok(new
        {
            success = true,
            message = contact.c_ContactId == 0
                ? "Contact Inserted Successfully!"
                : "Contact Updated Successfully!"
        });
    }

    // ========================= DELETE =========================
    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        int status = await _contactRepo.Delete(id);

        if (status == 1)
        {
            return Ok(new
            {
                success = true,
                message = "Contact Deleted Successfully!"
            });
        }

        return BadRequest(new
        {
            success = false,
            message = "There was some error while deleting the contact"
        });
    }
}
