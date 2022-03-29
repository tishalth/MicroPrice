using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using static Check_CarPrice.Model.Login_Model;

namespace Check_CarPrice.WebService
{
    public class Login_WebService
    {
        public RestClient client;

        //public IRestResponse PostLogin(PostLogin dta)
        //{
        //    Appz_WebService iAPI = new Appz_WebService();
        //    client = iAPI.UAT("pj/API_car_price/index.php/Index/api_login_check_price");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("personnel_code", " val1");
        //    request.AddHeader("applno_car", " val2");
        //    request.AddHeader("licno", " val3");
        //    request.AddHeader("provice", " val4");
        //    request.AddHeader("engno", " val5");
        //    request.AddHeader("chasno", " val6");
        //    request.AddHeader("brand", " val7");
        //    request.AddHeader("model", " val8");
        //    request.AddHeader("fuel", " val9");
        //    request.AddHeader("type_car", " val10");
        //    request.AddHeader("grp_car", " val11");
        //    request.AddHeader("cde_pickup", " val12");
        //    request.AddHeader("cde_special", " val13");
        //    request.AddHeader("head_price", " val14");
        //    request.AddHeader("estimate_price", " val15");
        //    request.AddHeader("finamt", " val16");
        //    request.AddHeader("accessory", " val17");
        //    request.AddHeader("year_car", " val18");
        //    request.AddHeader("type_grp_car", " val19");
        //    request.AddHeader("status", " val20");
        //    request.AddHeader("Cookie", "PHPSESSID=m01r9adp2v7k8bhprm3keo8pg0");
        //    request.AlwaysMultipartFormData = true;
        //    request.AddParameter("username", dta.username);
        //    request.AddParameter("password", dta.password);
        //    IRestResponse response = client.Execute(request);

        //    return response;
        //}
    }
}
