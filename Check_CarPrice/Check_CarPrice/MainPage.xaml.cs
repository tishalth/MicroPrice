
using Check_CarPrice.Persistence;
using Check_CarPrice.View;
using Check_CarPrice.WebService;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.Login_Model;

namespace Check_CarPrice
{
    public partial class MainPage : TabbedPage
    {
       
        
        public MainPage(User user)
        {
          
            InitializeComponent();
            this.Children.Add(new Home_View(user) { IconImageSource = "home.png" });
            this.Children.Add(new CarPriceList_View(user) { IconImageSource = "dollar.png" });
            this.Children.Add(new Approve_View(user) { IconImageSource = "checkmark.png" });
            this.Children.Add(new Other_View(user) { IconImageSource = "other.png" });
           
        }




    }
}
