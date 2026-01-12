using Aquila.Data.Core.Exceptions;
using Aquila.Data.Presentation.Models;
using Aquila.Data.Presentation.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Aquila.Data.Presentation.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _repo;

        public OrdersController(IOrderRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index() => View(_repo.GetOrdersWithUsers());

        public IActionResult Create() => View();

     
            [HttpPost]
            public IActionResult Create(OrderViewModel model)
            {
                if (!ModelState.IsValid)
                    return View(model);

                try
                {
                    _repo.Add(model);

                    TempData["Success"] = "Order created successfully.";
                    return RedirectToAction("Index");
                }
                catch (ConstraintViolationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(model);
                }
            }
        
    }
}
