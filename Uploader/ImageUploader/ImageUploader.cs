
using System;
using System.IO;
using SixLabors.ImageSharp.Formats;
using imag = SixLabors.ImageSharp;
using System.Collections;
using System.Collections.Generic;
namespace AdminPanel
{
  public class ImageUploader: BaseUploader
  {
     public ImageGenerator ImageGenerator {get;}
     public ImageSizeRepository ImageSizeRepository {get;}
     public ImageRepository ImageRepository {get;}
     public ImageUploader(ImageGenerator imageGenerator,ImageSizeRepository imageSizeRepository,ImageRepository imageRepository)
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
            Trumbneils = ImageGenerator.Generate(image,ImageSizeRepository.TestGetAll());
            image.Thrumbneils = Trumbneils;
             image.Path =".."+ image.Path.Substring(18)+image.Name; //Заменить в срочном порядке
            ImageRepository.Add(image);    
            ImageRepository.Save();
       }
       
  }
 

}