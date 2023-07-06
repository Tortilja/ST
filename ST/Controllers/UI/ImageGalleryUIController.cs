using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST.Data;
using ST.Models.ViewModels.UI;

namespace ST.Controllers.UI
{
    public class ImageGalleryUIController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ImageGalleryUIController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ImageGaleryUIVM imageGalleryUIVM = new ImageGaleryUIVM()
            {
                ImageGallery = _db.ImageGallery.Include(u => u.Event),
                Event = _db.Event
            };
            return View(imageGalleryUIVM);
        }
    }
}
