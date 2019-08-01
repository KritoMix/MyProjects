using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace AdminPanel
{
    public class ApiController : Controller
    {
       public ImageRepository ImageRepository {get;set;} 
       public ImageSizeRepository ImageSizeRepository {get;set;}
       public ProductRepository ProductRepository {get;set;} 
       public ApiController(ImageRepository imageRepository, ImageSizeRepository imageSizeRepository,ProductRepository productRepository)
       {     ProductRepository = productRepository;
              ImageRepository = imageRepository;
              ImageSizeRepository = imageSizeRepository;
       }
      [HttpGet] 
      //Получить все продукты
       public string GetAllProduct() 
       {  
           return JsonConvert.SerializeObject(ProductRepository.Context.Product.ToList());
       } 
     [HttpGet]  
     //Получить все картинки
       public string GetAllImage() 
       {  
          return JsonConvert.SerializeObject(ImageRepository.Context.Images.ToList());
       } 
     [HttpGet] 
      //Получить все Размеры
       public string GetAllTrumbneilSize() 
       {  
          
            return JsonConvert.SerializeObject(ImageSizeRepository.Context.ThrumbneilSizes.ToList());
       } 
         [HttpGet]
         //Получить все продукты с Картинками
         public string GetAllProductToImage() 
       {  
           return JsonConvert.SerializeObject(ProductRepository.GetAll().ToList());
       } 
        [HttpGet]  
     //Получить все картинки
       public string GetAllImageToTrumbneils() 
       {  
          return JsonConvert.SerializeObject(ImageRepository.GetAll().ToList());
       } 
        [HttpGet] 
      //Получить все Размеры
       public string GetAllSizeToTrumbneils() 
       {  
          
            return JsonConvert.SerializeObject(ImageSizeRepository.GetAll().ToList());
       } 
    }
}