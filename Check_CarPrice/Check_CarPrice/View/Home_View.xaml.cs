using Check_CarPrice.WebService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.Dashboard_Models;
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
            var personalcode = _userlogin.data.Select(b => b.personnel_code).FirstOrDefault();
            var func = _userlogin.role.func.Where(a=>a.func_id== "F0010" || a.func_id == "F0007" || a.func_id == "F0008" || a.func_id == "F0009").Select(a=>a.func_id).FirstOrDefault();
            var branch = _userlogin.data.Select(b => b.branch_no).FirstOrDefault();
            var area = _userlogin.data.Select(b => b.area_no).FirstOrDefault();
            SentToAPI mMo = new SentToAPI();
            mMo.personnel_code = _userlogin.data.First().personnel_code;
            mMo.storename = "ST_DASHBOARD_COUNT";
            mMo.c1 = func;
            mMo.c2 = func=="F0010"? personalcode : func=="F0007"?branch:func=="F0008"?area:"";
            var data = GETDATSDASHBOARD(mMo);
            if (data != null)
            {
                lbCarList.Text = data.Select(a=>a.ALL).FirstOrDefault().ToString();
                lbAddCarRequest.Text = data.Select(a => a.REQUEST).FirstOrDefault().ToString();
                lbWaitApprove.Text = data.Select(a => a.WAITAPPROVE).FirstOrDefault().ToString();
                lbApproved.Text = data.Select(a => a.APPROVED).FirstOrDefault().ToString();
                lbNotApprove.Text = data.Select(a => a.NOTAPPROVED).FirstOrDefault().ToString();
                lbUpdateList.Text = data.Select(a => a.UPDATELIST).FirstOrDefault().ToString();
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
           
            //var data = iAPI.GetDataDashboardView();
            //if (data != null)
            //{
            //    if (_userlogin.role.func.Where(a => a.func_id == "F0010").Count() != 0) //staff
            //    {

            //        //var datacount = data.Where(a => a.branch_code == _userlogin.data.Select(b => b.branch_no).FirstOrDefault() && a.create_by == _userlogin.data.Select(b => b.personnel_code).FirstOrDefault()).ToList();

            //        var datacount = data.Where(a => a.create_by == _userlogin.data.Select(b => b.personnel_code).FirstOrDefault()).ToList();
            //        if (datacount != null)
            //        {
            //            lbCarList.Text = datacount.Count().ToString();
            //            lbAddCarRequest.Text = datacount.Where(a => a.status_appcar == "1").Count().ToString();
            //            lbWaitApprove.Text = datacount.Where(a => a.status_appcar == "2").Count().ToString();
            //            lbApproved.Text = datacount.Where(a => a.status_appcar == "9").Count().ToString();
            //            lbNotApprove.Text = datacount.Where(a => a.status_appcar == "8").Count().ToString();
            //            lbUpdateList.Text = datacount.Where(a => a.status_appcar == "7").Count().ToString();
            //        }
            //        else
            //        {
            //            lbCarList.Text = "0";
            //            lbAddCarRequest.Text = "0";
            //            lbWaitApprove.Text = "0";
            //            lbApproved.Text = "0";
            //            lbNotApprove.Text = "0";
            //            lbUpdateList.Text = "0";
            //        }
            //    }
            //    else if (_userlogin.role.func.Where(a=>a.func_id== "F0007").Count() != 0) //branch
            //    {

            //        //var datacount = data.Where(a => a.branch_code == _userlogin.data.Select(b => b.branch_no).FirstOrDefault() && a.create_by == _userlogin.data.Select(b => b.personnel_code).FirstOrDefault()).ToList();

            //        var datacount = data.Where(a => a.branch_code == _userlogin.data.Select(b => b.branch_no).FirstOrDefault()).ToList();
            //        if (datacount != null)
            //        {
            //            lbCarList.Text = datacount.Count().ToString();
            //            lbAddCarRequest.Text = datacount.Where(a => a.status_appcar == "1").Count().ToString();
            //            lbWaitApprove.Text = datacount.Where(a => a.status_appcar == "2").Count().ToString();
            //            lbApproved.Text = datacount.Where(a => a.status_appcar == "9").Count().ToString();
            //            lbNotApprove.Text = datacount.Where(a => a.status_appcar == "8").Count().ToString();
            //            lbUpdateList.Text = datacount.Where(a => a.status_appcar == "7").Count().ToString();
            //        }
            //        else
            //        {
            //            lbCarList.Text = "0";
            //            lbAddCarRequest.Text = "0";
            //            lbWaitApprove.Text = "0";
            //            lbApproved.Text = "0";
            //            lbNotApprove.Text = "0";
            //            lbUpdateList.Text = "0";
            //        }
            //    }
            //    else if (_userlogin.role.func.Where(a => a.func_id == "F0008").Count() != 0) //hub
            //    {
            //        var datacount = data.Where(a => a.hub == _userlogin.data.Select(b => b.area_no).FirstOrDefault()).ToList();
            //        if (datacount != null)
            //        {
            //            lbCarList.Text = datacount.Count().ToString();
            //            lbAddCarRequest.Text = datacount.Where(a => a.status_appcar == "1").Count().ToString();
            //            lbWaitApprove.Text = datacount.Where(a => a.status_appcar == "2").Count().ToString();
            //            lbApproved.Text = datacount.Where(a => a.status_appcar == "9").Count().ToString();
            //            lbNotApprove.Text = datacount.Where(a => a.status_appcar == "8").Count().ToString();
            //            lbUpdateList.Text = datacount.Where(a => a.status_appcar == "7").Count().ToString();
            //        }
            //        else
            //        {
            //            lbCarList.Text = "0";
            //            lbAddCarRequest.Text = "0";
            //            lbWaitApprove.Text = "0";
            //            lbApproved.Text = "0";
            //            lbNotApprove.Text = "0";
            //            lbUpdateList.Text = "0";
            //        }
            //    }
            //    else if (_userlogin.role.func.Where(a => a.func_id == "F0009").Count() != 0)  //all
            //    {
            //        var datacount = data.ToList();
            //        if (datacount != null)
            //        {
            //            lbCarList.Text = datacount.Count().ToString();
            //            lbAddCarRequest.Text = datacount.Where(a => a.status_appcar == "1").Count().ToString();
            //            lbWaitApprove.Text = datacount.Where(a => a.status_appcar == "2").Count().ToString();
            //            lbApproved.Text = datacount.Where(a => a.status_appcar == "9").Count().ToString();
            //            lbNotApprove.Text = datacount.Where(a => a.status_appcar == "8").Count().ToString();
            //            lbUpdateList.Text = datacount.Where(a => a.status_appcar == "7").Count().ToString();
            //        }
            //        else
            //        {
            //            lbCarList.Text = "0";
            //            lbAddCarRequest.Text = "0";
            //            lbWaitApprove.Text = "0";
            //            lbApproved.Text = "0";
            //            lbNotApprove.Text = "0";
            //            lbUpdateList.Text = "0";
            //        }
            //    }
            //    else
            //    {
            //        lbCarList.Text = "0";
            //        lbAddCarRequest.Text = "0";
            //        lbWaitApprove.Text = "0";
            //        lbApproved.Text = "0";
            //        lbNotApprove.Text = "0";
            //        lbUpdateList.Text = "0";
            //    }
            //}
            //else
            //{
            //    lbCarList.Text = "0";
            //    lbAddCarRequest.Text = "0";
            //    lbWaitApprove.Text = "0";
            //    lbApproved.Text = "0";
            //    lbNotApprove.Text = "0";
            //    lbUpdateList.Text = "0";
            //}
        }


        public List<DASHBOARD> GETDATSDASHBOARD(SentToAPI mMo)
        {
            Appz_WebService iAPI = new Appz_WebService();
            var response = iAPI.Sent(mMo);
            if (response != null)
            {

                var posts = JsonConvert.DeserializeObject<List<DASHBOARD>>(response.Content);
                return posts;
            }
            else return null;
        }



    }
}
