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
using GalaSoft.MvvmLight.Views;

namespace ZalandoShop.ViewModels
{
    public class ZalandoViewModel : ViewModelBase
    {
        private readonly IZalandoServcie _zalnadoService;
        private readonly INavigationService _navigationService;

        public ZalandoViewModel(IZalandoServcie zalandoService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _zalnadoService = zalandoService;

        }


        #region Properties

        private string _isProgressBarVisible;
        public string IsProgressBarVisible
        {
            get { return _isProgressBarVisible; }
            set { Set(ref _isProgressBarVisible, value); }
        }

        private Genders _selectedGender;
        public Genders SelectedGender
        {
            get { return _selectedGender; }
            set { Set(ref _selectedGender, value); }
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
                        await GetSuggestItems();

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
                    _querySubmittedCommand = new RelayCommand(() =>
                   {
                       // await SearchArtcles();
                   });


                }

                return _querySubmittedCommand;
            }
        }


        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(async () =>
                    {
                        if (await SearchArtcles())
                        {
                            _navigationService.NavigateTo("ResultPage");
                        }
                        else
                        {
                            MessageDialogHelper.MessageBox("please select search item");
                        }

                    });

                }

                return _searchCommand;
            }
        }

        #endregion Commands

        #region Methods


        private async Task GetSuggestItems()
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
                    MessageDialogHelper.MessageBox("No Suggested Item  Found.");
                }
            }

            else
            {
                MessageDialogHelper.MessageBox("No Internet Connection Available");
            }

            IsProgressBarVisible = "False";
        }

        private async Task<bool> SearchArtcles()
        {
            bool result = false;
            // Detect if Internet can be reached
            if (NetworkHelper.Instance.ConnectionInformation.IsInternetAvailable)
            {
                IsProgressBarVisible = "True";

                if (!string.IsNullOrEmpty(SearchText))
                {
                    // get all artciles 
                    var articles = await _zalnadoService.GetArticles(SearchText, SelectedGender.ToString());

                    if (articles.content != null)
                    {
                        List<Content> items = articles.content;
                        ContentList = new ObservableCollection<Content>(items);
                        ArticleSource.ArticeList = new ObservableCollection<Content>(items);
                        result = true;
                    }
                    else
                    {
                        MessageDialogHelper.MessageBox("No Such Article Found !");
                    }
                }

                else
                {
                    MessageDialogHelper.MessageBox("please select search item");
                }
            }

            else
            {
                MessageDialogHelper.MessageBox("No Internet Connection Available");
            }

            return result;
            IsProgressBarVisible = "False";
        }

        #endregion
    }
}
