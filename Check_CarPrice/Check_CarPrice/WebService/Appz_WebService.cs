using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.Login_Model;

namespace Check_CarPrice.WebService
{
    public class Appz_WebService
    {
        public RestClient client;
        Exception _error = null;

        public RestClient UAT(string dta)
        {
            //DEV
            //client = new RestClient("https://dev-microsystem.microleasingplc.com/" + dta);
            //UT
            //client = new RestClient("https://devapi.microleasingplc.com/" + dta);
            //UAT
            client = new RestClient("https://api-uat.microleasingplc.com/" + dta);
            //Production
            //client = new RestClient("https://apip01.microleasingplc.com/" + dta);
            return client;
        }

        //public RestClient UAT(string dta)
        //{
        //    client = new RestClient("https://devapi.microleasingplc.com/" + dta);
        //    return client;
        //}


        #region DEV
        //public RestClient ClienUAT(string Data)
        //{
        //    if (Data == "GetDataCarDetail") client = new RestClient("https://dev-microsystem.microleasingplc.com/pj/API_car_price/index.php/Index/V_table/V_CarDetail");

        //    return client;

        //}
        #endregion

        #region UAT
        //public RestClient ClienUAT(string Data)
        //{
        //    if (Data == "GetDataCarDetail") client = new RestClient("https://devapi.microleasingplc.com/pj/API_car_price/index.php/Index/V_table/V_CarDetail");

        //    return client;

        //}
        #endregion



        public User PostUserLogin(string username,string password,string systemid)
        {
           
            try
            {
                Appz_WebService iAPI = new Appz_WebService();
                client = iAPI.UAT("pj/API_car_price/index.php/Index/user_login");
                //pj/API_car_price/index.php/Index/user_login
               
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Cookie", "PHPSESSID=99ik14vetgr52noc6cpi0tqbs0");
                request.AlwaysMultipartFormData = true;
                request.AddParameter("username", username);
                request.AddParameter("password", password);
                request.AddParameter("system_id", systemid);
                IRestResponse response = client.Execute(request);
                if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
                {
                    return null;
                }
                else
                {
                    var posts = JsonConvert.DeserializeObject<User>(response.Content);
                    return posts;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            

        }


        public List<DataGenAppID> GenAppID(string branchid,string applno,string typegrpcar)
        {
            var client = UAT("pj/API_car_price/index.php/Index/Gen_app_id");
            //var _client = ClienUAT("GetDataCarDetail");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cookie", "PHPSESSID=r0livissl0jrilkgppm9ljlo44");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("branch_code", branchid);
            request.AddParameter("applno_car", applno);
            request.AddParameter("type_grp_car", typegrpcar);
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<GenAppID>(response.Content);
                return posts.data;
            }
        }




        //public List<DataCarDetail> GetDataCarDetail()
        //{
        //    var client = UAT("pj/API_car_price/index.php/Index/V_table/V_CarDetail");
        //    //var _client = ClienUAT("GetDataCarDetail");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("Cookie", "PHPSESSID=6cbfvsutfv0lpj2kkinf1q2d41");
        //    request.AlwaysMultipartFormData = true;
        //    IRestResponse response = client.Execute(request);
        //    if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        var posts = JsonConvert.DeserializeObject<CarDetail>(response.Content);
        //        return posts.data;
        //    }
        //}

        public List<DataInformationTransactionCar> GetDataInformationTransactionCar()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/information_transaction_car");
            //var client = ClienUAT("GetStyle1");
            //var client = new RestClient("https://devapi.microleasingplc.com/pj/API_car_price/index.php/Index/select_group_car?");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=2js0o743qvjjlbpvrcp5h018s1");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<InformationTransactionCar>(response.Content);
                return posts.data;
            }

        }

        public List<DataAccessories> GetDataAccessories()
        {
            try
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
                    var posts = JsonConvert.DeserializeObject<Accessories>(response.Content);
                    return posts.data;
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public List<DataCampaign> GetCampaign()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/campaign");
            //var client = ClienUAT("GetCampaign");
            //var client = new RestClient("https://devapi.microleasingplc.com/pj/API_car_price/index.php/Index/V_table/campaign");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=soehfc5dt1r5s5afe3uc0nkib7");
            IRestResponse response = client.Execute(request);
            var posts = JsonConvert.DeserializeObject<Campaign>(response.Content);
            Console.WriteLine(response.Content);
            return posts.data;
        }

        public List<DataInformationCar> GetDataInformationCar()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/information_car");
            //var client = ClienUAT("GetDataInformationCar");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=drflorbmhn0t32d13adpii7uh6");
            IRestResponse response = client.Execute(request);
            if (response.Content != "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                var posts = JsonConvert.DeserializeObject<InformationCar>(response.Content);
                return posts.data;
            }
            else return null;

