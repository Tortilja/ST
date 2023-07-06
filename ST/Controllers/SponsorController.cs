using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ST.Data;
using ST.Models;
using ST.Models.ViewModels.AP;

namespace ST.Controllers
{
    public class SponsorController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SponsorController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Sponsor> objList = _db.Sponsor;
            return View(objList);
        }
        //Get - UPSERT
        public IActionResult Upsert(int? id)
        {

            SponsorVM sponsorVM = new SponsorVM()
            {
                Sponsor = new Sponsor(),
            };

            if (id == null)
            {
                return View(sponsorVM);
            }
            else
            {
                sponsorVM.Sponsor = _db.Sponsor.Find(id);
                if (sponsorVM.Sponsor == null)
                {
                    return NotFound();
                }
                return View(sponsorVM);
            }
        }
        //Post - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(SponsorVM sponsorVM)
        {

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (sponsorVM.Sponsor.Sponsor_ID == 0)
                {
                    //Creating
                    string upload = webRootPath + WC.SponsorImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    sponsorVM.Sponsor.Sponsor_Image = fileName + extension;

                    _db.Sponsor.Add(sponsorVM.Sponsor);
                }
                else
                {
                    //updating
                    var objFromDb = _db.Sponsor.AsNoTracking().FirstOrDefault(u => u.Sponsor_ID == sponsorVM.Sponsor.Sponsor_ID);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.SponsorImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Sponsor_Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        sponsorVM.Sponsor.Sponsor_Image = fileName + extension;
                    }
                    else
                    {
                        sponsorVM.Sponsor.Sponsor_Image = objFromDb.Sponsor_Image;
                    }
                    _db.Sponsor.Update(sponsorVM.Sponsor);
                }


                _db.SaveChanges();
                return RedirectToAction("Index");
            }
      
            return View(sponsorVM);
        }
        //Get - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Sponsor sponsor = _db.Sponsor.FirstOrDefault(u => u.Sponsor_ID == id);
            if (sponsor == null)
            {
                return NotFound();
            }
            return View(sponsor);
        }

        //Post - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Sponsor.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.SponsorImagePath;
            var oldFile = Path.Combine(upload, obj.Sponsor_Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.Sponsor.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
