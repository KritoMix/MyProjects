using Microsoft.EntityFrameworkCore;

namespace AdminPanel 
{
  public class AppContext : DbContext
  {
     public AppContext(DbContextOptions<AppContext> options) : base(options)
      {

      }
      public DbSet<Product> Product {get;set;}
      public DbSet<Image> Images {get;set;}
      public DbSet<Thrumbneil> Thrumbneils {get;set;}
      public DbSet<ThrumbneilSize> ThrumbneilSizes {get;set;}
      
  }

}