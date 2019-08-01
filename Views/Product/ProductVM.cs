using System.Collections.Generic;
namespace AdminPanel
{
    public class ProductVM
    {
        public Product ProductM {get;set;}

        public List<Image> Images {get;set;}

        public Image CurrentImage {get;set;}
    }
}