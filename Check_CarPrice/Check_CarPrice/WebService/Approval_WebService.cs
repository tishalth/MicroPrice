using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;


namespace Check_CarPrice.WebService
{
    public class Approval_WebService
    {
        public RestClient client;

        public bool UpdateTransactionApprove(string appid, string status_appcar, string create_by)
        {
            bool ret = true;
            try
            {
                Appz_WebService iAPI = new Appz_WebService();
                client = iAPI.UAT("pj/API_car_price/index.php/Index/update_transaction_approve");
                //var _client = ClienUAT("UpdateTransactionApprove");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("app_id", appid);
                request.AddParameter("status_appcar", status_appcar);
                request.AddParameter("create_by", create_by);
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
