using System;
namespace AdminPanel
{
  public class Thrumbneil
  {
      public int Id {get;set;}
      public int ImageId {get;set;}
      public int ThrumbneilSizeId {get;set;}
      public string Name {get;set;}
      public string Path {get;set;}
      public Image Image {get;set;}
      public ThrumbneilSize ThrumbneilSize {get;set;}
  }

}