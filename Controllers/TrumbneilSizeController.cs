

using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;



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
        public IActionResult Size()
       {  TrumbneilSizeVM TSVM = new TrumbneilSizeVM();
          TSVM.ThrumbneilSizes = ImageSizeRepository.GetAll().ToList();
          return View(TSVM);
       }
      
      [HttpPost]
        public IActionResult Edit(int Id)
       {
           return View(ImageSizeRepository.Context.ThrumbneilSizes.FirstOrDefault(i=>i.Id==Id)); 
       }
       
        public IActionResult Create(TrumbneilSizeVM TSVM)
        {   
           TSVM.ThrumbneilSize = new ThrumbneilSize(){Width=200,Height=200};
          
           return View(TSVM);
           // return View("Edit",new ThrumbneilSize());  
        }
        public IActionResult Save(TrumbneilSizeVM size,bool Check)
        {
           var chec = Request.Form["Check"];
           //Создать новый размер
           if(size.ThrumbneilSize.Id == 0)
            {
               if(chec.Count == 0)
               {
                 ImageUploader up = new ImageUploader(new ImageGenerator(),ImageSizeRepository,ImageRepository);
                 up.TrumbneilsCreate(size.ThrumbneilSize);
               }
               else
               {
                 ImageSizeRepository.Add(size.ThrumbneilSize);
                 ImageSizeRepository.Save();
               }
              
            }
            //Изменить существующий
            else
            {
               ThrumbneilSize Size = ImageSizeRepository.GetAll().FirstOrDefault(s=>s.Id ==size.Id );
                   if(Size != null)
                   {
                    Size.Height = size.ThrumbneilSize.Height;
                    Size.Width = size.ThrumbneilSize.Width;
                    Size.NameSize = size.ThrumbneilSize.NameSize;
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
                FileInfo Info = new FileInfo("wwwroot\\"+i.Path.Substring(3));
                Info.Delete();
              } 
           
            ImageSizeRepository.Remove(Sizes);
            ImageSizeRepository.Save();
           }
            return RedirectToAction("Size");   
       }
 }
 

}