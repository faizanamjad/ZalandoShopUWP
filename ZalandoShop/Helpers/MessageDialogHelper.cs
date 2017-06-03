using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace ZalandoShop.Helpers
{
   public  class MessageDialogHelper
    {
     

        public static async void MessageBox(string message)
        {

            var dialog = new MessageDialog(message.ToString());
            await dialog.ShowAsync();

        }
    }
}
