using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Linq;

namespace AdminPanel
{
  public abstract class BaseUploader:IUploader<Image>
  {
   public abstract void Upload(Image im);
   protected string GenerateFilePath(params string[] File)
   {      var Root = "root";
          var Data = DateTime.Now.Year;
          var March = DateTime.Now.Day;
      for(int i = 0;i<File.Count();i++)
      {
          DirectoryInfo dirInfo = new DirectoryInfo("wwwroot\\Upload\\");
          var CurrentFileData = dirInfo.GetDirectories().ToList();
            if(CurrentFileData.Any(c=>c.Name == Data.ToString()))  //Если есть хоть одна 
            {
               var CurrentFile = dirInfo.Root;
            }
            else
            {
               dirInfo.Create();
            }
     
  

      }
     return Root;
   
   
   }
  }
}