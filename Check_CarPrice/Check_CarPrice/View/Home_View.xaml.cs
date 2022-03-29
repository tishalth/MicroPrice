using Check_CarPrice.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.Login_Model;

namespace Check_CarPrice.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home_View : ContentPage
    {
        private User _userlogin;
        
        protected override bool OnBackButtonPressed() => true;
        public Home_View(User user)
        {
            _userlogin = user;
            InitializeComponent();
            SetData();
        }

        private void SetData()
        {
            Dashboard_WebService iAPI = new Dashboard_WebService();
            var data = iAPI.GetDataDashboardView();
            if (data != null)
            {
                if (_userlogin.role.func.Where(a => a.func_id == "F0010").Count() != 0) //staff
                {

                    //var datacount = data.Where(a => a.branch_code == _userlogin.data.Select(b => b.branch_no).FirstOrDefault() && a.create_by == _userlogin.data.Select(b => b.personnel_code).FirstOrDefault()).ToList();

                    var datacount = data.Where(a => a.create_by == _userlogin.data.Select(b => b.personnel_code).FirstOrDefault()).ToList();
                    if (datacount != null)
                    {
                        lbCarList.Text = datacount.Count().ToString();
                        lbAddCarRequest.Text = datacount.Where(a => a.status_appcar == "1").Count().ToString();
                        lbWaitApprove.Text = datacount.Where(a => a.status_appcar == "2").Count().ToString();
                        lbApproved.Text = datacount.Where(a => a.status_appcar == "9").Count().ToString();
                        lbNotApprove.Text = datacount.Where(a => a.status_appcar == "8").Count().ToString();
                        lbUpdateList.Text = datacount.Where(a => a.status_appcar == "7").Count().ToString();
                    }
                    else
                    {
                        lbCarList.Text = "0";
                        lbAddCarRequest.Text = "0";
                        lbWaitApprove.Text = "0";
                        lbApproved.Text = "0";
                        lbNotApprove.Text = "0";
                        lbUpdateList.Text = "0";
                    }
                }
                else if (_userlogin.role.func.Where(a=>a.func_id== "F0007").Count() != 0) //branch
                {
                    
                    //var datacount = data.Where(a => a.branch_code == _userlogin.data.Select(b => b.branch_no).FirstOrDefault() && a.create_by == _userlogin.data.Select(b => b.personnel_code).FirstOrDefault()).ToList();

                    var datacount = data.Where(a => a.branch_code == _userlogin.data.Select(b => b.branch_no).FirstOrDefault()).ToList();
                    if (datacount != null)
                    {
                        lbCarList.Text = datacount.Count().ToString();
                        lbAddCarRequest.Text = datacount.Where(a => a.status_appcar == "1").Count().ToString();
                        lbWaitApprove.Text = datacount.Where(a => a.status_appcar == "2").Count().ToString();
                        lbApproved.Text = datacount.Where(a => a.status_appcar == "9").Count().ToString();
                        lbNotApprove.Text = datacount.Where(a => a.status_appcar == "8").Count().ToString();
                        lbUpdateList.Text = datacount.Where(a => a.status_appcar == "7").Count().ToString();
                    }
                    else
                    {
                        lbCarList.Text = "0";
                        lbAddCarRequest.Text = "0";
                        lbWaitApprove.Text = "0";
                        lbApproved.Text = "0";
                        lbNotApprove.Text = "0";
                        lbUpdateList.Text = "0";
                    }
                }
                else if (_userlogin.role.func.Where(a => a.func_id == "F0008").Count() != 0) //hub
                {
                    var datacount = data.Where(a => a.hub == _userlogin.data.Select(b => b.area_no).FirstOrDefault()).ToList();
                    if (datacount != null)
                    {
                        lbCarList.Text = datacount.Count().ToString();
                        lbAddCarRequest.Text = datacount.Where(a => a.status_appcar == "1").Count().ToString();
                        lbWaitApprove.Text = datacount.Where(a => a.status_appcar == "2").Count().ToString();
                        lbApproved.Text = datacount.Where(a => a.status_appcar == "9").Count().ToString();
                        lbNotApprove.Text = datacount.Where(a => a.status_appcar == "8").Count().ToString();
                        lbUpdateList.Text = datacount.Where(a => a.status_appcar == "7").Count().ToString();
                    }
                    else
                    {
                        lbCarList.Text = "0";
                        lbAddCarRequest.Text = "0";
                        lbWaitApprove.Text = "0";
                        lbApproved.Text = "0";
                        lbNotApprove.Text = "0";
                        lbUpdateList.Text = "0";
                    }
                }
                else if (_userlogin.role.func.Where(a => a.func_id == "F0009").Count() != 0)  //all
                {
                    var datacount = data.ToList();
                    if (datacount != null)
                    {
                        lbCarList.Text = datacount.Count().ToString();
                        lbAddCarRequest.Text = datacount.Where(a => a.status_appcar == "1").Count().ToString();
                        lbWaitApprove.Text = datacount.Where(a => a.status_appcar == "2").Count().ToString();
                        lbApproved.Text = datacount.Where(a => a.status_appcar == "9").Count().ToString();
                        lbNotApprove.Text = datacount.Where(a => a.status_appcar == "8").Count().ToString();
                        lbUpdateList.Text = datacount.Where(a => a.status_appcar == "7").Count().ToString();
                    }
                    else
                    {
                        lbCarList.Text = "0";
                        lbAddCarRequest.Text = "0";
                        lbWaitApprove.Text = "0";
                        lbApproved.Text = "0";
                        lbNotApprove.Text = "0";
                        lbUpdateList.Text = "0";
                    }
                }
                else
                {
                    lbCarList.Text = "0";
                    lbAddCarRequest.Text = "0";
                    lbWaitApprove.Text = "0";
                    lbApproved.Text = "0";
                    lbNotApprove.Text = "0";
                    lbUpdateList.Text = "0";
                }
            }
            else
            {
                lbCarList.Text = "0";
                lbAddCarRequest.Text = "0";
                lbWaitApprove.Text = "0";
                lbApproved.Text = "0";
                lbNotApprove.Text = "0";
                lbUpdateList.Text = "0";
            }
        }
    }
}
