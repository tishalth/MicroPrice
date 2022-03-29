using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.Check_Model;

namespace Check_CarPrice.WebService
{
    public class Check_WebService
    {
        public RestClient client;
        Exception _error = null;
        

        public List<DataInformationCar> PostDataInformationCar(string id)
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/select_app_id");
            //var client = ClienUAT("PostDataInformationCar");
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
            request.AddHeader("Cookie", "PHPSESSID=0b3eq8k4aacpp2u6a817biail4");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("table", "information_car");
            request.AddParameter("app_id", id);
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<InformationCar>(response.Content);
                return posts.data;
            }
        }

        public List<DataTransactionCar> PostDataTransactionCar(string id)
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/select_app_id");
            //var client = ClienUAT("PostDataTransactionCar");
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
            request.AddHeader("Cookie", "PHPSESSID=0b3eq8k4aacpp2u6a817biail4");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("table", "information_transaction_car");
            request.AddParameter("app_id", id);
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

        public List<DataTypeCredit> PostDataTypeCredit()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/USEDCARTYP");
            //var client = ClienUAT("PostDataTypeCredit");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=fa9q38u0pnl00e6ub8bmo6ovo3");
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<TypeCredit>(response.Content);
                return posts.data;
            }

        }

        public List<DataEmployee> GetEmployee()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/Employee");
            //var client = ClienUAT("GetEmployee");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=0b3eq8k4aacpp2u6a817biail4");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("cde_head_tail", "01");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<Employee>(response.Content);
                return posts.data;
            }

        }

        public List<DataStatus> GetStatus()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/status_appcar");
            //var client = ClienUAT("GetStatus");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=n73lnk76ib3rvg27ng147sqds5");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<Status>(response.Content);
                return posts.data;
            }

        }

        public List<DataTransactionApprove> GetTransactionApprove()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/transaction_approve");
            //var client = ClienUAT("GetTransactionApprove");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=204u70figvj9f07fmnv9cp1og0");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<TransactionApprove>(response.Content);
                return posts.data;
            }

        }

        //public List<DataCarPolicy> GetDataCarPolicy()
        //{
        //    Appz_WebService iAPI = new Appz_WebService();
        //    client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/car_policy");
        //    //var client = ClienUAT("GetDataCarPolicy");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("Cookie", "PHPSESSID=tp7drgnu6jh0tgnmcsa3seahs7");
        //    IRestResponse response = client.Execute(request);
        //    if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        var posts = JsonConvert.DeserializeObject<CarPolicy>(response.Content);
        //        return posts.data;
        //    }

        //}

        public bool PostDataInformationCodeCar(DataInformationCodeCar data)
        {
            bool ret = true;
            try
            {
                Appz_WebService iAPI = new Appz_WebService();
                client = iAPI.UAT("pj/API_car_price/index.php/Index/price_for_use");
                //var client = ClienUAT("PostDataInformationCodeCar");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Cookie", "PHPSESSID=7l7l8h3kr8uifpqdfj5ekpef23");
                request.AlwaysMultipartFormData = true;
                request.AddParameter("cde_car", data.cde_car);
                request.AddParameter("app_id", data.app_id);
                request.AddParameter("middle_price", data.middle_price);
                request.AddParameter("Distance_01", data.Distance_01);
                request.AddParameter("Distance_02", data.Distance_02);
                request.AddParameter("rate_01", data.rate_01);
                request.AddParameter("rate_02", data.rate_02);
                request.AddParameter("remark", data.remark);
                request.AddParameter("create_by", data.create_by);
                request.AddParameter("status_appcar", data.status_appcar);
                request.AddParameter("finamt", data.finamt);
                request.AddParameter("model", data.model);
                request.AddParameter("cde_policy", data.cde_policy);
                request.AddParameter("year_car", data.year_car);
                request.AddParameter("type_car", data.type_car);
                IRestResponse response = client.Execute(request);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex;
            }
            return ret;
        }


        public List<DataInformationCodeCar> GetDataInformationCodeCar()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/information_codecar");
            //var client = ClienUAT("GetDataInformationCodeCar");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=g6vcf2jg8936hok00ujv4q72l7");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<InformationCodeCar>(response.Content);
                return posts.data;
            }
        }

        public List<DataInformationCar> GetDataInformationCar()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/information_car");
            //var client = ClienUAT("GetDataInformationCar");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=g6vcf2jg8936hok00ujv4q72l7");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<InformationCar>(response.Content);
                return posts.data;
            }
        }

        public List<DataGroupCar> GetDataGroupCar()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/group_car");
            //var client = ClienUAT("GetDataGroupCar");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=uu65ff9c3i6786ncn8p95et2m1");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<GroupCar>(response.Content);
                return posts.data;
            }
        }

        public List<DataStyleCar> GetDataStyleCar()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_StyleCar");
            //var client = ClienUAT("GetDataStyleCar");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=uu65ff9c3i6786ncn8p95et2m1");
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

        public List<DataTypeCar> GetDataTypeCar()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/type_car");
            //var client = ClienUAT("GetDataTypeCar");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=uu65ff9c3i6786ncn8p95et2m1");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<TypeCar>(response.Content);
                return posts.data;
            }
        }

        public List<Model.Appz_Model.DataAccessories> GetDataAccessories()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/accessory");
            //var client = ClienUAT("GetDataAccessories");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=uu65ff9c3i6786ncn8p95et2m1");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<Model.Appz_Model.Accessories>(response.Content);
                return posts.data;
            }
        }

        //public List<DataCharacterCar> GetDataCharacterCar()
        //{
        //    Appz_WebService iAPI = new Appz_WebService();
        //    client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_CharacterCar");
        //    //var client = ClienUAT("GetDataAccessories");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("Cookie", "PHPSESSID=ppkn5rhs6hg3dr51rv4art4455");
        //    IRestResponse response = client.Execute(request);
        //    if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        var posts = JsonConvert.DeserializeObject<CharacterCar>(response.Content);
        //        return posts.data;
        //    }
        //}
    }
}
