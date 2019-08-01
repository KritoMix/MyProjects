using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Im = SixLabors.ImageSharp;
using Pioneer.Pagination;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
namespace AdminPanel
{
    public class ProductController : Controller
    {
       public ImageRepository ImageRepository {get;set;} 
       public ProductRepository ProductRepository {get;set;}
       public IPaginatedMetaService _paginatedMetaService {get;}
       public ProductController(ImageRepository imageRepository,ProductRepository productRepository,IPaginatedMetaService  paginatedMetaService)
       {     
              ImageRepository = imageRepository;
              ProductRepository = productRepository;
             _paginatedMetaService  =  paginatedMetaService ;
       }
        
        public IActionResult Products(int Id=1)
        {
     
          var Products = ProductRepository.GetAll().Skip((Id-1) * 10).Take(10).ToList();
             var totalNumberInCollection = 5;
             var itemsPerPage = 2;
             ViewBag.PaginatedMeta = _paginatedMetaService.GetMetaData(totalNumberInCollection, Id, itemsPerPage);
         
          foreach(Product i in Products)
          { 
            if(i.Image == null)
            { 
              i.Image = new Image(){Path = "..\\Image\\null.png" };
            }
          }
          return View(Products);
        } 
        public IActionResult CreateProduct()
        {
          return View("EditProduct",new Product());
        }
        [HttpPost]
        public IActionResult EditProduct(int Id)
        {    Product product = new Product();
            if(Id!=0)
            {
              product = ProductRepository.GetAll().FirstOrDefault(i=>i.Id==Id);
            }
             else
             {
                int ProductId = int.Parse(Request.Form["Id"]);
                var ProductName = Request.Form["Name"];
                var ProductDictionary = Request.Form["Dictionary"];
                var ProductPrice = int.Parse(Request.Form["Price"]);
                
                product = new Product(){Id=ProductId,Name = ProductName,Dictionary = ProductDictionary,Price = ProductPrice}; 
                
             }
           
            return View(product); 
        }
        public IActionResult RemoveProduct(Product product)
        {
             var Products = ProductRepository.GetAll().Where(s=>s.Id == product.Id).First();
           if(Products != null)
           {
            ProductRepository.Remove(Products);
            ProductRepository.Save();
           }
            return RedirectToAction("Products");   
        }
        public IActionResult SaveProduct(Product product)
        {
          if(product.Id!=0)
          {
              var OldProduct = ProductRepository.GetAll().Where(p=>p.Id == product.Id).First();
              var NawImage = ImageRepository.GetAll().FirstOrDefault(i=>i.Id==product.ImageId);
              OldProduct.Image = NawImage;
              OldProduct.Name = product.Name;
              OldProduct.Dictionary = product.Dictionary;
              OldProduct.Price = product.Price;
              ProductRepository.Save();
              return RedirectToAction("Products");
          }
          else
          {
           
           if(product.ImageId!=null)
           {
            product.Image = ImageRepository.GetAll().Single(s=>s.Id == product.ImageId);
            ProductRepository.Add(product);
            ProductRepository.Save();
            return RedirectToAction("Products");
           }
           else
           {
              ViewBag.Message = "Добавьте картинку!";
              ProductRepository.Add(product);
              ProductRepository.Save();
              return RedirectToAction("Products");
            // return View("EditProduct",new Product());
           }
          }

         
        }
       public IActionResult ChooseImage()
       { 
         int ProductId = int.Parse(Request.Form["Id"]);
         var ProductName = Request.Form["Name"];
         var ProductDictionary = Request.Form["Dictionary"];
         var ProductPrice = int.Parse(Request.Form["Price"]);
          Product NewProduct = new Product(){Id=ProductId,Name = ProductName,Dictionary = ProductDictionary,Price = ProductPrice};
          ProductVM productVM = new ProductVM(){ProductM = NewProduct, Images = ImageRepository.GetAll().ToList()};
         return View(productVM);
       }
       
        public IActionResult AddImageToProduct(Product Pro,int Id1)
        {
          
          var Image = ImageRepository.GetAll().Single(i=>i.Id==Id1);
          Pro.Image = Image;
          return View("EditProduct",Pro);
        }
    }
}