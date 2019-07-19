
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
namespace AdminPanel
{
  public class TrumbneilRepository
  {
     // private List<ThrumbneilSize> ImageSizes {get;set;}
    public AppContext Context {get;}
    public TrumbneilRepository(AppContext context)
    {       
    Context = context;
    }
    public IEnumerable<Thrumbneil> GetAll()
    {
        return Context.Thrumbneils;
    }
    public void Add(Thrumbneil thrumbneil)
    {
      Context.Thrumbneils.Add(thrumbneil);
    }
    public void Save()
    {
        Context.SaveChanges();
    }
    public void Remove(Thrumbneil thrumbneil)
    {
      
      Context.Remove(thrumbneil);
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