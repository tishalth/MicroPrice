using System;
using System.Collections.Generic;
using System.Text;

namespace Check_CarPrice.Model
{
    public class Check_Model
    {
        public class DataTransactionCar
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
        }

        public class TransactionCar
        {
            public string msg { get; set; }
            public List<DataTransactionCar> data { get; set; }
        }

      
        public class DataTypeCredit
        {
            public string prmcde { get; set; }
            public string USEDCARTYP { get; set; }
        }

        public class TypeCredit
        {
            public string msg { get; set; }
            public List<DataTypeCredit> data { get; set; }
        }
       
        public class DataInformationCodeCar
        {
            public int Rid { get; set; }
            public string cde_car { get; set; }
            public string app_id { get; set; }
            public float middle_price { get; set; }
            public string Distance_01 { get; set; }
            public string Distance_02 { get; set; }
            public float rate_01 { get; set; }
            public float rate_02 { get; set; }
            public string remark { get; set; }
            public string create_by { get; set; }
            public DateTime create_date { get; set; }
            public int status { get; set; }
            public string cde_policy { get; set; }
            public string status_appcar { get; set; }
            public float finamt { get; set; }
            public string model { get; set; }
            public string year_car { get; set; }
            public string type_car { get; set; }

        }

        public class InformationCodeCar
        {
            public string msg { get; set; }
            public List<DataInformationCodeCar> data { get; set; }
        }

        public class DataTransactionApprove
        {
            public int RId { get; set; }
            public string app_id { get; set; }
            public string status_appcar { get; set; }
            public int status { get; set; }
            public string create_date { get; set; }
            public string create_by { get; set; }
        }

        public class TransactionApprove
        {
            public string msg { get; set; }
            public List<DataTransactionApprove> data { get; set; }
        }

        

        public class DataGroupCar
        {
            public int Rid { get; set; }
            public string cde_head_tail { get; set; }
            public string name_head_tail { get; set; }
            public string cde_grpcar { get; set; }
            public string name_grpcar { get; set; }
            public string cde_style { get; set; }
            public string name_style { get; set; }
            public string cde_type_style { get; set; }
            public string cde_style_car { get; set; }
            public string create_date { get; set; }
            public string create_by { get; set; }
        }

        public class GroupCar
        {
            public string msg { get; set; }
            public List<DataGroupCar> data { get; set; }
        }

        public class DataStyleCar
        {
            public string type_grp_car { get; set; }
            public string grp_car { get; set; }
            public string namegrp_car { get; set; }
            public string cde_pickup { get; set; }
            public string name_pickup { get; set; }
            public string cde_special { get; set; }
            public string name_special { get; set; }
        }

        public class StyleCar
        {
            public string msg { get; set; }
            public List<DataStyleCar> data { get; set; }
        }

    
        //public class DataCharacterCar
        //{
        //    public string cde_head_tail { get; set; }
        //    public string name_head_tail { get; set; }
        //    public string cde_grpcar { get; set; }
        //    public string name_grpcar { get; set; }
        //    public string cde_style { get; set; }
        //    public string name_style { get; set; }
        //    public string cde_type_style { get; set; }
        //    public string name_type_style { get; set; }
        //    public string cde_style_car { get; set; }
        //    public string name_style_car { get; set; }
        //    public string cde_type_car { get; set; }
        //    public string type_car { get; set; }
        //}

        //public class CharacterCar
        //{
        //    public string msg { get; set; }
        //    public List<DataCharacterCar> data { get; set; }
        //}
    }
}
