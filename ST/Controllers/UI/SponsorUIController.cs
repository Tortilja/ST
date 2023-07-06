using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST.Data;
using ST.Models;
using ST.Models.ViewModels.UI;

namespace ST.Controllers.UI
{
    public class SponsorUIController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SponsorUIController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            SponsorUIVM sponsorUIVM = new SponsorUIVM()
            {
                Sponsor = _db.Sponsor
            };
            return View(sponsorUIVM);
        }

    }
}
