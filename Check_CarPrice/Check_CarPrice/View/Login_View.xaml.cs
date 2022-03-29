
using Check_CarPrice.Persistence;
using Check_CarPrice.WebService;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.Login_Model;

namespace Check_CarPrice.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login_View : ContentPage
    {
        
        Exception _error = null;

        private SQLiteAsyncConnection _connection;
        private ObservableCollection<DataAccessories> _accessories;
        private ObservableCollection<DataCampaign> _campaign;
        private ObservableCollection<DataCarPolicy> _policy;
        private ObservableCollection<DataProvince> _province;
        private ObservableCollection<DataUseCarType> _usecartype;
        private ObservableCollection<DataBrand> _brand;
        private ObservableCollection<DataCharacterCar> _charactercar;
        private ObservableCollection<DataTypeCar> _typecar;
        private ObservableCollection<DataStatus> _status;
        private ObservableCollection<LoginData> _userlogin;
        private ObservableCollection<DataVersion> _version;


       


        protected override bool OnBackButtonPressed() => true;


        public Login_View()
        {
            InitializeComponent();
           
        }

        public async Task CheckVersion(User user)
        {

            try
            {
                var version = $"{VersionTracking.CurrentVersion}";

                Appz_WebService iapi = new Appz_WebService();
                _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
                await _connection.CreateTableAsync<DataVersion>();
                var ver = await _connection.Table<DataVersion>().ToListAsync();
                _version = new ObservableCollection<DataVersion>(ver);

               
                DataVersion dta = new DataVersion();
                dta.system_version = user.role.version.system_version;
                dta.data_version = user.role.version.data_version;
                dta.createdate = DateTime.Now;

                if (_version.Count() == 0)
                {
                    await LoadData();

                    SQLDelete("DataVersion");
                    var ver1 = await _connection.Table<DataVersion>().ToListAsync();
                    _version = new ObservableCollection<DataVersion>(ver1);

                    await _connection.InsertAsync(dta);
                    _version.Add((DataVersion)dta);
                }
                else
                {
                    if(user.role.version.system_version!=_version.Select(a=>a.system_version).LastOrDefault()||
                        user.role.version.data_version != _version.Select(a => a.data_version).LastOrDefault())
                    {
                        DeleteData();
                        await LoadData();

                        SQLDelete("DataVersion");
                        var ver1 = await _connection.Table<DataVersion>().ToListAsync();
                        _version = new ObservableCollection<DataVersion>(ver1);

                        await _connection.InsertAsync(dta);
                        _version.Add((DataVersion)dta);
                    }
                    else
                    {
                        await LoadData();
                        await _connection.InsertAsync(dta);
                    }
                }


                #region
                //await _connection.CreateTableAsync<DataAccessories>();
                //var acc = await _connection.Table<DataAccessories>().ToListAsync();
                //_accessories = new ObservableCollection<DataAccessories>(acc);
                //if (_accessories.Count() == 0)
                //{
                //    var dataAcces = iapi.GetDataAccessories();
                //    foreach (object data in dataAcces)
                //    {
                //        await _connection.InsertAsync(data);
                //        _accessories.Add((DataAccessories)data);
                //    }
                //}

                //await _connection.CreateTableAsync<DataCampaign>();
                //var cam = await _connection.Table<DataCampaign>().ToListAsync();
                //_campaign = new ObservableCollection<DataCampaign>(cam);
                //if (_campaign.Count() == 0)
                //{
                //    var dataCam = iapi.GetCampaign();
                //    foreach (object data in dataCam)
                //    {
                //        await _connection.InsertAsync(data);
                //        _campaign.Add((DataCampaign)data);
                //    }
                //}

                //await _connection.CreateTableAsync<DataCarPolicy>();
                //var policy = await _connection.Table<DataCarPolicy>().ToListAsync();
                //_policy = new ObservableCollection<DataCarPolicy>(policy);
                //if (_policy.Count() == 0)
                //{
                //    var dataPolicy = iapi.GetDataCarPolicy();
                //    foreach (object data in dataPolicy)
                //    {
                //        await _connection.InsertAsync(data);
                //        _policy.Add((DataCarPolicy)data);
                //    }
                //}

                //await _connection.CreateTableAsync<DataProvince>();
                //var province = await _connection.Table<DataProvince>().ToListAsync();
                //_province = new ObservableCollection<DataProvince>(province);
                //if (_province.Count() == 0)
                //{
                //    var dataProvince = iapi.GetProvince();
                //    foreach (object data in dataProvince)
                //    {
                //        await _connection.InsertAsync(data);
                //        _province.Add((DataProvince)data);
                //    }
                //}


                //await _connection.CreateTableAsync<DataUseCarType>();
                //var usecartype = await _connection.Table<DataUseCarType>().ToListAsync();
                //_usecartype = new ObservableCollection<DataUseCarType>(usecartype);
                //if (_usecartype.Count() == 0)
                //{
                //    var dataUsecartype = iapi.GetDataUseCarType();
                //    foreach (object data in dataUsecartype)
                //    {
                //        await _connection.InsertAsync(data);
                //        _usecartype.Add((DataUseCarType)data);
                //    }
                //}

                //await _connection.CreateTableAsync<DataBrand>();
                //var brand = await _connection.Table<DataBrand>().ToListAsync();
                //_brand = new ObservableCollection<DataBrand>(brand);
                //if (_brand.Count() == 0)
                //{
                //    var dataBrand = iapi.GetBrand();
                //    foreach (object data in dataBrand)
                //    {
                //        await _connection.InsertAsync(data);
                //        _brand.Add((DataBrand)data);
                //    }
                //}

                //await _connection.CreateTableAsync<DataCharacterCar>();
                //var charac = await _connection.Table<DataCharacterCar>().ToListAsync();
                //_charactercar = new ObservableCollection<DataCharacterCar>(charac);
                //if (_charactercar.Count() == 0)
                //{
                //    var dataCharac = iapi.GetDataCharacterCar();
                //    foreach (object data in dataCharac)
                //    {
                //        await _connection.InsertAsync(data);
                //        _charactercar.Add((DataCharacterCar)data);
                //    }
                //}

                //await _connection.CreateTableAsync<DataTypeCar>();
                //var typecarr = await _connection.Table<DataTypeCar>().ToListAsync();
                //_typecar = new ObservableCollection<DataTypeCar>(typecarr);
                //if (_typecar.Count() == 0)
                //{
                //    var dataTypecar = iapi.GetTypeCar();
                //    foreach (object data in dataTypecar)
                //    {
                //        await _connection.InsertAsync(data);
                //        _typecar.Add((DataTypeCar)data);
                //    }
                //}

                //await _connection.CreateTableAsync<DataStatus>();
                //var statuss = await _connection.Table<DataStatus>().ToListAsync();
                //_status = new ObservableCollection<DataStatus>(statuss);
                //if (_status.Count() == 0)
                //{
                //    var dataStatus = iapi.GetStatus();
                //    foreach (object data in dataStatus)
                //    {
                //        await _connection.InsertAsync(data);

                //        _status.Add((DataStatus)data);
                //    }
                //}

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
                //}

                #endregion

            }
            catch (Exception ex)
            {
                await DisplayAlert("แจ้งเตือน", "โหลดข้อมูลเบื้องต้นไม่สำเร็จ", "ตกลง");
                _error = ex;
            }
        }

        public async void SQLDelete(string table)
        {
            if (table.ToList().Count != 0)
            {
                string sql = $"DELETE FROM {table}";
                await _connection.ExecuteAsync(sql);
            }

        }

        public async void DeleteData()
        {
            
            SQLDelete("DataAccessories");
            var acc = await _connection.Table<DataAccessories>().ToListAsync();
            _accessories = new ObservableCollection<DataAccessories>(acc);

            SQLDelete("DataCampaign");
            var cam = await _connection.Table<DataCampaign>().ToListAsync();
            _campaign = new ObservableCollection<DataCampaign>(cam);

            SQLDelete("DataCarPolicy");
            var po = await _connection.Table<DataCarPolicy>().ToListAsync();
            _policy = new ObservableCollection<DataCarPolicy>(po);

            SQLDelete("DataProvince");
            var pro = await _connection.Table<DataProvince>().ToListAsync();
            _province = new ObservableCollection<DataProvince>(pro);

            SQLDelete("DataUseCarType");
            var use = await _connection.Table<DataUseCarType>().ToListAsync();
            _usecartype = new ObservableCollection<DataUseCarType>(use);

            SQLDelete("DataBrand");
            var brand = await _connection.Table<DataBrand>().ToListAsync();
            _brand = new ObservableCollection<DataBrand>(brand);

            SQLDelete("DataCharacterCar");
            var cha = await _connection.Table<DataCharacterCar>().ToListAsync();
            _charactercar = new ObservableCollection<DataCharacterCar>(cha);

            SQLDelete("DataTypeCar");
            var type = await _connection.Table<DataTypeCar>().ToListAsync();
            _typecar = new ObservableCollection<DataTypeCar>(type);

            SQLDelete("DataStatus");
            var sta = await _connection.Table<DataStatus>().ToListAsync();
            _status = new ObservableCollection<DataStatus>(sta);

        }

        public async Task LoadData()
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
                var datar = iapi.GetCampaign();
                if (datar != null)
                {
                    var dataCam = datar.Where(a => a.status == "enable").ToList();
                    foreach (object data in dataCam)
                    {
                        await _connection.InsertAsync(data);
                        _campaign.Add((DataCampaign)data);
                    }
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

                var dataProvince = GetDataProvince();
                foreach (object data in dataProvince)
                {
                    await _connection.InsertAsync(data);
                    _province.Add((DataProvince)data);
                }
               
                //var dataProvince = iapi.GetProvince();
                //foreach (object data in dataProvince)
                //{
                //    await _connection.InsertAsync(data);
                //    _province.Add((DataProvince)data);
                //}
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



        }


        private async void btnlogin_Clicked(object sender, EventArgs e)
        {
            Appz_WebService lWeb = new Appz_WebService();
            PopupLoad.IsVisible = true;
            PageLogin.Opacity = 0.5;
            await Task.Delay(2000);

            var error = Validation();
            if (error != "") await DisplayAlert("ผิดพลาด", error, "OK");
            else
            {
                var data = lWeb.PostUserLogin(txtusername.Text.Trim(), txtpassword.Text.Trim(), "S0010");
                if (data == null)
                {
                    await DisplayAlert("ผิดพลาด", "กรอก Username หรือ Password ผิด กรุณาลองใหม่", "ตกลง");
                    PageLogin.Opacity = 1.0;
                }
                else if (data.role.func.Count() == 0)
                {
                    await DisplayAlert("ผิดพลาด", "ท่านไม่มีสิทธิเข้าใช้งาน", "ตกลง");
                    PageLogin.Opacity = 1.0;
                }
                else
                {
                    //CreateUserTable(data);
                    //var errorr = CheckData();
                    //if (errorr != "")
                    //{
                    //    await DisplayAlert("ผิดพลาด", errorr, "ตกลง");
                    //}
                    //else
                    //{
                        await CheckVersion(data);
                        Clear();
                        await Navigation.PushAsync(new MainPage(data));
                    //}

                }
            }
            PopupLoad.IsVisible = false;
        }


        public string Validation()
        {
            string error = "";
            if (txtusername.Text.Trim() == "") error = "กรุณากรอก Username";
            else if (txtpassword.Text.Trim() == "") error = "กรุณากรอก Password";
            else if (txtusername.Text.Trim() == "" && txtpassword.Text.Trim() == null) error = "กรุณากรอก Username และ Password";

            return error;
        }

        public void Clear()
        {
            txtpassword.Text = "";
            txtusername.Text = "";
        }

        public List<DataProvince> GetDataProvince()
        {
            Appz_WebService iAPI = new Appz_WebService();
            var response = iAPI.Sent(GetProvince());
            if (response != null)
            {

                var posts = JsonConvert.DeserializeObject<List<DataProvince>>(response.Content);
                return posts;
            }
            else return null;
        }

        public SentToAPI GetProvince()
        {
            SentToAPI mMo = new SentToAPI();
          
            mMo.storename = "GETPROVINCE";
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