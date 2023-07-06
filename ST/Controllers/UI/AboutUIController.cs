using Microsoft.AspNetCore.Mvc;
using ST.Data;
using ST.Models;

namespace ST.Controllers.UI
{
    public class AboutUIController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AboutUIController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<About> objList = _db.About;
            return View(objList);
        }
    }
}
