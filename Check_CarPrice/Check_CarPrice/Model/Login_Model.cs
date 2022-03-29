using System;
using System.Collections.Generic;
using System.Text;

namespace Check_CarPrice.Model
{
    public class Login_Model
    {
        public class DataUser
        {
            public string personnel_code { get; set; }
            public string personnel_name_TH { get; set; }
            public string personnel_name_EN { get; set; }
            public string personnel_last_TH { get; set; }
            public string personnel_last_EN { get; set; }
            public string telephone { get; set; }
            public string e_mail { get; set; }
            public string off_phone { get; set; }
            public string ad_account { get; set; }
            public string branch_no { get; set; }
            public string branch_name_th { get; set; }
            public string branch_name_en { get; set; }
            public string depart_code { get; set; }
            public string department_name_TH { get; set; }
            public string department_name_EN { get; set; }
            public string abbreviation { get; set; }
            public string position_code { get; set; }
            public string position_level_code { get; set; }
            public string position_name_TH { get; set; }
            public string position_name_EN { get; set; }
            public string level_no { get; set; }
            public string level_name_TH { get; set; }
            public string level_name_EN { get; set; }
            public string map_nav_code { get; set; }
            public string hub { get; set; }
            public string area_no { get; set; }
            public string datetime { get; set; }
            public bool logged_in { get; set; }
        }
        public class Version
        {
            public int RID { get; set; }
            public string system_id { get; set; }
            public string system_version { get; set; }
            public string data_version { get; set; }
            public object date_record { get; set; }
            public string remark { get; set; }
        }

        public class Page
        {
            public string role_id { get; set; }
            public string page_id { get; set; }
            public string page_name { get; set; }
        }

        public class Func
        {
            public string role_id { get; set; }
            public string func_id { get; set; }
            public string func_name { get; set; }
        }

        public class Role
        {
            public Version version { get; set; }
            public List<Page> page { get; set; }
            public List<Func> func { get; set; }
        }

        public class User
        {
            public string msg { get; set; }
            public List<DataUser> data { get; set; }
            public Role role { get; set; }
        }





        //-----------------------------------------------------------------------------------------------------





        public class LoginTest
        {
            public string personnel_code { get; set; }
            public string personnel_name_TH { get; set; }
            public string personnel_name_EN { get; set; }
            public string personnel_last_TH { get; set; }
            public string personnel_last_EN { get; set; }
            public object telephone { get; set; }
            public object e_mail { get; set; }
            public object off_phone { get; set; }
            public string ad_account { get; set; }
            public string branch_no { get; set; }
            public string branch_name_th { get; set; }
            public string branch_name_en { get; set; }
            public string depart_code { get; set; }
            public string department_name_TH { get; set; }
            public string department_name_EN { get; set; }
            public string abbreviation { get; set; }
            public string position_code { get; set; }
            public string position_level_code { get; set; }
            public string position_name_TH { get; set; }
            public string position_name_EN { get; set; }
            public string level_no { get; set; }
            public string level_name_TH { get; set; }
            public string level_name_EN { get; set; }
            public string map_nav_code { get; set; }
            public string datetime { get; set; }
            public bool logged_in { get; set; }
        }

    }


}
