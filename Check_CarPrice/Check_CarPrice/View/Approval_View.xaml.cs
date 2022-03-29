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
using static Check_CarPrice.Model.Approve_Model;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.Login_Model;

namespace Check_CarPrice.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Approval_View : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private User _userlogin;
        private ObservableCollection<DataCharacterCar> _charactercar;

        protected override bool OnBackButtonPressed() => true;
      
        public DataCarDetail _dta;
        public Approval_View(User user,SelectedItemChangedEventArgs dta)
        {
            _userlogin = user;
            Appz_WebService iAPI = new Appz_WebService();
            var getdata = GETCARDETAILALL_lIST(((DataApproveList)dta.SelectedItem).app_id);
            if (getdata != null)
            {
                _dta = getdata.FirstOrDefault();

            }
            else
            {
                _dta = null;
            }

            InitializeComponent();
          
        }

        protected override async void OnAppearing()
        {
            if (_charactercar == null)
            {
                _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
                var charac = await _connection.Table<DataCharacterCar>().ToListAsync();
                _charactercar = new ObservableCollection<DataCharacterCar>(charac);

                SetData();
            }


            base.OnAppearing();
        }



        private async void btnApprove_Clicked(object sender, EventArgs e)
        {
            Approval_WebService iAPI = new Approval_WebService();
            var response = await DisplayAlert("แจ้งเตือน", "คุณต้องการอนุมัติรายการนี้ใช่หรือไม่", "ใช่", "ไม่ใช่");
            if (response == true)
            {
                if (!iAPI.UpdateTransactionApprove(_dta.app_id, "9", _userlogin.data.Select(a=>a.personnel_code).FirstOrDefault()))
                {
                    await DisplayAlert("แจ้งเตือน", "ไม่สามารถทำรายการได้", "ตกลง");
                }
                else
                {
                    await DisplayAlert("แจ้งเตือน", "ทำรายการสำเร็จ", "ตกลง");
                    await Navigation.PushAsync(new MainPage(_userlogin));
                }
            }
            
        }
        private async void btnNotApprove_Clicked(object sender, EventArgs e)
        {
            Approval_WebService iAPI = new Approval_WebService();
            var response = await DisplayAlert("แจ้งเตือน", "ไม่อนุมัติรายการนี้ใช่หรือไม่", "ใช่", "ไม่ใช่");
            if (response == true)
            {
                if(!iAPI.UpdateTransactionApprove(_dta.app_id, "8", _userlogin.data.Select(a=>a.personnel_code).FirstOrDefault()))
                {
                    await DisplayAlert("แจ้งเตือน", "ไม่สามารถทำรายการได้", "ตกลง");
                }
                else
                {
                    await DisplayAlert("แจ้งเตือน", "ทำรายการสำเร็จ", "ตกลง");
                    await Navigation.PushAsync(new MainPage(_userlogin));
                }
            }
        }


        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage(_userlogin));
        }

        private void SetData()
        {
            NumberMaskBehavior iModel = new NumberMaskBehavior();

            if (_userlogin.role.func.Where(a=> a.func_id=="F0005" || a.func_id== "F0006").ToList().Count() != 0)
            {
                PageAppproval.IsVisible = true; 
                Appz_WebService iAPI = new Appz_WebService();

                var dta = _dta;
                lbHeadPrice.Text = dta.head_price==""|| dta.head_price ==null?"-": iModel.FormatData(dta.head_price) + " บาท";
                lbMiddlePrice.Text = dta.middle_price==""|| dta.middle_price==null?"-":iModel.FormatData(dta.middle_price) + " บาท";
                lbLicno.Text = dta.licno==""|| dta.licno==null?"-":dta.licno;
                lbProvince.Text = dta.provice==""|| dta.provice==null?"-": dta.provice;
                lbChasno.Text = dta.chasno==""|| dta.chasno==null?"-":dta.chasno;

                if (dta.type_grp_car == "01")
                {
                    engine.IsVisible = true;
                    lbEngno.Text = dta.engno==""|| dta.engno==null?"-":dta.engno;
                    lbGrpCar.Text = _charactercar.Where(a => a.cde_head_tail == dta.type_grp_car && a.cde_grpcar == dta.grp_car && a.cde_style_car == dta.cde_pickup).FirstOrDefault().name_grpcar;
                    lbPickup.Text = _charactercar.Where(a => a.cde_head_tail == dta.type_grp_car && a.cde_grpcar == dta.grp_car && a.cde_style_car == dta.cde_pickup).FirstOrDefault().name_style_car;
                    if (dta.cde_special != "") lbSpecial.Text = _charactercar.Where(a => a.cde_head_tail == dta.type_grp_car && a.cde_grpcar == dta.grp_car && a.cde_style_car == dta.cde_special).FirstOrDefault().name_style_car;
                    else lbSpecial.Text = "-";

                    lbAccessories.IsVisible = true;
                    lbAccessories.Text = dta.nameaccessory==""|| dta.nameaccessory==null?"-":dta.nameaccessory;
                    Fuel.IsVisible = true;
                    lbFuel.Text = dta.fuel == "01" ? "ดีเซล" : dta.fuel == "02" ? "ก๊าซ CNG" : "-";

                }
                else if (dta.type_grp_car == "02")
                {
                    engine.IsVisible = false;
                    Access.IsVisible = false;
                    Fuel.IsVisible = false;

                    lbGrpCar.Text = _charactercar.Where(a => a.cde_head_tail == dta.type_grp_car && a.cde_type_style == dta.grp_car && a.cde_style_car == dta.cde_pickup).FirstOrDefault().name_type_style;
                    lbPickup.Text = _charactercar.Where(a => a.cde_head_tail == dta.type_grp_car && a.cde_type_style == dta.grp_car && a.cde_style_car == dta.cde_pickup).FirstOrDefault().name_style_car;
                    if (dta.cde_special != "") lbSpecial.Text = _charactercar.Where(a => a.cde_head_tail == dta.type_grp_car && a.cde_type_style == dta.grp_car && a.cde_style_car == dta.cde_special).FirstOrDefault().name_style_car;
                    else lbSpecial.Text = "-";
                }




                lbTypeCar.Text = dta.name_typ_car==""|| dta.name_typ_car==null?"-":dta.name_typ_car;
                lbModel.Text = dta.model==""|| dta.model==null?"-":dta.model;
                lbName.Text = dta.type_grp_car == "01" ?"ข้อมูลตัวแม่": dta.type_grp_car=="02"?"ข้อมูลตัวลูก":"-";
                lbFinamt.Text = dta.finamt==""|| dta.finamt==null?"-":iModel.FormatData(dta.finamt) + " บาท";
                lbDistance01.Text = dta.Distance_01==""|| dta.Distance_01==null?"-":dta.Distance_01.ToString() + " เดือน";
                lbDistance02.Text = dta.Distance_02==""|| dta.Distance_02==null?"-":dta.Distance_02.ToString() + " เดือน";
                lbRate01.Text = dta.rate_01==""|| dta.rate_01==null?"-":dta.rate_01.ToString() + " %";
                lbRate02.Text = dta.rate_02 == "" || dta.rate_02 == null ? "-" : dta.rate_02.ToString() + " %";
                lbDateApprove.Text = dta.create_date_approve==""|| dta.create_date_approve==null?"-":String.Format("{0:dd/MM/yyyy}", dta.create_date_approve);
                lbStatus.Text = dta.namestatus==""|| dta.namestatus==null?"-":dta.namestatus;
                lbRemark.Text = dta.remark_cdecar==""|| dta.remark_cdecar==null?"-":dta.remark_cdecar;

                if (_userlogin.role.func.Where(a => a.func_id == "F0005").ToList().Count() != 0)
                {
                    btnApprove.IsVisible = false;
                    btnNotApprove.IsVisible = false;
                    btnCancel.IsVisible = false;

                }
                else if(_userlogin.role.func.Where(a => a.func_id == "F0006").ToList().Count() != 0)
                {
                    btnApprove.IsVisible = true;
                    btnNotApprove.IsVisible = true;
                    btnCancel.IsVisible = true;
                }
                else if (_userlogin.role.func.Where(a => a.func_id == "F0005" && a.func_id == "F0006").ToList().Count() != 0)
                {
                    btnApprove.IsVisible = true;
                    btnNotApprove.IsVisible = true;
                    btnCancel.IsVisible = true;
                }

                if (dta.status_appcar == "9")
                {
                    btnApprove.IsVisible = false;
                    btnNotApprove.IsVisible = false;
                    btnCancel.IsVisible = false;
                   
                }

            


            }
            else
            {
                PageAppproval.IsVisible = false;
            }
            


        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage(_userlogin));
        }

        public List<DataCarDetail> GETCARDETAILALL_lIST(string appid)
        {
            Appz_WebService iAPI = new Appz_WebService();
            var response = iAPI.Sent(GETCARDETAIL(appid));
            if (response != null)
            {

                var posts = JsonConvert.DeserializeObject<List<DataCarDetail>>(response.Content);
                return posts;
            }
            else return null;
        }

        public SentToAPI GETCARDETAIL(string appid)
        {
            SentToAPI mMo = new SentToAPI();
            mMo.personnel_code = _userlogin.data.First().personnel_code;
            mMo.storename = "ST_CARDETAILALL_GET";
            mMo.c1 = appid;
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