            //return posts.data;


        }

        public List<DataProvince> GetProvince()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/province");
            //client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/PROVINCE");
            //var client = ClienUAT("GetProvince");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=5irvsup5aab9tkobvaemerub26");
            IRestResponse response = client.Execute(request);
            var posts = JsonConvert.DeserializeObject<Province>(response.Content);

            return posts.data;
        }
        public List<DataTypeCar> GetTypeCar()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/type_car");
            //var client = ClienUAT("GetTypeCar");
            //var client = new RestClient("https://devapi.microleasingplc.com/pj/API_car_price/index.php/Index/V_table/type_car");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=5irvsup5aab9tkobvaemerub26");
            IRestResponse response = client.Execute(request);
            var posts = JsonConvert.DeserializeObject<TypeCar>(response.Content);

            Console.WriteLine(posts.data);
            return posts.data;
        }

        public List<DataBrand> GetBrand()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_BRAND");
            //var client = ClienUAT("GetBrand");
            //var client = new RestClient("https://devapi.microleasingplc.com/pj/API_car_price/index.php/Index/V_table/V_BRAND");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=5irvsup5aab9tkobvaemerub26");
            IRestResponse response = client.Execute(request);
            var posts = JsonConvert.DeserializeObject<Brand>(response.Content);

            return posts.data;
        }

        public List<DataUseCarType> GetDataUseCarType()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/USEDCARTYP");
            //var client = ClienUAT("GetDataUseCarType");
            //var client = new RestClient("https://devapi.microleasingplc.com/pj/API_car_price/index.php/Index/V_table/USEDCARTYP");
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
                var posts = JsonConvert.DeserializeObject<UseCarType>(response.Content);
                return posts.data;
            }

        }

        public List<DataDealerAgent> GetDataDealerAgent()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/Dealer_Agent_NAV");
            //var client = ClienUAT("GetDataDealerAgent");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=4uiluu40lq2kjne6fn0svgulf7");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<DealerAgent>(response.Content);
                return posts.data;
            }

        }


        public List<DataCustomer> GetDataCustomer()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/Contact_customer");
            //var client = ClienUAT("GetDataCustomer");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=4uiluu40lq2kjne6fn0svgulf7");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<Customer>(response.Content);
                return posts.data;
            }

        }

        public List<DataEmployee> GetDataEmployee()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/Employee");
            //var client = ClienUAT("GetDataEmployee");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=4uiluu40lq2kjne6fn0svgulf7");
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

        public List<DataCarPolicy> GetDataCarPolicy()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/car_policy");
            //var client = ClienUAT("GetDataCarPolicy");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=tp7drgnu6jh0tgnmcsa3seahs7");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<CarPolicy>(response.Content);
                return posts.data;
            }

        }

        public List<DataCharacterCar> GetDataCharacterCar()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_CharacterCar");
            //var client = ClienUAT("GetDataAccessories");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=ppkn5rhs6hg3dr51rv4art4455");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<CharacterCar>(response.Content);
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

        public List<DataDealer> GetDealer()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_DEALER");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=tihcu1hosilrn842f128k4ogu2");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<Dealer>(response.Content);
                return posts.data;
            }

        }

        public List<DataDealer> GetAgent()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_AGENT");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=tihcu1hosilrn842f128k4ogu2");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<Dealer>(response.Content);
                return posts.data;
            }

        }

        public List<DataV_StyleSubMaster1> GetDataV_StyleSubMaster1()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_StyleSubMaster1");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=tp957m8bf89fr8id0dnghb0rs5");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<V_StyleSubMaster1>(response.Content);
                return posts.data;
            }

        }

        public List<DataV_StyleSubMaster2> GetDataV_StyleSubMaster2()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_StyleSubMaster2");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=tp957m8bf89fr8id0dnghb0rs5");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<V_StyleSubMaster2>(response.Content);
                return posts.data;
            }

        }

        public IRestResponse Sent(SentToAPI dta)
        {
            try
            {
                Appz_WebService iAPI = new Appz_WebService();
            var client = iAPI.UAT("pj/API_car_price/index.php/Index/store_ct");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cookie", "PHPSESSID=lef4ikmp1otrnb8ag9kr9jn2t0");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("store_name", dta.storename);
            request.AddParameter("personnel_code", dta.personnel_code);
            request.AddParameter("c1", dta.c1);
            request.AddParameter("c2", "");
            request.AddParameter("c3", "");
            request.AddParameter("c4", "");
            request.AddParameter("c5", "");
            request.AddParameter("c6", "");
            request.AddParameter("c7", "");
            request.AddParameter("c8", "");
            request.AddParameter("c9", "");
            request.AddParameter("c10", "");
            request.AddParameter("c11", "");
            request.AddParameter("c12", "");
            request.AddParameter("c13", "");
            request.AddParameter("c14", "");
            request.AddParameter("c15", "");
            request.AddParameter("c16", "");
            request.AddParameter("c17", "");
            request.AddParameter("c18", "");
            request.AddParameter("c19", "");
            request.AddParameter("c20", "");
            request.AddParameter("c21", "");
            request.AddParameter("c22", "");
            request.AddParameter("c23", "");
            request.AddParameter("c24", "");
            request.AddParameter("c25", "");
            request.AddParameter("c26", "");
            request.AddParameter("c27", "");
            request.AddParameter("c28", "");
            request.AddParameter("c29", "");
            request.AddParameter("c30", "");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                return response;
            }
        }
            catch (Exception ex)
            {
                return null;
            }

}


    }
}
