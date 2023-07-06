using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ST.Data;
using ST.Models;
using ST.Models.ViewModels.AP;

namespace ST.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EventController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Event> objList = _db.Event;
            return View(objList);
        }

        //Get - UPSERT
        public IActionResult Upsert(int? id)
        {

            EventVM eventVM = new EventVM()
            {
                Event = new Event(),
            };

            if (id == null)
            {
                //Create
                return View(eventVM);
            }
            else
            {
                //Edit
                eventVM.Event = _db.Event.Find(id);
                if (eventVM.Event == null)
                {
                    return NotFound();
                }
                return View(eventVM);
            }
        }


        //Post - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(EventVM eventVM)
        {

            if (ModelState.IsValid)
            {
                if (eventVM.Event.Event_ID == 0)
                {
                    //Creating

                    _db.Event.Add(eventVM.Event);
                }
                else
                {
                    //updating
                    _db.Event.Update(eventVM.Event);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventVM);
        }
        //Get - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Event.Find(id);
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
            var obj = _db.Event.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Event.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
