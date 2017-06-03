using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZalandoShop.Services;
using ZalandoShop.Views;

namespace ZalandoShop.ViewModels
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        /// 
        public const string ResultPageKey = "ResultPage";
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new NavigationService();
            nav.Configure(ResultPageKey, typeof(ResultPage));


            SimpleIoc.Default.Register<INavigationService>(() => nav);

            SimpleIoc.Default.Register<IZalandoServcie, ZalandoService>();
            SimpleIoc.Default.Register<ZalandoViewModel>();
        }

        public ZalandoViewModel ZalandoMain => ServiceLocator.Current.GetInstance<ZalandoViewModel>();
    }
}
