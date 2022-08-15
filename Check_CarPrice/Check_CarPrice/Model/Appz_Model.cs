using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Check_CarPrice.Model
{
    public class Appz_Model
    {
        public class DataGenAppID
        {
            public string app_id { get; set; }
            public string run_number { get; set; }
        }

        public class GenAppID
        {
            public string msg { get; set; }
            public List<DataGenAppID> data { get; set; }
        }
        public class DataCarDetail
        {
            public string app_id { get; set; }
            public string applno_car { get; set; }
            public string type_credit { get; set; }
            public string name_typecredit { get; set; }
            public string type_dealer { get; set; }
            public string type_contact { get; set; }
            public string name_dealer_agent { get; set; }
            public string cde_dealer_agent { get; set; }
            public string remark_transactioncar { get; set; }
            public DateTime remark_date { get; set; }
            public string remark_description_transaction { get; set; }
            public string typ_person { get; set; }
            public string type_cont_channel { get; set; }
            public string name_cus { get; set; }
            public string perosonal_cde { get; set; }
            public string personal_name { get; set; }
            public string contno { get; set; }
            public string name_contno { get; set; }
            public DateTime createdate_transaction { get; set; }
            public string licno { get; set; }
            public string provice { get; set; }
            public string engno { get; set; }
            public string chasno { get; set; }
            public string cde_brand { get; set; }
            public string brand_name { get; set; }
            public string model { get; set; }
            public string fuel { get; set; }
            public string type_car { get; set; }
            public string name_typ_car { get; set; }
            public string grp_car { get; set; }
            public string cde_pickup { get; set; }
            public string cde_special { get; set; }
            public string head_price { get; set; }
            public string finamt { get; set; }
            public string accessory { get; set; }
            public string nameaccessory { get; set; }
            public string year_car { get; set; }
            public string type_grp_car { get; set; }
            public int run_number { get; set; }
            public string campaign_id { get; set; }
            public string campaign { get; set; }
            public string cde_car { get; set; }
            public string middle_price { get; set; }
            public string Distance_01 { get; set; }
            public string Distance_02 { get; set; }
            public string rate_01 { get; set; }
            public string rate_02 { get; set; }
            public string remark_cdecar { get; set; }
            public string cde_policy { get; set; }
            public string name_policy { get; set; }
            public int codecar_status { get; set; }
            public string status_appcar { get; set; }
            public string namestatus { get; set; }
            public string create_date_approve { get; set; }
            public string branch_code { get; set; }
            public string branch_nane { get; set; }
            public string personnel_code { get; set; }
            public string personnel_name { get; set; }
            public string namegrp_car { get; set; }
            public string name_pickup { get; set; }
            public string name_special { get; set; }
            public string name_style { get; set; }
            public string accessory_name { get; set; }
        }

        public class CarDetail
        {
            public string msg { get; set; }
            public List<DataCarDetail> data { get; set; }
        }

        public class DataInformationTransactionCar
        {
            public int Rid { get; set; }
            public string app_id { get; set; }
            public string applno_car { get; set; }
            public string type_credit { get; set; }
            public string type_dealer { get; set; }
            public string type_contact { get; set; }
            public string name_dealer_agent { get; set; }
            public string cde_dealer_agent { get; set; }
            public string remark { get; set; }
            public string remark_description { get; set; }
            public DateTime remark_date { get; set; }
            public string typ_person { get; set; }
            public string type_cont_channel { get; set; }
            public string name_cus { get; set; }
            public string perosonal_cde { get; set; }
            public string personal_name { get; set; }
            public string contno { get; set; }
            public string name_contno { get; set; }
            public DateTime create_date { get; set; }
            public string create_by { get; set; }
            public int status { get; set; }
            public string type_grp_car { get; set; }
            public string branch_code { get; set; }
            public int run_number { get; set; }
        }

        public class InformationTransactionCar
        {
            public string msg { get; set; }
            public List<DataInformationTransactionCar> data { get; set; }
        }

        public class DataInformationCar
        {
            public int Rid { get; set; }
            public string app_id { get; set; }
            public string applno_car { get; set; }
            public string licno { get; set; }
            public string provice { get; set; }
            public string engno { get; set; }
            public string chasno { get; set; }
            public string brand { get; set; }
            public string model { get; set; }
            public string fuel { get; set; }
            public string type_car { get; set; }
            public string grp_car { get; set; }
            public string cde_pickup { get; set; }
            public string cde_special { get; set; }
            public float head_price { get; set; }
            public float estimate_price { get; set; }
            public float finamt { get; set; }
            public string accessory { get; set; }
            public string year_car { get; set; }
            public string type_grp_car { get; set; }
            public int status { get; set; }
            public string create_date { get; set; }
            public string create_by { get; set; }
            public string run_number { get; set; }
            public string campaign_id { get; set; }
            //public int branch_code { get; set; }
        }

        public class InformationCar
        {
            public string msg { get; set; }
            public List<DataInformationCar> data { get; set; }
        }

        public class DataImage
        {
            public int Rid { get; set; }
            public string app_id { get; set; }
            public string applno_car { get; set; }
            public string img1_head { get; set; }
            public string img2_head { get; set; }
            public string img3_head { get; set; }
            public string img4_head { get; set; }
            public string img5_head { get; set; }
            public string img6_head { get; set; }
            public string img7_head { get; set; }
            public string img8_head { get; set; }
            public string img9_head { get; set; }
            public string img10_head { get; set; }
            public string img11_head { get; set; }
            public string img12_head { get; set; }
            public string img13_head { get; set; }
            public string img14_head { get; set; }
            public string img15_head { get; set; }
            public string img16_tail { get; set; }
            public string img17_tail { get; set; }
            public string img18_tail { get; set; }
            public string img19_tail { get; set; }
            public string img20_tail { get; set; }
            public string img21_tail { get; set; }
            public string img22 { get; set; }
            public string img23 { get; set; }
            public string img24 { get; set; }
            public string img25 { get; set; }
            public string img26 { get; set; }
            public string img27 { get; set; }
            public string img28 { get; set; }
            public string img29 { get; set; }
            public string type_grp_car { get; set; }
            public string create_date { get; set; }
            public string end_date { get; set; }
            public string create_by { get; set; }
            public string status { get; set; }
        }

        public class Image
        {
            public string msg { get; set; }
            public List<DataImage> data { get; set; }
        }
        public class DataAccessories
        {
            public int Rid { get; set; }
            public string cde_accessory { get; set; }
            public string accessory { get; set; }
            public string status { get; set; }
            public string create_date { get; set; }
            public string create_by { get; set; }
        }

        public class Accessories
        {
            public string msg { get; set; }
            public List<DataAccessories> data { get; set; }
        }

        public class DataProvince
        {
            public string CODE { get; set; }
            public string DESC_TH { get; set; }
            public string DESC_EN { get; set; }
        }

        public class Province
        {
            public string msg { get; set; }
            public List<DataProvince> data { get; set; }
        }

        public class DataUseCarType
        {
            public string prmcde { get; set; }
            public string USEDCARTYP { get; set; }
        }

        public class UseCarType
        {
            public string msg { get; set; }
            public List<DataUseCarType> data { get; set; }
        }

        public class DataTypeCar
        {
            public int Rid { get; set; }
            public string cde_typ_car { get; set; }
            public string name_typ_car { get; set; }
            public string create_date { get; set; }
            public string create_by { get; set; }
            public string type_grp_car { get; set; }
            public string code_KP { get; set; }
        }

        public class TypeCar
        {
            public string msg { get; set; }
            public List<DataTypeCar> data { get; set; }
        }

        public class DataBrand
        {
            public string CODE { get; set; }
            public string DESC_TH { get; set; }
            public string DESC_EN { get; set; }
        }

        public class Brand
        {
            public string msg { get; set; }
            public List<DataBrand> data { get; set; }
        }

        public class DataCampaign
        {
            public int Rid { get; set; }
            public string campaign_id { get; set; }
            public string campaign { get; set; }
            public string path_pdf { get; set; }
            public string status { get; set; }
            public string start_date { get; set; }
            public string end_date { get; set; }
            public string create_date { get; set; }
            public string create_by { get; set; }
        }

        public class Campaign
        {
            public string msg { get; set; }
            public List<DataCampaign> data { get; set; }
        }

        public class DataDealerAgent
        {
            public string No_ { get; set; }
            public string Name { get; set; }
            public string VendorPostingGroup { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string PhoneNo_ { get; set; }
        }

        public class DealerAgent
        {
            public string msg { get; set; }
            public List<DataDealerAgent> data { get; set; }
        }

        public class DataCustomer
        {
            public string contno { get; set; }
            public string idno { get; set; }
            public string name_cus { get; set; }
        }

        public class Customer
        {
            public string msg { get; set; }
            public List<DataCustomer> data { get; set; }
        }

        public class DataEmployee
        {
            public string personnel_code { get; set; }
            public string personnel_name { get; set; }
            public string branch_code { get; set; }
            public string branch_nane { get; set; }
            public string branch_no { get; set; }
            public string hub { get; set; }
        }

        public class Employee
        {
            public string msg { get; set; }
            public List<DataEmployee> data { get; set; }
        }

        public class DataStatus
        {
            public int Rid { get; set; }
            public string cde_status_appcar { get; set; }
            public string status_appcar { get; set; }
            public string create_date { get; set; }
            public string create_by { get; set; }
            public int status { get; set; }
        }

        public class Status
        {
            public string msg { get; set; }
            public List<DataStatus> data { get; set; }
        }

        

        public class DataCarPolicy
        {
            public int Rid { get; set; }
            public string cde_policy { get; set; }
            public string name_policy { get; set; }
            public int status { get; set; }
            public string create_date { get; set; }
            public string create_by { get; set; }
        }

        public class CarPolicy
        {
            public string msg { get; set; }
            public List<DataCarPolicy> data { get; set; }
        }

        public class DataCharacterCar
        {
            public string Rid { get; set; }
            public string cde_head_tail { get; set; }
            public string name_head_tail { get; set; }
            public string cde_grpcar { get; set; }
            public string name_grpcar { get; set; }
            public string cde_style { get; set; }
            public string name_style { get; set; }
            public string cde_type_style { get; set; }
            public string name_type_style { get; set; }
            public string cde_style_car { get; set; }
            public string name_style_car { get; set; }
            public string cde_type_car { get; set; }
            public string type_car { get; set; }
        }

        public class CharacterCar
        {
            public string msg { get; set; }
            public List<DataCharacterCar> data { get; set; }
        }

        //public class DataGrantMapUser
        //{
        //    public int Rid { get; set; }
        //    public string grntuser_id { get; set; }
        //    public string role_id { get; set; }
        //    public string personnal_code { get; set; }
        //    public int status { get; set; }
        //    public string createby { get; set; }
        //    public DateTime createon { get; set; }
        //    public string updateby { get; set; }
        //    public DateTime updateon { get; set; }
        //}

        //public class GrantMapUser
        //{
        //    public string msg { get; set; }
        //    public List<DataGrantMapUser> data { get; set; }
        //}


        public class LoginData
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
            public string datetime { get; set; }
            public bool logged_in { get; set; }
        }

        public class Login
        {
            public string msg { get; set; }
            public List<LoginData> data { get; set; }
        }

        public class DataDealer
        {
            public string No_ { get; set; }
            public string Name { get; set; }
            public string VendorPostingGroup { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string PhoneNo_ { get; set; }
        }

        public class Dealer
        {
            public string msg { get; set; }
            public List<DataDealer> data { get; set; }
        }
        public class DataV_StyleSubMaster1
        {
            public string cde_type_style { get; set; }
            public string name_type_style { get; set; }
            public string cde_grpcar { get; set; }
            public string name_grpcar { get; set; }
        }

        public class V_StyleSubMaster1
        {
            public string msg { get; set; }
            public List<DataV_StyleSubMaster1> data { get; set; }
        }

        public class DataV_StyleSubMaster2
        {
            
            public string cde_type_style { get; set; }
            public string name_type_style { get; set; }
            public string cde_style_car { get; set; }
            public string name_style_car { get; set; }
            public string row_number { get; set; }
            public string cde_style { get; set; }
        }

        public class V_StyleSubMaster2
        {
            public string msg { get; set; }
            public List<DataV_StyleSubMaster2> data { get; set; }
        }

        public class DataVersion
        {
            //public int Rid { get; set; }
            public string system_version { get; set; }
            public string data_version { get; set; }
            public DateTime createdate  { get; set; }
         
        }

        public class Version
        {
            public string msg { get; set; }
            public List<DataVersion> data { get; set; }
        }

        


        public class SentToAPI
        {
            public string personnel_code { get; set; }
            public string storename { get; set; }
            public string c1 { get; set; }
            public string c2 { get; set; }
            public string c3 { get; set; }
            public string c4 { get; set; }
            public string c5 { get; set; }
            public string c6 { get; set; }
            public string c7 { get; set; }
            public string c8 { get; set; }
            public string c9 { get; set; }
            public string c10 { get; set; }
            public string c11 { get; set; }
            public string c12 { get; set; }
            public string c13 { get; set; }
            public string c14 { get; set; }
            public string c15 { get; set; }
            public string c16 { get; set; }
            public string c17 { get; set; }
            public string c18 { get; set; }
            public string c19 { get; set; }
            public string c20 { get; set; }
            public string c21 { get; set; }
            public string c22 { get; set; }
            public string c23 { get; set; }
            public string c24 { get; set; }
            public string c25 { get; set; }
            public string c26 { get; set; }
            public string c27 { get; set; }
            public string c28 { get; set; }
            public string c29 { get; set; }
            public string c30 { get; set; }

        }
    }
}
