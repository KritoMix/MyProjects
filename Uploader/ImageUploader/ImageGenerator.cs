
using System;
using System.Linq;
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
     
      public List<Thrumbneil> GenerateCreate(List<Image> Images,ThrumbneilSize size,string PathFile)
      {
        List<Thrumbneil> Thrumbneils = new List<Thrumbneil>();
        foreach(Image i in Images)
        {
           string FileMiny = DateTime.Now.ToBinary().ToString()+"_"+size.Width+"_"+size.Height +".png";
           string Path =".."+ PathFile.Substring(18)+FileMiny; //Заменить в срочном порядке
           byte[] CreateImage = File.ReadAllBytes(PathFile+i.Name);
            using (imag.Image<imag.PixelFormats.Rgba32> image = imag.Image.Load(CreateImage))
                 {
                    image.Mutate(x => x.Resize(size.Width, size.Height));
                    image.Save(PathFile + FileMiny); 
                 }
           Thrumbneil trumbneil = new Thrumbneil(){ Path = Path, Name = FileMiny};
           Thrumbneils.Add(trumbneil);
        }
       return Thrumbneils;
      }
      public void GenerateUpdate(ThrumbneilSize size)
      {
       List<Thrumbneil> Thrumbneils = new List<Thrumbneil>();
       foreach(Thrumbneil i in size.ThrumbneilSizes)
       { 
        //Выгрузить в переменную картинку в байтах
        byte[] CreateImage = File.ReadAllBytes("wwwroot\\"+i.Path.Substring(3));
        //Удалить картинку по адресу
        FileInfo Info = new FileInfo("wwwroot\\"+i.Path.Substring(3));
        Info.Delete();
        //Назначить новый путь и имя картинке 
        int Namb = i.Name.IndexOf("_"); 
        string NewName = i.Name.Substring(0,Namb+1);
        NewName+=size.NameSize+".png";
        i.Name = NewName;
        int Namb1 = i.Path.IndexOf("_"); 
        string NewPath = i.Path.Substring(0,Namb1+1);
        NewPath += size.NameSize+".png";
        i.Path = NewPath;
        //Записать по новому пути и с новым именем
        using (imag.Image<imag.PixelFormats.Rgba32> image = imag.Image.Load(CreateImage))
                 {  
                     
                    image.Mutate(x => x.Resize(size.Width, size.Height));
                    image.Save("wwwroot\\"+i.Path.Substring(3)); 
                    
                 }
         
       }
      
      }
       public List<Thrumbneil> Generate(Image im,List<ThrumbneilSize> Size)
       {
         List<Thrumbneil> Thrumbneils = new List<Thrumbneil>();
        foreach(ThrumbneilSize i in Size)
        {
               string FileMiny = DateTime.Now.ToBinary().ToString()+"_"+i.Width+"_"+i.Height +".png";
               byte[] CreateImage = File.ReadAllBytes(im.Path+im.Name);
                using (imag.Image<imag.PixelFormats.Rgba32> image = imag.Image.Load(CreateImage))
                 {
                    image.Mutate(x => x.Resize(i.Width, i.Height));
                    image.Save(im.Path + FileMiny); 
                 }
               string Path =".."+ im.Path.Substring(18)+FileMiny;
               Thrumbneils.Add(new Thrumbneil(){ Name = FileMiny, Path = Path, ThrumbneilSize = i});
        }
             return Thrumbneils;

       }
  }

}