using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoShop.Models
{
   public class FacetRoot
    {
        public string filter { get; set; }
        public List<Facet> facets { get; set; }
    }

    public class Facet
    {
        public string key { get; set; }
        public string displayName { get; set; }
        public int count { get; set; }
    }

    
}
