using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.Update_Model;

namespace Check_CarPrice.WebService
{
    public class Update_Webservice
    {
        public RestClient client;

        Exception _error = null;
        public List<DataInformationTransactionCar> PostDataInformationTransactionCar(string id)
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
            request.AddHeader("Cookie", "PHPSESSID=vpp1p46618p9erp54eqlv8g2b6");
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
                var posts = JsonConvert.DeserializeObject<InformationTransactionCar>(response.Content);
                return posts.data;
            }
        }

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
            request.AddHeader("Cookie", "PHPSESSID=vpp1p46618p9erp54eqlv8g2b6");
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

        public List<DataEmployee> GetDataEmployee()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/Employee");
            //var client = ClienUAT("GetDataEmployee");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=0ul7q8o0d01t2dfokd72dn4rv3");
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

        public List<DataDealerAgent> GetDealerAgent()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/Dealer_Agent_NAV");
            //var client = ClienUAT("GetDealerAgent");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=0ul7q8o0d01t2dfokd72dn4rv3");
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

        public List<DataStyleCar> GetStyleCar()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/group_car");
            //var client = ClienUAT("GetStyleCar");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=h492t6r6fc96qoslsk0o1uq3p7");
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

        public bool PostUpdateDataTransactionCar(DataInformationTransactionCar dta)
        {
            bool ret = true;
            try
            {
                Appz_WebService iAPI = new Appz_WebService();
                client = iAPI.UAT("pj/API_car_price/index.php/Index/update_information_transaction_car");
                //var client = ClienUAT("PostUpdateDataTransactionCar");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("app_id", dta.app_id);
                request.AddParameter("type_credit", dta.type_credit);
                request.AddParameter("type_dealer", dta.type_dealer);
                request.AddParameter("type_contact", dta.type_contact);
                request.AddParameter("name_dealer_agent", dta.name_dealer_agent);
                request.AddParameter("cde_dealer_agent", dta.cde_dealer_agent);
                request.AddParameter("remark", dta.remark);
                request.AddParameter("typ_person", dta.typ_person);
                request.AddParameter("type_cont_channel", dta.type_cont_channel);
                request.AddParameter("name_cus", dta.name_cus);
                request.AddParameter("perosonal_cde", dta.perosonal_cde);
                request.AddParameter("personal_name", dta.personal_name);
                request.AddParameter("contno", dta.contno);
                request.AddParameter("name_contno", dta.name_contno);
                request.AddParameter("type_grp_car", dta.type_grp_car);
                if (dta.remark == "1")
                {
                    request.AddParameter("remark_description", dta.remark_description);
                }
                else if (dta.remark == "2")
                {
                    request.AddParameter("remark_date", dta.remark_date);
                    request.AddParameter("remark_description", dta.remark_description);
                }

                request.AddParameter("branch_code", dta.branch_code);
                request.AddParameter("run_number", dta.run_number);
                IRestResponse response = client.Execute(request);

            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex;

            }
            return ret;
        }

        public bool PostUpdateDataInformationCar(DataInformationCar dta)
        {
            bool ret = true;
            try
            {
                Appz_WebService iAPI = new Appz_WebService();
                client = iAPI.UAT("pj/API_car_price/index.php/Index/update_information_car");
                //var client = ClienUAT("PostUpdateDataInformationCar");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("app_id", dta.app_id);
                //request.AddParameter("applno_car", dta.applno_car);
                request.AddParameter("licno", dta.licno);
                request.AddParameter("provice", dta.provice);
                request.AddParameter("engno", dta.engno);
                request.AddParameter("chasno", dta.chasno);
                request.AddParameter("brand", dta.brand);
                request.AddParameter("model", dta.model);
                request.AddParameter("fuel", dta.fuel);
                request.AddParameter("type_car", dta.type_car);
                request.AddParameter("grp_car", dta.grp_car);
                request.AddParameter("cde_pickup", dta.cde_pickup);
                request.AddParameter("cde_special", dta.cde_special);
                request.AddParameter("head_price", dta.head_price);
                request.AddParameter("estimate_price", dta.estimate_price);
                request.AddParameter("finamt", dta.finamt);
                request.AddParameter("accessory", dta.accessory);
                request.AddParameter("year_car", dta.year_car);
                request.AddParameter("type_grp_car", dta.type_grp_car);
               
                request.AddParameter("campaign_id", dta.campaign_id);
                
                IRestResponse response = client.Execute(request);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex;

            }
            return ret;


        }

        public List<DataAllStyleCar> GetAllStyleCar()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_StyleCar");
            //var client = ClienUAT("GetAllStyleCar");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=qhbc49hkrmunm7cqhkl3oq6pt6");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<AllStyleCar>(response.Content);
                return posts.data;
            }
        }

        public bool PostUpdateDataImage(DataImage dta)
        {
            bool ret = true;
            try
            {
                Appz_WebService iAPI = new Appz_WebService();
                Update_Webservice iModel = new Update_Webservice();
                client = iAPI.UAT("pj/API_car_price/index.php/Index/update_information_image");
                client.Timeout = -1;
                //if (iModel.GetDataPicture() != null)
                //{
                //    var dtapic = iModel.GetDataPicture().Where(a => a.app_id == dta.app_id && a.status == 1).FirstOrDefault();

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
                request.AddHeader("Cookie", "PHPSESSID=c7tss57knsmlnkg16bein7dtb4");
                request.AddParameter("app_id", dta.app_id);
                request.AddParameter("applno_car", dta.applno_car);

                if (dta.img1_head != null) request.AddFile("img1_head", dta.img1_head);
                if (dta.img2_head != null) request.AddFile("img2_head", dta.img2_head);
                if (dta.img3_head != null) request.AddFile("img3_head", dta.img3_head);
                if (dta.img4_head != null) request.AddFile("img4_head", dta.img4_head);
                if (dta.img5_head != null) request.AddFile("img5_head", dta.img5_head);
                if (dta.img6_head != null) request.AddFile("img6_head", dta.img6_head);
                if (dta.img7_head != null) request.AddFile("img7_head", dta.img7_head);
                if (dta.img8_head != null) request.AddFile("img8_head", dta.img8_head);
                if (dta.img9_head != null) request.AddFile("img9_head", dta.img9_head);
                if (dta.img10_head != null) request.AddFile("img10_head", dta.img10_head);
                if (dta.img11_head != null) request.AddFile("img11_head", dta.img11_head);
                if (dta.img12_head != null) request.AddFile("img12_head", dta.img12_head);
                if (dta.img13_head != null) request.AddFile("img13_head", dta.img13_head);
                if (dta.img14_head != null) request.AddFile("img14_head", dta.img14_head);
                if (dta.img15_head != null) request.AddFile("img15_head", dta.img15_head);
                if (dta.img16_tail != null) request.AddFile("img16_tail", dta.img16_tail);
                if (dta.img17_tail != null) request.AddFile("img17_tail", dta.img17_tail);
                if (dta.img18_tail != null) request.AddFile("img18_tail", dta.img18_tail);
                if (dta.img19_tail != null) request.AddFile("img19_tail", dta.img19_tail);
                if (dta.img20_tail != null) request.AddFile("img20_tail", dta.img20_tail);
                if (dta.img21_tail != null) request.AddFile("img21_tail", dta.img21_tail);
                if (dta.img22 != null) request.AddFile("img22", dta.img22);
                if (dta.img23 != null) request.AddFile("img23", dta.img23);
                if (dta.img24 != null) request.AddFile("img24", dta.img24);
                if (dta.img25 != null) request.AddFile("img25", dta.img25);
                if (dta.img26 != null) request.AddFile("img26", dta.img26);
                if (dta.img27 != null) request.AddFile("img27", dta.img27);
                if (dta.img28 != null) request.AddFile("img28", dta.img28);


                request.AddParameter("type_grp_car", dta.type_grp_car);
                request.AddParameter("create_by", dta.create_by);
                IRestResponse response = client.Execute(request);
                //}
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex;

            }
            return ret;
        }

        public List<DataPicture> GetDataPicture()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_information_image");
            //var client = ClienUAT("GetAllStyleCar");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=8gt433bi5ph39e9nvqiafte6g3");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<Picture>(response.Content);
                return posts.data;
            }
        }

    }
}
