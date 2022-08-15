using Check_CarPrice.Persistence;
using Check_CarPrice.ViewModels;
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
    public partial class CarPriceListMaster_View : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private User _userlogin;
        private ObservableCollection<DataCharacterCar> _charactercar;

        protected override bool OnBackButtonPressed() => true;
       
        public DataTransactionCar _dtatran;
     
        public CarPriceListMaster_View(User user, DataTransactionCar dta)
        {
            Appz_WebService iAPI = new Appz_WebService();
            _dtatran = dta;
            _userlogin = user;
            InitializeComponent();
          
        }

        protected override async void OnAppearing()
        {
            if (_charactercar == null)
            {
                _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

                var charac = await _connection.Table<DataCharacterCar>().ToListAsync();
                _charactercar = new ObservableCollection<DataCharacterCar>(charac);

                if (_dtatran.type_grp_car == "01")
                {
                    SetData();
                }

            }

            base.OnAppearing();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuCarPriceList_View(_userlogin, _dtatran));
        }

        private void SetData()
        {
            SentToAPI mMo = new SentToAPI();
            mMo.personnel_code = _userlogin.data.First().personnel_code;
            mMo.storename = "ST_CARDETAILALL_GET";
            mMo.c1 = _dtatran.app_id;
            var alldata = GETCARDETAILALL_lIST(mMo);
            if (alldata != null)
            {

                var dta = alldata.FirstOrDefault();
                lbLicno.Text = dta.licno;
                lbAppno.Text = dta.applno_car;
                lbProvince.Text = dta.provice;

                lbGrpCar.Text = _charactercar.Where(a => a.cde_head_tail == dta.type_grp_car && a.cde_grpcar == dta.grp_car && a.cde_style_car == dta.cde_pickup).FirstOrDefault().name_grpcar;
                lbPickup.Text = _charactercar.Where(a => a.cde_head_tail == dta.type_grp_car && a.cde_grpcar == dta.grp_car && a.cde_style_car == dta.cde_pickup).FirstOrDefault().name_style_car;
                if (dta.cde_special != "") lbSpecial.Text = _charactercar.Where(a => a.cde_head_tail == dta.type_grp_car && a.cde_grpcar == dta.grp_car && a.cde_style_car == dta.cde_special).FirstOrDefault().name_style_car;
                else lbSpecial.Text = "-";

                lbTypeCar.Text = dta.name_typ_car;
                lbBrand.Text = dta.brand_name;
                lbModel.Text = dta.model == "" ? "-" : dta.model;
                lbEngno.Text = dta.engno;
                lbChasno.Text = dta.chasno;
                lbFuel.Text = dta.fuel == "01" ? "ดีเซล" : "ก๊าซ CNG";
                lbYear.Text = "พ.ศ. " + dta.year_car;
                lbAccessories.Text = dta.accessory_name;

                lbHeadPrice.Text = dta.head_price == "" ? "" : dta.head_price + " บาท";
                lbMiddlePrice.Text = dta.middle_price == "" ? "" : dta.middle_price + " บาท";
                lbFinamt.Text = dta.finamt == "" ? "" : dta.finamt + " บาท";

                lbDistance01.Text = dta.Distance_01 == "" ? "" : dta.Distance_01.ToString() + " เดือน";
                lbDistance02.Text = dta.Distance_02 == "" ? "" : dta.Distance_02.ToString() + " เดือน";
                lbRate01.Text = dta.rate_01 == "" ? "" : dta.rate_01.ToString() + " %";
                lbRate02.Text = dta.rate_02 == "" ? "" : dta.rate_02.ToString() + " %";

                lbPolicy.Text = dta.name_policy == "" ? "-" : dta.name_policy;
                lbRemarkCheck.Text = dta.remark_cdecar == "" ? "-" : dta.remark_cdecar;
                lbStatus.Text = dta.namestatus;

            }
        }

        public List<DataCarDetail> GETCARDETAILALL_lIST(SentToAPI mMo)
        {
            Appz_WebService iAPI = new Appz_WebService();
            var response = iAPI.Sent(mMo);
            if (response != null)
            {

                var posts = JsonConvert.DeserializeObject<List<DataCarDetail>>(response.Content);
                return posts;
            }
            else return null;
        }

        public SentToAPI GETCARDETAIL()
        {
            SentToAPI mMo = new SentToAPI();
            mMo.personnel_code = _userlogin.data.First().personnel_code;
            mMo.storename = "ST_CARDETAILALL_GET";
            mMo.c1 = _dtatran.app_id;
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