
using System;
using System.IO;
using imag = SixLabors.ImageSharp;
using System.Collections;
using System.Collections.Generic;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
namespace AdminPanel
{
  public class ImageGenerator
  {    
     

      public void Generate()
      {
        
      }
       public List<Thrumbneil> Generate(Image im,List<SizeImage> Size)
       {
         List<Thrumbneil> Thrumbneils = new List<Thrumbneil>();
        foreach(SizeImage i in Size)
        {
               string FileMiny = DateTime.Now.ToBinary().ToString()+"_"+i.Width+"_"+i.Height +".png";
               byte[] CreateImage = File.ReadAllBytes(im.Path+im.Name);
                using (imag.Image<imag.PixelFormats.Rgba32> image = imag.Image.Load(CreateImage))
                 {
                    image.Mutate(x => x.Resize(i.Width, i.Height));
                    image.Save(im.Path + FileMiny); 
                 }
               string Path =".."+ im.Path.Substring(18)+FileMiny;
               Thrumbneils.Add(new Thrumbneil(){ Name = FileMiny, Path = Path  });
        }
             return Thrumbneils;

       }
  }

}