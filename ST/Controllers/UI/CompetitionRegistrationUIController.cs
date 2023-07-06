using Microsoft.AspNetCore.Mvc;
using ST.Data;
using ST.Models;
using ST.Models.ViewModels.AP;
using ST.Models.ViewModels.UI;

namespace ST.Controllers.UI
{
    public class CompetitionRegistrationUIController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CompetitionRegistrationUIController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<CompetitionRegistration> objList = _db.CompetitionRegistration;
            return View(objList);
        }

        //Get - Create
        public IActionResult Create()
        {
            return View();
        }

        //Post - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CompetitionRegistration obj)
        {
            if (ModelState.IsValid)
            {
                _db.CompetitionRegistration.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }




    }
}
