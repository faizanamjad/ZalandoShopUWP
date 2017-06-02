using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZalandoShop.Models;

namespace ZalandoShop.Services
{
   public interface IZalandoServcie 
    {
        Task<ArticleRoot> GetArticles(string usertext, string usergender);

        Task<List<string>> GetAutoSuggestItems();
    }
}
