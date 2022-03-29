using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.Login_Model;

namespace Check_CarPrice.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Other_View : ContentPage
    {
        
        protected override bool OnBackButtonPressed() => true;

        public User _userlogin;
        public Other_View(User user)
        {
            _userlogin = user;
            InitializeComponent();

        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login_View());
        }
    }
}