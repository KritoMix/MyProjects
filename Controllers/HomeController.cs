using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Im = SixLabors.ImageSharp;

using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
namespace AdminPanel
{
    public class HomeController : Controller
    {
       public ImageRepository ImageRepository {get;set;} 
       public ImageSizeRepository ImageSizeRepository {get;set;}
        
       public HomeController(ImageRepository imageRepository, ImageSizeRepository imageSizeRepository)
       {     
              ImageRepository = imageRepository;
              ImageSizeRepository = imageSizeRepository;
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
           var Images = ImageRepository.GetAll().ToList();
           return View(Images);
       }
       
       
        public IActionResult ImageDelete()
        {
           var Images = ImageRepository.GetAll().ToList();
           foreach(Image i in Images)
           {
               ImageRepository.Remove(i);
           }
             var Trumb1 = ImageSizeRepository.GetAll().ToList();
            foreach(ThrumbneilSize n in Trumb1)
            {
               ImageSizeRepository.Remove(n);
            }          
            
           ImageRepository.Save();
           ImageSizeRepository.Save();
           return RedirectToAction("ImagePanel");
        }
        public IActionResult ImageUploader(IFormFile file)
        {
          if(file!=null && "image/png"== file.ContentType)
          {
            byte[] ImageByte;
            
            using(MemoryStream memory = new MemoryStream())
            {
               file.CopyTo(memory);
               ImageByte = memory.ToArray();

            }
             var tups = file.GetType();
             var ImageFul = Im.Image.Load(ImageByte);
             ImageUploader up = new ImageUploader(new ImageGenerator(),ImageSizeRepository,ImageRepository);
             up.Upload(ImageFul);
          }  
         
          
          // IFormFile forms = formFile;
           return RedirectToAction("ImagePanel");
        }
        public IActionResult RemoveImage()
        {
            return RedirectToAction("ImagePanel");
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {   
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
