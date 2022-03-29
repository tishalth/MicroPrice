
using Check_CarPrice.Persistence;
using Check_CarPrice.WebService;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.CarPriceList_Model;
using static Check_CarPrice.Model.Login_Model;

namespace Check_CarPrice.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarPriceList_View : ContentPage
    {
        
        private User _userlogin;
        public DataTransactionCar _dta;

        protected override bool OnBackButtonPressed() => true;


        public List<DataTransactionCar> data;
        public CarPriceList_View(User user)
        {
            _userlogin = user;
            InitializeComponent();
            SetData();
            RoleUser();
        }

        public void SetData()
        {
            CarPriceList_WebService iapi = new CarPriceList_WebService();
            lblData.Text = "รายการรถ";
            //var data = GETTRANSACTION_lIST();

            //if (data != null)
            //{
                pkFilter.SelectedIndex = 0;
            //}
            //else
            //{
            //    lstData.ItemsSource = data;
            //}

        }

        private void RoleUser()
        {
            if (_userlogin.role.func.Where(a => a.func_id == "F0002").Count() != 0)
            {
                btnInsert.IsVisible = true;
            }
            else btnInsert.IsVisible = false;
        }


        private async void btnInsert_Clicked(object sender, EventArgs e)
        {
            PopupLoad.IsVisible = true;
            PageCarPriceList.Opacity = 0.5;
            await Task.Delay(1000);
            await Navigation.PushAsync(new InsertCarPrice_View(_userlogin));
            PopupLoad.IsVisible = false;
        }


        private void pkFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarPriceList_WebService iapi = new CarPriceList_WebService();
            lblData.Text = pkFilter.Items[pkFilter.SelectedIndex];
            string filter = "";
            if (pkFilter.SelectedIndex == 0) filter = "";
            else if (pkFilter.SelectedIndex == 1) filter = "2";  //รออนุมัติ
            else if (pkFilter.SelectedIndex == 2) filter = "1";  //รอตรวจสอบ
            else if (pkFilter.SelectedIndex == 3) filter = "3";  //พิจารณายอดสินเชื่อเร่งด่วน
            else if (pkFilter.SelectedIndex == 4) filter = "4";  //ดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม
            else if (pkFilter.SelectedIndex == 5) filter = "6";  //ขออนุมัติยอดสินเชื่อด้วยหลักเกณฑ์อื่น
            else if (pkFilter.SelectedIndex == 6) filter = "7";  //รายละเอียดไม่ถูกต้อง
            else if (pkFilter.SelectedIndex == 7) filter = "11"; //รออนุมัติเพิ่อเปิดสัญญา
            else if (pkFilter.SelectedIndex == 8) filter = "8";  //ไม่ผ่านหลักเกณฑ์
            else if (pkFilter.SelectedIndex == 9) filter = "9";  //อนุมัติแล้ว

            data = GETTRANSACTION_lIST();
            if (data != null)
            {
                if (_userlogin.role.func.Where(a => a.func_id == "F0010").Count() != 0) //staff
                {
                    var datascreen = data.Where(a => a.branch_code == _userlogin.data.Select(b => b.branch_no).FirstOrDefault() && a.create_by == _userlogin.data.Select(b => b.personnel_code).FirstOrDefault()).ToList();
                    if (datascreen != null)
                    {
                        if (filter == "") lstData.ItemsSource = datascreen.Where(a => a.status == 1).ToList().OrderByDescending(a => a.create_date);
                        else lstData.ItemsSource = datascreen.Where(a => a.status == 1 && a.status_approve == filter).ToList().OrderByDescending(a => a.create_date);
                    }
                    else
                    {
                        lstData.ItemsSource = null;
                    }
                }

                else if (_userlogin.role.func.Where(a => a.func_id == "F0007").Count() != 0) //branch
                {
                    //var datascreen = data.Where(a => a.branch_code == _userlogin.data.Select(b => b.branch_no).FirstOrDefault() && a.create_by == _userlogin.data.Select(b => b.personnel_code).FirstOrDefault()).ToList();

                    var datascreen = data.Where(a => a.branch_code == _userlogin.data.Select(b => b.branch_no).FirstOrDefault()).ToList();
                    if (datascreen != null)
                    {
                        if (filter == "") lstData.ItemsSource = datascreen.Where(a => a.status == 1).ToList().OrderByDescending(a=>a.create_date);
                        else lstData.ItemsSource = datascreen.Where(a => a.status == 1 && a.status_approve == filter).ToList().OrderByDescending(a => a.create_date);
                    }
                    else
                    {
                        lstData.ItemsSource = null;
                    }
                }
                else if (_userlogin.role.func.Where(a => a.func_id == "F0008").Count() != 0) //hub
                {
                    var datascreen = data.Where(a => a.hub == _userlogin.data.Select(b => b.area_no).FirstOrDefault()).ToList();
                    if (datascreen != null)
                    {
                        if (filter == "") lstData.ItemsSource = datascreen.Where(a => a.status == 1).ToList().OrderByDescending(a => a.create_date);
                        else lstData.ItemsSource = datascreen.Where(a => a.status == 1 && a.status_approve == filter).ToList().OrderByDescending(a => a.create_date);
                    }
                    else
                    {
                        lstData.ItemsSource = null;
                    }
                }
                else if (_userlogin.role.func.Where(a => a.func_id == "F0009").Count() != 0) //all
                {
                    if (filter == "") lstData.ItemsSource = data.Where(a => a.status == 1).ToList().OrderByDescending(a => a.create_date);
                    else lstData.ItemsSource = data.Where(a => a.status == 1 && a.status_approve == filter).ToList().OrderByDescending(a => a.create_date);
                }
                else
                {
                    lstData.ItemsSource = null;
                }
            }
            else lstData.ItemsSource = null;
        }

        private void lstData_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _dta = (DataTransactionCar)e.SelectedItem;
            Navigation.PushAsync(new MenuCarPriceList_View(_userlogin, _dta));
        }

        public List<DataTransactionCar> GETTRANSACTION_lIST()
        {
            Appz_WebService iAPI = new Appz_WebService();
            var response = iAPI.Sent(GETTRANSACTION());
            if (response != null)
            {

                var posts = JsonConvert.DeserializeObject<List<DataTransactionCar>>(response.Content);
                return posts;
            }
            else return null;
        }

        public SentToAPI GETTRANSACTION()
        {
            SentToAPI mMo = new SentToAPI();
            mMo.personnel_code = _userlogin.data.First().personnel_code;
            mMo.storename = "ST_TRANSACTIONCAR_GET";
            mMo.c1 = "";
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