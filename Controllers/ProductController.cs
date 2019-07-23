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
    public class ProductController : Controller
    {
       public ImageRepository ImageRepository {get;set;} 
       public ProductRepository ProductRepository {get;set;}
        
       public ProductController(ImageRepository imageRepository,ProductRepository productRepository)
       {     
              ImageRepository = imageRepository;
              ProductRepository = productRepository;
       }
        
        public IActionResult Products() => View(ProductRepository.GetAll().ToList());
        public IActionResult CreateProduct()
        {
          return View("EditProduct",new Product());
        }
        [HttpPost]
        public IActionResult EditProduct(int Id)
        {
            Product product = ProductRepository.Context.Product.FirstOrDefault(i=>i.Id==Id);
            return View(product); 
        }
        public IActionResult RemoveProduct(Product product)
        {
             var Products = ProductRepository.GetAll().Single(s=>s.Id == product.Id);
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
              
          }
          else
          {
            //var Imag = ImageRepository.GetAll().First();
            //Product NewProduct = new Product();
           // NewProduct = product;
            //NewProduct.Image = Imag;
            product.Image = new Image();
            ProductRepository.Add(product);
            ProductRepository.Save();
          }

          return RedirectToAction("Products");
        }
    
    }
}