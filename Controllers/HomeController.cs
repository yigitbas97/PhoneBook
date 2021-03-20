using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using PhoneBook.Entities;
using PhoneBook.EntitiesDAO;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonDAO _phoneBookDb;
        IHostingEnvironment _hostingEnvironment;

        public HomeController(IPersonDAO phoneBookDb, IHostingEnvironment hostingEnvironment)
        {
            _phoneBookDb = phoneBookDb;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var phoneBookList = _phoneBookDb.GetAll();
            var model = new PhoneBookListIndexModel { People = phoneBookList };

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new PhoneBookAddModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PhoneBookAddModel model) // async used to save image to stream
        {
            if (ModelState.IsValid)
            {
                // Control of ImageSize
                if (model.Image == null || model.Image.Length == 0)
                {
                    model.ImageName = "defaultpp.jpg";
                }

                else
                {
                    // Save image to path
                    var path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot\\images", model.Image.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }

                    //This variable use for person.ImageName
                    model.ImageName = model.Image.FileName;
                }

                Person person = new Person();
                person.Name = model.Name;
                person.Surname = model.Surname;
                person.PhoneNumber1 = model.PhoneNumber1;
                person.PhoneNumber2 = model.PhoneNumber2;
                person.Email = model.Email;
                person.ImageName = model.ImageName;

                _phoneBookDb.Add(person);

                TempData["message"] = "Your contact added to database succesfully.";
                TempData["status"] = "alert alert-success";
                return RedirectToAction("Index", "Home");
            }

            TempData["message"] = "Your contact did not add to database !";
            TempData["status"] = "alert alert-danger";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var updatedPerson = _phoneBookDb.GetById(id);

            var model = new PhoneBookUpdateModel
            {
                Id = updatedPerson.Id,
                Name = updatedPerson.Name,
                Surname = updatedPerson.Surname,
                PhoneNumber1 = updatedPerson.PhoneNumber1,
                PhoneNumber2 = updatedPerson.PhoneNumber2,
                Email = updatedPerson.Email,
                ImageName = updatedPerson.ImageName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PhoneBookUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var updatedPerson = _phoneBookDb.GetById(model.Id); // Person is exist or not

                if (updatedPerson != null)
                {

                    updatedPerson.Id = model.Id;
                    updatedPerson.Name = model.Name;
                    updatedPerson.Surname = model.Surname;
                    updatedPerson.PhoneNumber1 = model.PhoneNumber1;
                    updatedPerson.PhoneNumber2 = model.PhoneNumber2;
                    updatedPerson.Email = model.Email;

                    // If client chooses new image
                    if (model.Image != null)
                    {
                        // Save image to path
                        var path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot\\images", model.Image.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.Image.CopyToAsync(stream);
                        }

                        //This variable use for person.ImageName
                        updatedPerson.ImageName = model.Image.FileName;
                    }

                    _phoneBookDb.Update(updatedPerson);

                    TempData["message"] = "Person updated succesfully.";
                    TempData["status"] = "alert alert-warning";
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["message"] = "Person did not update !";
            TempData["status"] = "alert alert-danger";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id)
        {
            var deletedPerson = _phoneBookDb.GetById(id);

            if (deletedPerson != null)
            {
                _phoneBookDb.Delete(deletedPerson);
                TempData["message"] = "Person deleted from database succesfully.";
                TempData["status"] = "alert alert-warning";
                return RedirectToAction("Index", "Home");
            }

            TempData["message"] = "Person is not exist !";
            TempData["status"] = "alert alert-danger";
            return RedirectToAction("Index", "Home");
        }
    }
}
