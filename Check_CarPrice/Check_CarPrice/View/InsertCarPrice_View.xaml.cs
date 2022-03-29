
using Check_CarPrice.Persistence;
using Check_CarPrice.ViewModels;
using Check_CarPrice.WebService;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.Login_Model;
using System.Windows.Input;
using System.IO;
using System.Text.RegularExpressions;

namespace Check_CarPrice.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InsertCarPrice_View : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<DataAccessories> _accessories;
        private ObservableCollection<DataCampaign> _campaign;
        private ObservableCollection<DataCarPolicy> _policy;
        private ObservableCollection<DataProvince> _province;
        private ObservableCollection<DataUseCarType> _usecartype;
        private ObservableCollection<DataBrand> _brand;
        private ObservableCollection<DataCharacterCar> _charactercar;
        private ObservableCollection<DataTypeCar> _typecar;
        private User _userlogin;
        //private ObservableCollection<DataGrantMapUser> _gmapuser;
        private ObservableCollection<DataStatus> _status;

        public string _appno;
        public List<DataDealer> Dealer;
        public List<DataDealer> Agent;
        public List<DataCustomer> Customer;
        public List<DataEmployee> Employee;
        Exception _error = null;


        protected override bool OnBackButtonPressed() => true;
        public InsertCarPrice_View(User user)
        {

            InitializeComponent();
            _userlogin = user;

            //SetData();
            SetMainPage();

        }

        protected override async void OnAppearing()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            if (_typecar == null)
            {
                var recipes = await _connection.Table<DataAccessories>().ToListAsync();
                _accessories = new ObservableCollection<DataAccessories>(recipes);
                var cam = await _connection.Table<DataCampaign>().ToListAsync();
                _campaign = new ObservableCollection<DataCampaign>(cam);
                var policy = await _connection.Table<DataCarPolicy>().ToListAsync();
                _policy = new ObservableCollection<DataCarPolicy>(policy);
                var province = await _connection.Table<DataProvince>().ToListAsync();
                _province = new ObservableCollection<DataProvince>(province);
                var usecartype = await _connection.Table<DataUseCarType>().ToListAsync();
                _usecartype = new ObservableCollection<DataUseCarType>(usecartype);
                var brand = await _connection.Table<DataBrand>().ToListAsync();
                _brand = new ObservableCollection<DataBrand>(brand);
                var charac = await _connection.Table<DataCharacterCar>().ToListAsync();
                _charactercar = new ObservableCollection<DataCharacterCar>(charac);
                var typecar = await _connection.Table<DataTypeCar>().ToListAsync();
                _typecar = new ObservableCollection<DataTypeCar>(typecar);
                //var login = await _connection.Table<LoginData>().ToListAsync();
                //_userlogin = new ObservableCollection<LoginData>(login);


                SetAllData();
                SetData();
            }
            base.OnAppearing();
        }

        private void SetAllData()
        {
            Appz_WebService iModel = new Appz_WebService();

            pkMasProvince.ItemsSource = _province;
            pkSubProvince.ItemsSource = _province;
            pkMasCarType.ItemsSource = _typecar.Where(a => a.type_grp_car == "01").ToList();
            pkSubCarType.ItemsSource = _typecar.Where(a => a.type_grp_car == "02").ToList();
            pkMasBrand.ItemsSource = _brand;
            pkCamPaign.ItemsSource = _campaign;
            pkMasAccessories.ItemsSource = _accessories.Where(a => a.status == "active").ToList();
            pkLoanType.ItemsSource = _usecartype;

            Dealer = iModel.GetDealer();
            Agent = iModel.GetAgent();
            Customer = iModel.GetDataCustomer();
            Employee = iModel.GetDataEmployee();
        }

        public string _runnumber;

        private void SetData()
        {
            //System.Globalization.CultureInfo _cultureEnInfo = new System.Globalization.CultureInfo("en-US");
            //DateTime dateEng = Convert.ToDateTime(DateTime.Now, _cultureEnInfo);

            Appz_WebService iModel = new Appz_WebService();

            if (_userlogin != null)
            {
                enPersonal_code.Text = _userlogin.data.Select(a => a.personnel_code).FirstOrDefault();
                enPersonal_Name.Text = _userlogin.data.Select(a => a.personnel_name_TH).FirstOrDefault() + " " + _userlogin.data.Select(a => a.personnel_last_TH).FirstOrDefault();
                enBranchName.Text = _userlogin.data.Select(a => a.branch_name_th).FirstOrDefault();

                var app = iModel.GenAppID(_userlogin.data.Select(a => a.branch_no).FirstOrDefault(), "", "").FirstOrDefault();
                if (app != null)
                {
                    _runnumber = app.run_number;
                    enAppno.Text = app.app_id;
                    _appno = enAppno.Text;
                }

                ////enAppno.Text = _userlogin.data.Select(a => a.branch_no).FirstOrDefault() + dateEng.ToString("yy") + DateTime.Now.ToString("MM") + _appno;
                //var appno = _userlogin.data.Select(a => a.branch_no).FirstOrDefault().Substring(3) + dateEng.ToString("yy") + DateTime.Now.ToString("MM");
                //_runnumber = iModel.GenAppID(_userlogin.data.Select(a => a.branch_no).FirstOrDefault(), appno, "").Select(a => a.run_number).FirstOrDefault().ToString();
                //enAppno.Text = appno + _runnumber;
                //_appno = enAppno.Text;
            }




        }
        public void SetMainPage()
        {
            frmPage1.IsVisible = true;
            frmPage2.IsVisible = false;
            btnNext1.IsVisible = true;
            btnMenu.IsVisible = true;

        }
        private string ValidationGenaeral()
        {

            string ret = "";
            if (pkLoanType.SelectedIndex == -1) ret = "กรุณาเลือกประเภทสินเชื่อ";
            else if (pkDealerType.SelectedIndex == -1) ret = "กรุณาเลือกดีลเลอร์ตัวแม่-ตัวลูก";
            else if (pkConnectionMaster.SelectedIndex == -1) ret = "กรุณาเลือกผู้ติดต่อ";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 0 && pkConnectionMaster.SelectedIndex == 0 && (string.IsNullOrEmpty(sbConnectionMaster1Name.Text) == true || string.IsNullOrEmpty(sbConnectionMaster1Code.Text) == true)) ret = "กรุณาข้อมูลนายหน้าให้ครบถ้วน";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 0 && pkConnectionMaster.SelectedIndex == 1 && (string.IsNullOrEmpty(sbConnectionMaster2Name.Text) == true || string.IsNullOrEmpty(sbConnectionMaster2Code.Text) == true)) ret = "กรุณาข้อมูลคู่ค้าให้ครบถ้วน";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 0 && pkConnectionMaster.SelectedIndex == 2 && pkMasAdvisorType.SelectedIndex == -1) ret = "กรุณาเลือกผู้แนะนำ";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 0 && pkConnectionMaster.SelectedIndex == 2 && pkMasAdvisorType.SelectedIndex == 0 && (string.IsNullOrEmpty(sbMasAdvisorType1Name.Text) == true || string.IsNullOrEmpty(sbMasAdvisorType1Appno.Text) == true)) ret = "กรุณากรอกข้อมูลลูกค้าเก่าให้ครบถ้วน";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 0 && pkConnectionMaster.SelectedIndex == 2 && pkMasAdvisorType.SelectedIndex == 1 && (string.IsNullOrEmpty(enMasAdvisorType2Name.Text) == true || pkSocialtype.SelectedIndex == -1)) ret = "กรุณากรอกข้อมูลลูกค้าใหม่ติดต่อมาให้ครบถ้วน";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 0 && pkConnectionMaster.SelectedIndex == 2 && pkMasAdvisorType.SelectedIndex == 2 && (string.IsNullOrEmpty(sbMasAdvisorType3EmNo.Text) == true || string.IsNullOrEmpty(sbMasAdvisorType3Name.Text) == true)) ret = "กรุณากรอกข้อมูลรหัสพนักงานให้ครบถ้วน";


            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 1 && pkConnectionMaster.SelectedIndex == 0 && (string.IsNullOrEmpty(sbConnectionMaster1Name.Text) == true || string.IsNullOrEmpty(sbConnectionMaster1Code.Text) == true)) ret = "กรุณาข้อมูลนายหน้าให้ครบถ้วน";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 1 && pkConnectionMaster.SelectedIndex == 1 && (string.IsNullOrEmpty(sbConnectionMaster2Name.Text) == true || string.IsNullOrEmpty(sbConnectionMaster2Code.Text) == true)) ret = "กรุณาข้อมูลคู่ค้าให้ครบถ้วน";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 1 && pkConnectionMaster.SelectedIndex == 2 && pkMasAdvisorType.SelectedIndex == -1) ret = "กรุณาเลือกผู้แนะนำ";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 1 && pkConnectionMaster.SelectedIndex == 2 && pkMasAdvisorType.SelectedIndex == 0 && (string.IsNullOrEmpty(sbMasAdvisorType1Name.Text) == true || string.IsNullOrEmpty(sbMasAdvisorType1Appno.Text) == true)) ret = "กรุณากรอกข้อมูลลูกค้าเก่าให้ครบถ้วน";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 1 && pkConnectionMaster.SelectedIndex == 2 && pkMasAdvisorType.SelectedIndex == 1 && (string.IsNullOrEmpty(enMasAdvisorType2Name.Text) == true || pkSocialtype.SelectedIndex == -1)) ret = "กรุณากรอกข้อมูลลูกค้าใหม่ติดต่อมาให้ครบถ้วน";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 1 && pkConnectionMaster.SelectedIndex == 2 && pkMasAdvisorType.SelectedIndex == 2 && (string.IsNullOrEmpty(sbMasAdvisorType3EmNo.Text) == true || string.IsNullOrEmpty(sbMasAdvisorType3Name.Text) == true)) ret = "กรุณากรอกข้อมูลรหัสพนักงานให้ครบถ้วน";


            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 1 && pkConnectionSubMaster.SelectedIndex == -1) ret = "กรุณาเลือกผู้ติดต่อ";

            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 1 && pkConnectionSubMaster.SelectedIndex == 0 && (string.IsNullOrEmpty(sbConnectionSubMaster1Name.Text) == true || string.IsNullOrEmpty(sbConnectionSubMaster1Code.Text) == true)) ret = "กรุณาข้อมูลนายหน้าให้ครบถ้วน";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 1 && pkConnectionSubMaster.SelectedIndex == 1 && (string.IsNullOrEmpty(sbConnectionSubMaster2Name.Text) == true || string.IsNullOrEmpty(sbConnectionSubMaster2Code.Text) == true)) ret = "กรุณาข้อมูลคู่ค้าให้ครบถ้วน";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 1 && pkConnectionSubMaster.SelectedIndex == 2 && pkSubMasAdvisorType.SelectedIndex == -1) ret = "กรุณาเลือกผู้แนะนำ";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 1 && pkConnectionSubMaster.SelectedIndex == 2 && pkSubMasAdvisorType.SelectedIndex == 0 && (string.IsNullOrEmpty(sbSubMasAdvisorType1Name.Text) == true || string.IsNullOrEmpty(sbSubMasAdvisorType1Appno.Text) == true)) ret = "กรุณากรอกข้อมูลลูกค้าเก่าให้ครบถ้วน";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 1 && pkConnectionSubMaster.SelectedIndex == 2 && pkSubMasAdvisorType.SelectedIndex == 1 && (string.IsNullOrEmpty(enSubMasAdvisorType2Name.Text) == true || pkSubSocialtype.SelectedIndex == -1)) ret = "กรุณากรอกข้อมูลลูกค้าใหม่ติดต่อมาให้ครบถ้วน";
            else if (pkDealerType.SelectedIndex != -1 && pkDealerType.SelectedIndex == 1 && pkConnectionSubMaster.SelectedIndex == 2 && pkSubMasAdvisorType.SelectedIndex == 2 && (string.IsNullOrEmpty(sbSubMasAdvisorType3EmNo.Text) == true || string.IsNullOrEmpty(sbSubMasAdvisorType3Name.Text) == true)) ret = "กรุณากรอกข้อมูลรหัสพนักงานให้ครบถ้วน";
            else if (pkNote.SelectedIndex == -1) ret = "กรุณาเลือกหมายเหตุ";
            else if (pkNote.SelectedIndex == 0 && String.IsNullOrEmpty(enRemarkDes.Text)) ret = "กรุณากรอกอื่นๆ";
            else if (pkNote.SelectedIndex == 1 && String.IsNullOrEmpty(enRemarkDes.Text)) ret = "กรุณากรอกอื่นๆ";
            else if (pkNote.SelectedIndex == 1 && startDatePicker.Date == startDatePicker.MinimumDate) ret = "กรุณากรอกวันที่เสร็จสิ้นการดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม";


            //else if (pkNote.SelectedIndex == 1 && pkNote.Items[pkNote.SelectedIndex] == "ดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม" && startDatePicker.Date == startDatePicker.MinimumDate) ret = "กรุณากรอกวันที่เสร็จสิ้นการดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม";
            //else if (pkNote.Items[pkNote.SelectedIndex] == "ดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม")
            //{
            //    if (startDatePicker.Date == startDatePicker.MinimumDate) ret = "กรุณากรอกวันที่เสร็จสิ้นการดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม";
            //}
            else if (pkNumMasterandSubMaster.SelectedIndex == -1) ret = "กรุณาเลือกตัวแม่-ตัวลูก";


            //else if (pkDealerType.SelectedIndex != -1)
            //{

            //    if (pkDealerType.SelectedIndex == 0)
            //    {
            //        if (pkConnectionMaster.SelectedIndex == 0)
            //        {
            //            if (string.IsNullOrEmpty(sbConnectionMaster1Name.Text) == true || string.IsNullOrEmpty(sbConnectionMaster1Code.Text) == true) ret = "กรุณาข้อมูลนายหน้าให้ครบถ้วน";
            //            else ret = "";
            //        }
            //        else if (pkConnectionMaster.SelectedIndex == 1)
            //        {
            //            if (string.IsNullOrEmpty(sbConnectionMaster2Name.Text) == true || string.IsNullOrEmpty(sbConnectionMaster2Code.Text) == true) ret = "กรุณาข้อมูลคู่ค้าให้ครบถ้วน";
            //            else ret = "";
            //        }
            //        else if (pkConnectionMaster.SelectedIndex == 2)
            //        {
            //            if (pkMasAdvisorType.SelectedIndex == -1) ret = "กรุณาเลือกผู้แนะนำ";
            //            else if (pkMasAdvisorType.SelectedIndex == 0)
            //            {
            //                if (string.IsNullOrEmpty(sbMasAdvisorType1Name.Text) == true || string.IsNullOrEmpty(sbMasAdvisorType1Appno.Text) == true) ret = "กรุณากรอกข้อมูลลูกค้าเก่าให้ครบถ้วน";
            //                else ret = "";
            //            }
            //            else if (pkMasAdvisorType.SelectedIndex == 1)
            //            {
            //                if (string.IsNullOrEmpty(enMasAdvisorType2Name.Text) == true || pkSocialtype.SelectedIndex == -1) ret = "กรุณากรอกข้อมูลลูกค้าใหม่ติดต่อมาให้ครบถ้วน";
            //                else ret = "";
            //            }
            //            else if (pkMasAdvisorType.SelectedIndex == 2)
            //            {
            //                if (string.IsNullOrEmpty(sbMasAdvisorType3EmNo.Text) == true || string.IsNullOrEmpty(sbMasAdvisorType3Name.Text) == true) ret = "กรุณากรอกข้อมูลรหัสพนักงานให้ครบถ้วน";
            //                else ret = "";
            //            }
            //        }
            //        else ret = "";

            //    }

            //    else if (pkDealerType.SelectedIndex == 1)
            //    //else
            //    {
            //        if (pkConnectionSubMaster.SelectedIndex == 0)
            //        {
            //            if (string.IsNullOrEmpty(sbConnectionSubMaster1Name.Text) == true || string.IsNullOrEmpty(sbConnectionSubMaster1Code.Text) == true) ret = "กรุณาข้อมูลนายหน้าให้ครบถ้วน";
            //            else ret = "";
            //        }
            //        else if (pkConnectionSubMaster.SelectedIndex == 1)
            //        {
            //            if (string.IsNullOrEmpty(sbConnectionSubMaster2Name.Text) == true || string.IsNullOrEmpty(sbConnectionSubMaster2Code.Text) == true) ret = "กรุณาข้อมูลคู่ค้าให้ครบถ้วน";
            //            else ret = "";
            //        }
            //        else if (pkConnectionSubMaster.SelectedIndex == 2)
            //        {
            //            if (pkSubMasAdvisorType.SelectedIndex == -1) ret = "กรุณาเลือกผู้แนะนำ";
            //            if (pkSubMasAdvisorType.SelectedIndex == 0)
            //            {
            //                if (string.IsNullOrEmpty(sbSubMasAdvisorType1Name.Text) == true || string.IsNullOrEmpty(sbSubMasAdvisorType1Appno.Text) == true) ret = "กรุณากรอกข้อมูลลูกค้าเก่าให้ครบถ้วน";
            //                else ret = "";
            //            }
            //            else if (pkSubMasAdvisorType.SelectedIndex == 1)
            //            {
            //                if (string.IsNullOrEmpty(enSubMasAdvisorType2Name.Text) == true || pkSubSocialtype.SelectedIndex == -1) ret = "กรุณากรอกข้อมูลลูกค้าใหม่ติดต่อมาให้ครบถ้วน";
            //                else ret = "";
            //            }
            //            else if (pkSubMasAdvisorType.SelectedIndex == 2)
            //            {
            //                if (string.IsNullOrEmpty(sbSubMasAdvisorType3EmNo.Text) == true || string.IsNullOrEmpty(sbSubMasAdvisorType3Name.Text) == true) ret = "กรุณากรอกข้อมูลรหัสพนักงานให้ครบถ้วน";
            //                else ret = "";
            //            }
            //        }
            //        else ret = "";

            //    }
            //    else ret = "";
            //}

            //else if (pkNote.SelectedIndex == -1) ret = "กรุณาเลือกหมายเหตุ";
            //else if (pkNote.Items[pkNote.SelectedIndex] == "ดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม")
            //{
            //    if (startDatePicker.Date == startDatePicker.MinimumDate) ret = "กรุณากรอกวันที่เสร็จสิ้นการดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม";
            //}
            //else if (pkNumMasterandSubMaster.SelectedIndex == -1) ret = "กรุณาเลือกตัวแม่-ตัวลูก";

            return ret;
        }
        public void MasterandSubMaster()
        {
            frmPage1.IsVisible = false;
            frmPage2.IsVisible = true;
            frmPage3.IsVisible = false;
            btnBack3.IsVisible = false;
            btnNext1.IsVisible = false;
            btnMenu.IsVisible = false;
            btnBack2.IsVisible = true;
        }
        private string CheckValidation()
        {
            string error = "";
            if (pkNumMasterandSubMaster.SelectedIndex == 0)
            {
                error = ValidationMaster();
            }
            else if (pkNumMasterandSubMaster.SelectedIndex == 1)
            {
                error = ValidatonPage3();
            }
            else if (pkNumMasterandSubMaster.SelectedIndex == 2)
            {
                error = ValidationMaster();
                error += ValidatonPage3();
            }
            return error;
        }
        private string ValidatonPage3()
        {
            InsertCarPrice_WebService iAPI = new InsertCarPrice_WebService();
            //var chasno = iAPI.GetDataInformationCarInsert().ToList();
            var cdetypestyle = pkSubStyle2.SelectedIndex==-1?"":((DataV_StyleSubMaster2)pkSubStyle2.SelectedItem).cde_style_car;
            string ret = "";
            if (String.IsNullOrEmpty(enSubLicno.Text)) ret = "กรุณากรอกเลขทะเบียน";
            else if (pkSubProvince.SelectedIndex == -1) ret = "กรุณาเลือกจังหวัด";
            else if (String.IsNullOrEmpty(enSChasno.Text)) ret = "กรุณากรอกเลขตัวรถ";
            //else if (chasno.Count() != 0 && chasno.Where(a => a.chasno == enSChasno.Text).Count() > 0) ret = "เลขตัวรถนี้เคยทำรายการแล้ว";
            else if (pkSubStyle1.SelectedIndex == -1) ret = "กรุณาเลือกประเภทลักษณะรถ";
            else if (pkSubStyle2.SelectedIndex == -1) ret = "กรุณาเลือกลักษณะกระบะ";
         
            else if (pkSubStyle2.SelectedIndex != 1 && pkSubStyle1.Items[pkSubStyle1.SelectedIndex] == "รถกึ่งพ่วง" && (cdetypestyle == "T003" || cdetypestyle == "T015")&& pkSubStyle3.SelectedIndex == -1) ret = "กรุณาเลือกลักษณะพิเศษ";
            //{
               
            //    if (pkSubStyle1.Items[pkSubStyle1.SelectedIndex] == "รถกึ่งพ่วง" && (cdetypestyle == "T003" || cdetypestyle == "T015"))
            //    {
            //        if (pkSubStyle3.SelectedIndex == -1) ret = "กรุณาเลือกลักษณะพิเศษ";
            //    }
            //}
            else if (pkSubCarType.SelectedIndex == -1) ret = "กรุณาเลือกประเภท";
            else if (String.IsNullOrEmpty(enSubCarPrice.Text)) ret = "กรุณากรอกราคาขายตัวลูก";
            else if (String.IsNullOrEmpty(enSubYearCar.Text)) ret = "กรุณากรอกปีจดทะเบียน";
            else if (enSubYearCar.Text.Length != 4) ret = "กรุณากรอกปีจดทะเบียนให้ครบ 4 หลัก";
            //else if (imgSubPic1.Text == null) ret = "กรุณาเลือกรูปภาพมุมหน้า-ฝั่งซ้าย";
            //else if (imgSubPic2.Text == null) ret = "กรุณาเลือกรูปภาพมุมหน้า-ฝั่งขวา";
            //else if (imgSubPic3.Text == null) ret = "กรุณาเลือกรูปภาพมุมหลัง-ฝั่งขวา";
            //else if (imgSubPic4.Text == null) ret = "กรุณาเลือกรูปภาพมุมหลัง-ฝั่งซ้าย";
            //else if (imgSubPic5.Text == null) ret = "กรุณาเลือกรูปภาพรูปยกดั๊มพ์";
            //else if (imgSubPic6.Text == null) ret = "กรุณาเลือกรูปภาพพื้นกระบะและแผงข้างด้านใน";
            //else if (imgSubPic7.Text == null) ret = "กรุณาเลือกรูปภาพรูปเลขตัวรถ";
            //else if (imgSubPic8.Text == null) ret = "กรุณาเลือกรูปภาพรายการจดทะเบียน";
            //else if (imgSubPic9.Text == null) ret = "กรุณาเลือกรูปภาพบันทึกเจ้าหน้าที่";
            //else if (imgSubPic10.Text == null) ret = "กรุณาเลือกรูปภาพรายการเสียภาษี";
            //else if (imgSubPic11.Text == null) ret = "กรุณาเลือกรูปภาพแซสซีฝั่งขวา";
            //else if (imgSubPic12.Text == null) ret = "กรุณาเลือกรูปภาพแซสซีฝั่งซ้าย";

            return ret;
        }
        public string ValidationMaster()
        {
            InsertCarPrice_WebService iAPI = new InsertCarPrice_WebService();
            //var chasno = iAPI.GetDataInformationCarInsert().ToList();
            string ret = "";
            if (String.IsNullOrEmpty(enMasLicno.Text)) ret = "กรุณากรอกเลขทะเบียน";
            else if (pkMasProvince.SelectedIndex == -1) ret = "กรุณาเลือกจังหวัด";
            else if (String.IsNullOrEmpty(enMasChasno.Text)) ret = "กรุณากรอกเลขตัวรถ";
            //else if (chasno.Count() != 0 && chasno.Where(a => a.chasno == enMasChasno.Text).Count() > 0) ret = "เลขตัวรถนี้เคยทำรายการแล้ว";
            else if (String.IsNullOrEmpty(enMasEngno.Text)) ret = "กรุณากรอกเลขเครื่องยนต์";
            else if (pkMasBrand.SelectedIndex == -1) ret = "กรุณาเลือกยี่ห้อรถ";
            else if (pkMasFuel.SelectedIndex == -1) ret = "กรุณาเลือกเชื้อเพลิง";
            else if (pkMasCarType.SelectedIndex == -1) ret = "กรุณาเลือกประเภทรถ";
            else if (pkMasStyle1.SelectedIndex == -1 && pkMasStyle2.SelectedIndex == -1) ret = "กรุณาเลือกลักษณะรถ";
            else if (pkMasStyle1.SelectedIndex == -1) ret = "กรุณาเลือกกลุ่มรถ";
            else if (pkMasStyle2.SelectedIndex == -1) ret = "กรุณาเลือกลักษณะกระบะ";
            else if (String.IsNullOrEmpty(enMasHeadPrice.Text)) ret = "กรุณากรอกราคาขายตัวแม่";
            else if (pkMasAccessories.SelectedIndex == -1) ret = "กรุณาเลือกอุปกรณ์เสริม";
            else if (String.IsNullOrEmpty(enMasYearCar.Text)) ret = "กรุณากรอกปีจดทะเบียน";
            else if (enMasYearCar.Text.Length != 4) ret = "กรุณากรอกปีจดทะเบียนให้ครบ 4 หลัก";
            else if (Convert.ToInt32(enMasYearCar.Text) < 2400 || Convert.ToInt32(enMasYearCar.Text) > 2700) ret = "กรุณากรอกปีจดทะเบียนให้ถูกต้อง";
            else if (pkMasStyle2.SelectedIndex != -1)
            {
                if (((Model.InsertCarPrice_Model.DataVStyle1)pkMasStyle1.SelectedItem).cde_grpcar == "02")
                {
                    if (((Model.InsertCarPrice_Model.DataVStyle3)pkMasStyle2.SelectedItem).cde_style_car == "H019" ||
                       ((Model.InsertCarPrice_Model.DataVStyle3)pkMasStyle2.SelectedItem).cde_style_car == "H020")
                    {
                        if (pkMasStyle3.SelectedIndex == -1) ret = "กรุณาเลือกลักษณะพิเศษ";
                    }
                }
                else if (((Model.InsertCarPrice_Model.DataVStyle1)pkMasStyle1.SelectedItem).cde_grpcar == "03")
                {
                    if (((Model.InsertCarPrice_Model.DataVStyle3)pkMasStyle2.SelectedItem).cde_style_car == "H019" ||
                        ((Model.InsertCarPrice_Model.DataVStyle3)pkMasStyle2.SelectedItem).cde_style_car == "H020" ||
                        ((Model.InsertCarPrice_Model.DataVStyle3)pkMasStyle2.SelectedItem).cde_style_car == "H021" ||
                        ((Model.InsertCarPrice_Model.DataVStyle3)pkMasStyle2.SelectedItem).cde_style_car == "H080")
                    {
                        if (pkMasStyle3.SelectedIndex == -1) ret = "กรุณาเลือกลักษณะพิเศษ";
                    }
                }
            }


            #region Validation ImageMaster
            //else if (imgMasPic1.Text == null) ret = "กรุณาเลือกรูปภาพมุมหน้า-ด้านคนขับ";
            //else if (imgMasPic2.Text == null) ret = "กรุณาเลือกรูปภาพมุมหน้า-ด้ายโดยสาร";
            //else if (imgMasPic3.Text == null) ret = "กรุณาเลือกรูปภาพมุมหลัง-ด้ายคนขับ";
            //else if (imgMasPic4.Text == null) ret = "กรุณาเลือกรูปภาพรูปยกดั๊มพ์";
            //else if (imgMasPic5.Text == null) ret = "กรุณาเลือกรูปภาพพื้นกระบะมุมข้างด้านใน";
            //else if (imgMasPic6.Text == null) ret = "กรุณาเลือกรูปภาพภายในห้องโดยสาร(ถ่ายติดหัวเกียร์)";
            //else if (imgMasPic7.Text == null) ret = "กรุณาเลือกรูปภาพหน้าปัดเลขไมล์";
            //else if (imgMasPic8.Text == null) ret = "กรุณาเลือกรูปภาพกระจังหน้า(ถ่ายติดทะเบียนรถ)";
            //else if (imgMasPic9.Text == null) ret = "กรุณาเลือกรูปภาพยกหัวเก๋ง(บริเวณใต้หัวเก๋ง)";
            //else if (imgMasPic10.Text == null) ret = "กรุณาเลือกรูปภาพคอแซสซี(ฝั่งคนขับ)";
            //else if (imgMasPic11.Text == null) ret = "กรุณาเลือกรูปภาพคอแซสซี(ฝั่งคนนั่ง)";
            //else if (imgMasPic12.Text == null) ret = "กรุณาเลือกรูปภาพเครื่องยนต์ฝั่งคนขับ(เห็นทั้งหมด)";
            //else if (imgMasPic13.Text == null) ret = "กรุณาเลือกรูปภาพเครื่องยนต์ฝั่งคนนั่ง(เห็นทั้งหมด)";
            //else if (imgMasPic14.Text == null) ret = "กรุณาเลือกรูปภาพรูปเลขตัวรถ";
            //else if (imgMasPic15.Text == null) ret = "กรุณาเลือกรูปภาพแซสซี(ฝั่งคนขับ)";
            //else if (imgMasPic16.Text == null) ret = "กรุณาเลือกรูปภาพแซสซี(ฝั่งคนนั่ง)";
            //else if (imgMasPic17.Text == null) ret = "กรุณาเลือกรูปภาพรูปเกียร์";
            //else if (imgMasPic18.Text == null) ret = "กรุณาเลือกรูปภาพเพลากลาง เพลาท้าย";
            //else if (imgMasPic19.Text == null) ret = "กรุณาเลือกรูปภาพรายการจดทะเบียน";
            //else if (imgMasPic20.Text == null) ret = "กรุณาเลือกรูปภาพบันทึกเจ้าหน้าที่";
            //else if (imgMasPic21.Text == null) ret = "กรุณาเลือกรูปภาพรายการเสียภาษี";
            #endregion

            return ret;
        }

        private DataInformationTransactionCar GetDataGeneralMaster()
        {
            DataInformationTransactionCar dta = new DataInformationTransactionCar();
            dta.applno_car = enAppno.Text;
            dta.type_credit = ((DataUseCarType)pkLoanType.SelectedItem).prmcde;
            dta.type_dealer = (pkDealerType.SelectedIndex == 0 ? 1 : 2).ToString();
            dta.type_contact = (pkConnectionMaster.SelectedIndex == 0 ? 1 : pkConnectionMaster.SelectedIndex == 1 ? 2 : 3).ToString();
            dta.remark = (pkNote.SelectedIndex == 0 ? 1 : pkNote.SelectedIndex == 1 ? 2 : 3).ToString();
            if (pkNote.SelectedIndex == 0) dta.remark_description = enRemarkDes.Text.Trim();
            else if (pkNote.SelectedIndex == 1)
            {
                dta.remark_date = startDatePicker.Date;
                dta.remark_description = enRemarkDes.Text.Trim();
            }
            else
            {
                dta.remark_date = DateTime.MinValue;
                dta.remark_description = "";
            }
            //if (pkNote.Items[pkNote.SelectedIndex] == "ดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม") dta.remark_date = startDatePicker.Date;
            //else dta.remark_date = DateTime.MinValue;
            //if (pkNote.Items[pkNote.SelectedIndex] == "สภาพรถไม่ตรงกับหน้าเล่ม" || pkNote.Items[pkNote.SelectedIndex] == "ดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม") dta.remark_description = enRemarkDes.Text;
            //else dta.remark_description = "";
            dta.create_by = _userlogin.data.Select(a => a.personnel_code).FirstOrDefault();
            dta.status = 1;
            dta.branch_code = _userlogin.data.Select(a => a.branch_no).FirstOrDefault();
            dta.run_number = Convert.ToInt32(_runnumber);
            //dta.app_id = enAppno.Text;
            if (pkConnectionMaster.SelectedIndex == 0)
            {
                dta.cde_dealer_agent = sbConnectionMaster1Code.Text;
                dta.name_dealer_agent = sbConnectionMaster1Name.Text;
            }
            else if (pkConnectionMaster.SelectedIndex == 1)
            {
                dta.cde_dealer_agent = sbConnectionMaster2Code.Text;
                dta.name_dealer_agent = sbConnectionMaster2Name.Text;
            }
            else
            {
                dta.typ_person = (pkMasAdvisorType.SelectedIndex == 0 ? 1 : pkMasAdvisorType.SelectedIndex == 1 ? 2 : 3).ToString();
                if (pkMasAdvisorType.SelectedIndex == 0)
                {
                    dta.name_contno = sbMasAdvisorType1Name.Text;
                    dta.contno = sbMasAdvisorType1Appno.Text;
                }
                else if (pkMasAdvisorType.SelectedIndex == 1)
                {
                    dta.name_cus = enMasAdvisorType2Name.Text;
                    dta.type_cont_channel = pkSocialtype.SelectedIndex == 0 ? "1" : pkSocialtype.SelectedIndex == 1 ? "2" : pkSocialtype.SelectedIndex == 2 ? "3" : "4";

                }
                else if (pkMasAdvisorType.SelectedIndex == 2)
                {
                    dta.perosonal_cde = sbMasAdvisorType3EmNo.Text;
                    dta.personal_name = sbMasAdvisorType3Name.Text;
                }
            }

            return dta;
        }

        private DataInformationTransactionCar GetDataGeneralSubMaster()
        {
            DataInformationTransactionCar dta = new DataInformationTransactionCar();
            dta.applno_car = enAppno.Text;
            dta.type_credit = ((DataUseCarType)pkLoanType.SelectedItem).prmcde;
            dta.type_dealer = (pkDealerType.SelectedIndex == 0 ? 1 : 2).ToString();
            dta.type_contact = (pkConnectionSubMaster.SelectedIndex == 0 ? 1 : pkConnectionSubMaster.SelectedIndex == 1 ? 2 : 3).ToString();
            dta.remark = (pkNote.SelectedIndex == 0 ? 1 : pkNote.SelectedIndex == 1 ? 2 : 3).ToString();
            if (pkNote.Items[pkNote.SelectedIndex] == "ดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม") dta.remark_date = startDatePicker.Date;
            else dta.remark_date = DateTime.MinValue;
            if (pkNote.Items[pkNote.SelectedIndex] == "สภาพรถไม่ตรงกับหน้าเล่ม" || pkNote.Items[pkNote.SelectedIndex] == "ดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม") dta.remark_description = enRemarkDes.Text;
            else dta.remark_description = "";
            dta.create_by = _userlogin.data.Select(a => a.personnel_code).FirstOrDefault();
            dta.status = 1;
            dta.branch_code = _userlogin.data.Select(a => a.branch_no).FirstOrDefault();
            dta.run_number = Convert.ToInt32(_runnumber);
            if (pkConnectionSubMaster.SelectedIndex == 0)
            {
                dta.cde_dealer_agent = sbConnectionSubMaster1Code.Text;
                dta.name_dealer_agent = sbConnectionSubMaster1Name.Text;
            }
            else if (pkConnectionSubMaster.SelectedIndex == 1)
            {
                dta.cde_dealer_agent = sbConnectionSubMaster2Code.Text;
                dta.name_dealer_agent = sbConnectionSubMaster2Name.Text;
            }
            else
            {
                dta.typ_person = (pkSubMasAdvisorType.SelectedIndex == 0 ? 1 : pkSubMasAdvisorType.SelectedIndex == 1 ? 2 : 3).ToString();
                if (pkSubMasAdvisorType.SelectedIndex == 0)
                {
                    dta.name_contno = sbSubMasAdvisorType1Name.Text;
                    dta.contno = sbSubMasAdvisorType1Appno.Text;
                }
                else if (pkSubMasAdvisorType.SelectedIndex == 1)
                {
                    dta.name_cus = enSubMasAdvisorType2Name.Text;
                    dta.type_cont_channel = pkSubSocialtype.SelectedIndex == 0 ? "1" : pkSubSocialtype.SelectedIndex == 1 ? "2" : pkSubSocialtype.SelectedIndex == 2 ? "3" : "4";

                }
                else if (pkSubMasAdvisorType.SelectedIndex == 2)
                {
                    dta.perosonal_cde = sbSubMasAdvisorType3EmNo.Text;
                    dta.personal_name = sbSubMasAdvisorType3Name.Text;
                }
            }

            return dta;
        }

        private bool PostDataGenaral()
        {
            bool ret = true;
            InsertCarPrice_WebService iapi = new InsertCarPrice_WebService();
            try
            {
                //ดีลเลอร์เดียวกัน --------------------
                if (pkDealerType.SelectedIndex == 0)
                {
                    var dta = GetDataGeneralMaster();
                    dta.type_grp_car = "01";
                    dta.app_id = enAppno.Text + "01";
                    if (!iapi.PostInformation_Transaction_Car(dta))
                    {
                        return false;
                    }
                    var dta_ = GetDataGeneralMaster();
                    dta_.type_grp_car = "02";
                    dta_.app_id = enAppno.Text + "02";
                    if (!iapi.PostInformation_Transaction_Car(dta_))
                    {
                        return false;
                    }
                }
                //คนละดีเลอร์----------------
                else
                {

                    var dta = GetDataGeneralMaster();
                    dta.type_grp_car = "01";
                    dta.app_id = enAppno.Text + "01";
                    if (!iapi.PostInformation_Transaction_Car(dta))
                    {
                        return false;
                    }
                    var dta_ = GetDataGeneralSubMaster();
                    dta_.type_grp_car = "02";
                    dta_.app_id = enAppno.Text + "02";
                    if (!iapi.PostInformation_Transaction_Car(dta_))
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex;
            }
            return ret;

        }

        private bool PostDataMaster()
        {
            bool ret = true;

            DataInformationCar dta = new DataInformationCar();
            dta.applno_car = enAppno.Text;
            dta.licno = enMasLicno.Text;
            dta.provice = ((DataProvince)pkMasProvince.SelectedItem).DESC_TH;
            dta.engno = enMasEngno.Text.Trim().ToUpper();
            dta.chasno = enMasChasno.Text.Trim().ToUpper();
            dta.brand = ((DataBrand)pkMasBrand.SelectedItem).CODE;
            dta.fuel = pkMasFuel.SelectedIndex == 0 ? "01" : "02";
            dta.type_car = ((DataTypeCar)pkMasCarType.SelectedItem).cde_typ_car;
            dta.grp_car = ((Model.InsertCarPrice_Model.DataVStyle1)pkMasStyle1.SelectedItem).cde_grpcar;
            dta.cde_pickup = ((Model.InsertCarPrice_Model.DataVStyle3)pkMasStyle2.SelectedItem).cde_style_car;
            if (((Model.InsertCarPrice_Model.DataVStyle1)pkMasStyle1.SelectedItem).cde_grpcar == "01")
            {
                dta.cde_special = "";
            }
            else if (((Model.InsertCarPrice_Model.DataVStyle1)pkMasStyle1.SelectedItem).cde_grpcar == "02")
            {
                if (((Model.InsertCarPrice_Model.DataVStyle3)pkMasStyle2.SelectedItem).cde_style_car == "H019" || ((Model.InsertCarPrice_Model.DataVStyle3)pkMasStyle2.SelectedItem).cde_style_car == "H020")
                {
                    dta.cde_special = ((DataCharacterCar)pkMasStyle3.SelectedItem).cde_style_car;
                }
                else dta.cde_special = "";
            }
            else if (((Model.InsertCarPrice_Model.DataVStyle1)pkMasStyle1.SelectedItem).cde_grpcar == "03")
            {
                if (((Model.InsertCarPrice_Model.DataVStyle3)pkMasStyle2.SelectedItem).cde_style_car == "H019" ||
                    ((Model.InsertCarPrice_Model.DataVStyle3)pkMasStyle2.SelectedItem).cde_style_car == "H020" ||
                    ((Model.InsertCarPrice_Model.DataVStyle3)pkMasStyle2.SelectedItem).cde_style_car == "H021" ||
                    ((Model.InsertCarPrice_Model.DataVStyle3)pkMasStyle2.SelectedItem).cde_style_car == "H080")
                {
                    dta.cde_special = ((DataCharacterCar)pkMasStyle3.SelectedItem).cde_style_car;
                }
                else dta.cde_special = "";
            }

            //if (pkMasStyle3.SelectedIndex != -1)
            //{
            //    dta.cde_special = ((DataCharacterCar)pkMasStyle3.SelectedItem).cde_style_car;
            //}

            dta.head_price = float.Parse(enMasHeadPrice.Text);
            dta.estimate_price = float.Parse("0");
            dta.finamt = float.Parse("0");
            dta.accessory = pkMasAccessories.SelectedIndex == -1 ? "" : ((Model.Appz_Model.DataAccessories)pkMasAccessories.SelectedItem).cde_accessory;
            dta.year_car = enMasYearCar.Text;
            dta.type_grp_car = "01";
            dta.status = 1;

            dta.campaign_id = pkCamPaign.SelectedIndex == -1 ? "0" : ((DataCampaign)pkCamPaign.SelectedItem).campaign_id;
            dta.create_by = _userlogin.data.Select(a => a.personnel_code).FirstOrDefault();
            dta.app_id = enAppno.Text + dta.type_grp_car;

            InsertCarPrice_WebService iapi = new InsertCarPrice_WebService();
            // ret = iapi.PostDataInformationCar(dta);
            if (!iapi.PostDataInformationCar(dta))
            {
                return false;
            }
            return ret;
        }
        private bool PostDataSubMaster()
        {
            bool ret = true;
            DataInformationCar dta = new DataInformationCar();

            dta.applno_car = enAppno.Text;
            dta.licno = enSubLicno.Text;
            dta.provice = ((DataProvince)pkSubProvince.SelectedItem).DESC_TH;
            dta.engno = "";
            dta.chasno = enSChasno.Text.Trim().ToUpper();
            dta.brand = "";
            dta.fuel = "";
            dta.type_car = ((DataTypeCar)pkSubCarType.SelectedItem).cde_typ_car;
            dta.grp_car = ((DataV_StyleSubMaster1)pkSubStyle1.SelectedItem).cde_type_style;     //แยกประเภทรถพ่วงกึ่งพ่วง
            dta.cde_pickup = ((DataV_StyleSubMaster2)pkSubStyle2.SelectedItem).cde_style_car;
            if (pkSubStyle3.SelectedIndex != -1) dta.cde_special = ((DataV_StyleSubMaster2)pkSubStyle3.SelectedItem).cde_style_car;
            else dta.cde_special = "";
            //if(pkSubStyle3.SelectedIndex!=-1) dta.cde_special = ((DataV_StyleSubMaster2)pkSubStyle3.SelectedItem).cde_style_car;
            dta.head_price = float.Parse(enSubCarPrice.Text);
            dta.estimate_price = float.Parse("0");
            dta.finamt = float.Parse("0");
            dta.accessory = "";
            dta.year_car = enSubYearCar.Text;
            dta.type_grp_car = "02";
            dta.status = 1;
            //dta.run_number = _appno;
            dta.campaign_id = pkCamPaign.SelectedIndex == -1 ? "0" : ((DataCampaign)pkCamPaign.SelectedItem).campaign_id;
            dta.create_by = _userlogin.data.Select(a => a.personnel_code).FirstOrDefault();
            dta.app_id = enAppno.Text + dta.type_grp_car;

            InsertCarPrice_WebService iapi = new InsertCarPrice_WebService();
            ret = iapi.PostDataInformationCar(dta);
            return ret;
        }
        private void PostImageMaster()
        {
            DataImage dta = new DataImage();
            dta.create_by = _userlogin.data.Select(a => a.personnel_code).FirstOrDefault();
            dta.applno_car = enAppno.Text;
            //dta.img1_head = img1.Source;        //มุมหน้า-ด้านคนขับ
            dta.img1_head = imgMasPic1.Text;        //มุมหน้า-ด้านคนขับ
            dta.img2_head = imgMasPic2.Text;        //มุมหน้า-ด้านโดยสาร
            dta.img3_head = imgMasPic3.Text;        //มุมหลัง-ด้านคนขับ
            dta.img26 = imgMasPic4.Text;            //รูปยกดั๊มพ์
            dta.img27 = imgMasPic5.Text;            //พื้นกระบะมุมข้างด้านใน
            dta.img4_head = imgMasPic6.Text;        //ภายในห้องโดยสาร(ถ่ายติดหัวเกียร์)
            dta.img5_head = imgMasPic7.Text;        //หน้าปัดเลขไมล์
            dta.img6_head = imgMasPic8.Text;        //กระจังหน้า(ถ่ายติดทะเบียนรถ)
            dta.img7_head = imgMasPic9.Text;        //ยกหัวเก๋ง(บริเวณใต้หัวเก๋ง)
            dta.img8_head = imgMasPic10.Text;       //คอแซสซี(ฝั่งคนขับ)
            dta.img9_head = imgMasPic11.Text;       //คอแซสซี(ฝั่งคนนั่ง)
            dta.img10_head = imgMasPic12.Text;      //เครื่องยนต์ฝั่งคนขับ(เห็นทั้งหมด)
            dta.img11_head = imgMasPic13.Text;      //เครื่องยนต์ฝั่งคนนั่ง(เห็นทั้งหมด)
            dta.img28 = imgMasPic14.Text;           //รูปเลขตัวรถ
            dta.img12_head = imgMasPic15.Text;      //แซสซี(ฝั่งคนขับ)
            dta.img13_head = imgMasPic16.Text;      //แซสซี(ฝั่งคนนั่ง)
            dta.img14_head = imgMasPic17.Text;      //รูปเกียร์
            dta.img15_head = imgMasPic18.Text;      //เพลากลาง เพลาท้าย
            dta.img23 = imgMasPic19.Text;           //รายการจดทะเบียน
            dta.img24 = imgMasPic20.Text;           //บันทึกเจ้าหน้าที่
            dta.img25 = imgMasPic21.Text;           //รายการเสียภาษี
            dta.img29 = imgMasPic22.Text;           //มุมหลัง-ฝั่งซ้าย

            dta.type_grp_car = "01";
            dta.app_id = enAppno.Text + dta.type_grp_car;
            InsertCarPrice_WebService iModel = new InsertCarPrice_WebService();
            iModel.PostImageMaster(dta);
        }

        private void PostImageSubMaster()
        {
            DataImage dta = new DataImage();
            dta.create_by = _userlogin.data.Select(a => a.personnel_code).FirstOrDefault();
            dta.applno_car = enAppno.Text;
            dta.img16_tail = imgSubPic1.Text;
            dta.img17_tail = imgSubPic2.Text;
            dta.img18_tail = imgSubPic3.Text;
            dta.img19_tail = imgSubPic4.Text;
            dta.img26 = imgSubPic5.Text;
            dta.img27 = imgSubPic6.Text;
            dta.img28 = imgSubPic7.Text;
            dta.img23 = imgSubPic8.Text;
            dta.img24 = imgSubPic9.Text;
            dta.img25 = imgSubPic10.Text;
            dta.img20_tail = imgSubPic11.Text;
            dta.img21_tail = imgSubPic12.Text;

            dta.type_grp_car = "02";
            dta.app_id = enAppno.Text + dta.type_grp_car;
            InsertCarPrice_WebService iModel = new InsertCarPrice_WebService();
            iModel.PostImageSubMaster(dta);

        }

        private void btnMasCarStyle_Clicked(object sender, EventArgs e)
        {
            InsertCarPrice_WebService iAPI = new InsertCarPrice_WebService();
            pkMasStyle1.ItemsSource = iAPI.GetDataVStyle1().Where(a => a.cde_head_tail == "01").ToList();
            frmMasCarStyle.IsVisible = true;
        }

        //public static async Task<string> Convertbase64Async(Stream stream)
        //{
        //    var bytes = new byte[stream.Length];
        //    await stream.ReadAsync(bytes, 0, (int)stream.Length);
        //    string base64 = Convert.ToBase64String(bytes);
        //    return base64;
        //}

        //public static Stream GetFileStream(string fileName) => File.OpenRead(fileName);


        //public async Task<string> GetImage()
        //{
        //    try
        //    {
        //        var photo = await MediaPicker.PickPhotoAsync();
        //        return await LoadPhotoAsync(photo);
        //        //Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
        //    }
        //    //catch (FeatureNotSupportedException fnsEx)
        //    //{
        //    //    // Feature is now supported on the device
        //    //}
        //    //catch (PermissionException pEx)
        //    //{
        //    //    // Permissions not granted
        //    //}
        //    catch (Exception ex)
        //    {
        //        //Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
        //        return null;
        //    }
        //}

        //public async Task<string> LoadPhotoAsync(FileResult photo)
        //{
        //    // canceled
        //    if (photo == null)
        //    {
        //        return null;

        //    }
        //    // save the file into local storage
        //    var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
        //    using (var stream = await photo.OpenReadAsync())
        //    using (var newStream = File.OpenWrite(newFile))
        //        await stream.CopyToAsync(newStream);

        //    return newFile;
        //}

        private async Task<string> GetImage()
        {
            var file = await MediaPicker.PickPhotoAsync();
            if (file != null)
            {
                var a = file.FullPath;
                return a;
            }
            else return null;
        }

        //private async ImageSource GetImage1()
        //{
        //    var file = await MediaPicker.PickPhotoAsync();
        //    if (file != null)
        //    {
        //        var stream = await file.OpenReadAsync();
        //        var result = ImageSource.FromStream(() => stream);
        //        return result;
        //    }
        //    else return null;



        //}



        private async void btnMasPic1_Clicked(object sender, EventArgs e)
        {

            //byte[] imageData;
            //var file = await MediaPicker.PickPhotoAsync();
            //if (file != null)
            //{
            //    var stream = await file.OpenReadAsync();
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        stream.CopyTo(ms);
            //        imageData = ms.ToArray();
            //    }
            //    byte[] resizedImage = await ImageResizer.ResizeImage(imageData, 400, 400);

            //    var x = Convert.ToBase64String(resizedImage);
            //    imgMasPic1.Text =  Convert.ToBase64String(resizedImage);
            //    //img1.Source = ImageSource.FromStream(() => new MemoryStream(resizedImage));

            //}




            //(sender as Button).IsEnabled = false;
            imgMasPic1.Text = await GetImage();
            if (imgMasPic1.Text == null) btnMasPic1.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic1.BackgroundColor = Color.LightGray;

        }

        private async void btnMasPic2_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic2.Text = await GetImage();
            if (imgMasPic2.Text == null) btnMasPic2.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic2.BackgroundColor = Color.LightGray;

        }

        private async void btnMasPic3_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic3.Text = await GetImage();
            if (imgMasPic3.Text == null) btnMasPic3.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic3.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic4_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic4.Text = await GetImage();
            if (imgMasPic4.Text == null) btnMasPic4.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic4.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic5_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic5.Text = await GetImage();
            if (imgMasPic5.Text == null) btnMasPic5.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic5.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic6_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic6.Text = await GetImage();
            if (imgMasPic6.Text == null) btnMasPic6.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic6.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic7_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic7.Text = await GetImage();
            if (imgMasPic7.Text == null) btnMasPic7.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic7.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic8_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic8.Text = await GetImage();
            if (imgMasPic8.Text == null) btnMasPic8.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic8.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic9_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic9.Text = await GetImage();
            if (imgMasPic9.Text == null) btnMasPic9.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic9.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic10_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic10.Text = await GetImage();
            if (imgMasPic10.Text == null) btnMasPic10.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic10.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic11_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic11.Text = await GetImage();
            if (imgMasPic11.Text == null) btnMasPic11.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic11.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic12_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic12.Text = await GetImage();
            if (imgMasPic12.Text == null) btnMasPic12.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic12.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic13_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic13.Text = await GetImage();
            if (imgMasPic13.Text == null) btnMasPic13.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic13.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic14_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic14.Text = await GetImage();
            if (imgMasPic14.Text == null) btnMasPic14.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic14.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic15_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic15.Text = await GetImage();
            if (imgMasPic15.Text == null) btnMasPic15.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic15.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic16_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic16.Text = await GetImage();
            if (imgMasPic16.Text == null) btnMasPic16.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic16.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic17_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic17.Text = await GetImage();
            if (imgMasPic17.Text == null) btnMasPic17.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic17.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic18_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic18.Text = await GetImage();
            if (imgMasPic18.Text == null) btnMasPic18.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic18.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic19_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic19.Text = await GetImage();
            if (imgMasPic19.Text == null) btnMasPic19.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic19.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic20_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic20.Text = await GetImage();
            if (imgMasPic20.Text == null) btnMasPic20.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic20.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic21_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgMasPic21.Text = await GetImage();
            if (imgMasPic21.Text == null) btnMasPic21.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic21.BackgroundColor = Color.LightGray;
        }

        private async void btnMasPic22_Clicked(object sender, EventArgs e)
        {
            imgMasPic22.Text = await GetImage();
            if (imgMasPic22.Text == null) btnMasPic22.BackgroundColor = Color.FromHex("#F1C40F");
            else btnMasPic22.BackgroundColor = Color.LightGray;
        }

        private void btnSubCarStyle_Clicked(object sender, EventArgs e)
        {
            frmSubCarStyle.IsVisible = true;
            Appz_WebService iAPI = new Appz_WebService();
            pkSubStyle1.ItemsSource = iAPI.GetDataV_StyleSubMaster1();
        }

        private async void btnSubPic1_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgSubPic1.Text = await GetImage();
            if (imgSubPic1.Text == null) btnSubPic1.BackgroundColor = Color.FromHex("#F1C40F");
            else btnSubPic1.BackgroundColor = Color.LightGray;
        }

        private async void btnSubPic2_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgSubPic2.Text = await GetImage();
            if (imgSubPic2.Text == null) btnSubPic2.BackgroundColor = Color.FromHex("#F1C40F");
            else btnSubPic2.BackgroundColor = Color.LightGray;
        }

        private async void btnSubPic3_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgSubPic3.Text = await GetImage();
            if (imgSubPic3.Text == null) btnSubPic3.BackgroundColor = Color.FromHex("#F1C40F");
            else btnSubPic3.BackgroundColor = Color.LightGray;
        }

        private async void btnSubPic4_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgSubPic4.Text = await GetImage();
            if (imgSubPic4.Text == null) btnSubPic4.BackgroundColor = Color.FromHex("#F1C40F");
            else btnSubPic4.BackgroundColor = Color.LightGray;
        }

        private async void btnSubPic5_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgSubPic5.Text = await GetImage();
            if (imgSubPic5.Text == null) btnSubPic5.BackgroundColor = Color.FromHex("#F1C40F");
            else btnSubPic5.BackgroundColor = Color.LightGray;
        }

        private async void btnSubPic6_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgSubPic6.Text = await GetImage();
            if (imgSubPic6.Text == null) btnSubPic6.BackgroundColor = Color.FromHex("#F1C40F");
            else btnSubPic6.BackgroundColor = Color.LightGray;
        }

        private async void btnSubPic7_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgSubPic7.Text = await GetImage();
            if (imgSubPic7.Text == null) btnSubPic7.BackgroundColor = Color.FromHex("#F1C40F");
            else btnSubPic7.BackgroundColor = Color.LightGray;
        }

        private async void btnSubPic8_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgSubPic8.Text = await GetImage();
            if (imgSubPic8.Text == null) btnSubPic8.BackgroundColor = Color.FromHex("#F1C40F");
            else btnSubPic8.BackgroundColor = Color.LightGray;
        }

        private async void btnSubPic9_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgSubPic9.Text = await GetImage();
            if (imgSubPic9.Text == null) btnSubPic9.BackgroundColor = Color.FromHex("#F1C40F");
            else btnSubPic9.BackgroundColor = Color.LightGray;
        }

        private async void btnSubPic10_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgSubPic10.Text = await GetImage();
            if (imgSubPic10.Text == null) btnSubPic10.BackgroundColor = Color.FromHex("#F1C40F");
            else btnSubPic10.BackgroundColor = Color.LightGray;
        }

        private async void btnSubPic11_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgSubPic11.Text = await GetImage();
            if (imgSubPic11.Text == null) btnSubPic11.BackgroundColor = Color.FromHex("#F1C40F");
            else btnSubPic11.BackgroundColor = Color.LightGray;
        }

        private async void btnSubPic12_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;
            imgSubPic12.Text = await GetImage();
            if (imgSubPic12.Text == null) btnSubPic12.BackgroundColor = Color.FromHex("#F1C40F");
            else btnSubPic12.BackgroundColor = Color.LightGray;
        }

        private void btnBack2_Clicked(object sender, EventArgs e)
        {
            frmPage1.IsVisible = true;
            frmPage2.IsVisible = false;
            frmPage3.IsVisible = false;
            btnSave.IsVisible = false;
            btnBack2.IsVisible = false;
            btnBack3.IsVisible = false;
            btnMenu.IsVisible = true;
            btnNext1.IsVisible = true;
            btnNext2.IsVisible = false;
        }

        private void btnBack3_Clicked(object sender, EventArgs e)
        {
            frmPage1.IsVisible = false;
            frmPage2.IsVisible = true;
            frmPage3.IsVisible = false;
            btnSave.IsVisible = false;
            btnBack2.IsVisible = true;
            btnBack3.IsVisible = false;
            btnMenu.IsVisible = false;
            btnNext1.IsVisible = false;
            btnNext2.IsVisible = true;
        }

        private void btnNext1_Clicked(object sender, EventArgs e)
        {
            string errorPage1 = ValidationGenaeral();
            if (errorPage1 != "")
            {
                DisplayAlert("", errorPage1, "ตกลง");
            }
            else
            {
                if (pkNumMasterandSubMaster.SelectedIndex == 0)
                {
                    MasterandSubMaster();
                    btnSave.IsVisible = true;
                    btnNext2.IsVisible = false;
                }

                if (pkNumMasterandSubMaster.SelectedIndex == 1)
                {
                    frmPage1.IsVisible = false;
                    frmPage2.IsVisible = false;
                    frmPage3.IsVisible = true;
                    btnSave.IsVisible = true;
                    btnBack2.IsVisible = true;
                    btnBack3.IsVisible = false;
                    btnMenu.IsVisible = false;
                    btnNext1.IsVisible = false;
                    btnNext2.IsVisible = false;
                }
                else if (pkNumMasterandSubMaster.SelectedIndex == 2)
                {
                    MasterandSubMaster();
                    btnSave.IsVisible = false;
                    btnNext2.IsVisible = true;
                }
            }
        }

        private bool Save()
        {
            bool ret = true;
            //ret = PostDataGenaral();
            if (PostDataGenaral() == true)
            {
                //เพิ่มข้อมูลตัวแม่
                if (pkNumMasterandSubMaster.SelectedIndex == 0)
                {
                    if (PostDataMaster() == true)
                    {
                        PostImageMaster();
                        DisplayAlert("หน้าเพิ่มข้อมูลรายการรถ", "บันทึกข้อมูลเรียบร้อย", "ตกลง");
                        ret = true;
                    }
                    else return false;
                }
                //เพิ่มข้อมูลตัวลูก
                else if (pkNumMasterandSubMaster.SelectedIndex == 1)
                {
                    if (PostDataSubMaster() == true)
                    {
                        PostImageSubMaster();
                        DisplayAlert("หน้าเพิ่มข้อมูลรายการรถ", "บันทึกข้อมูลเรียบร้อย", "ตกลง");
                        ret = true;
                    }
                    else return false;
                }
                else //ตัวแม่และตัวลูก
                {
                    if (PostDataMaster() == true && PostDataSubMaster() == true)
                    {
                        PostImageMaster();
                        PostImageSubMaster();
                        DisplayAlert("หน้าเพิ่มข้อมูลรายการรถ", "บันทึกข้อมูลเรียบร้อย", "ตกลง");
                        ret = true;
                    }
                    else return false;
                }
            }
            else
            {
                return false;
            }
            return ret;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            PopupLoad.IsVisible = true;
            //PageInsertCarPrice.Opacity = 0.5;
            await Task.Delay(2000);
           string error = CheckValidation();
            if (error != "")
            {
                await DisplayAlert("ผิดพลาด", error, "ตกลง");
            }
            else
            {
                if (!Save())
                {
                    await DisplayAlert("หน้าเพิ่มข้อมูลรายการรถ", "ไม่สามารถบันทึกข้อมูลได้", "ตกลง");
                }
                else
                {
                    await Navigation.PushAsync(new MainPage(_userlogin));
                }
            }
            PopupLoad.IsVisible = false;

        }

        private void pkDealerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pkNote.IsEnabled = true;
            pkCamPaign.IsEnabled = true;
            pkNumMasterandSubMaster.IsEnabled = true;

            if (pkDealerType.SelectedIndex == 0)
            {
                frmMaster.IsVisible = true;
                frmSubMaster.IsVisible = false;

                pkConnectionMaster.SelectedIndex = -1;
                pkMasAdvisorType.SelectedIndex = -1;

                enAppno.Text = _appno;

                pkNumMasterandSubMaster.SelectedIndex = -1;
                pkNumMasterandSubMaster.IsEnabled = true;
            }
            else if (pkDealerType.SelectedIndex == 1)
            {
                frmMaster.IsVisible = true;
                frmSubMaster.IsVisible = true;

                enAppno.Text = "T" + _appno;

                pkConnectionMaster.SelectedIndex = -1;
                pkConnectionSubMaster.SelectedIndex = -1;
                pkMasAdvisorType.SelectedIndex = -1;
                pkSubMasAdvisorType.SelectedIndex = -1;

                pkNumMasterandSubMaster.SelectedIndex = 2;
                pkNumMasterandSubMaster.IsEnabled = false;
            }
        }

        private void pkConnectionMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_appno = pkConnectionMaster.SelectedIndex == 0 ? "A" + enAppno.Text : pkConnectionMaster.SelectedIndex == 1 ? "D" + enAppno.Text : "O" + enAppno.Text;
            if (pkDealerType.SelectedIndex == 0)
            {
                if (pkConnectionMaster.SelectedIndex == 0)
                {
                    enAppno.Text = "A" + _appno;
                }
                else if (pkConnectionMaster.SelectedIndex == 1)
                {
                    enAppno.Text = "D" + _appno;
                }
                else if (pkConnectionMaster.SelectedIndex == 2)
                {
                    enAppno.Text = _appno;

                }
            }

            if (pkConnectionMaster.SelectedIndex == 0)
            {
                frmMasContact1.IsVisible = true;
                frmMasContact2.IsVisible = false;
                frmMasContact3.IsVisible = false;

                sbConnectionMaster2Name.Text = "";
                sbConnectionMaster2Code.Text = "";

                pkMasAdvisorType.SelectedIndex = -1;



            }
            else if (pkConnectionMaster.SelectedIndex == 1)
            {
                frmMasContact1.IsVisible = false;
                frmMasContact2.IsVisible = true;
                frmMasContact3.IsVisible = false;

                sbConnectionMaster1Name.Text = "";
                sbConnectionMaster1Code.Text = "";

                pkMasAdvisorType.SelectedIndex = -1;
            }
            else if (pkConnectionMaster.SelectedIndex == 2)
            {
                frmMasContact1.IsVisible = false;
                frmMasContact2.IsVisible = false;
                frmMasContact3.IsVisible = true;

                sbConnectionMaster1Name.Text = "";
                sbConnectionMaster1Code.Text = "";

                sbConnectionMaster2Name.Text = "";
                sbConnectionMaster2Code.Text = "";

            }
            else
            {
                frmMasContact1.IsVisible = false;
                frmMasContact2.IsVisible = false;
                frmMasContact3.IsVisible = false;

                sbConnectionMaster1Name.Text = "";
                sbConnectionMaster1Code.Text = "";

                sbConnectionMaster2Name.Text = "";
                sbConnectionMaster2Code.Text = "";

                pkMasAdvisorType.SelectedIndex = -1;
            }
        }

        private void pkMasAdvisorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pkDealerType.SelectedIndex == 0)
            {
                if (pkMasAdvisorType.SelectedIndex == 0)
                {
                    enAppno.Text = "O" + _appno;

                }
                else if (pkMasAdvisorType.SelectedIndex == 1)
                {
                    enAppno.Text = "N" + _appno;

                }
                else if (pkMasAdvisorType.SelectedIndex == 2)
                {
                    enAppno.Text = "M" + _appno;

                }
            }

            if (pkMasAdvisorType.SelectedIndex == 0)
            {
                stMasAdvisorType1.IsVisible = true;

                stMasAdvisorType2.IsVisible = false;

                stMasAdvisorType3.IsVisible = false;

            }
            else if (pkMasAdvisorType.SelectedIndex == 1)
            {

                stMasAdvisorType1.IsVisible = false;
                stMasAdvisorType2.IsVisible = true;
                stMasAdvisorType3.IsVisible = false;


            }
            else if (pkMasAdvisorType.SelectedIndex == 2)
            {

                stMasAdvisorType1.IsVisible = false;
                stMasAdvisorType2.IsVisible = false;
                stMasAdvisorType3.IsVisible = true;

            }
            else
            {
                stMasAdvisorType1.IsVisible = false;
                stMasAdvisorType2.IsVisible = false;
                stMasAdvisorType3.IsVisible = false;
            }
        }

        private void pkConnectionSubMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pkConnectionSubMaster.SelectedIndex == 0)
            {
                frmSubMasContact1.IsVisible = true;
                frmSubMasContact2.IsVisible = false;
                frmSubMasContact3.IsVisible = false;
            }
            else if (pkConnectionSubMaster.SelectedIndex == 1)
            {
                frmSubMasContact1.IsVisible = false;
                frmSubMasContact2.IsVisible = true;
                frmSubMasContact3.IsVisible = false;
            }
            else if (pkConnectionSubMaster.SelectedIndex == 2)
            {
                frmSubMasContact1.IsVisible = false;
                frmSubMasContact2.IsVisible = false;
                frmSubMasContact3.IsVisible = true;
            }
            else
            {
                frmSubMasContact1.IsVisible = false;
                frmSubMasContact2.IsVisible = false;
                frmSubMasContact3.IsVisible = false;
            }
        }

        private void pkSubMasAdvisorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pkSubMasAdvisorType.SelectedIndex == 0)
            {
                stSubMasAdvisorType1.IsVisible = true;
                stSubMasAdvisorType2.IsVisible = false;
                stSubMasAdvisorType3.IsVisible = false;

            }
            else if (pkSubMasAdvisorType.SelectedIndex == 1)
            {
                stSubMasAdvisorType1.IsVisible = false;
                stSubMasAdvisorType2.IsVisible = true;
                stSubMasAdvisorType3.IsVisible = false;
            }
            else if (pkSubMasAdvisorType.SelectedIndex == 2)
            {
                stSubMasAdvisorType1.IsVisible = false;
                stSubMasAdvisorType2.IsVisible = false;
                stSubMasAdvisorType3.IsVisible = true;



            }
        }

        private void pkNote_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pkNote.SelectedIndex == 0)
            {
                stRemarkdes.IsVisible = true;
                DatetimePicker.IsVisible = false;
            }
            else if (pkNote.SelectedIndex == 1)
            {
                stRemarkdes.IsVisible = true;
                DatetimePicker.IsVisible = true;
            }
            else
            {
                stRemarkdes.IsVisible = false;
                DatetimePicker.IsVisible = false;
            }

        }


        private void pkMasStyle1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InsertCarPrice_WebService iAPI = new InsertCarPrice_WebService();
            if (pkMasStyle1.SelectedIndex != -1)
            {
                pkMasStyle2.ItemsSource = iAPI.GetDataVStyle3().Where(a => a.cde_head_tail == "01" && a.cde_style == "01" && a.cde_grpcar == ((Model.InsertCarPrice_Model.DataVStyle1)pkMasStyle1.SelectedItem).cde_grpcar).ToList();
                //pkMasStyle2.ItemsSource = iAPI.GetStyle2("01", ((Model.InsertCarPrice_Model.DataStyle1)pkMasStyle1.SelectedItem).cde_grpcar);
                pkMasStyle2.IsEnabled = true;
                stMasStyle3.IsVisible = false;
            }
            else
            {
                pkMasStyle2.SelectedIndex = -1;
                pkMasStyle2.IsEnabled = false;
            }
        }

        private void pkMasStyle2_SelectedIndexChanged(object sender, EventArgs e)
        {
            InsertCarPrice_WebService iAPI = new InsertCarPrice_WebService();

            //if (pkMasStyle1.SelectedIndex != -1)
            if (pkMasStyle1.SelectedIndex != -1 && pkMasStyle2.SelectedIndex != -1)
            {
                var cdestylecar01 = ((Model.InsertCarPrice_Model.DataVStyle1)pkMasStyle1.SelectedItem).cde_grpcar;
                var cdestylecar02 = ((Model.InsertCarPrice_Model.DataVStyle3)pkMasStyle2.SelectedItem).cde_style_car;

                if (cdestylecar01 == "02")
                {
                    if (cdestylecar02 == "H019")
                    {
                        pkMasStyle3.ItemsSource = _charactercar.Where(a => a.cde_head_tail == "01" && a.cde_style == "02" && a.name_grpcar == pkMasStyle1.Items[pkMasStyle1.SelectedIndex] && a.cde_type_style == "H01").ToList();
                        pkMasStyle3.IsEnabled = true;
                        stMasStyle3.IsVisible = true;
                    }
                    else if (cdestylecar02 == "H020")
                    {
                        pkMasStyle3.ItemsSource = _charactercar.Where(a => a.cde_head_tail == "01" && a.cde_style == "02" && a.name_grpcar == pkMasStyle1.Items[pkMasStyle1.SelectedIndex] && a.cde_type_style == "H02").ToList();
                        pkMasStyle3.IsEnabled = true;
                        stMasStyle3.IsVisible = true;
                    }
                    else
                    {
                        stMasStyle3.IsVisible = false;
                    }
                }
                else if (cdestylecar01 == "03")
                {
                    if (cdestylecar02 == "H019")
                    {
                        pkMasStyle3.ItemsSource = _charactercar.Where(a => a.cde_head_tail == "01" && a.cde_style == "02" && a.cde_grpcar == cdestylecar01 && a.cde_type_style == "H01").ToList();
                        pkMasStyle3.IsEnabled = true;
                        stMasStyle3.IsVisible = true;
                    }
                    else if (cdestylecar02 == "H020")
                    {
                        pkMasStyle3.ItemsSource = _charactercar.Where(a => a.cde_head_tail == "01" && a.cde_style == "02" && a.cde_grpcar == cdestylecar01 && a.cde_type_style == "H02").ToList();
                        pkMasStyle3.IsEnabled = true;
                        stMasStyle3.IsVisible = true;
                    }
                    else if (cdestylecar02 == "H021")
                    {
                        pkMasStyle3.ItemsSource = _charactercar.Where(a => a.cde_head_tail == "01" && a.cde_style == "02" && a.cde_grpcar == cdestylecar01 && a.cde_type_style == "H04").ToList();
                        pkMasStyle3.IsEnabled = true;
                        stMasStyle3.IsVisible = true;
                    }
                    else if (cdestylecar02 == "H080")
                    {
                        pkMasStyle3.ItemsSource = _charactercar.Where(a => a.cde_head_tail == "01" && a.cde_style == "02" && a.cde_grpcar == cdestylecar01 && a.cde_type_style == "H03").ToList();
                        pkMasStyle3.IsEnabled = true;
                        stMasStyle3.IsVisible = true;
                    }
                    else
                    {
                        stMasStyle3.IsVisible = false;
                    }

                }
                else
                {
                    stMasStyle3.IsVisible = false;
                }
            }
            else
            {

                pkMasStyle3.SelectedIndex = -1;
                stMasStyle3.IsVisible = false;
            }
        }

        private void pkSubStyle1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Appz_WebService iAPI = new Appz_WebService();
            //InsertCarPrice_WebService api = new InsertCarPrice_WebService();
            if (pkSubStyle1.SelectedIndex != -1)
            {
                var cdetypestyle = ((DataV_StyleSubMaster1)pkSubStyle1.SelectedItem).cde_type_style;
                pkSubStyle2.ItemsSource = iAPI.GetDataV_StyleSubMaster2().Where(a => a.cde_type_style == cdetypestyle).ToList();
                //pkSubStyle2.ItemsSource = api.GetStyle2("02", ((Model.InsertCarPrice_Model.DataStyle1)pkSubStyle1.SelectedItem).cde_grpcar);
                pkSubStyle2.IsEnabled = true;
                stSubStyle3.IsVisible = false;
            }
            else
            {
                pkSubStyle2.SelectedIndex = -1;
                pkSubStyle2.IsEnabled = false;
            }

        }

        private void pkSubStyle2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Appz_WebService iAPI = new Appz_WebService();
            // InsertCarPrice_WebService api = new InsertCarPrice_WebService();
            if (pkSubStyle2.SelectedIndex != -1)
            {
                var cdetypestyle = ((DataV_StyleSubMaster2)pkSubStyle2.SelectedItem).cde_style_car;
                if (pkSubStyle1.Items[pkSubStyle1.SelectedIndex] == "รถกึ่งพ่วง" && (cdetypestyle == "T003" || cdetypestyle == "T015"))
                {
                    pkSubStyle3.ItemsSource = iAPI.GetDataV_StyleSubMaster2().Where(a => a.cde_style == "02").ToList();
                    // pkSubStyle3.ItemsSource = api.GetStyle3("02", ((Model.InsertCarPrice_Model.DataStyle1)pkSubStyle1.SelectedItem).cde_grpcar, ((Model.InsertCarPrice_Model.DataStyle2)pkSubStyle2.SelectedItem).cde_style);
                    //pkSubStyle3.IsEnabled = true;

                    stSubStyle3.IsVisible = true;

                    //lbSubStyle3.IsVisible = false;
                    //pkSubStyle3.IsVisible = false;
                }
                else
                {
                    stSubStyle3.IsVisible = false;
                    pkSubStyle3.SelectedIndex = -1;
                    //lbSubStyle3.IsVisible = false;
                    //pkSubStyle3.IsVisible = false;
                }

            }
            else
            {
                pkSubStyle3.SelectedIndex = -1;
                stSubStyle3.IsVisible = false;
                //pkSubStyle3.IsEnabled = false;
            }

            //pkSubStyle3.ItemsSource = api.GetStyle3("02", pkSubStyle1.Items[pkSubStyle1.SelectedIndex], pkSubStyle2.Items[pkSubStyle2.SelectedIndex]);
            //pkSubStyle3.IsEnabled = true;
        }

        private void btnNext2_Clicked(object sender, EventArgs e)
        {
            string errorPage2 = ValidationMaster();
            if (errorPage2 != "")
            {
                DisplayAlert("ผิดพลาด", errorPage2, "ตกลง");
            }
            else
            {
                frmPage1.IsVisible = false;
                frmPage2.IsVisible = false;
                frmPage3.IsVisible = true;
                btnSave.IsVisible = true;
                btnBack2.IsVisible = false;
                btnBack3.IsVisible = true;
                btnMenu.IsVisible = false;
                btnNext1.IsVisible = false;
                btnNext2.IsVisible = false;
            }
        }

        private async void btnMenu_Clicked(object sender, EventArgs e)
        {
            PopupLoad.IsVisible = true;
            // PageInsertCarPrice.Opacity = 0.5;
            await Task.Delay(2000);
            await Navigation.PushAsync(new MainPage(_userlogin));
            PopupLoad.IsVisible = false;
        }


        //public void GetDealerAgent()
        //{
        //    Appz_WebService iAPI = new Appz_WebService();
        //    var data=iAPI.GetDataDealerAgent();
        //    DealerAgent = data;
        //}

        //public void GetCustomer()
        //{
        //    Appz_WebService iAPI = new Appz_WebService();
        //    var data = iAPI.GetDataCustomer();
        //    Customer = data;
        //}

        //public void GetEmployee()
        //{
        //    Appz_WebService iAPI = new Appz_WebService();
        //    var data = iAPI.GetDataEmployee();
        //    Employee = data;
        //}



        private void sbConnectionMaster1Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionMaster1Name.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = Agent.Where(a => a.Name.Contains(keyword)).ToList();
                lstConnectionMaster1Name.ItemsSource = suggestions.Select(a => a.Name);
                lstConnectionMaster1Name.IsVisible = true;
            }
        }

        private void lstConnectionMaster1Name_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionMaster1Name.Text = lstConnectionMaster1Name.SelectedItem.ToString();
            lstConnectionMaster1Name.IsVisible = false;
            sbConnectionMaster1Code.Text = Agent.Where(a => a.Name == sbConnectionMaster1Name.Text).FirstOrDefault().No_;
            lstConnectionMaster1Code.IsVisible = false;
        }


        private void sbConnectionMaster1Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionMaster1Code.Text.ToUpper();
            if (keyword.Length >= 1)
            {
                var suggestions = Agent.Where(a => a.No_.Contains(keyword)).ToList();
                lstConnectionMaster1Code.ItemsSource = suggestions.Select(a => a.No_);
                lstConnectionMaster1Code.IsVisible = true;
            }
        }

        private void lstConnectionMaster1Code_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionMaster1Code.Text = lstConnectionMaster1Code.SelectedItem.ToString();
            lstConnectionMaster1Code.IsVisible = false;
            sbConnectionMaster1Name.Text = Agent.Where(a => a.No_ == sbConnectionMaster1Code.Text).FirstOrDefault().Name;
            lstConnectionMaster1Name.IsVisible = false;
        }

        private void sbConnectionMaster2Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionMaster2Name.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = Dealer.Where(a => a.Name.Contains(keyword)).ToList();
                lstConnectionMaster2Name.ItemsSource = suggestions.Select(a => a.Name);
                lstConnectionMaster2Name.IsVisible = true;
            }
        }

        private void lstConnectionMaster2Name_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionMaster2Name.Text = lstConnectionMaster2Name.SelectedItem.ToString();
            lstConnectionMaster2Name.IsVisible = false;
            sbConnectionMaster2Code.Text = Dealer.Where(a => a.Name == sbConnectionMaster2Name.Text).FirstOrDefault().No_;
            lstConnectionMaster2Code.IsVisible = false;
        }

        private void sbConnectionMaster2Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionMaster2Code.Text.ToUpper();
            if (keyword.Length >= 1)
            {
                var suggestions = Dealer.Where(a => a.No_.Contains(keyword)).ToList();
                lstConnectionMaster2Code.ItemsSource = suggestions.Select(a => a.No_);
                lstConnectionMaster2Code.IsVisible = true;
            }
        }

        private void lstConnectionMaster2Code_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionMaster2Code.Text = lstConnectionMaster2Code.SelectedItem.ToString();
            lstConnectionMaster2Code.IsVisible = false;
            sbConnectionMaster2Name.Text = Dealer.Where(a => a.No_ == sbConnectionMaster2Code.Text).FirstOrDefault().Name;
            lstConnectionMaster2Name.IsVisible = false;
        }

        private void sbMasAdvisorType1Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbMasAdvisorType1Name.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = Customer.Where(a => a.name_cus.Contains(keyword)).ToList();
                lstMasAdvisorType1Name.ItemsSource = suggestions.Select(a => a.name_cus);
                lstMasAdvisorType1Name.IsVisible = true;
            }
        }

        private void lstMasAdvisorType1Name_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbMasAdvisorType1Name.Text = lstMasAdvisorType1Name.SelectedItem.ToString();
            lstMasAdvisorType1Name.IsVisible = false;
            sbMasAdvisorType1Appno.Text = Customer.Where(a => a.name_cus == sbMasAdvisorType1Name.Text).FirstOrDefault().contno;
            lstMasAdvisorType1Appno.IsVisible = false;
        }

        private void sbMasAdvisorType1Appno_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbMasAdvisorType1Appno.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = Customer.Where(a => a.contno.Contains(keyword)).ToList();
                lstMasAdvisorType1Appno.ItemsSource = suggestions.Select(a => a.contno);
                lstMasAdvisorType1Appno.IsVisible = true;
            }
        }

        private void lstMasAdvisorType1Appno_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbMasAdvisorType1Appno.Text = lstMasAdvisorType1Appno.SelectedItem.ToString();
            lstMasAdvisorType1Appno.IsVisible = false;
            sbMasAdvisorType1Name.Text = Customer.Where(a => a.contno == sbMasAdvisorType1Appno.Text).FirstOrDefault().name_cus;
            lstMasAdvisorType1Name.IsVisible = false;
        }

        private void sbMasAdvisorType3EmNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbMasAdvisorType3EmNo.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = Employee.Where(a => a.personnel_code.Contains(keyword)).ToList();
                lstMasAdvisorType3EmNo.ItemsSource = suggestions.Select(a => a.personnel_code);
                lstMasAdvisorType3EmNo.IsVisible = true;
            }
        }

        private void lstMasAdvisorType3EmNo_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbMasAdvisorType3EmNo.Text = lstMasAdvisorType3EmNo.SelectedItem.ToString();
            lstMasAdvisorType3EmNo.IsVisible = false;
            sbMasAdvisorType3Name.Text = Employee.Where(a => a.personnel_code == sbMasAdvisorType3EmNo.Text).FirstOrDefault().personnel_name;
            lstMasAdvisorType3Name.IsVisible = false;
        }

        private void sbMasAdvisorType3Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbMasAdvisorType3Name.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = Employee.Where(a => a.personnel_name.Contains(keyword)).ToList();
                lstMasAdvisorType3Name.ItemsSource = suggestions.Select(a => a.personnel_name);
                lstMasAdvisorType3Name.IsVisible = true;
            }
        }

        private void lstMasAdvisorType3Name_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbMasAdvisorType3Name.Text = lstMasAdvisorType3Name.SelectedItem.ToString();
            lstMasAdvisorType3Name.IsVisible = false;
            sbMasAdvisorType3EmNo.Text = Employee.Where(a => a.personnel_name == sbMasAdvisorType3Name.Text).FirstOrDefault().personnel_code;
            lstMasAdvisorType3EmNo.IsVisible = false;
        }

        private void sbConnectionSubMaster1Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionSubMaster1Name.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = Agent.Where(a => a.Name.Contains(keyword)).ToList();
                lstConnectionSubMaster1Name.ItemsSource = suggestions.Select(a => a.Name);
                lstConnectionSubMaster1Name.IsVisible = true;
            }
        }

        private void lstConnectionSubMaster1Name_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionSubMaster1Name.Text = lstConnectionSubMaster1Name.SelectedItem.ToString();
            lstConnectionSubMaster1Name.IsVisible = false;
            sbConnectionSubMaster1Code.Text = Agent.Where(a => a.Name == sbConnectionSubMaster1Name.Text).FirstOrDefault().No_;
            lstConnectionSubMaster1Code.IsVisible = false;
        }

        private void sbConnectionSubMaster1Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionSubMaster1Code.Text.ToUpper();
            if (keyword.Length >= 1)
            {
                var suggestions = Agent.Where(a => a.No_.Contains(keyword)).ToList();
                lstConnectionSubMaster1Code.ItemsSource = suggestions.Select(a => a.No_);
                lstConnectionSubMaster1Code.IsVisible = true;
            }
        }

        private void lstConnectionSubMaster1Code_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionSubMaster1Code.Text = lstConnectionSubMaster1Code.SelectedItem.ToString();
            lstConnectionSubMaster1Code.IsVisible = false;
            sbConnectionSubMaster1Name.Text = Agent.Where(a => a.No_ == sbConnectionSubMaster1Code.Text).FirstOrDefault().Name;
            lstConnectionSubMaster1Name.IsVisible = false;
        }

        private void sbConnectionSubMaster2Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionSubMaster2Name.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = Dealer.Where(a => a.Name.Contains(keyword)).ToList();
                lstConnectionSubMaster2Name.ItemsSource = suggestions.Select(a => a.Name);
                lstConnectionSubMaster2Name.IsVisible = true;
            }
        }

        private void lstConnectionSubMaster2Name_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionSubMaster2Name.Text = lstConnectionSubMaster2Name.SelectedItem.ToString();
            lstConnectionSubMaster2Name.IsVisible = false;
            sbConnectionSubMaster2Code.Text = Dealer.Where(a => a.Name == sbConnectionSubMaster2Name.Text).FirstOrDefault().No_;
            lstConnectionSubMaster2Code.IsVisible = false;
        }

        private void sbConnectionSubMaster2Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionSubMaster2Code.Text.ToUpper();
            if (keyword.Length >= 1)
            {
                var suggestions = Dealer.Where(a => a.No_.Contains(keyword)).ToList();
                lstConnectionSubMaster2Code.ItemsSource = suggestions.Select(a => a.No_);
                lstConnectionSubMaster2Code.IsVisible = true;
            }
        }

        private void lstConnectionSubMaster2Code_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionSubMaster2Code.Text = lstConnectionSubMaster2Code.SelectedItem.ToString();
            lstConnectionSubMaster2Code.IsVisible = false;
            sbConnectionSubMaster2Name.Text = Dealer.Where(a => a.No_ == sbConnectionSubMaster2Code.Text).FirstOrDefault().Name;
            lstConnectionSubMaster2Name.IsVisible = false;
        }

        private void sbSubMasAdvisorType1Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbSubMasAdvisorType1Name.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = Customer.Where(a => a.name_cus.Contains(keyword)).ToList();
                lstSubMasAdvisorType1Name.ItemsSource = suggestions.Select(a => a.name_cus);
                lstSubMasAdvisorType1Name.IsVisible = true;
            }
        }

        private void lstSubMasAdvisorType1Name_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbSubMasAdvisorType1Name.Text = lstSubMasAdvisorType1Name.SelectedItem.ToString();
            lstSubMasAdvisorType1Name.IsVisible = false;
            sbSubMasAdvisorType1Appno.Text = Customer.Where(a => a.name_cus == sbSubMasAdvisorType1Name.Text).FirstOrDefault().contno;
            lstSubMasAdvisorType1Appno.IsVisible = false;
        }

        private void sbSubMasAdvisorType1Appno_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbSubMasAdvisorType1Appno.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = Customer.Where(a => a.contno.Contains(keyword)).ToList();
                lstSubMasAdvisorType1Appno.ItemsSource = suggestions.Select(a => a.contno);
                lstSubMasAdvisorType1Appno.IsVisible = true;
            }
        }

        private void lstSubMasAdvisorType1Appno_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbSubMasAdvisorType1Appno.Text = lstSubMasAdvisorType1Appno.SelectedItem.ToString();
            lstSubMasAdvisorType1Appno.IsVisible = false;
            sbSubMasAdvisorType1Name.Text = Customer.Where(a => a.contno == sbSubMasAdvisorType1Appno.Text).FirstOrDefault().name_cus;
            lstSubMasAdvisorType1Name.IsVisible = false;
        }

        private void sbSubMasAdvisorType3EmNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbSubMasAdvisorType3EmNo.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = Employee.Where(a => a.personnel_code.Contains(keyword)).ToList();
                lstSubMasAdvisorType3EmNo.ItemsSource = suggestions.Select(a => a.personnel_code);
                lstSubMasAdvisorType3EmNo.IsVisible = true;
            }
        }

        private void lstSubMasAdvisorType3EmNo_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbSubMasAdvisorType3EmNo.Text = lstSubMasAdvisorType3EmNo.SelectedItem.ToString();
            lstSubMasAdvisorType3EmNo.IsVisible = false;
            sbSubMasAdvisorType3Name.Text = Employee.Where(a => a.personnel_code == sbSubMasAdvisorType3EmNo.Text).FirstOrDefault().personnel_name;
            lstSubMasAdvisorType3Name.IsVisible = false;
        }

        private void sbSubMasAdvisorType3Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbSubMasAdvisorType3Name.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = Employee.Where(a => a.personnel_name.Contains(keyword)).ToList();
                lstSubMasAdvisorType3Name.ItemsSource = suggestions.Select(a => a.personnel_name);
                lstSubMasAdvisorType3Name.IsVisible = true;
            }
        }

        private void lstSubMasAdvisorType3Name_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbSubMasAdvisorType3Name.Text = lstSubMasAdvisorType3Name.SelectedItem.ToString();
            lstSubMasAdvisorType3Name.IsVisible = false;
            sbSubMasAdvisorType3EmNo.Text = Employee.Where(a => a.personnel_name == sbSubMasAdvisorType3Name.Text).FirstOrDefault().personnel_code;
            lstSubMasAdvisorType3EmNo.IsVisible = false;
        }

        [Obsolete]
        private void ibtnHelpMasCarStyle_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://drive.google.com/file/d/1BW0RmAIo-69XsCdCzl7RowXUqK5pi8px/view?usp=sharing"));
        }

        [Obsolete]
        private void ibtnHelpSubMasCarStyle_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://drive.google.com/file/d/1Fg7V6qCqdobvSyS6OQO7mmAKNtPGgQZS/view?usp=sharing"));
        }

        [Obsolete]
        private void ibtnHelpSubMasCarType_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://drive.google.com/file/d/1tWxpOEAdWe6nSy2IhbeO9dpjgAfdQ0Eu/view?usp=sharing"));
        }

        private void ibtnClearCampaign_Clicked(object sender, EventArgs e)
        {
            pkCamPaign.SelectedIndex = -1;
        }



        //private async Task btnMasPic22_ClickedAsync(object sender, EventArgs e)
        //{
        //    imgMasPic22.Text = await GetImage();
        //    if (imgMasPic22.Text == null) btnMasPic21.BackgroundColor = Color.FromHex("#F1C40F");
        //    else btnMasPic22.BackgroundColor = Color.LightGray;
        //}


        public async void LoadData()
        {
            Appz_WebService iapi = new Appz_WebService();
            await _connection.CreateTableAsync<DataAccessories>();

            var acc = await _connection.Table<DataAccessories>().ToListAsync();
            _accessories = new ObservableCollection<DataAccessories>(acc);
            if (_accessories.Count() == 0)
            {
                var dataAcces = iapi.GetDataAccessories();
                foreach (object data in dataAcces)
                {
                    await _connection.InsertAsync(data);
                    _accessories.Add((DataAccessories)data);
                }
            }


            await _connection.CreateTableAsync<DataCampaign>();
            var cam = await _connection.Table<DataCampaign>().ToListAsync();
            _campaign = new ObservableCollection<DataCampaign>(cam);
            if (_campaign.Count() == 0)
            {
                var dataCam = iapi.GetCampaign();
                foreach (object data in dataCam)
                {
                    await _connection.InsertAsync(data);
                    _campaign.Add((DataCampaign)data);
                }
            }


            await _connection.CreateTableAsync<DataCarPolicy>();
            var policy = await _connection.Table<DataCarPolicy>().ToListAsync();
            _policy = new ObservableCollection<DataCarPolicy>(policy);
            if (_policy.Count() == 0)
            {
                var dataPolicy = iapi.GetDataCarPolicy();
                foreach (object data in dataPolicy)
                {
                    await _connection.InsertAsync(data);
                    _policy.Add((DataCarPolicy)data);
                }
            }


            await _connection.CreateTableAsync<DataProvince>();
            var province = await _connection.Table<DataProvince>().ToListAsync();
            _province = new ObservableCollection<DataProvince>(province);
            if (_province.Count() == 0)
            {
                var dataProvince = iapi.GetProvince();
                foreach (object data in dataProvince)
                {
                    await _connection.InsertAsync(data);
                    _province.Add((DataProvince)data);
                }
            }



            await _connection.CreateTableAsync<DataUseCarType>();
            var usecartype = await _connection.Table<DataUseCarType>().ToListAsync();
            _usecartype = new ObservableCollection<DataUseCarType>(usecartype);
            if (_usecartype.Count() == 0)
            {
                var dataUsecartype = iapi.GetDataUseCarType();
                foreach (object data in dataUsecartype)
                {
                    await _connection.InsertAsync(data);
                    _usecartype.Add((DataUseCarType)data);
                }
            }


            await _connection.CreateTableAsync<DataBrand>();
            var brand = await _connection.Table<DataBrand>().ToListAsync();
            _brand = new ObservableCollection<DataBrand>(brand);
            if (_brand.Count() == 0)
            {
                var dataBrand = iapi.GetBrand();
                foreach (object data in dataBrand)
                {
                    await _connection.InsertAsync(data);
                    _brand.Add((DataBrand)data);
                }
            }


            await _connection.CreateTableAsync<DataCharacterCar>();
            var charac = await _connection.Table<DataCharacterCar>().ToListAsync();
            _charactercar = new ObservableCollection<DataCharacterCar>(charac);
            if (_charactercar.Count() == 0)
            {
                var dataCharac = iapi.GetDataCharacterCar();
                foreach (object data in dataCharac)
                {
                    await _connection.InsertAsync(data);
                    _charactercar.Add((DataCharacterCar)data);
                }
            }


            await _connection.CreateTableAsync<DataTypeCar>();
            var typecarr = await _connection.Table<DataTypeCar>().ToListAsync();
            _typecar = new ObservableCollection<DataTypeCar>(typecarr);
            if (_typecar.Count() == 0)
            {
                var dataTypecar = iapi.GetTypeCar();
                foreach (object data in dataTypecar)
                {
                    await _connection.InsertAsync(data);
                    _typecar.Add((DataTypeCar)data);
                }
            }


            await _connection.CreateTableAsync<DataStatus>();
            var statuss = await _connection.Table<DataStatus>().ToListAsync();
            _status = new ObservableCollection<DataStatus>(statuss);
            if (_status.Count() == 0)
            {
                var dataStatus = iapi.GetStatus();
                foreach (object data in dataStatus)
                {
                    await _connection.InsertAsync(data);

                    _status.Add((DataStatus)data);
                }
            }


            //await _connection.CreateTableAsync<DataGrantMapUser>();
            //var grantmapuser = await _connection.Table<DataGrantMapUser>().ToListAsync();
            //_gmapuser = new ObservableCollection<DataGrantMapUser>(grantmapuser);
            //if (_gmapuser.Count() == 0)
            //{
            //    var dataGrantUser = iapi.GetGrantMapUser();
            //    foreach (object data in dataGrantUser)
            //    {
            //        await _connection.InsertAsync(data);
            //        _gmapuser.Add((DataGrantMapUser)data);
            //    }

            //}

        }

        private void enMasEngno_TextChanged(object sender, TextChangedEventArgs e)
        {
            var isValid = Regex.IsMatch(e.NewTextValue, "^[a-zA-Z0-9]+$");
            //var isValid = Regex.IsMatch(e.NewTextValue, "^[a-zA-Z0-9ก-๏]+$");

            if (e.NewTextValue.Length > 0)
            {
                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }

        private void enMasChasno_TextChanged(object sender, TextChangedEventArgs e)
        {
            var isValid = Regex.IsMatch(e.NewTextValue, "^[a-zA-Z0-9]+$");

            if (e.NewTextValue.Length > 0)
            {
                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }

        private void enSChasno_TextChanged(object sender, TextChangedEventArgs e)
        {
            var isValid = Regex.IsMatch(e.NewTextValue, "^[a-zA-Z0-9]+$");
           

            if (e.NewTextValue.Length > 0)
            {
                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }

        private void enMasLicno_TextChanged(object sender, TextChangedEventArgs e)
        {
            var isValid = Regex.IsMatch(e.NewTextValue, "^[-0-9ก-๏]+$");

            if (e.NewTextValue.Length > 0)
            {
                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }

        private void enSubLicno_TextChanged(object sender, TextChangedEventArgs e)
        {
            var isValid = Regex.IsMatch(e.NewTextValue, "^[-0-9ก-๏]+$");

            if (e.NewTextValue.Length > 0)
            {
                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }
    }
}