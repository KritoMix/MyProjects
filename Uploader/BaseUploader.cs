using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Linq;
using im = SixLabors.ImageSharp;
namespace AdminPanel
{
  public abstract class BaseUploader:IUploader<im.Image<im.PixelFormats.Rgba32>>
  {
    
      
   public abstract void Upload(im.Image<im.PixelFormats.Rgba32> im);
   protected virtual string GenerateFilePath(params string[] File)
   {      var Root = "wwwroot\\Upload\\";
          var Data = DateTime.Now.Year.ToString();
          var March = DateTime.Now.Month.ToString();
     
     
        
          DirectoryInfo dirInfo = new DirectoryInfo(Root);
          var CurrentFileData = dirInfo.GetDirectories().ToList();
            if(CurrentFileData.Any(c=>c.Name == Data))  //Если есть хоть одна 
            {
               Root = dirInfo.FullName+Data+"\\";
               
            }
            else
            {

               dirInfo.CreateSubdirectory(DateTime.Now.Year.ToString());
               Root = dirInfo.FullName+DateTime.Now.Year.ToString()+"\\";
            }
          DirectoryInfo dirInfo1 = new DirectoryInfo(Root);
            CurrentFileData = dirInfo.GetDirectories().ToList();
             if(CurrentFileData.Any(c=>c.Name == March))  //Если есть хоть одна 
             {
               Root = dirInfo.FullName + March+"\\";
             }
            else
             {
               
               dirInfo1.CreateSubdirectory(March);
               Root = dirInfo1.FullName + DateTime.Now.Month.ToString()+"\\";
             }
             
      
     return Root;
   
   
   }
  }
}