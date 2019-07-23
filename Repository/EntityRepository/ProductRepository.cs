using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AdminPanel
{
  public class ProductRepository
  {
    public AppContext Context {get;}
    public ProductRepository(AppContext context)
    {       
    Context = context;
    }
    public IEnumerable<Product> GetAll()
    {
        return Context.Product.Include(x=>x.Image).ToList();
    }
    public void Add(Product product)
    {
      Context.Product.Add(product);
    }
    public void Save()
    {
        Context.SaveChanges();
    }
    public void Remove(Product product)
    {
      
      Context.Remove(product);
    }
  }

}