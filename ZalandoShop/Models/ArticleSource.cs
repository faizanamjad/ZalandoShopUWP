using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;
using ZalandoShop.ViewModels;

namespace ZalandoShop.Models
{
    public class ArticleSource : IIncrementalSource<Content>
    {
        public static ObservableCollection<Content> ArticeList { get; set; }

        List<Content> _articlelist;
        public ArticleSource()
        {
            _articlelist = new List<Content>();
            foreach (var item in ArticeList)
            {
                var articleItem = new Content {
                    Name = item.name,
                    Size = item.units.FirstOrDefault().size,
                    Price = item.units.FirstOrDefault().price.formatted,
                    ImageUrl = item.media.images.FirstOrDefault().smallUrl
                    
                };
                _articlelist.Add(articleItem);
            }
        }


        public async Task<IEnumerable<Content>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Gets items from the collection according to pageIndex and pageSize parameters.
            var result = (from p in _articlelist
                          select p).Skip(pageIndex * pageSize).Take(pageSize);

            // Simulates a longer request...
            await Task.Delay(1000);

            return result;
        }
    }
}
