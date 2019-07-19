using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.EntityFrameworkCore;
namespace AdminPanel
{
  public class ImageRepository
  {

    // private List<Image> Images {get;set;}
     public AppContext Context {get;}
     public ImageRepository(AppContext context)
     {
        Context = context;
       // Images = new List<Image>();
     }
     public IEnumerable<Image> GetAll()
     {
        return Context.Images.Include(x=>x.Thrumbneils).ToList();
     }
     public void Add(Image image)
     {
         Context.Images.Add(image);
     }
     public void Save()
     {
         Context.SaveChanges();
     }
     public void Remove(Image image)
     {
      Context.Remove(image);
     }
     /* 
     public void TestAdd(Image image)
     {
       Images.Add(image);
     }
     public List<Image> TestGetAll()
     {
        return Images;
     }
     */
  }

}