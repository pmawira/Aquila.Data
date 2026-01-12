using Aquila.Data.Core.Exceptions;
using Aquila.Data.Presentation.Models;
using Aquila.Data.Presentation.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Aquila.Data.Presentation.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _repo;

        public CustomersController(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index() => View(_repo.GetAll());

        public IActionResult Create() => View();

        //[HttpPost]
        //public IActionResult Create(CustomerViewModel model)
        //{
        //    try
        //    {

        //    }
        //    catch ()
        //    { 
        //    }
        //    _repo.Add(model);
        //    return RedirectToAction(nameof(Index));
        //}
        [HttpPost]
        public IActionResult Create(CustomerViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _repo.Add(model);
                TempData["Success"] = "Customer added successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (ConstraintViolationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "An unexpected error occurred. Please try again."
                );
                return View(model);
            }
        }

        public IActionResult Edit(int id) => View(_repo.GetById(id));

        [HttpPost]
        public IActionResult Edit(CustomerViewModel model)
        {
            _repo.Update(model);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
