using Check_CarPrice.Persistence;
using Check_CarPrice.View.MenuCarPriceList;
using Check_CarPrice.WebService;
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

namespace Check_CarPrice.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuCarPriceList_View : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private User _userlogin;
        //private DataCarDetail _dta;

        protected override bool OnBackButtonPressed() => true;
        
        public DataTransactionCar _dtatran;
        public MenuCarPriceList_View(User user,DataTransactionCar dta)
        {
            InitializeComponent();
            Appz_WebService iAPI = new Appz_WebService();
            _dtatran= dta;
            _userlogin = user;

            SetData();

        }

        private void SetData()
        {

            if (_dtatran.type_grp_car == "01")
            {
                ObservableCollection<string> data = new ObservableCollection<string>();
                data.Add("ข้อมูลเบื้องต้น");
                data.Add("ข้อมูลตัวแม่");
                data.Add("รูปภาพ");
                lstMenu.ItemsSource = data;
               
            }
            else
            {
                ObservableCollection<string> data = new ObservableCollection<string>();
                data.Add("ข้อมูลเบื้องต้น");
                data.Add("ข้อมูลตัวลูก");
                data.Add("รูปภาพ");
                lstMenu.ItemsSource = data;
            }

            if (_userlogin.role.func.Where(a => a.func_id == "F0005").Count() != 0) //Checker
            {
                //1.รอตรวจสอบ 2.พิจารณายอดสินเชื่อเร่งด่วน 3.ดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม 4.ขออนุมัติยอดสินเชื่อด้วยหลักเกณฑ์อื่น 5.รอประเมินราคาและอนุมัติ
                if (_dtatran.status_approve == "1" || _dtatran.status_approve == "3" || _dtatran.status_approve == "4" || _dtatran.status_approve == "6" || _dtatran.status_approve == "11")
                {
                    btnCheck.IsVisible = true;
                    //btnUpdate.IsVisible = true;
                    btnDelete.IsVisible = true;
                }
                else
                {
                    btnCheck.IsVisible = false;
                    //btnUpdate.IsVisible = true;
                    btnUpdate.IsVisible = false;
                    btnDelete.IsVisible = false;
                    btnOpenAppno.IsVisible = false;
                }
            }
            
            else if (_userlogin.role.func.Where(a => a.func_id == "F0003").Count() != 0) //Applicant
            {
                //1.รายละเอียดไม่ถูกต้อง(แก้ข้อมูลได้ทั้งหมด 2.ดำเนินการปรับปรุง, เปลี่ยน, ซ่อมแซม(แก้ได้เฉพาะข้อมูลรูปภาพรถส่วนอื่นแก้ไม่ได้)
                if (_dtatran.status_approve == "3" || _dtatran.status_approve == "4" || _dtatran.status_approve == "6")
                {
                    btnOpenAppno.IsVisible = true;
                }
                if (_dtatran.status_approve == "7" || _dtatran.status_approve == "4")
                {
                    btnUpdate.IsVisible = true;
                }
               
            }
            else
            {
                btnCheck.IsVisible = false;
                btnUpdate.IsVisible = false;
                btnDelete.IsVisible = false;
                btnOpenAppno.IsVisible = false;
            }
        }


        private void lstMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem.ToString() == "ข้อมูลเบื้องต้น")
            {
                Navigation.PushAsync(new CarPriceListGeneral_View(_userlogin, _dtatran));
            }
            else if(e.SelectedItem.ToString() == "ข้อมูลตัวแม่")
            {
                Navigation.PushAsync(new CarPriceListMaster_View(_userlogin, _dtatran));
            }
            else if (e.SelectedItem.ToString() == "ข้อมูลตัวลูก")
            {
                
                Navigation.PushAsync(new CarPriceListSubMaster_View(_userlogin, _dtatran));
            }
            else if (e.SelectedItem.ToString() == "รูปภาพ")
            {
                Navigation.PushAsync(new CarPriceListImage_View(_userlogin, _dtatran));
            }

        }

        private async void btnCheck_Clicked(object sender, EventArgs e)
        {
            PopupLoad.IsVisible = true;
            PageMenu.Opacity = 0.5;
            await Task.Delay(2000);
            await Navigation.PushAsync(new Check_View(_userlogin, _dtatran));
            PopupLoad.IsVisible = false;
        }

        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            PopupLoad.IsVisible = true;
            PageMenu.Opacity = 0.5;
            await Task.Delay(2000);
            if (_dtatran.status_approve == "7" || _dtatran.status_approve == "4")
            {
                await Navigation.PushAsync(new CarPriceUpdate_View(_userlogin, _dtatran));
            }
            else
            {
                btnUpdate.IsVisible = false;
            }
            PopupLoad.IsVisible = false;

        }

        private  void btnDelete_Clicked(object sender, EventArgs e)
        {
            PageMenu.Opacity = 0.5;
            PopupDelete.IsVisible = true;
           
        }

        private async void btnOpenAppno_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("แจ้งเตือน", "คุณต้องการอนุมัติราคาใช่หรือไม่", "ใช่", "ไม่ใช่");
            if (response == true)
            {
                Approval_WebService iAPI = new Approval_WebService();
                if (!iAPI.UpdateTransactionApprove(_dtatran.app_id, "11", _userlogin.data.Select(a => a.personnel_code).FirstOrDefault()))
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

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            PopupLoad.IsVisible = true;
            PageMenu.Opacity = 0.5;
            await Task.Delay(100);
            await Navigation.PushAsync(new MainPage(_userlogin));
            PopupLoad.IsVisible = false;
        }

        private async void btnDeleteFinally_Clicked(object sender, EventArgs e)
        {
            PopupLoad.IsVisible = false;
            var response = await DisplayAlert("แจ้งเตือน", "คุณต้องการลบรายการนี้ใช่หรือไม่", "ใช่", "ไม่ใช่");
            if (response == true)
            {
                CarPriceList_WebService iApi = new CarPriceList_WebService();
                if (iApi.PostDelete(_dtatran.app_id, _userlogin.data.Select(a => a.personnel_code).FirstOrDefault()) == true)
                {
                    await Navigation.PushAsync(new MainPage(_userlogin));
                    await DisplayAlert("ข้อมูลรายการรถ", "ลบข้อมูลเรียบร้อย", "ตกลง");
                }
                else
                {
                    await Navigation.PushAsync(new MainPage(_userlogin));
                    await DisplayAlert("ข้อมูลรายการรถ", "ลบข้อมูลรายการรถไม่สำเร็จ", "ตกลง");
                    PageMenu.Opacity = 0;
                }
            }
            else
            {
                await Navigation.PushAsync(new MainPage(_userlogin));
                PageMenu.Opacity = 0;
            }
            //PopupLoad.IsVisible = false;
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage(_userlogin));
        }
    }
}