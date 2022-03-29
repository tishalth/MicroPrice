using System;
using System.Collections.Generic;
using System.Text;

namespace Check_CarPrice.Model
{
    public class Approve_Model
    {
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

        public class DataApproveList
        {
            public string app_id { get; set; }
            public string licno { get; set; }
            public string createdate_transaction { get; set; }
            public string branch_nane { get; set; }
            public string name_typ_car { get; set; }
            public string brand_name { get; set; }
            public string namestatus { get; set; }
        }
    }
}
