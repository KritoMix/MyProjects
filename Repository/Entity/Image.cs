using System;
using System.Collections;
using System.Collections.Generic;
namespace AdminPanel
{
    public class Image
    {
      public int Id {get;set;}
      public string Name {get;set;}
      public string Path {get;set;}
      public ICollection<Thrumbneil> Thrumbneils {get;set;}

    }
}