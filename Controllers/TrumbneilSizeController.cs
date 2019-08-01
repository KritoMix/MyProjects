

using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace AdminPanel
{
 public class TrumbneilSizeController : Controller
 {
       public ImageRepository ImageRepository {get;set;} 
       public ImageSizeRepository ImageSizeRepository {get;set;}
 public TrumbneilSizeController(ImageRepository imageRepository, ImageSizeRepository imageSizeRepository)
       {    
              ImageRepository = imageRepository;
              ImageSizeRepository = imageSizeRepository;
       }
        public IActionResult Size() => View(ImageSizeRepository.GetAll().ToList());
      
      [HttpPost]
        public IActionResult Edit(int id)
       {   
           ThrumbneilSize thrumbneilSize = ImageSizeRepository.Context.ThrumbneilSizes.FirstOrDefault(i=>i.Id==id);
           return View(thrumbneilSize); 
          
       }
       
        public IActionResult Create() => View("Edit",new ThrumbneilSize());
        
        public IActionResult Save(ThrumbneilSize size,bool Check)
        {
           var chec = Request.Form["Check"];
           //Создать новый размер
           if(size.Id == 0)
            {
               if(chec.Count == 0)
               {
                 ImageUploader up = new ImageUploader(new ImageGenerator(),ImageSizeRepository,ImageRepository);
                 up.TrumbneilsCreate(size);
               }
               else
               {
                 ImageSizeRepository.Add(size);
                 ImageSizeRepository.Save();
               }
              
            }
            //Изменить существующий
            else
            {
               ThrumbneilSize Size = ImageSizeRepository.GetAll().FirstOrDefault(s=>s.Id ==size.Id );
                   if(Size != null)
                   {
                    Size.Height = size.Height;
                    Size.Width = size.Width;
                    Size.NameSize = size.NameSize;
                   }
                    ImageUploader up = new ImageUploader(new ImageGenerator(),ImageSizeRepository,ImageRepository);
                   
                    if(Size.ThrumbneilSizes!=null)
                    {
                       up.TrumbnailsUpdate(Size);
                    ImageSizeRepository.Save();
                    }
            }
           
           return RedirectToAction("Size");
           // return RedirectToAction("Size");
        }
        public IActionResult RemoveSize(ThrumbneilSize size)
        { 
           var Sizes = ImageSizeRepository.GetAll().Single(s=>s.Id == size.Id);
           if(Sizes.ThrumbneilSizes != null)
           {
              foreach(Thrumbneil i in Sizes.ThrumbneilSizes)
              {
                FileInfo Info = new FileInfo("wwwroot\\"+i.Path.Substring(1));
                Info.Delete();
              } 
           
            ImageSizeRepository.Remove(Sizes);
            ImageSizeRepository.Save();
           }
            return RedirectToAction("Size");   
       }
 }
 

}