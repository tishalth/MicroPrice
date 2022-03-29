using System;
using System.Collections.Generic;
using System.Text;

namespace Check_CarPrice.Model
{
    public class CarPriceList_Model
    {
        public class DataTransactionCar
        {
            public string applno_car { get; set; }
            public string app_id { get; set; }
            public string licno { get; set; }
            public int status { get; set; }
            public string status_approve { get; set; }
            public string status_appcar { get; set; }
            public DateTime create_date { get; set; }
            public string Branch_Owner { get; set; }
            public string type_grp_car { get; set; }
            public string name_head_tail { get; set; }
            public string name_typ_car { get; set; }
            public string name_style_car { get; set; }
            public string character_car { get; set; }
            public string brand { get; set; }
            public string branch_code { get; set; }
            public string create_by { get; set; }
            public string hub { get; set; }

        }

        public class TransactionCar
        {
            public string msg { get; set; }
            public List<DataTransactionCar> data { get; set; }
        }

        
    }
}
