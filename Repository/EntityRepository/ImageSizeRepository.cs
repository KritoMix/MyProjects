using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
namespace AdminPanel
{
  public class ImageSizeRepository
  {
    private List<SizeImage> ImageSizes {get;set;}
    public AppContext Context {get;}
    public ImageSizeRepository()//AppContext context
    {
      ImageSizes = new List<SizeImage>();
               ImageSizes.Add(new SizeImage(){ Width = 100, Height = 100 , NameSize = "100_100" });
               ImageSizes.Add(new SizeImage(){ Width = 400, Height = 400 , NameSize = "400_400" });
               ImageSizes.Add(new SizeImage(){ Width = 800, Height = 800 , NameSize = "800_800" });
    // Context = context;
    }
    public IEnumerable<SizeImage> GetAll()
    {
        return Context.ImageSizes;
    }
    public void Add(SizeImage imageSize)
    {
      Context.ImageSizes.Add(imageSize);
    }
    public void Save()
    {
        Context.SaveChanges();
    }
    public void TestAdd(SizeImage imageSize)
    {
      ImageSizes.Add(imageSize);
    }
    public List<SizeImage> TestGetAll()
    {
      return ImageSizes;
    }
  
  }

}