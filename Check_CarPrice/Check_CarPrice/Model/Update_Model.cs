using System;
using System.Collections.Generic;
using System.Text;

namespace Check_CarPrice.Model
{
    public class Update_Model
    {
      

        public class DataProvince
        {
            public string CODE { get; set; }
            public string DESC_TH { get; set; }
            public string DESC_EN { get; set; }
        }

      
        public class DataStyleCar
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

        public class StyleCar
        {
            public string msg { get; set; }
            public List<DataStyleCar> data { get; set; }
        }

        public class DataAllStyleCar
        {
            public string type_grp_car { get; set; }
            public string grp_car { get; set; }
            public string namegrp_car { get; set; }
            public string cde_pickup { get; set; }
            public string name_pickup { get; set; }
            public string cde_special { get; set; }
            public string name_special { get; set; }
        }

        public class AllStyleCar
        {
            public string msg { get; set; }
            public List<DataAllStyleCar> data { get; set; }
        }

        public class DataPicture
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
            public string type_grp_car { get; set; }
            public string create_date { get; set; }
            public string end_date { get; set; }
            public string create_by { get; set; }
            public int status { get; set; }
        }

        public class Picture
        {
            public string msg { get; set; }
            public List<DataPicture> data { get; set; }
        }

    }
}
