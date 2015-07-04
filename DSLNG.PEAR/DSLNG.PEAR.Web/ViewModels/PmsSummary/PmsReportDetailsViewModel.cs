﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.PmsSummary
{
    public class PmsReportDetailsViewModel
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public string KpiTypeYearly { get; set; }
        public string KpiPeriodYearly { get; set; }
        public string KpiRemarkYearly { get; set; }
        public string KpiGroup { get; set; }
        public string KpiName { get; set; }
        public string KpiUnit { get; set; }
        public string KpiPeriod { get; set; }
        public double? KpiActualYearly { get; set; }
        public double? KpiActualMonthly { get; set; }
        public List<KpiAchievment> KpiAchievmentMonthly { get; set; }
        public List<KpiRelation> KpiRelations { get; set; }

        public class KpiAchievment
        {
            public string Type { get; set; }
            public string Period { get; set; }
            public string Remark { get; set; }
        }

        public class KpiRelation
        {
            public string Name { get; set; }
            public string Unit { get; set; }
            public string Method { get; set; }
            public double? ActualYearly { get; set; }
            public double? ActualMonthly { get; set; }
        }
    }
}