using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace AdminPanel
{
    public class HomeController : Controller
    {
       public ImageRepository ImageRepository {get;set;} 
       public HomeController(ImageRepository imageRepository)
       {
              ImageRepository = imageRepository;

       }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ImagePanel()
        {
            ImageUploader up = new ImageUploader();
            up.Upload(new Image());
            
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {   
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
