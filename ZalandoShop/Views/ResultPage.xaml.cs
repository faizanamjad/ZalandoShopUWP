using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ZalandoShop.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResultPage : Page
    {
        public ResultPage()
        {
            this.InitializeComponent();

            var collection = new IncrementalLoadingCollection<PeopleSource, Person>();
            PeopleListView.ItemsSource = collection;
        }

        public class PeopleSource : IIncrementalSource<Person>
        {
            private readonly List<Person> _people;

            public PeopleSource()
            {
                // Creates an example collection.
                _people = new List<Person>();

                for (int i = 1; i <= 200; i++)
                {
                    var p = new Person { Name = "Person " + i };
                    _people.Add(p);
                }
            }



            public async Task<IEnumerable<Person>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
            {
                // Gets items from the collection according to pageIndex and pageSize parameters.
                var result = (from p in _people
                              select p).Skip(pageIndex * pageSize).Take(pageSize);

                // Simulates a longer request...
                await Task.Delay(1000);

                return result;
            }
        }

    }
    public class Person
    {
        public string Name { get; set; }
    }

}