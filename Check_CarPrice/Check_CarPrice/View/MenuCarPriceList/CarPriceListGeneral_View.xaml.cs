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
    public partial class CarPriceListGeneral_View : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private User _userlogin;
        //private ObservableCollection<DataGrantMapUser> _gmapuser;

        protected override bool OnBackButtonPressed() => true;
        //public LoginData _userlogin;
        public DataTransactionCar _dta;
        public DataCarDetail _data;
        public CarPriceListGeneral_View(User user, DataTransactionCar dta)
        {
            Appz_WebService iAPI = new Appz_WebService();
            _userlogin = user;
            _dta = dta;
            //_data = iAPI.GetDataCarDetail().Where(a => a.app_id == dta.app_id).FirstOrDefault();

            InitializeComponent();
            SetData();

        }


        private void SetData()
        {
            Appz_WebService iAPI = new Appz_WebService();
            SentToAPI mMo = new SentToAPI();
            mMo.personnel_code = _userlogin.data.First().personnel_code;
            mMo.storename = "ST_CARDETAILALL_GET";
            mMo.c1 = _dta.app_id;
            var data = GETCARDETAILALL_lIST(mMo);

            SentToAPI mMo_1 = new SentToAPI();
            mMo_1.personnel_code = _userlogin.data.First().personnel_code;
            mMo_1.storename = "ST_CARDETAILALL_GET";
            mMo_1.c1 = _dta.app_id;
            mMo_1.c2 = _dta.applno_car;
            var datatwo = GETCARDETAILALL_lIST(mMo_1);

            if (data != null)
            {
                var dta = data.FirstOrDefault();

                lbLicno.Text = dta.licno;
                lbCreateDate.Text = dta.createdate_transaction.ToString("dd/MM/yyyy");
                //lbCreateDate.Text = String.Format("{0:dd/MM/yyyy}", dta.createdate_transaction);
                lbPersonalCode.Text = dta.personnel_code;
                lbPersonalName.Text = dta.personnel_name;
                lbAppno.Text = dta.applno_car;
                lbBranch.Text = dta.branch_nane;
                lbTypeCredit.Text = dta.name_typecredit;
                lbTypeDealer.Text = dta.type_dealer == "1" ? "ดีลเลอร์เดียวกัน" : "คนละดีลเลอร์";

                if (dta.type_grp_car == "01")           //ตัวแม่
                {
                    if (dta.type_dealer == "1")         //ดีลดลอร์เดียวกัน
                    {
                        lbTypeContact.Text = dta.type_contact == "1" ? "นายหน้า" : dta.type_contact == "2" ? "คู่ค้าเต็นท์" : "บุคคลแนะนำ";
                        if (dta.type_contact == "1")
                        {
                            con1.IsVisible = true;
                            con2.IsVisible = false;
                            con3.IsVisible = false;

                            lbNameDealerAgent1.Text = dta.name_dealer_agent;
                            lbCodeDealerAgent1.Text = dta.cde_dealer_agent;
                        }
                        else if (dta.type_contact == "2")
                        {
                            con1.IsVisible = false;
                            con2.IsVisible = true;
                            con3.IsVisible = false;

                            lbNameDealerAgent2.Text = dta.name_dealer_agent;
                            lbCodeDealerAgent2.Text = dta.cde_dealer_agent;
                        }
                        else if (dta.type_contact == "3")
                        {
                            con1.IsVisible = false;
                            con2.IsVisible = false;
                            con3.IsVisible = true;

                            lbTypePerson.Text = dta.typ_person == "1" ? "ลูกค้าเก่า" : dta.typ_person == "2" ? "ลูกค้าใหม่ติดต่อมาเอง" : "พนักงาน";

                            if (dta.typ_person == "1")
                            {
                                lbLeft1.Text = "ชื่อ-นามสกุล";
                                lbRight1.Text = dta.name_contno;
                                lbLeft2.Text = "เลขที่สัญญา";
                                lbRight2.Text = dta.contno;
                            }
                            else if (dta.typ_person == "2")
                            {
                                lbLeft1.Text = "ชื่อ-นามสกุล";
                                lbRight1.Text = dta.name_cus;
                                lbLeft2.Text = "ช่องทางลูกค้าติดต่อ";
                                lbRight2.Text = dta.type_cont_channel == "1" ? "Walk in" : dta.type_cont_channel == "2" ? "Line" : dta.type_cont_channel == "3" ? "โทรศัพท์" : "Facebook";
                            }
                            else if (dta.typ_person == "3")
                            {
                                lbLeft1.Text = "รหัสพนักงาน";
                                lbRight1.Text = dta.perosonal_cde;
                                lbLeft2.Text = "ชื่อ-นามสกุล";
                                lbRight2.Text = dta.personal_name;
                            }
                        }
                    }
                    else if (dta.type_dealer == "2")         //คนละดีลลเลอร์
                    {
                        lbTypeContact.Text = dta.type_contact == "1" ? "นายหน้า" : dta.type_contact == "2" ? "คู่ค้าเต็นท์" : "บุคคลแนะนำ";
                        if (dta.type_contact == "1")
                        {
                            con1.IsVisible = true;
                            con2.IsVisible = false;
                            con3.IsVisible = false;

                            lbNameDealerAgent1.Text = dta.name_dealer_agent;
                            lbCodeDealerAgent1.Text = dta.cde_dealer_agent;
                        }
                        else if (dta.type_contact == "2")
                        {
                            con1.IsVisible = false;
                            con2.IsVisible = true;
                            con3.IsVisible = false;

                            lbNameDealerAgent2.Text = dta.name_dealer_agent;
                            lbCodeDealerAgent2.Text = dta.cde_dealer_agent;
                        }
                        else if (dta.type_contact == "3")
                        {
                            con1.IsVisible = false;
                            con2.IsVisible = false;
                            con3.IsVisible = true;

                            lbTypePerson.Text = dta.typ_person == "1" ? "ลูกค้าเก่า" : dta.typ_person == "2" ? "ลูกค้าใหม่ติดต่อมาเอง" : "พนักงาน";

                            if (dta.typ_person == "1")
                            {
                                lbLeft1.Text = "ชื่อ-นามสกุล";
                                lbRight1.Text = dta.name_contno;
                                lbLeft2.Text = "เลขที่สัญญา";
                                lbRight2.Text = dta.contno;
                            }
                            else if (dta.typ_person == "2")
                            {
                                lbLeft1.Text = "ชื่อ-นามสกุล";
                                lbRight1.Text = dta.name_cus;
                                lbLeft2.Text = "ช่องทางลูกค้าติดต่อ";
                                lbRight2.Text = dta.type_cont_channel == "1" ? "Walk in" : dta.type_cont_channel == "2" ? "Line" : dta.type_cont_channel == "3" ? "โทรศัพท์" : "Facebook";
                            }
                            else if (dta.typ_person == "3")
                            {
                                lbLeft1.Text = "รหัสพนักงาน";
                                lbRight1.Text = dta.perosonal_cde;
                                lbLeft2.Text = "ชื่อ-นามสกุล";
                                lbRight2.Text = dta.personal_name;
                            }
                        }

                        //var dataD2 = iAPI.GetDataInformationTransactionCar().Where(a => a.applno_car == _dta.applno_car && a.app_id != _dta.app_id).FirstOrDefault();
                        if (datatwo != null)
                        {
                            var dataD2 = datatwo.FirstOrDefault();
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
                else if (dta.type_grp_car == "02")      //ตัวลูก
                {
                    //SentToAPI mMo_1 = new SentToAPI();
                    //mMo_1.personnel_code = _userlogin.data.First().personnel_code;
                    //mMo_1.storename = "ST_CARDETAILALL_GET";
                    //mMo_1.c1 = _dta.app_id;
                    //mMo_1.c2 = _dta.applno_car;
                    //var datatwo = GETCARDETAILALL_lIST(mMo_1);
                    //var dataD2 = iAPI.GetDataInformationTransactionCar().Where(a => a.applno_car == _dta.applno_car && a.app_id != _dta.app_id).FirstOrDefault();
                    if (datatwo != null)
                    {
                        var dataD2 = datatwo.FirstOrDefault();
                        if (dta.type_dealer == "1")         //ดีลดลอร์เดียวกัน
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
                        else if (dta.type_dealer == "2")         //คนละดีลลเลอร์
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

                            lbTypeContactD2.Text = dta.type_contact == "1" ? "นายหน้า" : dta.type_contact == "2" ? "คู่ค้าเต็นท์" : "บุคคลแนะนำ";
                            if (dta.type_contact == "1")
                            {
                                con1D2.IsVisible = true;
                                con2D2.IsVisible = false;
                                con3D2.IsVisible = false;

                                lbNameDealerAgent1D2.Text = dta.name_dealer_agent;
                                lbCodeDealerAgent1D2.Text = dta.cde_dealer_agent;
                            }
                            else if (dta.type_contact == "2")
                            {
                                con1D2.IsVisible = false;
                                con2D2.IsVisible = true;
                                con3D2.IsVisible = false;

                                lbNameDealerAgent2D2.Text = dta.name_dealer_agent;
                                lbCodeDealerAgent2D2.Text = dta.cde_dealer_agent;

                            }
                            else if (dta.type_contact == "3")
                            {
                                con1D2.IsVisible = false;
                                con2D2.IsVisible = false;
                                con3D2.IsVisible = true;
                                lbTypePersonD2.Text = dta.typ_person == "1" ? "ลูกค้าเก่า" : dta.typ_person == "2" ? "ลูกค้าใหม่ติดต่อมาเอง" : "พนักงาน";
                                {
                                    if (dta.typ_person == "1")
                                    {

                                        lbLeft1D2.Text = "ชื่อ-นามสกุล";
                                        lbRight1D2.Text = dta.name_contno;
                                        lbLeft2D2.Text = "เลขที่สัญญา";
                                        lbRight2D2.Text = dta.contno;

                                    }
                                    else if (dta.typ_person == "2")
                                    {

                                        lbLeft1D2.Text = "ชื่อ-นามสกุล";
                                        lbRight1D2.Text = dta.name_cus;
                                        lbLeft2D2.Text = "ช่องทางลูกค้าติดต่อ";
                                        lbRight2D2.Text = dta.type_cont_channel == "1" ? "Walk in" : dta.type_cont_channel == "2" ? "Line" : dta.type_cont_channel == "3" ? "โทรศัพท์" : "Facebook";


                                    }
                                    else if (dta.typ_person == "3")
                                    {

                                        lbLeft1D2.Text = "รหัสพนักงาน";
                                        lbRight1D2.Text = dta.perosonal_cde;
                                        lbLeft2D2.Text = "ชื่อ-นามสกุล";
                                        lbRight2D2.Text = dta.personal_name;

                                    }
                                }
                            }

                        }
                    }

                }

                lbRemark.Text = dta.remark_transactioncar == "1" ? "สภาพรถไม่ตรงกับหน้าเล่ม" : dta.remark_transactioncar == "2" ? "ดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม" : "รถสภาพปกติ";
                if (dta.remark_transactioncar == "1")
                {
                    RemarkDate.IsVisible = false;
                    RemarkDes.IsVisible = true;
                    lbRemarkDescription.Text = dta.remark_description_transaction;
                }
                else if (dta.remark_transactioncar == "2")
                {
                    RemarkDate.IsVisible = true;
                    RemarkDes.IsVisible = true;
                    lbRemarkDescription.Text = dta.remark_description_transaction;
                    lbRemarkDate.Text = String.Format("{0:dd/MM/yyyy}", dta.remark_date);
                }
                else
                {
                    RemarkDate.IsVisible = false;
                    RemarkDes.IsVisible = false;
                }

                lbCampaign.Text = dta.campaign == "" ? "-" : dta.campaign;

            }
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuCarPriceList_View(_userlogin, _dta));
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