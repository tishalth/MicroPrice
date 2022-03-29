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
using static Check_CarPrice.Model.Approve_Model;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.Login_Model;

namespace Check_CarPrice.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Approve_View : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private User _userlogin;
        //private ObservableCollection<DataGrantMapUser> _gmapuser;

        protected override bool OnBackButtonPressed() => true;
        //public LoginData _userlogin;
        public Approve_View(User user)
        {
            _userlogin = user;
            InitializeComponent();
            SetData();
            //RoleUser();
        }



        public void SetData()
        {
            //Appz_WebService iapi = new Appz_WebService();
            //if (_userlogin.role.func.Where(a => a.func_id== "F0005"|| a.func_id == "F0006").Count() != 0)    //F0005 = access_verify   , F0006 = AccessApprove
            //{
            var dtas = GetDataApproveList();
            if (dtas != null)
            {
                lstData.ItemsSource = dtas;
            }
            else lstData.ItemsSource = null;

            //var data = iapi.GetDataCarDetail().ToList();
            //if (data != null)
            //{
            //    var dta = data.Where(a => (a.status_appcar == "2" || a.status_appcar == "9") && a.codecar_status == 1).ToList();
            //    if (dta != null)
            //    {

            //        lstData.ItemsSource = dta.OrderByDescending(a => a.createdate_transaction).ToList();

            //    }
            //    else lstData.ItemsSource = null;
            //}
            //else lstData.ItemsSource = null;
            //}
            //else lstData.ItemsSource = null;
        }



        async void lstData_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new Approval_View(_userlogin, e));
        }


        public List<DataApproveList> GetDataApproveList()
        {
            Appz_WebService iAPI = new Appz_WebService();
            var response = iAPI.Sent(ApproveList());
            if (response != null)
            {

                var posts = JsonConvert.DeserializeObject<List<DataApproveList>>(response.Content);
                return posts;
            }
            else return null;
        }

        public SentToAPI ApproveList()
        {
            SentToAPI mMo = new SentToAPI();
            mMo.personnel_code = _userlogin.data.First().personnel_code;
            mMo.storename = "APPROVELIST";
            mMo.c1 = _userlogin.role.func.First().role_id;
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