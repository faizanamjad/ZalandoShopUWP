using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZalandoShop.Services;

namespace ZalandoShop.ViewModels
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            

            SimpleIoc.Default.Register<IZalandoServcie, ZalandoService>();
            SimpleIoc.Default.Register<ZalandoViewModel>();
        }

        public ZalandoViewModel ZalandoMain => ServiceLocator.Current.GetInstance<ZalandoViewModel>();
    }
}
