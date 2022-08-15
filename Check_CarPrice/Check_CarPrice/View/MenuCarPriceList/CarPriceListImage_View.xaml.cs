using Check_CarPrice.Persistence;
using Check_CarPrice.WebService;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.CarPriceList_Model;
using static Check_CarPrice.Model.Login_Model;

namespace Check_CarPrice.View.MenuCarPriceList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarPriceListImage_View : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private User _userlogin;
        //private ObservableCollection<DataGrantMapUser> _gmapuser;

        protected override bool OnBackButtonPressed() => true;
        //public LoginData _userlogin;
        public DataTransactionCar _dta;
        public CarPriceListImage_View(User user, DataTransactionCar dta)
        {
            _dta = dta;
            _userlogin = user;
            InitializeComponent();
            SetData();
            ////HideIMG();
        }


        private void SetData()
        {
            CarPriceListImage_WebService iAPI = new CarPriceListImage_WebService();
            lbLicno.Text = _dta.licno;
            lbTitle.Text = _dta.type_grp_car == "01" ? "รูปภาพตัวแม่" : "รูปภาพตัวลูก";

            SentToAPI mMo = new SentToAPI();
            mMo.personnel_code = _userlogin.data.First().personnel_code;
            mMo.storename = "ST_CARDETAIL_GET";
            mMo.c1 = _dta.app_id;
            var ddta = GETCARDETAIL_lIST(mMo);
            if(ddta.Count()!=0)
            //if (iAPI.GetImage() != null)
            {
                //var dta = iAPI.GetImage().Where(a => a.app_id == _dta.app_id && a.status == "1").FirstOrDefault();
                if (ddta != null)
                {
                   var dta= ddta.FirstOrDefault();
                    if (dta.type_grp_car == "01")
                    {
                        PicMas.IsVisible = true;
                        PicSub.IsVisible = false;

                        Mimg1_head.Source = dta.img1_head;
                        Mimg2_head.Source = dta.img2_head;
                        Mimg3_head.Source = dta.img3_head;
                        Mimg26.Source = dta.img26;
                        Mimg27.Source = dta.img27;
                        Mimg4_head.Source = dta.img4_head;
                        Mimg5_head.Source = dta.img5_head;
                        Mimg6_head.Source = dta.img6_head;
                        Mimg7_head.Source = dta.img7_head;
                        Mimg8_head.Source = dta.img8_head;
                        Mimg9_head.Source = dta.img9_head;
                        Mimg10_head.Source = dta.img10_head;
                        Mimg11_head.Source = dta.img11_head;
                        Mimg28.Source = dta.img28;
                        Mimg12_head.Source = dta.img12_head;
                        Mimg13_head.Source = dta.img13_head;
                        Mimg14_head.Source = dta.img14_head;
                        Mimg15_head.Source = dta.img15_head;
                        Mimg23.Source = dta.img23;
                        Mimg24.Source = dta.img24;
                        Mimg25.Source = dta.img25;
                        //Mimg29.Source = dta.img29;
                    }
                    else
                    {
                        PicMas.IsVisible = false;
                        PicSub.IsVisible = true;

                        Simg16_tail.Source = dta.img16_tail;
                        Simg17_tail.Source = dta.img17_tail;
                        Simg18_tail.Source = dta.img18_tail;
                        Simg19_tail.Source = dta.img19_tail;
                        Simg26.Source = dta.img26;
                        Simg27.Source = dta.img27;
                        Simg28.Source = dta.img28;
                        Simg23.Source = dta.img23;
                        Simg24.Source = dta.img24;
                        Simg25.Source = dta.img25;
                        Simg20_tail.Source = dta.img20_tail;
                        Simg21_tail.Source = dta.img21_tail;
                    }
                }
            }


        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuCarPriceList_View(_userlogin,_dta));
        }

        public void HideIMG()
        {
            Mimg1_head.IsVisible = Mimg1_head == null ? false : true;
            Mimg2_head.IsVisible = Mimg2_head == null ? false : true;
            Mimg3_head.IsVisible = Mimg3_head == null ? false : true;
            Mimg26.IsVisible = Mimg26 == null ? false : true;
            Mimg27.IsVisible = Mimg27 == null ? false : true;
            Mimg4_head.IsVisible = Mimg4_head == null ? false : true;
            Mimg5_head.IsVisible = Mimg5_head == null ? false : true;
            Mimg6_head.IsVisible = Mimg6_head == null ? false : true;
            Mimg7_head.IsVisible = Mimg7_head == null ? false : true;
            Mimg8_head.IsVisible = Mimg8_head == null ? false : true;
            Mimg9_head.IsVisible = Mimg9_head == null ? false : true;
            Mimg10_head.IsVisible = Mimg10_head == null ? false : true;
            Mimg11_head.IsVisible = Mimg11_head == null ? false : true;
            Mimg28.IsVisible = Mimg28 == null ? false : true;
            Mimg12_head.IsVisible = Mimg12_head == null ? false : true;
            Mimg13_head.IsVisible = Mimg13_head == null ? false : true;
            Mimg14_head.IsVisible = Mimg14_head == null ? false : true;
            Mimg15_head.IsVisible = Mimg15_head == null ? false : true;
            Mimg23.IsVisible = Mimg23 == null ? false : true;
            Mimg24.IsVisible = Mimg24 == null ? false : true;
            Mimg25.IsVisible = Mimg25 == null ? false : true;
            Simg16_tail.IsVisible = Simg16_tail == null ? false : true;
            Simg17_tail.IsVisible = Simg17_tail == null ? false : true;
            Simg18_tail.IsVisible = Simg18_tail == null ? false : true;
            Simg19_tail.IsVisible = Simg19_tail == null ? false : true;
            Simg26.IsVisible = Simg26 == null ? false : true;
            Simg27.IsVisible = Simg27 == null ? false : true;
            Simg28.IsVisible = Simg28 == null ? false : true;
            Simg23.IsVisible = Simg23 == null ? false : true;
            Simg24.IsVisible = Simg24 == null ? false : true;
            Simg20_tail.IsVisible = Simg20_tail == null ? false : true;
            Simg21_tail.IsVisible = Simg21_tail == null ? false : true;
           

        }

        public List<DataImage> GETCARDETAIL_lIST(SentToAPI mMo)
        {
            Appz_WebService iAPI = new Appz_WebService();
            var response = iAPI.Sent(mMo);
            if (response != null)
            {

                var posts = JsonConvert.DeserializeObject<List<DataImage>>(response.Content);
                return posts;
            }
            else return null;
        }


        public SentToAPI GETCARDETAIL()
        {
            SentToAPI mMo = new SentToAPI();
            mMo.personnel_code = _userlogin.data.First().personnel_code;
            mMo.storename = "ST_CARDETAIL_GET";
            mMo.c1 = _dta.app_id;
            mMo.c2 = "";
            mMo.c3 = "";
            mMo.c4 = "";
            mMo.c5 = "";
            mMo.c6 = "";
            mMo.c7 = "";
            mMo.c8 = "";
            mMo.c9 = "";
            mMo.c10 = "";
            mMo.c11 = "";
            mMo.c12 = "";
            mMo.c13 = "";
            mMo.c14 = "";
            mMo.c15 = "";
            mMo.c16 = "";
            mMo.c17 = "";
            mMo.c18 = "";
            mMo.c19 = "";
            mMo.c20 = "";
            mMo.c21 = "";
            mMo.c22 = "";
            mMo.c23 = "";
            mMo.c24 = "";
            mMo.c25 = "";
            mMo.c26 = "";
            mMo.c27 = "";
            mMo.c28 = "";
            mMo.c29 = "";
            mMo.c30 = "";
            return mMo;
        }
    }
}