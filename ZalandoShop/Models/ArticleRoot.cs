using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoShop.Models
{
    public class ArticleRoot
    {
        public List<Content> content { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int page { get; set; }
        public int size { get; set; }

     

    }

    public class Brand
    {
        public string key { get; set; }
        public string name { get; set; }
    }

    public class Price
    {
        public string formatted { get; set; }
    }

    public class Unit
    {
        public string id { get; set; }
        public string size { get; set; }
        public Price price { get; set; }
    }

    public class Image
    {
        public int orderNumber { get; set; }
        public string type { get; set; }
        public string thumbnailHdUrl { get; set; }
        public string smallUrl { get; set; }
        public string smallHdUrl { get; set; }
        public string mediumUrl { get; set; }
        public string mediumHdUrl { get; set; }
        public string largeUrl { get; set; }
        public string largeHdUrl { get; set; }
    }

    public class Media
    {
        public List<Image> images { get; set; }
    }

    public class Content
    {
        public string id { get; set; }
        public string name { get; set; }
        public Brand brand { get; set; }
        public List<Unit> units { get; set; }
        public Media media { get; set; }

        public string Name { get; set; }
        public string Size { get; set; }
        public string Price { get; set; }

        public string ImageUrl { get; set; }
    }

   
}