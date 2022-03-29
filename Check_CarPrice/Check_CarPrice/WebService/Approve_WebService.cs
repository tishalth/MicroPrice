using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using static Check_CarPrice.Model.Approve_Model;

namespace Check_CarPrice.WebService
{
    public class Approve_WebService
    {
        public RestClient client;
      
        public List<DataStyleCar> GetDataStyleCar()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_StyleCar");
            //var _client = ClienUAT("GetDataStyleCar");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=ucims90r3eamast92vfdk3hbi4");
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<StyleCar>(response.Content);
                return posts.data;
            }
        }
    }
}
