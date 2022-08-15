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
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.CarPriceList_Model;
using static Check_CarPrice.Model.Login_Model;
using static Check_CarPrice.Model.Update_Model;
using DataProvince = Check_CarPrice.Model.Appz_Model.DataProvince;

namespace Check_CarPrice.View.MenuCarPriceList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarPriceUpdate_View : ContentPage
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
      
        public string _appno;
        public List<DataDealerAgent> DealerAgent;
        public List<DataCustomer> Customer;
        public List<DataEmployee> Employee;
        public DataCarDetail _dta;
        public DataTransactionCar datatran;
        Exception _error = null;

        public CarPriceUpdate_View(User user, DataTransactionCar dta)
        {
            Appz_WebService iAPI = new Appz_WebService();
            InitializeComponent();
            _userlogin = user;
            datatran = dta;

            SentToAPI mMo = new SentToAPI();
            mMo.personnel_code = _userlogin.data.First().personnel_code;
            mMo.storename = "ST_CARDETAILALL_GET";
            mMo.c1 = datatran.app_id;

            var data = GETCARDETAILALL_lIST(mMo);
            if (data != null)
            {
                _dta = data.FirstOrDefault();
            }
           
            SetMainPage();
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

               
                SetAllData();
                SetData();
               
            }


            base.OnAppearing();
        }

        private void SetAllData()
        {
            Appz_WebService iAPI = new Appz_WebService();
            InsertCarPrice_WebService aAPI = new InsertCarPrice_WebService();
            pkMasProvince.ItemsSource = _province;
            pkSubProvince.ItemsSource = _province;
            pkMasCarType.ItemsSource = _typecar.Where(a=>a.type_grp_car=="01").ToList();
            pkSubCarType.ItemsSource = _typecar.Where(a => a.type_grp_car == "02").ToList();
            pkMasBrand.ItemsSource = _brand;
            pkCamPaign.ItemsSource = _campaign;
            pkMasAccessories.ItemsSource = _accessories.Where(a=>a.status=="active").ToList();
            pkLoanType.ItemsSource = _usecartype;
            DealerAgent = iAPI.GetDataDealerAgent();
            Customer = iAPI.GetDataCustomer();
            Employee = iAPI.GetDataEmployee();
            pkMasStyle1.ItemsSource = aAPI.GetDataVStyle1().Where(a => a.cde_head_tail == _dta.type_grp_car).ToList();
            pkMasStyle2.ItemsSource = aAPI.GetDataVStyle3().Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_style == "01" && a.cde_grpcar == _dta.grp_car).ToList();
            pkSubStyle1.ItemsSource = iAPI.GetDataV_StyleSubMaster1().ToList();
            pkSubStyle2.ItemsSource = iAPI.GetDataV_StyleSubMaster2().ToList();




        }

        private void SetData()
        {
            DisplayAlert("กรุณาแก้ไข", _dta.remark_cdecar, "ตกลง");
            InsertCarPrice_WebService iAPI = new InsertCarPrice_WebService();
            Appz_WebService aAPI = new Appz_WebService();

            SentToAPI mMo_1 = new SentToAPI();
            mMo_1.personnel_code = _userlogin.data.First().personnel_code;
            mMo_1.storename = "ST_CARDETAILALL_GET";
            mMo_1.c1 = _dta.app_id;
            mMo_1.c2 = _dta.applno_car;
            var datatwo = GETCARDETAILALL_lIST(mMo_1);


            enPersonal_code.Text = _dta.personnel_code;
            enPersonal_Name.Text = _dta.personnel_name;
            enBranchName.Text = _dta.branch_nane;
            pkLoanType.SelectedIndex = _usecartype.Select(a => a.prmcde).ToList().IndexOf(_usecartype.Where(a => a.prmcde.Trim() == _dta.type_credit).FirstOrDefault().prmcde);
            pkDealerType.SelectedIndex = _dta.type_dealer == "1" ? 0 : 1;
            if (_dta.type_grp_car == "01")
            {
                if (_dta.type_dealer == "1")
                {
                    frmMaster.IsVisible = true;
                    frmSubMaster.IsVisible = false;
                    pkConnectionMaster.SelectedIndex = _dta.type_contact == "1" ? 0 : _dta.type_contact == "2" ? 1 : 2;
                    if (pkConnectionMaster.SelectedIndex == 0)
                    {
                        sbConnectionMaster1Name.Text = _dta.name_dealer_agent;
                        sbConnectionMaster1Code.Text = _dta.cde_dealer_agent;
                        lstConnectionMaster1Name.IsVisible = false;
                        lstConnectionMaster1Code.IsVisible = false;
                    }
                    else if (pkConnectionMaster.SelectedIndex == 1)
                    {
                        sbConnectionMaster2Name.Text = _dta.name_dealer_agent;
                        sbConnectionMaster2Code.Text = _dta.cde_dealer_agent;

                        lstConnectionMaster2Name.IsVisible = false;
                        lstConnectionMaster2Code.IsVisible = false;
                    }
                    else if (pkConnectionMaster.SelectedIndex == 2)
                    {
                        if (_dta.typ_person != "")
                        {
                            pkMasAdvisorType.SelectedIndex = _dta.typ_person == "1" ? 0 : _dta.typ_person == "2" ? 1 : 2;
                            if (pkMasAdvisorType.SelectedIndex == 0)
                            {
                                sbMasAdvisorType1Name.Text = _dta.name_contno;
                                sbMasAdvisorType1Appno.Text = _dta.contno;

                                lstMasAdvisorType1Name.IsVisible = false;
                                lstMasAdvisorType1Appno.IsVisible = false;
                            }
                            else if (pkMasAdvisorType.SelectedIndex == 1)
                            {
                                enMasAdvisorType2Name.Text = _dta.name_cus;
                                pkSocialtype.SelectedIndex = _dta.type_cont_channel == "1" ? 0 : _dta.type_cont_channel == "2" ? 1 : _dta.type_cont_channel == "3" ? 2 : 3;
                            }
                            else if (pkMasAdvisorType.SelectedIndex == 2)
                            {
                                sbMasAdvisorType3EmNo.Text = _dta.perosonal_cde;
                                sbMasAdvisorType3Name.Text = _dta.personal_name;

                                lstMasAdvisorType3EmNo.IsVisible = false;
                                lstMasAdvisorType3Name.IsVisible = false;
                            }
                        }
                    }
                }
                else if (_dta.type_dealer == "2")
                {
                    frmMaster.IsVisible = true;
                    frmSubMaster.IsVisible = true;
                    pkConnectionMaster.SelectedIndex = _dta.type_contact == "1" ? 0 : _dta.type_contact == "2" ? 1 : 2;
                    if (pkConnectionMaster.SelectedIndex == 0)
                    {
                        sbConnectionMaster1Name.Text = _dta.name_dealer_agent;
                        sbConnectionMaster1Code.Text = _dta.cde_dealer_agent;

                        lstConnectionMaster1Name.IsVisible = false;
                        lstConnectionMaster1Code.IsVisible = false;
                    }
                    else if (pkConnectionMaster.SelectedIndex == 1)
                    {
                        sbConnectionMaster2Name.Text = _dta.name_dealer_agent;
                        sbConnectionMaster2Code.Text = _dta.cde_dealer_agent;

                        lstConnectionMaster2Name.IsVisible = false;
                        lstConnectionMaster2Code.IsVisible = false;
                    }
                    else if (pkConnectionMaster.SelectedIndex == 2)
                    {
                        if (_dta.typ_person != "")
                        {
                            pkMasAdvisorType.SelectedIndex = _dta.typ_person == "1" ? 0 : _dta.typ_person == "2" ? 1 : 2;
                            if (pkMasAdvisorType.SelectedIndex == 0)
                            {
                                sbMasAdvisorType1Name.Text = _dta.name_contno;
                                sbMasAdvisorType1Appno.Text = _dta.contno;

                                lstMasAdvisorType1Name.IsVisible = false;
                                lstMasAdvisorType1Appno.IsVisible = false;
                            }
                            else if (pkMasAdvisorType.SelectedIndex == 1)
                            {
                                enMasAdvisorType2Name.Text = _dta.name_cus;
                                pkSocialtype.SelectedIndex = _dta.type_cont_channel == "1" ? 0 : _dta.type_cont_channel == "2" ? 1 : _dta.type_cont_channel == "3" ? 2 : 3;
                            }
                            else if (pkMasAdvisorType.SelectedIndex == 2)
                            {
                                sbMasAdvisorType3EmNo.Text = _dta.perosonal_cde;
                                sbMasAdvisorType3Name.Text = _dta.personal_name;

                                lstMasAdvisorType3EmNo.IsVisible = false;
                                lstMasAdvisorType3Name.IsVisible = false;
                            }
                        }
                    }

                    //var dtaV2 = aAPI.GetDataInformationTransactionCar().Where(a => a.applno_car == _dta.applno_car && a.app_id != _dta.app_id).FirstOrDefault();
                    if (datatwo != null)
                    {
                        var dtaV2 = datatwo.FirstOrDefault();
                        pkConnectionSubMaster.SelectedIndex = dtaV2.type_contact == "1" ? 0 : dtaV2.type_contact == "2" ? 1 : 2;
                        if (pkConnectionSubMaster.SelectedIndex == 0)
                        {
                            sbConnectionSubMaster1Name.Text = dtaV2.name_dealer_agent;
                            sbConnectionSubMaster1Code.Text = dtaV2.cde_dealer_agent;
                        }
                        else if (pkConnectionSubMaster.SelectedIndex == 1)
                        {
                            sbConnectionSubMaster2Name.Text = dtaV2.name_dealer_agent;
                            sbConnectionSubMaster2Code.Text = dtaV2.cde_dealer_agent;
                        }
                        else if (pkConnectionSubMaster.SelectedIndex == 2)
                        {
                            if (dtaV2.typ_person != "")
                            {
                                pkSubMasAdvisorType.SelectedIndex = dtaV2.typ_person == "1" ? 0 : dtaV2.typ_person == "2" ? 1 : 2;
                                if (pkSubMasAdvisorType.SelectedIndex == 0)
                                {
                                    sbSubMasAdvisorType1Name.Text = dtaV2.name_contno;
                                    sbSubMasAdvisorType1Appno.Text = dtaV2.contno;

                                    lstSubMasAdvisorType1Name.IsVisible = false;
                                    lstSubMasAdvisorType1Appno.IsVisible = false;
                                }
                                else if (pkSubMasAdvisorType.SelectedIndex == 1)
                                {
                                    enSubMasAdvisorType2Name.Text = dtaV2.name_cus;
                                    pkSubSocialtype.SelectedIndex = dtaV2.type_cont_channel == "1" ? 0 : dtaV2.type_cont_channel == "2" ? 1 : dtaV2.type_cont_channel == "3" ? 2 : 3;
                                }
                                else if (pkSubMasAdvisorType.SelectedIndex == 2)
                                {
                                    sbSubMasAdvisorType3EmNo.Text = dtaV2.perosonal_cde;
                                    sbSubMasAdvisorType3Name.Text = dtaV2.personal_name;

                                    lstSubMasAdvisorType3EmNo.IsVisible = false;
                                    lstSubMasAdvisorType3Name.IsVisible = false;
                                }
                            }
                        }
                    }
                }
            }
            else if (_dta.type_grp_car == "02")
            {
                //var dtaV2 = aAPI.GetDataInformationTransactionCar().Where(a => a.applno_car == _dta.applno_car && a.app_id != _dta.app_id).FirstOrDefault();
               
                if (datatwo != null)
                {
                    var dtaV2 = datatwo.FirstOrDefault();
                    if (_dta.type_dealer == "1")
                    {
                        frmMaster.IsVisible = true;
                        frmSubMaster.IsVisible = false;
                        pkConnectionMaster.SelectedIndex = _dta.type_contact == "1" ? 0 : _dta.type_contact == "2" ? 1 : 2;
                        if (_dta.type_contact == "1")
                        {
                            sbConnectionMaster1Name.Text = _dta.name_dealer_agent;
                            sbConnectionMaster1Code.Text = _dta.cde_dealer_agent;
                            lstConnectionMaster1Name.IsVisible = false;
                            lstConnectionMaster1Code.IsVisible = false;
                        }
                        else if (_dta.type_contact == "2")
                        {
                            sbConnectionMaster2Name.Text = _dta.name_dealer_agent;
                            sbConnectionMaster2Code.Text = _dta.cde_dealer_agent;

                            lstConnectionMaster2Name.IsVisible = false;
                            lstConnectionMaster2Code.IsVisible = false;
                        }
                        else if (_dta.type_contact == "3")
                        {
                            if (_dta.typ_person != "")
                            {
                                pkMasAdvisorType.SelectedIndex = _dta.typ_person == "1" ? 0 : _dta.typ_person == "2" ? 1 : 2;
                                if (pkMasAdvisorType.SelectedIndex == 0)
                                {
                                    sbMasAdvisorType1Name.Text = _dta.name_contno;
                                    sbMasAdvisorType1Appno.Text = _dta.contno;

                                    lstMasAdvisorType1Name.IsVisible = false;
                                    lstMasAdvisorType1Appno.IsVisible = false;
                                }
                                else if (pkMasAdvisorType.SelectedIndex == 1)
                                {
                                    enMasAdvisorType2Name.Text = _dta.name_cus;
                                    pkSocialtype.SelectedIndex = _dta.type_cont_channel == "1" ? 0 : _dta.type_cont_channel == "2" ? 1 : _dta.type_cont_channel == "3" ? 2 : 3;
                                }
                                else if (pkMasAdvisorType.SelectedIndex == 2)
                                {
                                    sbMasAdvisorType3EmNo.Text = _dta.perosonal_cde;
                                    sbMasAdvisorType3Name.Text = _dta.personal_name;

                                    lstMasAdvisorType3EmNo.IsVisible = false;
                                    lstMasAdvisorType3Name.IsVisible = false;
                                }
                            }
                        }
                    }
                    else if (_dta.type_dealer == "2")
                    {
                        frmMaster.IsVisible = true;
                        frmSubMaster.IsVisible = true;
                        pkConnectionMaster.SelectedIndex = dtaV2.type_contact == "1" ? 0 : dtaV2.type_contact == "2" ? 1 : 2;
                        if (dtaV2.type_contact == "1")
                        {
                            sbConnectionMaster1Name.Text = dtaV2.name_dealer_agent;
                            sbConnectionMaster1Code.Text = dtaV2.cde_dealer_agent;
                            lstConnectionMaster1Name.IsVisible = false;
                            lstConnectionMaster1Code.IsVisible = false;
                        }
                        else if (dtaV2.type_contact == "2")
                        {
                            sbConnectionMaster2Name.Text = dtaV2.name_dealer_agent;
                            sbConnectionMaster2Code.Text = dtaV2.cde_dealer_agent;

                            lstConnectionMaster2Name.IsVisible = false;
                            lstConnectionMaster2Code.IsVisible = false;
                        }
                        else if (dtaV2.type_contact == "3")
                        {
                            if (dtaV2.typ_person != "")
                            {
                                pkMasAdvisorType.SelectedIndex = dtaV2.typ_person == "1" ? 0 : dtaV2.typ_person == "2" ? 1 : 2;
                                if (dtaV2.typ_person == "1")
                                {
                                    sbMasAdvisorType1Name.Text = dtaV2.name_contno;
                                    sbMasAdvisorType1Appno.Text = dtaV2.contno;

                                    lstMasAdvisorType1Name.IsVisible = false;
                                    lstMasAdvisorType1Appno.IsVisible = false;
                                }
                                else if (dtaV2.typ_person == "2")
                                {
                                    enMasAdvisorType2Name.Text = dtaV2.name_cus;
                                    pkSocialtype.SelectedIndex = dtaV2.type_cont_channel == "1" ? 0 : dtaV2.type_cont_channel == "2" ? 1 : dtaV2.type_cont_channel == "3" ? 2 : 3;
                                }
                                else if (dtaV2.typ_person == "3")
                                {
                                    sbMasAdvisorType3EmNo.Text = dtaV2.perosonal_cde;
                                    sbMasAdvisorType3Name.Text = dtaV2.personal_name;

                                    lstMasAdvisorType3EmNo.IsVisible = false;
                                    lstMasAdvisorType3Name.IsVisible = false;
                                }
                            }
                        }

                        pkConnectionSubMaster.SelectedIndex = _dta.type_contact == "1" ? 0 : _dta.type_contact == "2" ? 1 : 2;
                        if (_dta.type_contact == "1")
                        {
                            sbConnectionSubMaster1Name.Text = _dta.name_dealer_agent;
                            sbConnectionSubMaster1Code.Text = _dta.cde_dealer_agent;
                            lstConnectionSubMaster1Name.IsVisible = false;
                            lstConnectionSubMaster1Code.IsVisible = false;
                        }
                        else if (_dta.type_contact == "2")
                        {
                            sbConnectionSubMaster2Name.Text = _dta.name_dealer_agent;
                            sbConnectionSubMaster2Code.Text = _dta.cde_dealer_agent;

                            lstConnectionSubMaster2Name.IsVisible = false;
                            lstConnectionSubMaster2Code.IsVisible = false;
                        }
                        else if (_dta.type_contact == "3")
                        {
                            if (_dta.typ_person != "")
                            {
                                pkSubMasAdvisorType.SelectedIndex = _dta.typ_person == "1" ? 0 : _dta.typ_person == "2" ? 1 : 2;
                                if (_dta.typ_person == "1")
                                {
                                    sbSubMasAdvisorType1Name.Text = _dta.name_contno;
                                    sbSubMasAdvisorType1Appno.Text = _dta.contno;

                                    lstSubMasAdvisorType1Name.IsVisible = false;
                                    lstSubMasAdvisorType1Appno.IsVisible = false;
                                }
                                else if (_dta.typ_person == "2")
                                {
                                    enSubMasAdvisorType2Name.Text = _dta.name_cus;
                                    pkSubSocialtype.SelectedIndex = _dta.type_cont_channel == "1" ? 0 : _dta.type_cont_channel == "2" ? 1 : _dta.type_cont_channel == "3" ? 2 : 3;
                                }
                                else if (_dta.typ_person == "3")
                                {
                                    sbSubMasAdvisorType3EmNo.Text = _dta.perosonal_cde;
                                    sbSubMasAdvisorType3Name.Text = _dta.personal_name;

                                    lstSubMasAdvisorType3EmNo.IsVisible = false;
                                    lstSubMasAdvisorType3Name.IsVisible = false;
                                }
                            }
                        }

                    }
                }


            }


            pkNote.SelectedIndex = _dta.remark_transactioncar == "1" ? 0 : _dta.remark_transactioncar == "2" ? 1 : 2;
            if (_dta.remark_transactioncar == "1")
            {
                stRemarkdes.IsVisible = true;
                DatetimePicker.IsVisible = false;
                enRemarkDes.Text = _dta.remark_description_transaction;
            }
            else if (_dta.remark_transactioncar == "2")
            {
                stRemarkdes.IsVisible = true;
                DatetimePicker.IsVisible = true;
              
                startDatePicker.Date = _dta.remark_date;
                enRemarkDes.Text = _dta.remark_description_transaction;
            }
            if (_dta.campaign_id != "0")
            {
                pkCamPaign.SelectedIndex = _campaign.Select(a => a.campaign_id).ToList().IndexOf(_campaign.Where(a => a.campaign_id == _dta.campaign_id).FirstOrDefault().campaign_id);
            }
            pkNumMasterandSubMaster.SelectedIndex = _dta.type_grp_car == "01" ? 0 : 1;
            if (pkNumMasterandSubMaster.SelectedIndex == 0)
            {

                enMasLicno.Text = _dta.licno;
                pkMasProvince.SelectedIndex = _province.Select(a => a.DESC_TH).ToList().IndexOf(_province.Where(a => a.DESC_TH == _dta.provice).FirstOrDefault().DESC_TH);
                enMasEngno.Text = _dta.engno;
                enMasChasno.Text = _dta.chasno;
                pkMasBrand.SelectedIndex = _brand.Select(a => a.CODE).ToList().IndexOf(_brand.Where(a => a.CODE == _dta.cde_brand).FirstOrDefault().CODE);
                pkMasFuel.SelectedIndex = _dta.fuel == "01" ? 0 : 1;
                pkMasCarType.SelectedIndex = _typecar.Select(a => a.cde_typ_car).ToList().IndexOf(_typecar.Where(a => a.cde_typ_car == _dta.type_car).FirstOrDefault().cde_typ_car);

                pkMasStyle1.ItemsSource = iAPI.GetDataVStyle1().Where(a => a.cde_head_tail == _dta.type_grp_car).ToList();
                pkMasStyle1.SelectedIndex = iAPI.GetDataVStyle1().Select(a => a.cde_grpcar).ToList().IndexOf(iAPI.GetDataVStyle1().Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_grpcar == _dta.grp_car).FirstOrDefault().cde_grpcar);

                pkMasStyle2.ItemsSource = iAPI.GetDataVStyle3().Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_style == "01" && a.cde_grpcar == _dta.grp_car).ToList();
                pkMasStyle2.SelectedIndex = iAPI.GetDataVStyle3().Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_style == "01" && a.cde_grpcar == _dta.grp_car).Select(a => a.cde_style_car).ToList().IndexOf(iAPI.GetDataVStyle3().Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_style == "01" && a.cde_grpcar == _dta.grp_car && a.cde_style_car == _dta.cde_pickup).FirstOrDefault().cde_style_car);
                if (_dta.cde_special != "")
                {
                    pkMasStyle3.ItemsSource = _charactercar.Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_style == "02" && a.cde_grpcar == _dta.grp_car).ToList();
                    pkMasStyle3.SelectedIndex = _charactercar.Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_style == "02" && a.cde_grpcar == _dta.grp_car).Select(a => a.cde_style_car).ToList().IndexOf(_charactercar.Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_style == "02" && a.cde_grpcar == _dta.grp_car && a.cde_style_car == _dta.cde_special).FirstOrDefault().cde_style_car);

                    //pkMasStyle3.ItemsSource = iAPI.GetDataVStyle3().Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_style == "02" && a.cde_grpcar == _dta.grp_car).ToList();
                    //pkMasStyle3.SelectedIndex = iAPI.GetDataVStyle3().Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_style == "02" && a.cde_grpcar == _dta.grp_car).Select(a => a.cde_style_car).ToList().IndexOf(iAPI.GetDataVStyle3().Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_style == "02" && a.cde_grpcar == _dta.grp_car && a.cde_style_car == _dta.cde_special).FirstOrDefault().cde_style_car);
                }
                else stMasStyle3.IsVisible = false;
                enMasHeadPrice.Text = _dta.head_price.ToString();
                pkMasAccessories.SelectedIndex = _dta.accessory==""|| _dta.accessory==null?-1:_accessories.Select(a => a.cde_accessory).ToList().IndexOf(_accessories.Where(a => a.cde_accessory == _dta.accessory).FirstOrDefault().cde_accessory);
                enMasYearCar.Text = _dta.year_car;
            }
            else
            {
                enSubLicno.Text = _dta.licno;
                pkSubProvince.SelectedIndex = _province.Select(a => a.DESC_TH).ToList().IndexOf(_province.Where(a => a.DESC_TH == _dta.provice).FirstOrDefault().DESC_TH);
                enSChasno.Text = _dta.chasno;
                pkSubStyle1.SelectedIndex = aAPI.GetDataV_StyleSubMaster1().Select(a => a.cde_type_style).ToList().IndexOf(aAPI.GetDataV_StyleSubMaster1().Where(a => a.cde_type_style == _dta.grp_car).FirstOrDefault().cde_type_style);
                var dddd = aAPI.GetDataV_StyleSubMaster2().Where(a => a.cde_type_style == _dta.grp_car).Select(a => a.cde_style_car).ToList();
                var oooo = aAPI.GetDataV_StyleSubMaster2().Where(a => a.cde_type_style == _dta.grp_car && a.cde_style_car == _dta.cde_pickup).FirstOrDefault().cde_style_car;
                var uuuu = dddd.IndexOf(oooo);
                pkSubStyle2.SelectedIndex = aAPI.GetDataV_StyleSubMaster2().Where(a=>a.cde_type_style==_dta.grp_car).Select(a=>a.cde_style_car).ToList().IndexOf(aAPI.GetDataV_StyleSubMaster2().Where(a => a.cde_type_style==_dta.grp_car && a.cde_style_car == _dta.cde_pickup ).FirstOrDefault().cde_style_car);
                //pkSubStyle2.SelectedIndex = aAPI.GetDataV_StyleSubMaster2().Select(a=>a.row_number).ToList().IndexOf(aAPI.GetDataV_StyleSubMaster2().Where(a => a.cde_type_style==_dta.grp_car && a.cde_style_car == _dta.cde_pickup ).FirstOrDefault().row_number);

                // pkSubStyle2.SelectedIndex = aAPI.GetDataV_StyleSubMaster2().Select(a => a.cde_style_car).ToList().IndexOf(_charactercar.Where(a => a.cde_head_tail == _dta.type_grp_car && a.cde_grpcar == _dta.grp_car && a.cde_style_car == _dta.cde_pickup).FirstOrDefault().cde_style_car);

                //pkSubStyle2.SelectedIndex = aAPI.GetDataV_StyleSubMaster2().Select(a => a.cde_style_car).ToList().IndexOf(aAPI.GetDataV_StyleSubMaster2().Where(a => a.cde_style_car == _dta.cde_pickup).FirstOrDefault().cde_style_car);
                if (_dta.cde_special != "")
                {
                    pkSubStyle3.ItemsSource = aAPI.GetDataV_StyleSubMaster2().Where(a => a.cde_type_style == _dta.grp_car).ToList();
                    pkSubStyle3.SelectedIndex = aAPI.GetDataV_StyleSubMaster2().Select(a => a.cde_style_car).ToList().IndexOf(aAPI.GetDataV_StyleSubMaster2().Where(a => a.cde_style_car == _dta.cde_special).FirstOrDefault().cde_style_car);

                    //pkSubStyle3.SelectedIndex = aAPI.GetDataV_StyleSubMaster2().Select(a => a.row_number).ToList().IndexOf(aAPI.GetDataV_StyleSubMaster2().Where(a => a.cde_type_style == _dta.grp_car && a.cde_style_car == _dta.cde_special).FirstOrDefault().row_number);
                }
                else stSubStyle3.IsVisible = false;
                pkSubCarType.SelectedIndex = _typecar.Select(a => a.cde_typ_car).ToList().IndexOf(_typecar.Where(a => a.cde_typ_car == _dta.type_car).FirstOrDefault().cde_typ_car);
                enSubCarPrice.Text = _dta.head_price.ToString();
                enSubYearCar.Text = _dta.year_car;
            }

            enAppno.Text = _dta.applno_car;

            //ดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม(แก้ได้เฉพาะข้อมูลรูปภาพรถส่วนอื่นแก้ไม่ได้)
            if (_dta.status_appcar == "4")
            {
                if (_dta.type_grp_car == "01")
                {
                    DetailGeneral.IsEnabled = false;
                    DetailMaster.IsEnabled = false;
                }
                else if (_dta.type_grp_car == "02")
                {
                    DetailGeneral.IsEnabled = false;
                    DetailSubMaster.IsEnabled = false;
                }
            }
            else
            {
                DetailMaster.IsEnabled = true;
                DetailSubMaster.IsEnabled = true;
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
            else if (pkDealerType.SelectedIndex == 0)
            {
                if (pkConnectionMaster.SelectedIndex == 0)
                {
                    if (string.IsNullOrEmpty(sbConnectionMaster1Name.Text) == true || string.IsNullOrEmpty(sbConnectionMaster1Code.Text) == true) ret = "กรุณาข้อมูลนายหน้าให้ครบถ้วน";
                }
                else if (pkConnectionMaster.SelectedIndex == 1)
                {
                    if (string.IsNullOrEmpty(sbConnectionMaster2Name.Text) == true || string.IsNullOrEmpty(sbConnectionMaster2Code.Text) == true) ret = "กรุณาข้อมูลคู่ค้าให้ครบถ้วน";
                }
                else if (pkConnectionMaster.SelectedIndex == 2)
                {
                    if (pkMasAdvisorType.SelectedIndex == -1) ret = "กรุณาเลือกผู้แนะนำ";
                    if (pkMasAdvisorType.SelectedIndex == 0)
                    {
                        if (string.IsNullOrEmpty(sbMasAdvisorType1Name.Text) == true || string.IsNullOrEmpty(sbMasAdvisorType1Appno.Text) == true) ret = "กรุณากรอกข้อมูลลูกค้าเก่าให้ครบถ้วน";
                    }
                    else if (pkMasAdvisorType.SelectedIndex == 1)
                    {
                        if (string.IsNullOrEmpty(enMasAdvisorType2Name.Text) == true || pkSocialtype.SelectedIndex == -1) ret = "กรุณากรอกข้อมูลลูกค้าใหม่ติดต่อมาให้ครบถ้วน";
                    }
                    else if (pkMasAdvisorType.SelectedIndex == 2)
                    {
                        if (string.IsNullOrEmpty(sbMasAdvisorType3EmNo.Text) == true || string.IsNullOrEmpty(sbMasAdvisorType3Name.Text) == true) ret = "กรุณากรอกข้อมูลรหัสพนักงานให้ครบถ้วน";
                    }
                }
            }
           
            else
            {
                if (pkConnectionSubMaster.SelectedIndex == 0)
                {
                    if (string.IsNullOrEmpty(sbConnectionSubMaster1Name.Text) == true || string.IsNullOrEmpty(sbConnectionSubMaster1Code.Text) == true) ret = "กรุณาข้อมูลนายหน้าให้ครบถ้วน";
                }
                else if (pkConnectionSubMaster.SelectedIndex == 1)
                {
                    if (string.IsNullOrEmpty(sbConnectionSubMaster2Name.Text) == true || string.IsNullOrEmpty(sbConnectionSubMaster2Code.Text) == true) ret = "กรุณาข้อมูลคู่ค้าให้ครบถ้วน";
                }
                else if (pkConnectionSubMaster.SelectedIndex == 2)
                {
                    if (pkSubMasAdvisorType.SelectedIndex == -1) ret = "กรุณาเลือกผู้แนะนำ";
                    if (pkSubMasAdvisorType.SelectedIndex == 0)
                    {
                        if (string.IsNullOrEmpty(sbSubMasAdvisorType1Name.Text) == true || string.IsNullOrEmpty(sbSubMasAdvisorType1Appno.Text) == true) ret = "กรุณากรอกข้อมูลลูกค้าเก่าให้ครบถ้วน";
                    }
                    else if (pkSubMasAdvisorType.SelectedIndex == 1)
                    {
                        if (string.IsNullOrEmpty(enSubMasAdvisorType2Name.Text) == true || pkSubSocialtype.SelectedIndex == -1) ret = "กรุณากรอกข้อมูลลูกค้าใหม่ติดต่อมาให้ครบถ้วน";
                    }
                    else if (pkSubMasAdvisorType.SelectedIndex == 2)
                    {
                        if (string.IsNullOrEmpty(sbSubMasAdvisorType3EmNo.Text) == true || string.IsNullOrEmpty(sbSubMasAdvisorType3Name.Text) == true) ret = "กรุณากรอกข้อมูลรหัสพนักงานให้ครบถ้วน";
                    }
                }
            }

            if (pkNote.SelectedIndex == -1) ret = "กรุณาเลือกหมายเหตุ";
            else if (pkNote.Items[pkNote.SelectedIndex] == "ดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม")
            {
                if (startDatePicker.Date == startDatePicker.MinimumDate) ret = "กรุณากรอกวันที่เสร็จสิ้นการดำเนินการปรับปรุง,เปลี่ยน,ซ่อมแซม";
            }
            else if (pkNumMasterandSubMaster.SelectedIndex == -1) ret = "กรุณาเลือกตัวแม่-ตัวลูก";

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
            string ret = "";
            if (enSubLicno.Text == null) ret = "กรุณากรอกเลขทะเบียน";
            else if (pkSubProvince.SelectedIndex == -1) ret = "กรุณาเลือกจังหวัด";
            else if (pkSubStyle1.SelectedIndex == -1) ret = "กรุณาเลือกประเภทรถ";
            else if (pkSubStyle2.SelectedIndex == -1) ret = "กรุณาเลือกลักษณะกระบะ";
            else if (pkSubStyle2.SelectedIndex != 1)
            {
                var cdetypestyle = ((DataV_StyleSubMaster2)pkSubStyle2.SelectedItem).cde_style_car;
                if (pkSubStyle1.Items[pkSubStyle1.SelectedIndex] == "รถกึ่งพ่วง" && (cdetypestyle == "T003" || cdetypestyle == "T015"))
                {
                    if (pkSubStyle3.SelectedIndex == -1) ret = "กรุณาเลือกลักษณะพิเศษ";
                }
            }
            else if (pkSubCarType.SelectedIndex == -1) ret = "กรุณาเลือกประเภท";
            else if (enSubCarPrice.Text == null) ret = "กรุณากรอกราคาขายตัวลูก";
            else if (enSubYearCar.Text == null) ret = "กรุณากรอกปีจดทะเบียน";
            else if (enSubYearCar.Text.Length != 4) ret = "กรุณากรอกปีจดทะเบียนให้ครบ 4 หลัก";
          

            return ret;
        }
        public string ValidationMaster()
        {
            string ret = "";
            if (enMasLicno.Text == null) ret = "กรุณากรอกเลขทะเบียน";
            else if (pkMasProvince.SelectedIndex == -1) ret = "กรุณาเลือกจังหวัด";
            else if (enMasEngno.Text == null) ret = "กรุณากรอกเลขตัวรถ";
            else if (enMasChasno.Text == null) ret = "กรุณากรอกเลขตัวถัง";
            else if (pkMasBrand.SelectedIndex == -1) ret = "กรุณาเลือกยี่ห้อรถ";
            else if (pkMasFuel.SelectedIndex == -1) ret = "กรุณาเลือกเชื้อเพลิง";
            else if (pkMasCarType.SelectedIndex == -1) ret = "กรุณาเลือกประเภทรถ";
            else if (pkMasStyle1.SelectedIndex == -1 && pkMasStyle2.SelectedIndex == -1) ret = "กรุณาเลือกลักษณะรถ";
            else if (pkMasStyle1.SelectedIndex == -1) ret = "กรุณาเลือกกลุ่มรถ";
            else if (pkMasStyle2.SelectedIndex == -1) ret = "กรุณาเลือกลักษณะกระบะ";
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
            else if (enMasHeadPrice.Text == null) ret = "กรุณากรอกราคาขายตัวแม่";
            else if (pkMasAccessories.SelectedIndex == -1) ret = "กรุณาเลือกอุปกรณ์เสริม";
            else if (enMasYearCar.Text == null) ret = "กรุณากรอกปีจดทะเบียน";
            else if (enMasYearCar.Text.Length != 4) ret = "กรุณากรอกปีจดทะเบียนให้ครบ 4 หลัก";

         

            return ret;
        }

        private DataInformationTransactionCar GetDataGeneralMaster()
        {
          
       
            DataInformationTransactionCar dta = new DataInformationTransactionCar();

            dta.applno_car = _dta.applno_car;
            dta.app_id = _dta.app_id;
            dta.type_credit= ((DataUseCarType)pkLoanType.SelectedItem).prmcde;
            dta.type_dealer = _dta.type_dealer;
            dta.type_contact = _dta.type_contact;
            dta.remark = (pkNote.SelectedIndex == 0 ? 1 : pkNote.SelectedIndex == 1 ? 2 : 3).ToString();
            if (pkNote.SelectedIndex == 0)
            {
                dta.remark_description = enRemarkDes.Text.Trim();
            }
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
            dta.create_by = _userlogin.data.Select(a => a.personnel_code).FirstOrDefault();
            dta.status = 1;
            dta.branch_code = _userlogin.data.Select(a => a.branch_no).FirstOrDefault();
            dta.run_number = _dta.run_number;
            dta.type_grp_car = _dta.type_grp_car;

            dta.cde_dealer_agent = _dta.cde_dealer_agent;
            dta.name_dealer_agent = _dta.name_dealer_agent;
            dta.typ_person = _dta.typ_person;
            dta.type_cont_channel = _dta.type_cont_channel;
            dta.name_cus = _dta.name_cus;
            dta.perosonal_cde = _dta.perosonal_cde;
            dta.personal_name = _dta.personal_name;
            dta.contno = _dta.contno;
            dta.name_contno = _dta.name_contno;
       
            return dta;
        }

        private DataInformationTransactionCar GetDataGeneralSubMaster()
        {
            DataInformationTransactionCar dta = new DataInformationTransactionCar();
            dta.applno_car = _dta.applno_car;
            dta.type_credit = ((DataUseCarType)pkLoanType.SelectedItem).prmcde;
            dta.type_dealer = _dta.type_dealer;
            dta.type_contact = _dta.type_contact;
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
            dta.create_by = _userlogin.data.Select(a => a.personnel_code).FirstOrDefault();
            dta.status = 1;
            dta.branch_code = _userlogin.data.Select(a => a.branch_no).FirstOrDefault();
            dta.run_number = _dta.run_number;
            

            return dta;
        }



        private bool PostDataGenaral()
        {
            bool ret = true;
            Update_Webservice iapi = new Update_Webservice();
            Appz_WebService aAPI = new Appz_WebService();
            try
            {
                var data = GetDataGeneralMaster();
                if (!iapi.PostUpdateDataTransactionCar(data))
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

        private bool PostDataMaster()
        {
            bool ret = true;
            try
            {
                Update_Webservice iapi = new Update_Webservice();
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
                        var s = pkMasStyle3.SelectedItem;
                        dta.cde_special = ((DataCharacterCar)pkMasStyle3.SelectedItem).cde_style_car;
                    }
                    else dta.cde_special = "";
                }


                dta.head_price = float.Parse(enMasHeadPrice.Text);
                dta.estimate_price = float.Parse("0");
                dta.finamt = float.Parse("0");
                dta.accessory = ((Model.Appz_Model.DataAccessories)pkMasAccessories.SelectedItem).cde_accessory;
                dta.year_car = enMasYearCar.Text;
                dta.type_grp_car = "01";
                dta.status = 1;

                dta.campaign_id = pkCamPaign.SelectedIndex == -1 ? "0" : ((DataCampaign)pkCamPaign.SelectedItem).campaign_id;
                dta.create_by = _userlogin.data.Select(a => a.personnel_code).FirstOrDefault();
                dta.app_id = enAppno.Text + dta.type_grp_car;


                if (!iapi.PostUpdateDataInformationCar(dta))
                {
                    ret = false;
                }

            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex;
            }
            return ret;
             
        }


        private bool PostDataSubMaster()
        {
            bool ret = true;
            Update_Webservice iapi = new Update_Webservice();
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

            ret = iapi.PostUpdateDataInformationCar(dta);
            return ret;
        }


        private bool PostImage()
        {
            bool ret = true;
            try
            {
                Update_Webservice iModel = new Update_Webservice();
                SentToAPI mMo = new SentToAPI();
                mMo.personnel_code = _userlogin.data.First().personnel_code;
                mMo.storename = "ST_IMAGE_GET";
                mMo.c1 = _dta.app_id;
                var olddata = IMAGE_GET(mMo);
                if (olddata != null)
                //if (iModel.GetDataPicture() != null)
                {
                    var dtapic = olddata.FirstOrDefault();
                    //var dtapic = iModel.GetDataPicture().Where(a => a.app_id == _dta.app_id && a.status == 1).FirstOrDefault();
                    DataImage dta = new DataImage();
                    dta.create_by = _userlogin.data.Select(a => a.personnel_code).FirstOrDefault();
                    dta.applno_car = _dta.applno_car;
                    dta.type_grp_car = _dta.type_grp_car;
                    dta.app_id = _dta.app_id;
                    if (_dta.type_grp_car == "01")
                    {

                        dta.img1_head = imgMasPic1.Text;
                        dta.img2_head = imgMasPic2.Text;
                        dta.img3_head = imgMasPic3.Text;
                        dta.img26 = imgMasPic4.Text;
                        dta.img27 = imgMasPic5.Text;
                        dta.img4_head = imgMasPic6.Text;
                        dta.img5_head = imgMasPic7.Text;
                        dta.img6_head = imgMasPic8.Text;
                        dta.img7_head = imgMasPic9.Text;
                        dta.img8_head = imgMasPic10.Text;
                        dta.img9_head = imgMasPic11.Text;
                        dta.img10_head = imgMasPic12.Text;
                        dta.img11_head = imgMasPic13.Text;
                        dta.img28 = imgMasPic14.Text;
                        dta.img12_head = imgMasPic15.Text;
                        dta.img13_head = imgMasPic16.Text;
                        dta.img14_head = imgMasPic17.Text;
                        dta.img15_head = imgMasPic18.Text;
                        dta.img23 = imgMasPic19.Text;
                        dta.img24 = imgMasPic20.Text;
                        dta.img25 = imgMasPic21.Text;

                        if (!iModel.PostUpdateDataImage(dta))
                        {
                            return false;
                        }
                    }
                    else if (_dta.type_grp_car == "02")
                    {
                        dta.img16_tail = imgSubPic1.Text == "" ? dtapic.img16_tail : imgSubPic1.Text;
                        dta.img17_tail = imgSubPic2.Text == "" ? dtapic.img17_tail : imgSubPic2.Text;
                        dta.img18_tail = imgSubPic3.Text == "" ? dtapic.img18_tail : imgSubPic3.Text;
                        dta.img19_tail = imgSubPic4.Text == "" ? dtapic.img19_tail : imgSubPic4.Text;
                        dta.img26 = imgSubPic5.Text == "" ? dtapic.img26 : imgSubPic5.Text;
                        dta.img27 = imgSubPic6.Text == "" ? dtapic.img27 : imgSubPic6.Text;
                        dta.img28 = imgSubPic7.Text == "" ? dtapic.img28 : imgSubPic7.Text;
                        dta.img23 = imgSubPic8.Text == "" ? dtapic.img23 : imgSubPic8.Text;
                        dta.img24 = imgSubPic9.Text == "" ? dtapic.img24 : imgSubPic9.Text;
                        dta.img25 = imgSubPic10.Text == "" ? dtapic.img25 : imgSubPic10.Text;
                        dta.img20_tail = imgSubPic11.Text == "" ? dtapic.img20_tail : imgSubPic11.Text;
                        dta.img21_tail = imgSubPic12.Text == "" ? dtapic.img21_tail : imgSubPic12.Text;

                        if (!iModel.PostUpdateDataImage(dta))
                        {
                            return false;
                        }
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
        
        private void btnMasCarStyle_Clicked(object sender, EventArgs e)
        {
            InsertCarPrice_WebService iAPI = new InsertCarPrice_WebService();
            pkMasStyle1.ItemsSource = iAPI.GetDataVStyle1().Where(a => a.cde_head_tail == "01").ToList();
            frmMasCarStyle.IsVisible = true;
        }

        private async Task<string> GetImage()
        {
            var file = await MediaPicker.PickPhotoAsync();
            var a = file.FullPath;
            return a;
        }

        private async void btnMasPic1_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic1.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic2_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic2.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic3_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic3.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic4_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic4.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic5_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic5.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic6_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic6.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic7_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic7.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic8_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic8.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic9_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic9.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic10_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic10.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic11_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic11.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic12_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic12.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic13_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic13.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic14_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic14.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic15_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic15.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic16_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic16.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic17_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic17.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic18_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic18.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic19_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic19.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic20_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic20.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnMasPic21_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgMasPic21.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private void btnSubCarStyle_Clicked(object sender, EventArgs e)
        {
            frmSubCarStyle.IsVisible = true;
            Appz_WebService iAPI = new Appz_WebService();
            pkSubStyle1.ItemsSource = iAPI.GetDataV_StyleSubMaster1();
        }

        private async void btnSubPic1_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgSubPic1.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnSubPic2_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgSubPic2.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnSubPic3_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgSubPic3.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnSubPic4_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgSubPic4.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnSubPic5_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgSubPic5.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnSubPic6_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgSubPic6.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnSubPic7_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgSubPic7.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnSubPic8_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgSubPic8.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnSubPic9_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgSubPic9.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnSubPic10_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgSubPic10.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnSubPic11_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgSubPic11.Text = await GetImage();
            (sender as Button).IsEnabled = true;
        }

        private async void btnSubPic12_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            imgSubPic12.Text = await GetImage();
            (sender as Button).IsEnabled = true;
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
                DisplayAlert("ผิดพลาด", errorPage1, "ตกลง");
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
                if (_dta.status_appcar == "4")
                {
                    if (!PostImage() != true)
                    {
                        DisplayAlert("หน้าแก้ไขข้อมูลรายการรถ", "บันทึกข้อมูลเรียบร้อย", "ตกลง");
                        ret = true;
                    }
                }
                else
                {
                    if (pkNumMasterandSubMaster.SelectedIndex == 0) //เพิ่มข้อมูลตัวแม่
                    {
                        if (PostDataMaster() == true)
                        {
                            PostImage();
                            DisplayAlert("หน้าแก้ไขข้อมูลรายการรถ", "บันทึกข้อมูลเรียบร้อย", "ตกลง");
                            ret = true;
                        }
                        else return false;
                    }
                    //เพิ่มข้อมูลตัวลูก
                    else if (pkNumMasterandSubMaster.SelectedIndex == 1)
                    {
                        if (PostDataSubMaster() == true)
                        {
                            PostImage();
                            DisplayAlert("หน้าแก้ไขข้อมูลรายการรถ", "บันทึกข้อมูลเรียบร้อย", "ตกลง");
                            ret = true;
                        }
                        else return false;
                    }
                    else //ตัวแม่และตัวลูก
                    {
                        if (PostDataMaster() == true && PostDataSubMaster() == true)
                        {
                            PostImage();
                            //PostImageSubMaster();
                            DisplayAlert("หน้าแก้ไขข้อมูลรายการรถ", "บันทึกข้อมูลเรียบร้อย", "ตกลง");
                            ret = true;
                        }
                        else return false;
                    }
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
            PageInsertCarPrice.Opacity = 0.5;
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
            //pkNumMasterandSubMaster.IsEnabled = true;

            if (pkDealerType.SelectedIndex == 0)
            {
                frmMaster.IsVisible = true;
                frmSubMaster.IsVisible = false;

                enAppno.Text = _appno;
            }
            else if (pkDealerType.SelectedIndex == 1)
            {
                frmMaster.IsVisible = true;
                frmSubMaster.IsVisible = true;

                //enAppno.Text = "T" + _appno;
            }
        }

        private void pkConnectionMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pkConnectionMaster.SelectedIndex == 0)
            {
                frmMasContact1.IsVisible = true;
                frmMasContact2.IsVisible = false;
                frmMasContact3.IsVisible = false;
            }
            else if (pkConnectionMaster.SelectedIndex == 1)
            {
                frmMasContact1.IsVisible = false;
                frmMasContact2.IsVisible = true;
                frmMasContact3.IsVisible = false;
            }
            else if (pkConnectionMaster.SelectedIndex == 2)
            {
                frmMasContact1.IsVisible = false;
                frmMasContact2.IsVisible = false;
                frmMasContact3.IsVisible = true;
            }
        }

        private void pkMasAdvisorType_SelectedIndexChanged(object sender, EventArgs e)
        {
        
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
            if (pkNote.SelectedIndex==0)
            {
                stRemarkdes.IsVisible = true;
                DatetimePicker.IsVisible = false;
            }
            else if (pkNote.SelectedIndex==1)
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
         
            if (pkSubStyle2.SelectedIndex != -1)
            {
                var cdetypestyle = ((DataV_StyleSubMaster2)pkSubStyle2.SelectedItem).cde_style_car;
                if (pkSubStyle1.Items[pkSubStyle1.SelectedIndex] == "รถกึ่งพ่วง" && (cdetypestyle == "T003" || cdetypestyle == "T015"))
                {
                    pkSubStyle3.ItemsSource = iAPI.GetDataV_StyleSubMaster2().Where(a => a.cde_style == "02").ToList();
                    stSubStyle3.IsVisible = true;
                }
                else
                {
                    stSubStyle3.IsVisible = false;
                    pkSubStyle3.SelectedIndex = -1;
                }

            }
            else
            {
                pkSubStyle3.SelectedIndex = -1;
                stSubStyle3.IsVisible = false;
            }
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
            //PageInsertCarPrice.Opacity = 0.5;
            await Task.Delay(2000);
            await Navigation.PushAsync(new MainPage(_userlogin));
            PopupLoad.IsVisible = false;
        }



        private void sbConnectionMaster1Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionMaster1Name.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = DealerAgent.Where(a => a.Name.Contains(keyword)).ToList();
                lstConnectionMaster1Name.ItemsSource = suggestions.Select(a => a.Name);
                lstConnectionMaster1Name.IsVisible = true;
            }
        }

        private void lstConnectionMaster1Name_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionMaster1Name.Text = lstConnectionMaster1Name.SelectedItem.ToString();
            lstConnectionMaster1Name.IsVisible = false;
            sbConnectionMaster1Code.Text = DealerAgent.Where(a => a.Name == sbConnectionMaster1Name.Text).FirstOrDefault().No_;
            lstConnectionMaster1Code.IsVisible = false;
        }


        private void sbConnectionMaster1Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionMaster1Code.Text.ToUpper();
            if (keyword.Length >= 1)
            {
                var suggestions = DealerAgent.Where(a => a.No_.Contains(keyword)).ToList();
                lstConnectionMaster1Code.ItemsSource = suggestions.Select(a => a.No_);
                lstConnectionMaster1Code.IsVisible = true;
            }
        }

        private void lstConnectionMaster1Code_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionMaster1Code.Text = lstConnectionMaster1Code.SelectedItem.ToString();
            lstConnectionMaster1Code.IsVisible = false;
            sbConnectionMaster1Name.Text = DealerAgent.Where(a => a.No_ == sbConnectionMaster1Code.Text).FirstOrDefault().Name;
            lstConnectionMaster1Name.IsVisible = false;
        }

        private void sbConnectionMaster2Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionMaster2Name.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = DealerAgent.Where(a => a.Name.Contains(keyword)).ToList();
                lstConnectionMaster2Name.ItemsSource = suggestions.Select(a => a.Name);
                lstConnectionMaster2Name.IsVisible = true;
            }
        }

        private void lstConnectionMaster2Name_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionMaster2Name.Text = lstConnectionMaster2Name.SelectedItem.ToString();
            lstConnectionMaster2Name.IsVisible = false;
            sbConnectionMaster2Code.Text = DealerAgent.Where(a => a.Name == sbConnectionMaster2Name.Text).FirstOrDefault().No_;
            lstConnectionMaster2Code.IsVisible = false;
        }

        private void sbConnectionMaster2Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionMaster2Code.Text.ToUpper();
            if (keyword.Length >= 1)
            {
                var suggestions = DealerAgent.Where(a => a.No_.Contains(keyword)).ToList();
                lstConnectionMaster2Code.ItemsSource = suggestions.Select(a => a.No_);
                lstConnectionMaster2Code.IsVisible = true;
            }
        }

        private void lstConnectionMaster2Code_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionMaster2Code.Text = lstConnectionMaster2Code.SelectedItem.ToString();
            lstConnectionMaster2Code.IsVisible = false;
            sbConnectionMaster2Name.Text = DealerAgent.Where(a => a.No_ == sbConnectionMaster2Code.Text).FirstOrDefault().Name;
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
                var suggestions = DealerAgent.Where(a => a.Name.Contains(keyword)).ToList();
                lstConnectionSubMaster1Name.ItemsSource = suggestions.Select(a => a.Name);
                lstConnectionSubMaster1Name.IsVisible = false;
            }
        }

        private void lstConnectionSubMaster1Name_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionSubMaster1Name.Text = lstConnectionSubMaster1Name.SelectedItem.ToString();
            lstConnectionSubMaster1Name.IsVisible = false;
            sbConnectionSubMaster1Code.Text = DealerAgent.Where(a => a.Name == sbConnectionSubMaster1Name.Text).FirstOrDefault().No_;
            lstConnectionSubMaster1Code.IsVisible = false;
        }

        private void sbConnectionSubMaster1Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionSubMaster1Code.Text.ToUpper();
            if (keyword.Length >= 1)
            {
                var suggestions = DealerAgent.Where(a => a.No_.Contains(keyword)).ToList();
                lstConnectionSubMaster1Code.ItemsSource = suggestions.Select(a => a.No_);
                lstConnectionSubMaster1Code.IsVisible = false;
            }
        }

        private void lstConnectionSubMaster1Code_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionSubMaster1Code.Text = lstConnectionSubMaster1Code.SelectedItem.ToString();
            lstConnectionSubMaster1Code.IsVisible = false;
            sbConnectionSubMaster1Name.Text = DealerAgent.Where(a => a.No_ == sbConnectionSubMaster1Code.Text).FirstOrDefault().Name;
            lstConnectionSubMaster1Name.IsVisible = false;
        }

        private void sbConnectionSubMaster2Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionSubMaster2Name.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = DealerAgent.Where(a => a.Name.Contains(keyword)).ToList();
                lstConnectionSubMaster2Name.ItemsSource = suggestions.Select(a => a.Name);
                lstConnectionSubMaster2Name.IsVisible = false;
            }
        }

        private void lstConnectionSubMaster2Name_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionSubMaster2Name.Text = lstConnectionSubMaster2Name.SelectedItem.ToString();
            lstConnectionSubMaster2Name.IsVisible = false;
            sbConnectionSubMaster2Code.Text = DealerAgent.Where(a => a.Name == sbConnectionSubMaster1Name.Text).FirstOrDefault().No_;
            lstConnectionSubMaster2Code.IsVisible = false;
        }

        private void sbConnectionSubMaster2Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbConnectionSubMaster2Code.Text.ToUpper();
            if (keyword.Length >= 1)
            {
                var suggestions = DealerAgent.Where(a => a.No_.Contains(keyword)).ToList();
                lstConnectionSubMaster2Code.ItemsSource = suggestions.Select(a => a.No_);
                lstConnectionSubMaster2Code.IsVisible = false;
            }
        }

        private void lstConnectionSubMaster2Code_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbConnectionSubMaster2Code.Text = lstConnectionSubMaster2Code.SelectedItem.ToString();
            lstConnectionSubMaster2Code.IsVisible = false;
            sbConnectionSubMaster2Name.Text = DealerAgent.Where(a => a.No_ == sbConnectionSubMaster2Code.Text).FirstOrDefault().Name;
            lstConnectionSubMaster2Name.IsVisible = false;
        }

        private void sbSubMasAdvisorType1Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sbSubMasAdvisorType1Name.Text;
            if (keyword.Length >= 1)
            {
                var suggestions = Customer.Where(a => a.name_cus.Contains(keyword)).ToList();
                lstSubMasAdvisorType1Name.ItemsSource = suggestions.Select(a => a.name_cus);
                lstSubMasAdvisorType1Name.IsVisible = false;
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
                lstSubMasAdvisorType1Appno.IsVisible = false;
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
                lstSubMasAdvisorType3EmNo.IsVisible = false;
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
                lstSubMasAdvisorType3Name.IsVisible = false;
            }
        }

        private void lstSubMasAdvisorType3Name_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sbSubMasAdvisorType3Name.Text = lstSubMasAdvisorType3Name.SelectedItem.ToString();
            lstSubMasAdvisorType3Name.IsVisible = false;
            sbSubMasAdvisorType3EmNo.Text = Employee.Where(a => a.personnel_name == sbSubMasAdvisorType3Name.Text).FirstOrDefault().personnel_code;
            lstSubMasAdvisorType3EmNo.IsVisible = false;
        }

        private void enMasHeadPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
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

        public List<DataPicture> IMAGE_GET(SentToAPI mMo)
        {
            Appz_WebService iAPI = new Appz_WebService();
            var response = iAPI.Sent(mMo);
            if (response != null)
            {

                var posts = JsonConvert.DeserializeObject<List<DataPicture>>(response.Content);
                return posts;
            }
            else return null;
        }


        public SentToAPI GETCARDETAIL()
        {
            SentToAPI mMo = new SentToAPI();
            mMo.personnel_code = _userlogin.data.First().personnel_code;
            mMo.storename = "ST_IMAGE_GET";
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

        public SentToAPI GETCARDETAILALL()
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