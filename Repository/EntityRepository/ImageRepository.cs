using System.Collections.Generic;

namespace AdminPanel
{
  public class ImageRepository
  {

     private List<Image> Images {get;set;}
     public AppContext Context {get;}
     public ImageRepository()//AppContext context
     {
       // Context = context;
        Images = new List<Image>();
     }
     public IEnumerable<Image> GetAll()
     {
        return Context.Images;
     }
     public void Add(Image image)
     {
         Context.Images.Add(image);
     }
     public void Save()
     {
         Context.SaveChanges();
     }
     public void TestAdd(Image image)
     {
       Images.Add(image);
     }
     public List<Image> TestGetAll()
     {
        return Images;
     }
  
  }

}