using System;
using System.Collections.Generic;
using System.Text;

namespace Check_CarPrice.Model
{
   public class InsertCarPrice_Model
    {

        public class DataStyle1
        {
            public string cde_head_tail { get; set; }
            public string name_head_tail { get; set; }
            public string cde_grpcar { get; set; }
            public string name_grpcar { get; set; }
        }

        public class Style1
        {
            public string msg { get; set; }
            public List<DataStyle1> data { get; set; }
        }

        public class DataStyle2
        {
            public string cde_head_tail { get; set; }
            public string name_head_tail { get; set; }
            public string cde_grpcar { get; set; }
            public string name_grpcar { get; set; }
            public string cde_style { get; set; }
            public string name_style { get; set; }
        }

        public class Style2
        {
            public string msg { get; set; }
            public List<DataStyle2> data { get; set; }
        }

        public class DataStyle3
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
            public string name_style_car { get; set; }
            public string type_car { get; set; }
            public string type_style { get; set; }
            public string name_type_style { get; set; }
        }

        public class Style3
        {
            public string msg { get; set; }
            public List<DataStyle3> data { get; set; }
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

        public class DataVStyle1
        {
            public string cde_head_tail { get; set; }
            public string name_head_tail { get; set; }
            public string cde_grpcar { get; set; }
            public string name_grpcar { get; set; }
        }

        public class VStyle1
        {
            public string msg { get; set; }
            public List<DataVStyle1> data { get; set; }
        }
        public class DataVStyle2
        {
            public string cde_grpcar { get; set; }
            public string name_grpcar { get; set; }
            public string cde_style { get; set; }
            public string name_style { get; set; }
            public string cde_head_tail { get; set; }
        }

        public class VStyle2
        {
            public string msg { get; set; }
            public List<DataVStyle2> data { get; set; }
        }

        public class DataVStyle3
        {
            public string cde_head_tail { get; set; }
            public string name_head_tail { get; set; }
            public string cde_grpcar { get; set; }
            public string name_grpcar { get; set; }
            public string cde_style { get; set; }
            public string name_style { get; set; }
            public string cde_style_car { get; set; }
            public string name_style_car { get; set; }
            public string cde_type_style { get; set; }
            public string name_type_style { get; set; }
        }

        public class VStyle3
        {
            public string msg { get; set; }
            public List<DataVStyle3> data { get; set; }
        }
    }
}
