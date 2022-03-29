using Check_CarPrice.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Check_CarPrice.Model.Appz_Model;

[assembly: ExportFont("tahomo.ttf", Alias = "tahomo")]
[assembly: ExportFont("tahomo.ttf", Alias = "angsau")]
[assembly: ExportFont("tahomo.ttf", Alias = "angsaub")]
[assembly: ExportFont("IBMPlexSansThai-Medium.ttf", Alias = "PlexSans")]

namespace Check_CarPrice
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Xamarin.Essentials.VersionTracking.Track();


            MainPage = new NavigationPage(new Login_View());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
