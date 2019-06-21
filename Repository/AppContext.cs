using Microsoft.EntityFrameworkCore;

namespace AdminPanel 
{
  public class AppContext : DbContext
  {
     // public AppContext(DbContextOptions<AppContext> options) : base(options)
      //{

      //}
      public DbSet<Image> Images {get;set;}
      public DbSet<Thrumbneil> Thrumbneils {get;set;}
      public DbSet<SizeImage> ImageSizes {get;set;}
  }

}