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
    public class DrowController : Controller
    {
       public ImageRepository ImageRepository {get;set;} 
       public ImageSizeRepository ImageSizeRepository {get;set;}
       public ProductRepository ProductRepository {get;set;} 
       public IPaginatedMetaService _paginatedMetaService ;
       public DrowController(ImageRepository imageRepository, ImageSizeRepository imageSizeRepository,ProductRepository productRepository,IPaginatedMetaService  paginatedMetaService )
       {     ProductRepository = productRepository;
             ImageRepository = imageRepository;
             ImageSizeRepository = imageSizeRepository;
            _paginatedMetaService  =  paginatedMetaService ;
       }
        public IActionResult ImagePanel(int Id = 1)
        {   
             var Images = ImageRepository.GetAll().Skip((Id-1) * 10).Take(10).ToList();
             var totalNumberInCollection = 5;
             var itemsPerPage = 2;
             ViewBag.PaginatedMeta = _paginatedMetaService.GetMetaData(totalNumberInCollection, Id, itemsPerPage);
             return View("DrowPictures",Images);
             
        }
       public IActionResult DrowPictures(List<Image> Images )
       {
            
             return View(Images);
       } 
       public IActionResult DrowTrumbneils(int size,int Id = 1) 
       {    
             var Images = ImageRepository.GetAll().Skip((Id-1) * 10).Take(10).ToList();
             var totalNumberInCollection = ImageRepository.Context.Images.Count();
             var itemsPerPage = 2;
             ViewBag.PaginatedMeta = _paginatedMetaService.GetMetaData(totalNumberInCollection, Id, itemsPerPage);
            List<Thrumbneil> thrumbneils = new List<Thrumbneil>();
             foreach(Image i in Images)
             { 
               thrumbneils = i.Thrumbneils.Where(t=>t.ThrumbneilSizeId==size).ToList();  
             }    
             return View(thrumbneils);
       } 
        public IActionResult ImageUploader(IFormFile file)
        {
          if(file!=null && "image/jpeg"== file.ContentType)
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
        public IActionResult RemoveImage(Image image)
        {   //Получаем Image по Id
            var Imag = ImageRepository.GetAll().Single(s=>s.Id == image.Id);
             //Удаляем миниатюры этого Image
             foreach(Thrumbneil i in Imag.Thrumbneils)
              {
                FileInfo InfoThrumb = new FileInfo("wwwroot\\"+i.Path.Substring(1));
                InfoThrumb.Delete();
                //Удаляем из бд миниатюры
                //ImageRepository.Context.Thrumbneils.Remove(i);
              }    
              
              //Удаляем сам Image
              FileInfo Info = new FileInfo("wwwroot\\"+Imag.Path.Substring(1));
              Info.Delete(); 
             //Удаляем ссылки в продуктах
               var Products = ProductRepository.GetAll().Where(p=>p.Image!=null).ToList();
               var ProductsToImage = Products.Where(p=>p.Image.Id == Imag.Id);
               foreach(Product i in ProductsToImage){i.Image = null;}
              //Удаляем Image из БД
              ImageRepository.Remove(Imag);
              ImageRepository.Save();
               
            return RedirectToAction("ImagePanel");
        }
       
    
    
    
    }
}