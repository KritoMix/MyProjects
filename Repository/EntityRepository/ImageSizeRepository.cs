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
     //Context = context;
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