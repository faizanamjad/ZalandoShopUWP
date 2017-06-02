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
using System.Threading;

namespace ZalandoShop.ViewModels
{
    public class ZalandoViewModel : ViewModelBase
    {
        private readonly IZalandoServcie _zalnadoService;

        public ZalandoViewModel(IZalandoServcie zalandoService)
        {
            _zalnadoService = zalandoService;

            Gender = "male";
        }
      
    
        #region Properties

        private string _isProgressBarVisible;
        public string IsProgressBarVisible
        {
            get { return _isProgressBarVisible; }
            set { Set(ref _isProgressBarVisible, value); }
        }

        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set { Set(ref _gender, value); }
        }


        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set { Set(ref _SearchText, value); }
        }


        private List<string> _itemList;
        public List<string> ItemList
        {
            get { return _itemList; }
            set { Set(ref _itemList, value); }
        }


        private List<string> _suggestedItemList;
        public List<string> SuggestedItemList
        {
            get { return _suggestedItemList; }
            set { Set(ref _suggestedItemList, value); }
        }



        private ObservableCollection<Content> _contentList;
        public ObservableCollection<Content> ContentList
        {
            get { return _contentList; }
            set { Set(ref _contentList, value); }
        }


        
        private ObservableCollection<Content> _articleItemList;
        public ObservableCollection<Content> ArticleItemList
        {
            get { return _articleItemList; }
            set { Set(ref _articleItemList, value); }
        }

        #endregion Properties

        #region Commands


        private RelayCommand _loadCommand;
        public RelayCommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new RelayCommand(async () =>
                    {
                        // Detect if Internet can be reached
                        if (NetworkHelper.Instance.ConnectionInformation.IsInternetAvailable)
                        {
                            IsProgressBarVisible = "True";

                            // get all items for autosugegstionBox
                            List<string> items = await _zalnadoService.GetAutoSuggestItems();

                            if (items != null)
                            {
                                ItemList = new List<string>(items);
                            }
                            else
                            {
                                MessageDialogHelper.AlertMessage("No Suggested Item  Found.");
                            }
                        }

                        else
                        {
                            MessageDialogHelper.AlertMessage("No Internet Connection Available");
                        }

                        IsProgressBarVisible = "False";

                    });

                  
                }

                return _loadCommand;
            }
        }

        private RelayCommand _textChnagedCommand;
        public RelayCommand TextChangedCommand
        {
            get
            {
                if (_textChnagedCommand == null)
                {
                    _textChnagedCommand = new RelayCommand(() =>
                    {
                        if (ItemList != null)
                        {
                            var filtered = ItemList.Where(p => p.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
                            SuggestedItemList = new List<string>(filtered.Distinct());
                            
                        }

                    });
                }

                return _textChnagedCommand;
            }
        }



        private RelayCommand _querySubmittedCommand;
        public RelayCommand QuerySubmittedCommand
        {
            get
            {
                if (_querySubmittedCommand == null)
                {
                    _querySubmittedCommand = new RelayCommand(async () =>
                    {
                        // Detect if Internet can be reached
                        if (NetworkHelper.Instance.ConnectionInformation.IsInternetAvailable)
                        {
                            IsProgressBarVisible = "True";

                            if (!string.IsNullOrEmpty(SearchText))
                            {
                                // get all artciles 
                                var articles = await _zalnadoService.GetArticles(SearchText, Gender);

                                if (articles != null)
                                {
                                    List<Content> items = articles.content;
                                    ContentList = new ObservableCollection<Content>(items);
                                    ArticleSource.ArticeList = new ObservableCollection<Content>(items);
                                }
                            }
                           
                            else
                            {
                                MessageDialogHelper.AlertMessage("please select search item");
                            }
                        }

                        else
                        {
                            MessageDialogHelper.AlertMessage("No Internet Connection Available");
                        }

                        IsProgressBarVisible = "False";

                    });


                }

                return _querySubmittedCommand;
            }
        }
       
        #endregion Commands

    
    }
}
