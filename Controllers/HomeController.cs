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
using Pioneer.Pagination;

namespace AdminPanel
{
    public class HomeController : Controller
    {
       public ImageRepository ImageRepository {get;set;} 
       public ImageSizeRepository ImageSizeRepository {get;set;}
       public ProductRepository ProductRepository {get;set;} 
       public IPaginatedMetaService _paginatedMetaService ;
       public HomeController(ImageRepository imageRepository, ImageSizeRepository imageSizeRepository,ProductRepository productRepository,IPaginatedMetaService  paginatedMetaService )
       {     ProductRepository = productRepository;
             ImageRepository = imageRepository;
             ImageSizeRepository = imageSizeRepository;
            _paginatedMetaService  =  paginatedMetaService ;
       }
        public IActionResult Index()
        {
            return View();
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
