using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ST.Data;
using ST.Models;
using ST.Models.ViewModels.AP;

namespace ST.Controllers
{
    public class ImageGalleryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ImageGalleryController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<ImageGallery> objList = _db.ImageGallery.Include(u => u.Event);
            return View(objList);
        }

        //Get - UPSERT
        public IActionResult Upsert(int? id)
        {

            ImageGalleryVM imageGalleryVM = new ImageGalleryVM()
            {
                ImageGallery = new ImageGallery(),
                EventSelectList = _db.Event.Select(i => new SelectListItem
                {
                    Text = i.Event_Title,
                    Value = i.Event_ID.ToString()
                })
            };

            if (id == null)
            {
                return View(imageGalleryVM);
            }
            else
            {
                imageGalleryVM.ImageGallery = _db.ImageGallery.Find(id);
                if (imageGalleryVM.ImageGallery == null)
                {
                    return NotFound();
                }
                return View(imageGalleryVM);
            }
        }

        //Post - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ImageGalleryVM imageGalleryVM)
        {

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (imageGalleryVM.ImageGallery.Image_ID == 0)
                {
                    //Creating
                    string upload = webRootPath + WC.GalleryImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    imageGalleryVM.ImageGallery.Image_Name = fileName + extension;

                    _db.ImageGallery.Add(imageGalleryVM.ImageGallery);
                }
                else
                {
                    //updating
                    var objFromDb = _db.ImageGallery.AsNoTracking().FirstOrDefault(u => u.Image_ID == imageGalleryVM.ImageGallery.Image_ID);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.GalleryImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image_Name);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        imageGalleryVM.ImageGallery.Image_Name = fileName + extension;
                    }
                    else
                    {
                        imageGalleryVM.ImageGallery.Image_Name = objFromDb.Image_Name;
                    }
                    _db.ImageGallery.Update(imageGalleryVM.ImageGallery);
                }


                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            imageGalleryVM.EventSelectList = _db.Event.Select(i => new SelectListItem
            {
                Text = i.Event_Title,
                Value = i.Event_ID.ToString()
            });
            return View(imageGalleryVM);
        }

        //Get - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ImageGallery imageGallery = _db.ImageGallery.Include(u => u.Event).FirstOrDefault(u => u.Image_ID == id);
            //product.Category = _db.Category.Find(product.CategoryId);
            if (imageGallery == null)
            {
                return NotFound();
            }
            return View(imageGallery);
        }

        //Post - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.ImageGallery.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.GalleryImagePath;
            var oldFile = Path.Combine(upload, obj.Image_Name);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.ImageGallery.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
