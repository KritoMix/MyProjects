
using System;
using System.IO;
using SixLabors.ImageSharp.Formats;
using imag = SixLabors.ImageSharp;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace AdminPanel
{
  public class ImageUploader: BaseUploader
  {
     public ImageGenerator ImageGenerator {get;}
     public ImageSizeRepository ImageSizeRepository {get;}
     public ImageRepository ImageRepository {get;}
    
     public ImageUploader( ImageGenerator imageGenerator,ImageSizeRepository imageSizeRepository,ImageRepository imageRepository)
     {
         
         ImageGenerator = imageGenerator;
         ImageSizeRepository = imageSizeRepository;
         ImageRepository = imageRepository;
     }
       public override void Upload(imag.Image<imag.PixelFormats.Rgba32> im)
       {
          string Path = GenerateFilePath();
          string FileName = DateTime.Now.ToBinary().ToString()+"_"+im.Width+"_"+im.Height +".png";
          
          using(FileStream file = new FileStream(Path+FileName,FileMode.Create))
          {
              imag.ImageExtensions.SaveAsPng(im,file); 
              
          }
            
            Image image = new Image(){Path = Path, Name = FileName };
            List<Thrumbneil> Trumbneils = new List<Thrumbneil>();
            Trumbneils = ImageGenerator.Generate(image,ImageSizeRepository.GetAll().ToList());
            image.Thrumbneils = Trumbneils;
             image.Path =".."+ image.Path.Substring(18)+image.Name; //Заменить в срочном порядке
            
            ImageRepository.Add(image);
            
            
            /* 
            foreach(Thrumbneil i in Trumbneils)
            {
              i.Image = image;
              TrumbneilRepository.Add(i);
              
            }
            */
             
             
            ImageRepository.Save();
       }
       public void TrumbnailsUpdate(ThrumbneilSize size)
       {
        size.NameSize = size.Width+"_"+size.Height; 
        ImageGenerator.GenerateUpdate(size); 
       }
       public void TrumbneilsCreate(ThrumbneilSize size)
       {
         ThrumbneilSize Size = new ThrumbneilSize();
         Size = size;
         Size.NameSize = Size.Width+"_"+Size.Height;
        
         var Images = ImageRepository.GetAll().ToList();
        if(Images.Count != 0)
        {
            List<Thrumbneil> Trumbneils = ImageGenerator.GenerateCreate(Images,size,GenerateFilePath());
            var Images1 = ImageRepository.GetAll().ToList();
            for(int i = 0;i<Trumbneils.Count();i++)
            { 
              Trumbneils[i].ThrumbneilSize = Size;
              if(Images1[i].Thrumbneils!=null)
              {
               Images1[i].Thrumbneils.Add(Trumbneils[i]); 
              }
              else
              {
               Images1[i].Thrumbneils = new List<Thrumbneil>();
               Images1[i].Thrumbneils.Add(Trumbneils[i]);
              }
            }     
        }
         else
         {
           ImageSizeRepository.Add(Size);
         }
         
         //ImageSizeRepository.Add(Size);
         ImageSizeRepository.Save();
       
         //ImageSizeRepository.Add(Size);
        
       }
  }
 

}