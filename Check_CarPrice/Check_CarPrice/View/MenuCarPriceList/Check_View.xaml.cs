using Check_CarPrice.Model;
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
using static Check_CarPrice.Model.Check_Model;
using static Check_CarPrice.Model.Login_Model;

namespace Check_CarPrice.View.MenuCarPriceList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Check_View : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<DataAccessories> _accessories;
        private ObservableCollection<DataCampaign> _campaign;
        private ObservableCollection<DataCarPolicy> _policy;
        private ObservableCollection<DataProvince> _province;
        private ObservableCollection<DataUseCarType> _usecartype;
        private ObservableCollection<DataBrand> _brand;
        private ObservableCollection<Appz_Model.DataCharacterCar> _charactercar;
        private ObservableCollection<DataTypeCar> _typecar;
        private ObservableCollection<DataStatus> _status;

        private User _userlogin;
        public DataCarDetail _dta;
        public CarPriceList_Model.DataTransactionCar datatran;

        protected override bool OnBackButtonPressed() => true;
        Exception _error = null;
        



        public Check_View(User user,Model.CarPriceList_Model.DataTransactionCar dta)
        {
            Appz_WebService iAPI = new Appz_WebService();
            datatran = dta;
            _userlogin = user;
            _dta = GETCARDETAILALL_lIST().FirstOrDefault();
            //var data = iAPI.GetDataCarDetail();
            //if (data != null)
            //{
            //    _dta = data.Where(a => a.app_id == dta.app_id).FirstOrDefault();
            //}
           
            InitializeComponent();
            //SetData();
           
        }

        protected override async void OnAppearing()
        {
            if (_province == null)
            {
                _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

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
                var statuss = await _connection.Table<DataStatus>().ToListAsync();
                _status = new ObservableCollection<DataStatus>(statuss);
               
             

                SetData();
               
            }


            base.OnAppearing();
        }

        private void SetData()
        {
          
            Check_WebService iAPI = new Check_WebService();
            SetDataGeneral();
            var data = iAPI.PostDataTransactionCar(datatran.app_id).FirstOrDefault();
            if (data.type_grp_car == "01") SetMaster();
            else SetSubMaster();
            SetDataChecker();
        }


        private void SetDataGeneral()
        {
            Check_WebService iAPI = new Check_WebService();
            Appz_WebService aAPI = new Appz_WebService();
         
            //var data = iAPI.PostDataTransactionCar(_dta.app_id).FirstOrDefault();
            var data = GETCARDETAILALL_lIST().FirstOrDefault();
            lbCreateby.Text = data.personnel_code==""|| data.personnel_code==null?"-":data.personnel_code;
            lbEmName.Text = data.personnel_name == "" || data.personnel_name == null ? "-" : data.personnel_name;
            lbApplno.Text = data.applno_car == "" || data.applno_car == null ? "-" : data.applno_car;
            lbBranchName.Text = data.branch_nane == "" || data.branch_nane == null ? "-" : data.branch_nane;
            lbTypeCredit.Text = data.name_typecredit == "" || data.name_typecredit == null ? "-" : data.name_typecredit;
            //lbTypeCredit.Text = iAPI.PostDataTypeCredit().Where(a => a.prmcde.Trim() == data.type_credit).FirstOrDefault().USEDCARTYP;
            lbTypeDealer.Text = data.type_dealer == "" || data.type_dealer == null ? "-" : data.type_dealer == "1" ? "ดีลเลอร์เดียวกัน" : "คนละดีลเลอร์";
           ;

          

            if (_dta.type_grp_car == "01")           //ตัวแม่
            {
                if (_dta.type_dealer == "1")         //ดีลดลอร์เดียวกัน
                {
                    lbTypeContact.Text = _dta.type_contact == "1" ? "นายหน้า" : _dta.type_contact == "2" ? "คู่ค้าเต็นท์" : "บุคคลแนะนำ";
                    if (_dta.type_contact == "1")
                    {
                        con1.IsVisible = true;
                        con2.IsVisible = false;
                        con3.IsVisible = false;

                        lbNameDealerAgent1.Text = _dta.name_dealer_agent;
                        lbCodeDealerAgent1.Text = _dta.cde_dealer_agent;
                    }
                    else if (_dta.type_contact == "2")
                    {
                        con1.IsVisible = false;
                        con2.IsVisible = true;
                        con3.IsVisible = false;

                        lbNameDealerAgent2.Text = _dta.name_dealer_agent;
                        lbCodeDealerAgent2.Text = _dta.cde_dealer_agent;
                    }
                    else if (_dta.type_contact == "3")
                    {
                        con1.IsVisible = false;
                        con2.IsVisible = false;
                        con3.IsVisible = true;

                        lbTypePerson.Text = _dta.typ_person == "1" ? "ลูกค้าเก่า" : _dta.typ_person == "2" ? "ลูกค้าใหม่ติดต่อมาเอง" : "พนักงาน";

                        if (_dta.typ_person == "1")
                        {
                            lbLeft1.Text = "ชื่อ-นามสกุล";
                            lbRight1.Text = _dta.name_contno;
                            lbLeft2.Text = "เลขที่สัญญา";
                            lbRight2.Text = _dta.contno;
                        }
                        else if (_dta.typ_person == "2")
                        {
                            lbLeft1.Text = "ชื่อ-นามสกุล";
                            lbRight1.Text = _dta.name_cus;
                            lbLeft2.Text = "ช่องทางลูกค้าติดต่อ";
                            lbRight2.Text = _dta.type_cont_channel == "1" ? "Walk in" : _dta.type_cont_channel == "2" ? "Line" : _dta.type_cont_channel == "3" ? "โทรศัพท์" : "Facebook";
                        }
                        else if (_dta.typ_person == "3")
                        {
                            lbLeft1.Text = "รหัสพนักงาน";
                            lbRight1.Text = _dta.perosonal_cde;
                            lbLeft2.Text = "ชื่อ-นามสกุล";
                            lbRight2.Text = _dta.personal_name;
                        }
                    }
                }
                else if (_dta.type_dealer == "2")         //คนละดีลลเลอร์
                {
                    lbTypeContact.Text = _dta.type_contact == "1" ? "นายหน้า" : _dta.type_contact == "2" ? "คู่ค้าเต็นท์" : "บุคคลแนะนำ";
                    if (_dta.type_contact == "1")
                    {
                        con1.IsVisible = true;
                        con2.IsVisible = false;
                        con3.IsVisible = false;

                        lbNameDealerAgent1.Text = _dta.name_dealer_agent;
                        lbCodeDealerAgent1.Text = _dta.cde_dealer_agent;
                    }
                    else if (_dta.type_contact == "2")
                    {
                        con1.IsVisible = false;
                        con2.IsVisible = true;
                        con3.IsVisible = false;

                        lbNameDealerAgent2.Text = _dta.name_dealer_agent;
                        lbCodeDealerAgent2.Text = _dta.cde_dealer_agent;
                    }
                    else if (_dta.type_contact == "3")
                    {
                        con1.IsVisible = false;
                        con2.IsVisible = false;
                        con3.IsVisible = true;

                        lbTypePerson.Text = _dta.typ_person == "1" ? "ลูกค้าเก่า" : _dta.typ_person == "2" ? "ลูกค้าใหม่ติดต่อมาเอง" : "พนักงาน";

                        if (_dta.typ_person == "1")
                        {
                            lbLeft1.Text = "ชื่อ-นามสกุล";
                            lbRight1.Text = _dta.name_contno;
                            lbLeft2.Text = "เลขที่สัญญา";
                            lbRight2.Text = _dta.contno;
                        }
                        else if (_dta.typ_person == "2")
                        {
                            lbLeft1.Text = "ชื่อ-นามสกุล";
                            lbRight1.Text = _dta.name_cus;
                            lbLeft2.Text = "ช่องทางลูกค้าติดต่อ";
                            lbRight2.Text = _dta.type_cont_channel == "1" ? "Walk in" : _dta.type_cont_channel == "2" ? "Line" : _dta.type_cont_channel == "3" ? "โทรศัพท์" : "Facebook";
                        }
                        else if (_dta.typ_person == "3")
                        {
                            lbLeft1.Text = "รหัสพนักงาน";
                            lbRight1.Text = _dta.perosonal_cde;
                            lbLeft2.Text = "ชื่อ-นามสกุล";
                            lbRight2.Text = _dta.personal_name;
                        }
                    }

                    var dataD2 = aAPI.GetDataInformationTransactionCar().Where(a => a.applno_car == _dta.applno_car && a.app_id != _dta.app_id).FirstOrDefault();
                    if (dataD2 != null)
                    {
                        Dealer2.IsVisible = true;
                        lbTypeContactD2.Text = dataD2.type_contact == "1" ? "นายหน้า" : dataD2.type_contact == "2" ? "คู่ค้าเต็นท์" : "บุคคลแนะนำ";
                        if (dataD2.type_contact == "1")
                        {
                            con1D2.IsVisible = true;
                            con2D2.IsVisible = false;
                            con3D2.IsVisible = false;

                            lbNameDealerAgent1D2.Text = dataD2.name_dealer_agent;
                            lbCodeDealerAgent1D2.Text = dataD2.cde_dealer_agent;
                        }
                        else if (dataD2.type_contact == "2")
                        {
                            con1D2.IsVisible = false;
                            con2D2.IsVisible = true;
                            con3D2.IsVisible = false;

                            lbNameDealerAgent2D2.Text = dataD2.name_dealer_agent;
                            lbCodeDealerAgent2D2.Text = dataD2.cde_dealer_agent;

                        }
                        else if (dataD2.type_contact == "3")
                        {
                            con1D2.IsVisible = false;
                            con2D2.IsVisible = false;
                            con3D2.IsVisible = true;
                            lbTypePersonD2.Text = dataD2.typ_person == "1" ? "ลูกค้าเก่า" : dataD2.typ_person == "2" ? "ลูกค้าใหม่ติดต่อมาเอง" : "พนักงาน";
                            {
                                if (dataD2.typ_person == "1")
                                {

                                    lbLeft1D2.Text = "ชื่อ-นามสกุล";
                                    lbRight1D2.Text = dataD2.name_contno;
                                    lbLeft2D2.Text = "เลขที่สัญญา";
                                    lbRight2D2.Text = dataD2.contno;

                                }
                                else if (dataD2.typ_person == "2")
                                {

                                    lbLeft1D2.Text = "ชื่อ-นามสกุล";
                                    lbRight1D2.Text = dataD2.name_cus;
                                    lbLeft2D2.Text = "ช่องทางลูกค้าติดต่อ";
                                    lbRight2D2.Text = dataD2.type_cont_channel == "1" ? "Walk in" : dataD2.type_cont_channel == "2" ? "Line" : dataD2.type_cont_channel == "3" ? "โทรศัพท์" : "Facebook";


                                }
                                else if (dataD2.typ_person == "3")
                                {

                                    lbLeft1D2.Text = "รหัสพนักงาน";
                                    lbRight1D2.Text = dataD2.perosonal_cde;
                                    lbLeft2D2.Text = "ชื่อ-นามสกุล";
                                    lbRight2D2.Text = dataD2.personal_name;

                                }
                            }
                        }
                    }
                }

            }
            else if (_dta.type_grp_car == "02")      //ตัวลูก
            {
                var dataD2 = aAPI.GetDataInformationTransactionCar().Where(a => a.applno_car == _dta.applno_car && a.app_id != _dta.app_id).FirstOrDefault();

                if (_dta.type_dealer == "1")         //ดีลดลอร์เดียวกัน
                {
                    Dealer2.IsVisible = false;
                    lbTypeContact.Text = dataD2.type_contact == "1" ? "นายหน้า" : dataD2.type_contact == "2" ? "คู่ค้าเต็นท์" : "บุคคลแนะนำ";
                    if (dataD2.type_contact == "1")
                    {
                        con1.IsVisible = true;
                        con2.IsVisible = false;
                        con3.IsVisible = false;

                        lbNameDealerAgent1.Text = dataD2.name_dealer_agent;
                        lbCodeDealerAgent1.Text = dataD2.cde_dealer_agent;
                    }
                    else if (dataD2.type_contact == "2")
                    {
                        con1.IsVisible = false;
                        con2.IsVisible = true;
                        con3.IsVisible = false;

                        lbNameDealerAgent2.Text = dataD2.name_dealer_agent;
                        lbCodeDealerAgent2.Text = dataD2.cde_dealer_agent;

                    }
                    else if (dataD2.type_contact == "3")
                    {
                        con1.IsVisible = false;
                        con2.IsVisible = false;
                        con3.IsVisible = true;
                        lbTypePerson.Text = dataD2.typ_person == "1" ? "ลูกค้าเก่า" : dataD2.typ_person == "2" ? "ลูกค้าใหม่ติดต่อมาเอง" : "พนักงาน";
                        {
                            if (dataD2.typ_person == "1")
                            {

                                lbLeft1.Text = "ชื่อ-นามสกุล";
                                lbRight1.Text = dataD2.name_contno;
                                lbLeft2.Text = "เลขที่สัญญา";
                                lbRight2.Text = dataD2.contno;

                            }
                            else if (dataD2.typ_person == "2")
                            {

                                lbLeft1.Text = "ชื่อ-นามสกุล";
                                lbRight1.Text = dataD2.name_cus;
                                lbLeft2.Text = "ช่องทางลูกค้าติดต่อ";
                                lbRight2.Text = dataD2.type_cont_channel == "1" ? "Walk in" : dataD2.type_cont_channel == "2" ? "Line" : dataD2.type_cont_channel == "3" ? "โทรศัพท์" : "Facebook";


                            }
                            else if (dataD2.typ_person == "3")
                            {

                                lbLeft1.Text = "รหัสพนักงาน";
                                lbRight1.Text = dataD2.perosonal_cde;
                                lbLeft2.Text = "ชื่อ-นามสกุล";
                                lbRight2.Text = dataD2.personal_name;

                            }
                        }
                    }
                }
                else if (_dta.type_dealer == "2")         //คนละดีลลเลอร์
                {
                    Dealer2.IsVisible = true;
                    lbTypeContact.Text = dataD2.type_contact == "1" ? "นายหน้า" : dataD2.type_contact == "2" ? "คู่ค้าเต็นท์" : "บุคคลแนะนำ";
                    if (dataD2.type_contact == "1")
                    {
                        con1.IsVisible = true;
                        con2.IsVisible = false;
                        con3.IsVisible = false;

                        lbNameDealerAgent1.Text = dataD2.name_dealer_agent;
                        lbCodeDealerAgent1.Text = dataD2.cde_dealer_agent;
                    }
                    else if (dataD2.type_contact == "2")
                    {
                        con1.IsVisible = false;
                        con2.IsVisible = true;
                        con3.IsVisible = false;

                        lbNameDealerAgent2.Text = dataD2.name_dealer_agent;
                        lbCodeDealerAgent2.Text = dataD2.cde_dealer_agent;

                    }
                    else if (dataD2.type_contact == "3")
                    {
                        con1.IsVisible = false;
                        con2.IsVisible = false;
                        con3.IsVisible = true;
                        lbTypePerson.Text = dataD2.typ_person == "1" ? "ลูกค้าเก่า" : dataD2.typ_person == "2" ? "ลูกค้าใหม่ติดต่อมาเอง" : "พนักงาน";
                        {
                            if (dataD2.typ_person == "1")
                            {

                                lbLeft1.Text = "ชื่อ-นามสกุล";
                                lbRight1.Text = dataD2.name_contno;
                                lbLeft2.Text = "เลขที่สัญญา";
                                lbRight2.Text = dataD2.contno;

                            }
                            else if (dataD2.typ_person == "2")
                            {

                                lbLeft1.Text = "ชื่อ-นามสกุล";
                                lbRight1.Text = dataD2.name_cus;
                                lbLeft2.Text = "ช่องทางลูกค้าติดต่อ";
                                lbRight2.Text = dataD2.type_cont_channel == "1" ? "Walk in" : dataD2.type_cont_channel == "2" ? "Line" : dataD2.type_cont_channel == "3" ? "โทรศัพท์" : "Facebook";


                            }
                            else if (dataD2.typ_person == "3")
                            {

                                lbLeft1.Text = "รหัสพนักงาน";
                                lbRight1.Text = dataD2.perosonal_cde;
                                lbLeft2.Text = "ชื่อ-นามสกุล";
                                lbRight2.Text = dataD2.personal_name;

                            }
                        }
                    }

                    lbTypeContactD2.Text = _dta.type_contact == "1" ? "นายหน้า" : _dta.type_contact == "2" ? "คู่ค้าเต็นท์" : "บุคคลแนะนำ";
                    if (_dta.type_contact == "1")
                    {
                        con1D2.IsVisible = true;
                        con2D2.IsVisible = false;
                        con3D2.IsVisible = false;

                        lbNameDealerAgent1D2.Text = _dta.name_dealer_agent;
                        lbCodeDealerAgent1D2.Text = _dta.cde_dealer_agent;
                    }
                    else if (_dta.type_contact == "2")
                    {
                        con1D2.IsVisible = false;
                        con2D2.IsVisible = true;
                        con3D2.IsVisible = false;

                        lbNameDealerAgent2D2.Text = _dta.name_dealer_agent;
                        lbCodeDealerAgent2D2.Text = _dta.cde_dealer_agent;

                    }
                    else if (_dta.type_contact == "3")
                    {
                        con1D2.IsVisible = false;
                        con2D2.IsVisible = false;
                        con3D2.IsVisible = true;
                        lbTypePersonD2.Text = _dta.typ_person == "1" ? "ลูกค้าเก่า" : _dta.typ_person == "2" ? "ลูกค้าใหม่ติดต่อมาเอง" : "พนักงาน";
                        {
                            if (_dta.typ_person == "1")
                            {

                                lbLeft1D2.Text = "ชื่อ-นามสกุล";
                                lbRight1D2.Text = _dta.name_contno;
                                lbLeft2D2.Text = "เลขที่สัญญา";
                                lbRight2D2.Text = _dta.contno;

                            }
                            else if (_dta.typ_person == "2")
                            {

                                lbLeft1D2.Text = "ชื่อ-นามสกุล";
                                lbRight1D2.Text = _dta.name_cus;
                                lbLeft2D2.Text = "ช่องทางลูกค้าติดต่อ";
                                lbRight2D2.Text = _dta.type_cont_channel == "1" ? "Walk in" : _dta.type_cont_channel == "2" ? "Line" : _dta.type_cont_channel == "3" ? "โทรศัพท์" : "Facebook";


                            }
                            else if (_dta.typ_person == "3")
                            {

                                lbLeft1D2.Text = "รหัสพนักงาน";
                                lbRight1D2.Text = _dta.perosonal_cde;
                                lbLeft2D2.Text = "ชื่อ-นามสกุล";
                                lbRight2D2.Text = _dta.personal_name;

                            }
                        }
                    }

                }
            }

            lbRemark.Text = data.remark_transactioncar == "1" ? "สภาพรถไม่ตรงกับหน้าเล่ม" : data.remark_transactioncar == "2" ? "ดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม" : "รถสภาพปกติ";
            if (data.remark_transactioncar == "1")
            {
                RemarkDate.IsVisible = false;
                RemarkDes.IsVisible = true;
                lbRemarkDescription.Text = data.remark_description_transaction==""|| data.remark_description_transaction==null?"-":data.remark_description_transaction;
            }
            else if (data.remark_transactioncar == "2")
            {
                RemarkDate.IsVisible = true;
                RemarkDes.IsVisible = true;
                lbRemarkDescription.Text = data.remark_description_transaction==""|| data.remark_description_transaction==null?"-":data.remark_description_transaction;
                lbRemarkDate.Text = String.Format("{0:dd/MM/yyyy}", data.remark_date);
            }
            else
            {
                RemarkDate.IsVisible = false;
                RemarkDes.IsVisible = false;
            }

            lbCampaign.Text = data.campaign == "" || data.campaign ==null? "-" : data.campaign;
        }

        private void SetMaster()
        {
            Page1.IsVisible = true;
            Page2.IsVisible = true;
            Page3.IsVisible = false;

            NumberMaskBehavior iModel = new NumberMaskBehavior();
            //Check_WebService iAPI = new Check_WebService();
           
            if (_dta != null)
            {
                lbMlicno.Text = _dta.licno==""|| _dta.licno==null?"-":_dta.licno;
                lbMProvince.Text = _dta.provice == "" || _dta.provice == null ? "-" : _dta.provice;
                lbMEngno.Text = _dta.engno == "" || _dta.engno == null ? "-" : _dta.engno;
                lbMChasno.Text = _dta.chasno == "" || _dta.chasno == null ? "-" : _dta.chasno;
                lbMBrand.Text = _dta.brand_name == "" || _dta.brand_name == null ? "-" : _dta.brand_name;
                lbMFuel.Text = _dta.fuel == "01" ? "ดีเซล" : "ก๊าซ CNG";
                lbMTypeCar.Text = _dta.type_car == "" || _dta.type_car == null ? "-" : _typecar.Where(a => a.cde_typ_car == _dta.type_car).FirstOrDefault().name_typ_car;

                lbMGrpCar.Text = _charactercar.Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_grpcar == _dta.grp_car && a.cde_style_car == _dta.cde_pickup).FirstOrDefault().name_grpcar;
                lbMPickup.Text = _charactercar.Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_grpcar == _dta.grp_car && a.cde_style_car == _dta.cde_pickup).FirstOrDefault().name_style_car;
                if(_dta.cde_special!="") lbMSpecial.Text =  _charactercar.Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_grpcar == _dta.grp_car && a.cde_style_car == _dta.cde_special).FirstOrDefault().name_style_car;
                else lbMSpecial.Text = "-";
                //lbMGrpCar.Text = _dtaStyle==null?"": _dtaStyle.namegrp_car;
                //lbMPickup.Text = _dtaStyle == null ? "" : _dtaStyle.name_pickup;
                //lbMSpecial.Text = _dtaStyle == null ? "" : _dtaStyle.name_special;
                lbMHeadPrice.Text = _dta.head_price == "" || _dta.head_price == null ? "-" : iModel.FormatData(_dta.head_price) + " บาท";
                lbMAccessory.Text = _dta.accessory==""|| _dta.accessory ==null? "-":_accessories.Where(a => a.cde_accessory == _dta.accessory).FirstOrDefault().accessory;
                lbMYearCar.Text = _dta.year_car==""|| _dta.year_car==null?"-":"พ.ศ. " + _dta.year_car;
            }
           


        }

        private void SetSubMaster()
        {
            Page1.IsVisible = true;
            Page2.IsVisible = false;
            Page3.IsVisible = true;

            NumberMaskBehavior iModel = new NumberMaskBehavior();
           
            if (_dta != null)
            {
                lbSlicno.Text = _dta.licno == "" || _dta.licno == null ? "-" : _dta.licno;
                lbSProvince.Text = _dta.provice == "" || _dta.provice == null ? "-" : _dta.provice;
                lbSEngno.Text = _dta.chasno == "" || _dta.chasno == null ? "-" : _dta.chasno;
                lbSType.Text = _dta.name_typ_car == "" || _dta.name_typ_car == null ? "-" : _dta.name_typ_car;
                lbSGrpCar.Text = _charactercar.Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_type_style == _dta.grp_car && a.cde_style_car == _dta.cde_pickup).FirstOrDefault().name_type_style;
                lbSPickup.Text = _charactercar.Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_type_style == _dta.grp_car && a.cde_style_car == _dta.cde_pickup).FirstOrDefault().name_style_car;
                if (_dta.cde_special != "") lbSSpecial.Text = _charactercar.Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_type_style == _dta.grp_car && a.cde_style_car == _dta.cde_special).FirstOrDefault().name_style_car;
                else lbSSpecial.Text = "-";
                lbSHeadPrice.Text = _dta.head_price == "" || _dta.head_price == null ? "-" : iModel.FormatData(_dta.head_price) + " บาท";
                lbSYearCar.Text = _dta.year_car == "" || _dta.year_car == null ? "-" : "พ.ศ. " + _dta.year_car;
            }
        }

        private void SetDataChecker()
        {
            NumberMaskBehavior iModel = new NumberMaskBehavior();
            Appz_WebService iAPI = new Appz_WebService();
         
            if (_dta != null)
            {
               
                pkMStatus.ItemsSource = _status.Where(a => a.status == 1).ToList();
                pkPolicy.ItemsSource = _policy.OrderBy(a=>a.Rid).ToList();
                enModel.Text = _dta.model == "" ? "" : _dta.model;
                pkTypeCar.ItemsSource = _typecar;
                pkTypeCar.ItemsSource = _typecar;
                pkTypeCar.SelectedIndex = _dta.type_car==""?-1:_typecar.Select(a => a.cde_typ_car).ToList().IndexOf(_typecar.Where(a => a.cde_typ_car == _dta.type_car).FirstOrDefault().cde_typ_car);
                enYearCar.Text = _dta.year_car == "" ? "" : _dta.year_car;
                enStatus.Text = _dta.namestatus == "" ? "" : _dta.namestatus;
                enCdeCar.Text = _dta.cde_car == "" ? "" : _dta.cde_car;
                enRemark.Text = _dta.remark_cdecar == "" ? "" : _dta.remark_cdecar;
                lbFinamt.Text = _dta.type_grp_car == "01" ? "ยอดจัดตัวแม่" : "ยอดจัดตัวลูก";
                enFinamt.Text = _dta.finamt == "" ? "" : iModel.FormatData(_dta.finamt);
                enMiddlePrice.Text = _dta.middle_price == "" ? "" : iModel.FormatData(_dta.middle_price);
                enDistance01.Text = _dta.Distance_01 == "" ? "" : _dta.Distance_01;
                enRate01.Text = _dta.rate_01 == "" ? "" : _dta.rate_01;
                enDistance02.Text = _dta.Distance_02 == "" ? "" : _dta.Distance_02;
                enRate02.Text = _dta.rate_02 == "" ? "" : _dta.rate_02;

            }

        }

        private DataInformationCodeCar GetDataChecker()
        {
          
            DataInformationCodeCar data = new DataInformationCodeCar();
            Check_WebService iAPI = new Check_WebService();
            var datainfor = iAPI.GetDataInformationCar().Where(a => a.app_id == _dta.app_id && a.status == 1).FirstOrDefault();

            data.app_id = _dta.app_id;
            data.status_appcar = ((DataStatus)pkMStatus.SelectedItem).cde_status_appcar;
            data.create_by = _userlogin.data.Select(a=>a.personnel_code).FirstOrDefault();
            data.year_car = _dta.year_car == enYearCar.Text.Trim() ? enYearCar.Text.Trim() : _dta.year_car;
            data.type_car = _dta.type_car == ((DataTypeCar)pkTypeCar.SelectedItem).cde_typ_car ? _dta.type_car : ((DataTypeCar)pkTypeCar.SelectedItem).cde_typ_car; ;
            if (pkMStatus.Items[pkMStatus.SelectedIndex] == "ไม่ผ่านหลักเกณฑ์" || pkMStatus.Items[pkMStatus.SelectedIndex] == "รายละเอียดไม่ถูกต้อง")
            {
                var dta = iAPI.GetDataInformationCodeCar();
                if (dta != null)
                {
                    if (dta.Where(a => a.app_id == _dta.app_id && a.status == 1).Count() != 0)
                    {
                        var datacodecar = dta.Where(a => a.app_id == _dta.app_id && a.status == 1).FirstOrDefault();
                        data.model = datainfor.model.ToString();
                        data.type_car = datainfor.type_car;
                        data.year_car = datainfor.year_car;
                        data.remark = enRemark.Text;
                        data.finamt = datacodecar.finamt;
                        data.middle_price = datacodecar.middle_price;
                        data.Distance_01 = datacodecar.Distance_01;
                        data.rate_01 = datacodecar.rate_01;
                        data.Distance_02 = datacodecar.Distance_02;
                        data.rate_02 = datacodecar.rate_02;
                        data.cde_policy = datacodecar.cde_policy=="" ? "" : datacodecar.cde_policy;
                        data.cde_car = datacodecar.cde_car=="" ? "" : datacodecar.cde_car;
                       
                    }
                    else
                    {
                        data.remark = enRemark.Text;
                        //data.finamt = float.Parse(enFinamt.Text.Trim());
                        //data.middle_price = float.Parse(enMiddlePrice.Text.Trim());
                        //data.Distance_01 = enDistance01.Text.Trim();
                        //data.rate_01 = float.Parse(enRate01.Text.Trim());
                        //data.Distance_02 = enDistance02.Text.Trim();
                        //data.rate_02 = float.Parse(enRate02.Text.Trim());
                    }
                }
                else
                {
                    data.remark = enRemark.Text;
                }
            }
            else
            {
                data.cde_policy = pkPolicy.SelectedIndex==-1?"":((DataCarPolicy)pkPolicy.SelectedItem).cde_policy;
                data.model = enModel.Text.Trim();
                data.type_car = pkTypeCar.SelectedIndex == -1 ? "" : ((DataTypeCar)pkTypeCar.SelectedItem).cde_typ_car;
                data.year_car = enYearCar.Text.Trim();
                data.cde_car = enCdeCar.Text.Trim();
                data.remark = enRemark.Text;
                data.finamt = float.Parse(enFinamt.Text.Trim());
                data.middle_price = float.Parse(enMiddlePrice.Text.Trim());
                data.Distance_01 = enDistance01.Text.Trim();
                data.rate_01 = float.Parse(enRate01.Text.Trim());
                data.Distance_02 = enDistance02.Text.Trim();
                data.rate_02 = float.Parse(enRate02.Text.Trim());
            }

            return data;
        }


        private string Validation()
        {
            string ret = "";
         
            if (pkMStatus.SelectedIndex == -1)
            {
                if (pkMStatus.SelectedIndex == -1) ret = "กรุณาเลือกการพิจารณา";
                //else if (enModel.Text.Trim() == "") ret = "กรุณากรอกรุ่นรถ";
                else if (pkTypeCar.SelectedIndex == -1) ret = "กรุณาเลือกประเภทรถ";
                else if (enYearCar.Text.Trim() == "") ret = "กรุณากรอกปีจดทะบียน";
                else if (enCdeCar.Text.Trim() == "") ret = "กรุณากรอกรหัสรถ";
                else if (enFinamt.Text.Trim() == "") ret = "กรุณากรอกยอดจัด";
                else if (enMiddlePrice.Text.Trim() == "") ret = "กรุณากรอกราคากลาง";
                else if (enDistance01.Text.Trim() == "") ret = "กรุณากรอกระยะเวลารายการผ่อนชำระที่ 1";
                else if (enRate01.Text.Trim() == "") ret = "กรุณากรอกดอกเบี้ยรายการผ่อนชำระที่ 1";
                else if (enDistance02.Text.Trim() == "") ret = "กรุณากรอกระยะเวลารายการผ่อนชำระที่ 2";
                else if (enRate02.Text.Trim() == "") ret = "กรุณากรอกดอกเบี้ยรายการผ่อนชำระที่ 2";
            }
            else if (pkMStatus.Items[pkMStatus.SelectedIndex] == "ไม่ผ่านหลักเกณฑ์" || pkMStatus.Items[pkMStatus.SelectedIndex] == "รายละเอียดไม่ถูกต้อง")
            {
                ret = "";
           
            }
            else
            {
                if (pkMStatus.SelectedIndex == -1) ret = "กรุณาเลือกการพิจารณา";
                else if (pkTypeCar.SelectedIndex == -1) ret = "กรุณาเลือกประเภทรถ";
                else if (enYearCar.Text.Trim() == "") ret = "กรุณากรอกปีจดทะบียน";
                else if (enCdeCar.Text.Trim() == "") ret = "กรุณากรอกรหัสรถ";
                else if (enFinamt.Text.Trim() == "") ret = "กรุณากรอกยอดจัด";
                else if (enMiddlePrice.Text.Trim() == "") ret = "กรุณากรอกราคากลาง";
                else if (enDistance01.Text.Trim() == "") ret = "กรุณากรอกระยะเวลารายการผ่อนชำระที่ 1";
                else if (enRate01.Text.Trim() == "") ret = "กรุณากรอกดอกเบี้ยรายการผ่อนชำระที่ 1";
                else if (enDistance02.Text.Trim() == "") ret = "กรุณากรอกระยะเวลารายการผ่อนชำระที่ 2";
                else if (enRate02.Text.Trim() == "") ret = "กรุณากรอกดอกเบี้ยรายการผ่อนชำระที่ 2";
            }
            return ret;
        }

        private bool Check()
        {
            bool ret = true;
            try
            {
                Check_WebService iAPI = new Check_WebService();

              
                var data = GetDataChecker();
                if (!iAPI.PostDataInformationCodeCar(data))
                {
                    return false;
                }
                else return true;

            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex;
            }
            return ret;
        }

        private async void btnCheck_Clicked(object sender, EventArgs e)
        {
            PopupLoad.IsVisible = true;
            await Task.Delay(2000);
            string error = Validation();
            if (error != "")
            {
               await DisplayAlert("หน้าตรวจสอบ", error, "ตกลง");
            }
            else
            {
                if (Check() == true)
                { 
                    await Navigation.PushAsync(new MainPage(_userlogin));
                    await DisplayAlert("หน้าตรวจสอบ", "ตรวจสอบข้อมูลเรียบร้อย", "ตกลง");
                   
                }
                else
                {
                    await DisplayAlert("หน้าตรวจสอบ", "ไม่สามารถตรวจสอบข้อมูลได้", "ตกลง");
                }
            }
            PopupLoad.IsVisible = false;
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuCarPriceList_View(_userlogin, datatran));
        }

        private void pkMStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pkMStatus.Items[pkMStatus.SelectedIndex] == "ไม่ผ่านหลักเกณฑ์" || pkMStatus.Items[pkMStatus.SelectedIndex] == "รายละเอียดไม่ถูกต้อง")
            {
                stStatus.IsVisible = false;
                stFinamt.IsVisible = false;
            }
            else
            {
                stStatus.IsVisible = true;
                stFinamt.IsVisible = true;
            }

        }

        private void ibtnClearPolicy_Clicked(object sender, EventArgs e)
        {
            pkPolicy.SelectedIndex = -1;
        }


        public List<DataCarDetail> GETCARDETAILALL_lIST()
        {
            Appz_WebService iAPI = new Appz_WebService();
            var response = iAPI.Sent(GETCARDETAIL());
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
            mMo.c1 = datatran.app_id;
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