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
      async public static void AlertMessage (string message)
        {
            MessageDialog msg = new MessageDialog(message);
            await msg.ShowAsync();
        }
    }
}
