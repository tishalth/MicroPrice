using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using static Check_CarPrice.Model.CarPriceList_Model;

namespace Check_CarPrice.WebService
{
    public class CarPriceList_WebService
    {
        public RestClient client;

     
        public List<DataTransactionCar> GetDataTransactionCar()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/transaction_car");
            //var _client = ClienUAT("GetDataTransactionCar");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=eabbi6cupaup1fqfl3da0mn4l0");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<TransactionCar>(response.Content);
                return posts.data;
            }
        }

        public List<DataTransactionCar> PostTransactionCar(string status)
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/transaction_car");
            //var _client = ClienUAT("PostTransactionCar");
            // var client = new RestClient("https://dev-microsystem.microleasingplc.com/pj/API_car_price/index.php/Index/transaction_car");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("personnel_code", " val1");
            request.AddHeader("applno_car", " val2");
            request.AddHeader("licno", " val3");
            request.AddHeader("provice", " val4");
            request.AddHeader("engno", " val5");
            request.AddHeader("chasno", " val6");
            request.AddHeader("brand", " val7");
            request.AddHeader("model", " val8");
            request.AddHeader("fuel", " val9");
            request.AddHeader("type_car", " val10");
            request.AddHeader("grp_car", " val11");
            request.AddHeader("cde_pickup", " val12");
            request.AddHeader("cde_special", " val13");
            request.AddHeader("head_price", " val14");
            request.AddHeader("estimate_price", " val15");
            request.AddHeader("finamt", " val16");
            request.AddHeader("accessory", " val17");
            request.AddHeader("year_car", " val18");
            request.AddHeader("type_grp_car", " val19");
            request.AddHeader("status", " val20");
            request.AddHeader("Cookie", "PHPSESSID=bd9plbejhrtm6gn7utqu99diq5");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("status_approve", status);
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<TransactionCar>(response.Content);
                return posts.data;
            }

        }

        public bool PostDelete(string appid, string createby)
        {
            bool ret = true;
            try
            {
                Appz_WebService iAPI = new Appz_WebService();
                client = iAPI.UAT("pj/API_car_price/index.php/Index/delete_app_car");
                //var _client = ClienUAT("PostDelete");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("personnel_code", " val1");
                request.AddHeader("applno_car", " val2");
                request.AddHeader("licno", " val3");
                request.AddHeader("provice", " val4");
                request.AddHeader("engno", " val5");
                request.AddHeader("chasno", " val6");
                request.AddHeader("brand", " val7");
                request.AddHeader("model", " val8");
                request.AddHeader("fuel", " val9");
                request.AddHeader("type_car", " val10");
                request.AddHeader("grp_car", " val11");
                request.AddHeader("cde_pickup", " val12");
                request.AddHeader("cde_special", " val13");
                request.AddHeader("head_price", " val14");
                request.AddHeader("estimate_price", " val15");
                request.AddHeader("finamt", " val16");
                request.AddHeader("accessory", " val17");
                request.AddHeader("year_car", " val18");
                request.AddHeader("type_grp_car", " val19");
                request.AddHeader("status", " val20");
                request.AddHeader("Cookie", "PHPSESSID=k889fl0nrgoo9ca3pea1gabgp0");
                request.AlwaysMultipartFormData = true;
                request.AddParameter("app_id", appid);
                request.AddParameter("create_by", createby);
                IRestResponse response = client.Execute(request);

                ret = true;
            }
            catch
            {
                ret = false;
            }

            return ret;

        }


    }
}
