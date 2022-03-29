using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using static Check_CarPrice.Model.Appz_Model;
using static Check_CarPrice.Model.InsertCarPrice_Model;

namespace Check_CarPrice.WebService
{
    public class InsertCarPrice_WebService
    {
        public RestClient client;

        Exception _error = null;
      
        public List<DataStyle1> GetStyle1(string style1)
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/select_group_car");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cookie", "PHPSESSID=soehfc5dt1r5s5afe3uc0nkib7");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("cde_head_tail", style1);
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<Style1>(response.Content);
                return posts.data;
            }

        }

        public bool PostInformation_Transaction_Car(DataInformationTransactionCar dta)
        {
            bool ret = true;
            try
            {
                Appz_WebService iAPI = new Appz_WebService();
                client = iAPI.UAT("pj/API_car_price/index.php/Index/insert_information_transaction_car");
                //var client = ClienUAT("PostInformation_Transaction_Car");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Cookie", "PHPSESSID=42gmgdumh90sr1g9kt8eg29ve0");
                request.AlwaysMultipartFormData = true;
                request.AddParameter("app_id", dta.app_id);
                request.AddParameter("applno_car", dta.applno_car);
                request.AddParameter("type_credit", dta.type_credit);
                request.AddParameter("type_dealer", dta.type_dealer);
                request.AddParameter("type_contact", dta.type_contact);
                request.AddParameter("remark", dta.remark);
                request.AddParameter("typ_person", dta.typ_person);
                request.AddParameter("type_cont_channel", dta.type_cont_channel);
                request.AddParameter("name_cus", dta.name_cus);
                request.AddParameter("perosonal_cde", dta.perosonal_cde);
                request.AddParameter("contno", dta.contno);
                request.AddParameter("status", dta.status);
                request.AddParameter("create_by", dta.create_by);
                request.AddParameter("personal_login", dta.create_by);
                request.AddParameter("type_grp_car", dta.type_grp_car);
                request.AddParameter("name_dealer_agent", dta.name_dealer_agent);
                request.AddParameter("cde_dealer_agent", dta.cde_dealer_agent);
                request.AddParameter("personal_name", dta.personal_name);
                request.AddParameter("name_contno", dta.name_contno);
                if(dta.remark_date!=DateTime.MinValue) request.AddParameter("remark_date", dta.remark_date);
                request.AddParameter("remark_description", dta.remark_description);
                request.AddParameter("branch_code", dta.branch_code);
                request.AddParameter("run_number", dta.run_number);
                IRestResponse response = client.Execute(request);
                var f = response.StatusCode.ToString();
                //if (response.StatusCode.ToString() == "OK") ret = true;
                //else ret = false;

            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex;

            }
            return ret;


        }

        public bool PostDataInformationCar(DataInformationCar dta)
        {
            bool ret = true;
            try
            {
                Appz_WebService iAPI = new Appz_WebService();
                client = iAPI.UAT("pj/API_car_price/index.php/Index/insert_information_car");
                //var client = ClienUAT("PostDataInformationCar");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Cookie", "PHPSESSID=4uiluu40lq2kjne6fn0svgulf7");
                request.AlwaysMultipartFormData = true;
                request.AddParameter("app_id", dta.app_id);
                request.AddParameter("applno_car", dta.applno_car);
                request.AddParameter("licno", dta.licno);
                request.AddParameter("provice", dta.provice);
                request.AddParameter("engno", dta.engno);
                request.AddParameter("chasno", dta.chasno);
                request.AddParameter("brand", dta.brand);
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
                request.AddParameter("status", dta.status);
                //request.AddParameter("run_number", dta.run_number);
                request.AddParameter("campaign_id", dta.campaign_id);
                request.AddParameter("create_by", dta.create_by);
                
                IRestResponse response = client.Execute(request);
                //if (response.StatusCode.ToString() == "OK") ret = true;
                //else ret = false;
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex;
            }
            return ret;

        }

        public List<DataStyle2> GetStyle2(string style1, string style2)
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/select_cde_style");
            //var client = ClienUAT("GetStyle2");
            //var client = new RestClient("https://devapi.microleasingplc.com/pj/API_car_price/index.php/Index/select_cde_style");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cookie", "PHPSESSID=drflorbmhn0t32d13adpii7uh6");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("cde_head_tail", style1);
            request.AddParameter("cde_grpcar", style2);
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<Style2>(response.Content);
                return posts.data;
            }
        }

        public List<DataStyle3> GetStyle3(string style1, string style2, string style3)
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/select_style_car");
            //var client = ClienUAT("GetStyle3");
            //var client = new RestClient("https://devapi.microleasingplc.com/pj/API_car_price/index.php/Index/select_style_car");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cookie", "PHPSESSID=drflorbmhn0t32d13adpii7uh6");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("cde_head_tail", style1);
            request.AddParameter("cde_grpcar", style2);
            request.AddParameter("cde_style", style3);
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<Style3>(response.Content);
                return posts.data;
            }

        }
        public bool PostImageMaster(DataImage dta)
        {
            bool ret = true;
            try
            {
                Appz_WebService iAPI = new Appz_WebService();
                client = iAPI.UAT("pj/API_car_price/index.php/Index/insert_information_image_car");
                //var client = new RestClient("https://api-uat.microleasingplc.com/pj/API_car_price/index.php/Index/insert_information_image_car");
                client.Timeout = -1;
                //var request = new RestRequest(Method.POST);
                //request.AddHeader("personnel_code", " val1");
                //request.AddHeader("applno_car", " val2");
                //request.AddHeader("licno", " val3");
                //request.AddHeader("provice", " val4");
                //request.AddHeader("engno", " val5");
                //request.AddHeader("chasno", " val6");
                //request.AddHeader("brand", " val7");
                //request.AddHeader("model", " val8");
                //request.AddHeader("fuel", " val9");
                //request.AddHeader("type_car", " val10");
                //request.AddHeader("grp_car", " val11");
                //request.AddHeader("cde_pickup", " val12");
                //request.AddHeader("cde_special", " val13");
                //request.AddHeader("head_price", " val14");
                //request.AddHeader("estimate_price", " val15");
                //request.AddHeader("finamt", " val16");
                //request.AddHeader("accessory", " val17");
                //request.AddHeader("year_car", " val18");
                //request.AddHeader("type_grp_car", " val19");
                //request.AddHeader("status", " val20");
                //request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                //request.AddParameter("applno_car", dta.applno_car);
                //request.AddParameter("type_grp_car", dta.type_grp_car);
                //request.AddParameter("create_by", dta.create_by);
                //request.AddParameter("status", "1");
                //if (dta.img1_head != null)
                //{
                //    var request = new RestRequest(Method.POST);
                //    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                //    request.AddParameter("applno_car", dta.applno_car);
                //    request.AddParameter("type_grp_car", dta.type_grp_car);
                //    request.AddParameter("create_by", dta.create_by);
                //    request.AddParameter("status", "1");
                //    request.AddFile("img1_head", dta.img1_head);
                //    IRestResponse response = client.Execute(request);

                //}
                //if (dta.img2_head != null) request.AddFile("img2_head", dta.img2_head);
                //if (dta.img3_head != null) request.AddFile("img3_head", dta.img3_head);
                //if (dta.img4_head != null) request.AddFile("img4_head", dta.img4_head);
                //if (dta.img5_head != null) request.AddFile("img5_head", dta.img5_head);
                //if (dta.img6_head != null) request.AddFile("img6_head", dta.img6_head);
                //if (dta.img7_head != null) request.AddFile("img7_head", dta.img7_head);
                //if (dta.img8_head != null) request.AddFile("img8_head", dta.img8_head);
                //if (dta.img9_head != null) request.AddFile("img9_head", dta.img9_head);
                //if (dta.img10_head != null) request.AddFile("img10_head", dta.img10_head);
                //if (dta.img11_head != null) request.AddFile("img11_head", dta.img11_head);
                //if (dta.img12_head != null) request.AddFile("img12_head", dta.img12_head);
                //if (dta.img13_head != null) request.AddFile("img13_head", dta.img13_head);
                //if (dta.img14_head != null) request.AddFile("img14_head", dta.img14_head);
                //if (dta.img15_head != null) request.AddFile("img15_head", dta.img15_head);
                //if (dta.img23 != null) request.AddFile("img23", dta.img23);
                //if (dta.img24 != null) request.AddFile("img24", dta.img24);
                //if (dta.img25 != null) request.AddFile("img25", dta.img25);
                //if (dta.img26 != null) request.AddFile("img26", dta.img26);
                //if (dta.img27 != null) request.AddFile("img27", dta.img27);
                //if (dta.img28 != null) request.AddFile("img28", dta.img28);
                //if (dta.img29 != null) request.AddFile("img29", dta.img29);



                if (dta.img1_head != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1");
                    request.AddFile("img1_head", dta.img1_head);
                    IRestResponse response = client.Execute(request);

                }
                if (dta.img2_head != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); 
                    request.AddFile("img2_head", dta.img2_head);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img3_head != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); 
                    request.AddFile("img3_head", dta.img3_head);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img4_head != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); 
                    request.AddFile("img4_head", dta.img4_head);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img5_head != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); 
                    request.AddFile("img5_head", dta.img5_head);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img6_head != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); 
                    request.AddFile("img6_head", dta.img6_head);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img7_head != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); 
                    request.AddFile("img7_head", dta.img7_head);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img8_head != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); 
                    request.AddFile("img8_head", dta.img8_head);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img9_head != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); 
                    request.AddFile("img9_head", dta.img9_head);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img10_head != null) 
                { 
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by); 
                    request.AddFile("img10_head", dta.img10_head);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img11_head != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by); 
                    request.AddFile("img11_head", dta.img11_head);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img12_head != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by); 
                    request.AddFile("img12_head", dta.img12_head);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img13_head != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by); 
                    request.AddFile("img13_head", dta.img13_head);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img14_head != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by); 
                    request.AddFile("img14_head", dta.img14_head);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img15_head != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by); 
                    request.AddFile("img15_head", dta.img15_head);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img23 != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddFile("img23", dta.img23);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img24 != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddFile("img24", dta.img24);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img25 != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddFile("img25", dta.img25);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img26 != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddFile("img26", dta.img26);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img27 != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddFile("img27", dta.img27);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img28 != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddFile("img28", dta.img28);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img29 != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddFile("img29", dta.img29);
                    IRestResponse response = client.Execute(request);
                }


                //object data = dta;
                //foreach (var item in data)
                //{

                //}

                //for (int i = 0; i < request.Files.Count(); i++)
                //{
                //    var j = request.Files[0];
                //    //IRestResponse x = (IRestResponse)request.Files[i];
                //    //IRestResponse response = client.Execute(request.where.Files[i]);
                //}

                    //Console.WriteLine(request);


                    //IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex;
            }
            return ret;
        }

        public bool PostImageSubMaster(DataImage dta)
        {
            bool ret = true;
            try
            {



                //Appz_WebService iAPI = new Appz_WebService();
                //client = iAPI.UAT("pj/API_car_price/index.php/Index/insert_information_image_pickup");
                //client.Timeout = -1;
                //var request =  new  RestRequest(Method.POST);
                //request.AddHeader("personnel_code", " val1");
                //request.AddHeader("applno_car", " val2");
                //request.AddHeader("licno", " val3");
                //request.AddHeader("provice", " val4");
                //request.AddHeader("engno", " val5");
                //request.AddHeader("chasno", " val6");
                //request.AddHeader("brand", " val7");
                //request.AddHeader("model", " val8");
                //request.AddHeader("fuel", " val9");
                //request.AddHeader("type_car", " val10");
                //request.AddHeader("grp_car", " val11");
                //request.AddHeader("cde_pickup", " val12");
                //request.AddHeader("cde_special", " val13");
                //request.AddHeader("head_price", " val14");
                //request.AddHeader("estimate_price", " val15");
                //request.AddHeader("finamt", " val16");
                //request.AddHeader("accessory", " val17");
                //request.AddHeader("year_car", " val18");
                //request.AddHeader("type_grp_car", " val19");
                //request.AddHeader("status", " val20");
                //request.AddParameter("applno_car", dta.applno_car);
                //request.AddParameter("type_grp_car", dta.type_grp_car);
                //request.AddParameter("create_by", dta.create_by);
                //request.AddParameter("status", "1");
                //if (dta.img16_tail != null) request.AddFile("img16_tail", dta.img16_tail);
                //if (dta.img17_tail != null) request.AddFile("img17_tail", dta.img17_tail);
                //if (dta.img18_tail != null) request.AddFile("img18_tail", dta.img18_tail);
                //if (dta.img19_tail != null) request.AddFile("img19_tail", dta.img19_tail);
                //if (dta.img20_tail != null) request.AddFile("img20_tail", dta.img20_tail);
                //if (dta.img21_tail != null) request.AddFile("img21_tail", dta.img21_tail);
                //if (dta.img23 != null) request.AddFile("img23", dta.img23);
                //if (dta.img24 != null) request.AddFile("img24", dta.img24);
                //if (dta.img25 != null) request.AddFile("img25", dta.img25);
                //if (dta.img26 != null) request.AddFile("img26", dta.img26);
                //if (dta.img27 != null) request.AddFile("img27", dta.img27);
                //if (dta.img28 != null) request.AddFile("img28", dta.img28);
                //IRestResponse response =  client.Execute(request);


                Appz_WebService iAPI = new Appz_WebService();
                client = iAPI.UAT("pj/API_car_price/index.php/Index/insert_information_image_pickup");
                client.Timeout = -1;
                if (dta.img16_tail != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); request.AddFile("img16_tail", dta.img16_tail); 
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img17_tail != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); request.AddFile("img17_tail", dta.img17_tail); 
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img18_tail != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); request.AddFile("img18_tail", dta.img18_tail); 
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img19_tail != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); request.AddFile("img19_tail", dta.img19_tail); 
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img20_tail != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); request.AddFile("img20_tail", dta.img20_tail); 
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img21_tail != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); request.AddFile("img21_tail", dta.img21_tail);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img23 != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); request.AddFile("img23", dta.img23); 
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img24 != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); request.AddFile("img24", dta.img24); 
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img25 != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); request.AddFile("img25", dta.img25); 
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img26 != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); request.AddFile("img26", dta.img26); 
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img27 != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); request.AddFile("img27", dta.img27); 
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img28 != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddParameter("status", "1"); request.AddFile("img28", dta.img28);
                    IRestResponse response = client.Execute(request);
                }
                if (dta.img29 != null)
                {
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "PHPSESSID=9km8gnuopvgv75dan3mqjc1l12");
                    request.AddParameter("app_id", dta.app_id);
                    request.AddParameter("applno_car", dta.applno_car);
                    request.AddParameter("type_grp_car", dta.type_grp_car);
                    request.AddParameter("create_by", dta.create_by);
                    request.AddFile("img29", dta.img29);
                    IRestResponse response = client.Execute(request);
                }
                //IRestResponse response = client.Execute(request);

            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex;
            }
            return ret;
        }

        public List<DataGroupCar> GetDataGroupCar()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/group_car");
            //var client = ClienUAT("GetDataEmployee");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=eslturonsp7si04od111cib726");
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

        public List<DataVStyle1> GetDataVStyle1()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_Style1");
            //var client = ClienUAT("GetDataEmployee");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=mfpunc1q9vqg13kfc210h157q3");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<VStyle1>(response.Content);
                return posts.data;
            }
        }

        public List<DataVStyle2> GetDataVStyle2()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_Style2");
            //var client = ClienUAT("GetDataEmployee");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=mfpunc1q9vqg13kfc210h157q3");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<VStyle2>(response.Content);
                return posts.data;
            }
        }

        public List<DataVStyle3> GetDataVStyle3()
        {
            Appz_WebService iAPI = new Appz_WebService();
            client = iAPI.UAT("pj/API_car_price/index.php/Index/V_table/V_Style3");
            //var client = ClienUAT("GetDataEmployee");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "PHPSESSID=mfpunc1q9vqg13kfc210h157q3");
            IRestResponse response = client.Execute(request);
            if (response.Content == "{\"msg\":\"409\",\"data\":\"No Data\"}")
            {
                return null;
            }
            else
            {
                var posts = JsonConvert.DeserializeObject<VStyle3>(response.Content);
                return posts.data;
            }
        }

        public List<DataInformationCar> GetDataInformationCarInsert()
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
    }
}
