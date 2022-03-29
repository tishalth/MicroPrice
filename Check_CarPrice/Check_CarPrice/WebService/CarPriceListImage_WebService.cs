using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using static Check_CarPrice.Model.Appz_Model;

namespace Check_CarPrice.WebService
{
    public class CarPriceListImage_WebService
    {
        public RestClient client;
       
        public List<DataImage> GetImage()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_information_image");
            //var _client = ClienUAT("GetImage");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=128jj3k779btllg31r0kj0jm36");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<Image>(response.Content);
                return posts.data;
            }
        }
    }
}
