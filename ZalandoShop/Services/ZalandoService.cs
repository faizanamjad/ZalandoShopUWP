using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        //https://api.zalando.com/articles?category=mens-clothing&category=mens-shoes&category=premium-mens&category=sports-mens&category=mens-gift-cards&fullText=jeans&gender=male&fields=id,name,brand.key,brand.name,units.id,units.size,units.price.formatted

        public async Task<ArticleRoot> GetArticles(string usertext,string usergender)
        {
            string category = string.Empty;
            if (usergender == "male")
            {
                category = "category=mens-clothing&category=mens-shoes&category=premium-mens&category=sports-mens&category=mens-gift-cards";

            }
            else if (usergender == "female")
            {
                category = "category=womens-clothing&category=womens-shoes&category=premium-womens&category=sports-womens&category=womens-beauty";
            }
                string selectedfields = "id,name,brand.key,brand.name,units.id,units.size,units.price.formatted,media";

            string url = @"https://api.zalando.com/articles?"+category+"&fullText="+ usertext+ "&gender="+ usergender+ "&fields="+selectedfields+"&pageSize=200";
            
            HttpResponseMessage response = await client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            var collection = JsonConvert.DeserializeObject<ArticleRoot>(jsonString);
            return collection;

        }


        public async Task<List<string>> GetAutoSuggestItems()
        {
            List<string> _suggestedItemList = new List<string>();
            try
            {
                string url = "https://api.zalando.com/facets";
                HttpResponseMessage response = await client.GetAsync(url);
                var jsonString = await response.Content.ReadAsStringAsync();
                var suggestedItems = JsonConvert.DeserializeObject<List<FacetRoot>>(jsonString);

                for (int i = 0; i < suggestedItems.Count; i++)
                {
                    var collection = suggestedItems[i].facets;

                    foreach (var item in collection)
                    {
                        _suggestedItemList.Add(item.displayName);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return _suggestedItemList;
        }
    }
}
