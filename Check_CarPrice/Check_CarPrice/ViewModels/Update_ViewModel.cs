using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Check_CarPrice.ViewModels
{
   public class Update_ViewModel : BaseViewModel
    {
        [Obsolete]
        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Device.OpenUri(new System.Uri(url));
        });

        public Update_ViewModel()
        {
            Title = "แก้ไขรายการรถ";
        }
    }
}
