using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using static Check_CarPrice.Model.Dashboard_Models;

namespace Check_CarPrice.WebService
{
   public class Dashboard_WebService
    {
        public RestClient client;
        Exception _error = null;
        public List<DataDashboardView> GetDataDashboardView()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_DASHBOARDVIEW");
            //var client = ClienUAT("GetDataAccessories");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cookie", "PHPSESSID=99ik14vetgr52noc6cpi0tqbs0");
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<DashboardView>(response.Content);
                return posts.data;
            }
        }
    }
}
