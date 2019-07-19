using System.Collections;
using System.Collections.Generic;
using System;
namespace AdminPanel
{
  public class ThrumbneilSize
  {
    public int Id {get;set;}
    public string NameSize {get;set;}
    public int Height {get;set;}
    public int Width {get;set;}
    public ICollection<Thrumbneil> ThrumbneilSizes {get;set;}
  }

}