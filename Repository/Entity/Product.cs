using System;
using System.Collections;
using System.Collections.Generic;
namespace AdminPanel
{
    public class Product
    {
      public int Id {get;set;}
      public string Name {get;set;}

      public string Dictionary {get;set;}
      public int Price {get;set;}
      public int ImageId {get;set;}
      public Image Image {get;set;}
    }
}