using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZalandoShop.Models;

namespace ZalandoShop.Services
{
    public class ZalandoService : IZalandoServcie
    {
        HttpClient client = new HttpClient();
        public async Task<ArticleRoot> GetArticles(string url)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            var collection = JsonConvert.DeserializeObject<ArticleRoot>(jsonString);
            return collection;
          
        }

    }
}
