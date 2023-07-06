using Microsoft.AspNetCore.Mvc;
using ST.Data;
using ST.Models;
using ST.Models.ViewModels.AP;

namespace ST.Controllers
{
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AboutController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<About> objList = _db.About;
            return View(objList);
        }
        //Get - UPSERT
        public IActionResult Upsert(int? id)
        {

            AboutVM aboutVM = new AboutVM()
            {
                About = new About(),
            };

            if (id == null)
            {
                //Create
                return View(aboutVM);
            }
            else
            {
                //Edit
                aboutVM.About = _db.About.Find(id);
                if (aboutVM.About == null)
                {
                    return NotFound();
                }
                return View(aboutVM);
            }
        }


        //Post - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(AboutVM aboutVM)
        {

            if (ModelState.IsValid)
            {
                if (aboutVM.About.About_ID == 0)
                {
                    //Creating

                    _db.About.Add(aboutVM.About);
                }
                else
                {
                    //updating
                    _db.About.Update(aboutVM.About);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aboutVM);
        }
        //Get - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.About.Find(id);
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
            var obj = _db.About.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.About.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
