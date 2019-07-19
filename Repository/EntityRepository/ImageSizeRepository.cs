using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AdminPanel
{
  public class ImageSizeRepository
  {
     // private List<ThrumbneilSize> ImageSizes {get;set;}
    public AppContext Context {get;}
    public ImageSizeRepository(AppContext context)
    {        /*
               ImageSizes = new List<ThrumbneilSize>();
               ImageSizes.Add(new ThrumbneilSize(){ Width = 100, Height = 100 , NameSize = "100_100" });
               ImageSizes.Add(new ThrumbneilSize(){ Width = 400, Height = 400 , NameSize = "400_400" });
               ImageSizes.Add(new ThrumbneilSize(){ Width = 800, Height = 800 , NameSize = "800_800" });
             */
    Context = context;
    }
    public IEnumerable<ThrumbneilSize> GetAll()
    {
        return Context.ThrumbneilSizes.Include(x=>x.ThrumbneilSizes).ToList();
    }
    public void Add(ThrumbneilSize thrumbneilSize)
    {
      Context.ThrumbneilSizes.Add(thrumbneilSize);
    }
    public void Save()
    {
        Context.SaveChanges();
    }
    public void Remove(ThrumbneilSize thrumbneilSize)
    {
      
      Context.Remove(thrumbneilSize);
    }
    
    /* 
    public void TestAdd(ThrumbneilSize imageSize)
    {
      ImageSizes.Add(imageSize);
    }
    public List<ThrumbneilSize> TestGetAll()
    {
      return ImageSizes;
    }
    */
  
  }

}