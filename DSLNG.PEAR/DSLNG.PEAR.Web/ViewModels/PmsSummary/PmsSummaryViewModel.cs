﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.PmsSummary
{
    public class PmsSummaryViewModel
    {
        public int Id { get; set; }
        public string Pillar { get; set; }
        public string Kpi { get; set; }
        public string Unit { get; set; }
        public decimal Weight { get; set; }

        public double? TargetYearly { get; set; }
        public double? TargetMonthly { get; set; }
        public double? TargetYtd { get; set; }

        public double? ActualYearly { get; set; }
        public double? ActualMonthly { get; set; }
        public double? ActualYtd { get; set; }

        public double? IndexYearly { get; set; }
        public double? IndexMonthly { get; set; }
        public double? IndexYtd { get; set; }

        public double? Score { get; set; }

        public string TargetActualYearly
        {
            get
            {
                return string.Format(@"{1} / {0}", TargetYearly.HasValue ? TargetYearly.Value.ToString("0.##") : "-", ActualYearly.HasValue ? ActualYearly.Value.ToString("0.##") : "-");
            }
        }

        public string TargetActualMonthly
        {
            get
            {
                return string.Format(@"{1} / {0}", TargetMonthly.HasValue ? TargetMonthly.Value.ToString("0.##") : "-", ActualMonthly.HasValue ? ActualMonthly.Value.ToString("0.##") : "-");
            }
        }

        public string TargetActualYtd
        {
            get
            {
                return string.Format(@"{1} / {0}", TargetYtd.HasValue ? TargetYtd.Value.ToString("0.##") : "-", ActualYtd.HasValue ? ActualYtd.Value.ToString("0.##") : "-");
            }
        }

        public string IndexYearlyStr
        {
            get
            {
                return string.Format(@"{0}", IndexYearly.HasValue ? IndexYearly.Value.ToString("0.##") : "-");
            }
        }

        public string IndexMonthlyStr
        {
            get
            {
                return string.Format(@"{0}", IndexMonthly.HasValue ? IndexMonthly.Value.ToString("0.##") : "-");
            }
        }

        public string IndexYtdStr
        {
            get
            {
                return string.Format(@"{0}", IndexYtd.HasValue ? IndexYtd.Value.ToString("0.##") : "-");
            }
        }

        public string ScoreStr
        {
            get
            {
                return string.Format(@"{0}", Score.HasValue ? Score.Value.ToString("0.##") : "-");
            }
        }

        public int PillarOrder { get; set; }

        public int KpiOrder { get; set; }

        public string KpiColor { get; set; }

        public string PillarColor { get; set; }

        public double PillarWeight { get; set; }

        public string KpiName
        {
            get { return string.Format(@"{0} ({1})", Kpi, Unit); }
        }

        public string KpiNameWithColor
        {
            get { return string.Format(@"<span class='trafficlight' style='background-color:{0}'></span>{1}", KpiColor, KpiName); }
        }

        public string PillarNameWithColor
        {
            get { return string.Format(@"<span class='trafficlight' style='background-color:{0}'></span>{1}", PillarColor, string.Format(@"{0} ({1})", Pillar, PillarWeight.ToString("0"))); }
        }

        public string TotalScoreColor { get; set; }
    }
}