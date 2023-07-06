using Microsoft.AspNetCore.Mvc;
using ST.Data;
using ST.Models;
using ST.Models.ViewModels.AP;

namespace ST.Controllers
{
    public class CompetitionRegistrationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CompetitionRegistrationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<CompetitionRegistration> objList = _db.CompetitionRegistration;
            return View(objList);
        }
        //Get - UPSERT
        public IActionResult Upsert(int? id)
        {

            CompetitionRegistrationVM competitionRegistrationVM = new CompetitionRegistrationVM()
            {
                CompetitionRegistration = new CompetitionRegistration(),
            };

            if (id == null)
            {
                //Create
                return View(competitionRegistrationVM);
            }
            else
            {
                //Edit
                competitionRegistrationVM.CompetitionRegistration = _db.CompetitionRegistration.Find(id);
                if (competitionRegistrationVM.CompetitionRegistration == null)
                {
                    return NotFound();
                }
                return View(competitionRegistrationVM);
            }
        }


        //Post - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CompetitionRegistrationVM competitionRegistrationVM)
        {

            if (ModelState.IsValid)
            {
                if (competitionRegistrationVM.CompetitionRegistration.Competition_Registration_ID == 0)
                {
                    //Creating

                    _db.CompetitionRegistration.Add(competitionRegistrationVM.CompetitionRegistration);
                }
                else
                {
                    //updating
                    _db.CompetitionRegistration.Update(competitionRegistrationVM.CompetitionRegistration);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(competitionRegistrationVM);
        }
        //Get - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.CompetitionRegistration.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //Post - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.CompetitionRegistration.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.CompetitionRegistration.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
