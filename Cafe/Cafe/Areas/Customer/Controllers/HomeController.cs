using Cafe.Data;
using Cafe.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IToastNotification _toast;
        private readonly IWebHostEnvironment _he;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IToastNotification toast, IWebHostEnvironment he)
        {
            _logger = logger;
            _db = db;
            _toast = toast;
            _he = he;
        }

        public IActionResult Index()
        {
            var menu = _db.Menus.Where(i => i.Ozel).ToList();
            return View(menu);
        }
        public IActionResult CategoryDetails(int? id)
        {
            var menu = _db.Menus.Where(i => i.CategoryId == id).ToList();
            ViewBag.KategoriId = id;
            return View(menu);
        }
        public IActionResult Blog()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Blog(Blog blog)
        {

            if (ModelState.IsValid)
            {
                blog.Tarih = DateTime.Now;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(_he.WebRootPath, @"Site\blog");
                    var ext = Path.GetExtension(files[0].FileName);
                    if (blog.Image != null)
                    {
                        var imagePath = Path.Combine(_he.WebRootPath, blog.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + ext), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    blog.Image = @"\Site\blog\" + fileName + ext;
                }
                _db.Add(blog);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Yorumunuz iletildi yorumunu onaylandığında bloglar kısmından görebilirsiniz...");
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact([Bind("Id,Name,Email,Telefon,Mesaj")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Tarih = DateTime.Now;
                _db.Add(contact);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Teşekkür Ederiz, Mesajınız başarıyla iletildi...");
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        public IActionResult About()
        {
            var about = _db.Abouts.ToList();
            return View(about);
        }
        public IActionResult Galeri()
        {
            var galeri = _db.Galeris.ToList();
            return View(galeri);
        }
        public IActionResult Menu()
        {
            var menu = _db.Menus.ToList();
            return View(menu);
        }
        public IActionResult Rezervasyon()
        {
            return View();
        }

        // POST: Admin/Rezervasyon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rezervasyon([Bind("Id,Name,Email,TelefonNo,Sayi,Saat,Tarih")] Rezervasyon rezervasyon)
        {
            if (ModelState.IsValid)
            {
                _db.Add(rezervasyon);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Rezervasyon işleminiz başarıyla gerçekleşti...");
                return RedirectToAction(nameof(Index));
            }
            return View(rezervasyon);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
