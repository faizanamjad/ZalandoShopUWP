using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using ZalandoShop.Helpers;
using ZalandoShop.Models;
using ZalandoShop.Services;

namespace ZalandoShop.ViewModels
{
    public class ZalandoViewModel : ViewModelBase
    {
        private readonly IZalandoServcie _zalnadoService;
        
        public ZalandoViewModel(IZalandoServcie zalandoService)
        {
            _zalnadoService = zalandoService;
        }


        private ObservableCollection<Content> _contentList;

        public ObservableCollection<Content> ContentList
        {
            get { return _contentList; }
            set { Set(ref _contentList, value); }
        }

        private ObservableCollection<string> _itemList;

        public ObservableCollection<string> ItemList
        {
            get { return _itemList; }
            set { Set(ref _itemList, value); }
        }

        //private FeedItem _selectedFeedItem;

        //public FeedItem SelectedFeedItem
        //{
        //    get { return _selectedFeedItem; }
        //    set { Set(ref _selectedFeedItem, value); }
        //}

        private RelayCommand _loadCommand;

        public RelayCommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new RelayCommand(async() =>
                    {
                        // Detect if Internet can be reached
                        if (NetworkHelper.Instance.ConnectionInformation.IsInternetAvailable)
                        {
                            // get all artciles for autocomplete searchbox
                            var articles = await _zalnadoService.GetArticles("https://api.zalando.com/articles?category=clothing&category=Shoes&gender=male&pageSize=200&");
                            if (articles.totalElements > 0)
                            {
                                List<Content> items = articles.content;
                                ContentList = new ObservableCollection<Content>(items);
                                ObservableCollection<string> namelist = new ObservableCollection<string>();
                                foreach (var item in ContentList)
                                {
                                    namelist.Add(item.name);
                                }

                                ItemList = namelist;
                            }
                            else
                            {
                                MessageDialogHelper.AlertMessage("No Record FOund");
                            }
                        }
                        else
                        {
                            MessageDialogHelper.AlertMessage("No Internet Connection Available");
                        }
                       

                    });
                }

                return _loadCommand;
            }
        }

        private RelayCommand _loadResultCommand;


        public RelayCommand LoadResultCommand
        {
            get
            {
                if (_loadResultCommand == null)
                {
                    _loadResultCommand = new RelayCommand(() =>
                    {
                        ContentList.Count();
                        //Debug.WriteLine(SelectedFeedItem.Title);
                    });
                }

                return _loadResultCommand;
            }
        }

        //public RelayCommand ItemSelectedCommand
        //{
        //    get
        //    {
        //        if (_itemSelectedCommand == null)
        //        {
        //            _itemSelectedCommand = new RelayCommand(() =>
        //            {
        //                //Debug.WriteLine(SelectedFeedItem.Title);
        //            });
        //        }

        //        return _itemSelectedCommand;
        //    }
        //}
    }
}
