﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Check_CarPrice.Model
{
    public class Dashboard_Models
    {
        public class DataDashboardView
        {
            public string app_id { get; set; }
            public string status_appcar { get; set; }
            public string status { get; set; }
            public string create_date { get; set; }
            public string create_by { get; set; }
            public string branch_code { get; set; }
            public string branch_nane { get; set; }
            public string hub { get; set; }
        }

        public class DashboardView
        {
            public string msg { get; set; }
            public List<DataDashboardView> data { get; set; }
        }

    }
}
