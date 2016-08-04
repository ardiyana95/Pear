﻿

using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using DSLNG.PEAR.Web.ViewModels.DerTransaction;
using System.Collections.Generic;
using System.Linq;

namespace DSLNG.PEAR.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString LimitString(this HtmlHelper htmlHelper, string source, int length)
        {
            if (!string.IsNullOrEmpty(source) && source.Length > length)
            {
                var result = source.Substring(0, length);
                result += "... <a class=\"see-more\" href=\"#\" data-toggle=\"modal\" data-target=\"#modalDialog\">See More</a>";
                result += "<div style=\"display:none;color:#fff\" class=\"full-string\">";
                result += source;
                result += "</div>";
                return MvcHtmlString.Create(result);
            }
            return MvcHtmlString.Create(source);
        }

        public static string ParseToDateOrNumber(this HtmlHelper htmlHelper, string val)
        {
            var resultDate = new DateTime();
            bool isValid = false;
            if (string.IsNullOrEmpty(val))
            {
                return val;
            }

            if (val.Length == 4)
            {
                return val;
            }
            if (val.Length == 6 || val.Length == 7)
            {
                if (val.Length == 6)
                {
                    isValid = DateTime.TryParseExact("01-" + val, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                                                     DateTimeStyles.AllowWhiteSpaces, out resultDate);
                }
                else if (val.Length == 7)
                {
                    isValid = DateTime.TryParseExact("01-0" + val, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                                                     DateTimeStyles.AllowWhiteSpaces, out resultDate);
                }
            }
            else if (val.Length >= 20 && val.Length <= 22)
            {
                isValid = DateTime.TryParseExact(val, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out resultDate);
            }
            else
            {
                isValid = DateTime.TryParseExact(val, "d-M-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out resultDate);
            }

            return isValid ? resultDate.ToString("dd MMM yyyy") : ParseToNumber(val);
        }

        public static string ParseToNumber(this HtmlHelper htmlHelper, string val)
        {
            return ParseToNumber(val);
        }

        public static string DisplayDerValue(this HtmlHelper htmlHelper, string val, string defaultVal = "N/A", bool isRounded = true)
        {

            return !string.IsNullOrEmpty(val) ? RoundIt(isRounded, val) : defaultVal;
        }

        public static string DisplayCompleteDerValue(this HtmlHelper htmlHelper, string val, string measurement, string defaultMeasurement, string defaultVal = "N/A",
            bool isRounded = true)
        {
            if (
                (!string.IsNullOrEmpty(measurement) && measurement.ToLowerInvariant() == "mmbtu") ||
                (!string.IsNullOrEmpty(measurement) && measurement.ToLowerInvariant() == "bbtu") ||
                (!string.IsNullOrEmpty(defaultMeasurement) && defaultMeasurement.ToLowerInvariant() == "mmbtu") ||
                (!string.IsNullOrEmpty(defaultMeasurement) && defaultMeasurement.ToLowerInvariant() == "bbtu")
                )
            {
                if (string.IsNullOrEmpty(val))
                {
                    return defaultVal;
                }
                else
                {
                    return string.Format("{0} {1}", RoundIt(isRounded, val, 0), string.IsNullOrEmpty(measurement) ? defaultMeasurement : measurement);
                }
            }

            return !string.IsNullOrEmpty(val) ?
                string.Format("{0} {1}", RoundIt(isRounded, val), string.IsNullOrEmpty(measurement) ? defaultMeasurement : measurement) : defaultVal;
        }


        public static string DisplayCompleteDerValueForMmbtu(this HtmlHelper htmlHelper, string val, string defaultVal = "N/A")
        {
            if (string.IsNullOrEmpty(val))
            {
                return defaultVal;
            }
            else
            {
                return string.Format("{0} {1}", RoundIt(true, val, 0), "mmbtu");
            }
        }

        public static string DisplayCompleteDerValueForPlantAvailability(this HtmlHelper htmlHelper, string val, string defaultVal = "N/A")
        {
            if (string.IsNullOrEmpty(val))
            {
                return defaultVal;
            }
            else
            {
                return string.Format("{0} {1}", RoundIt(true, val, 1), "%");
            }
        }

        public static string DisplayDerLabel(this HtmlHelper htmlHelper, string val, string defaultVal)
        {
            return string.IsNullOrEmpty(val) ? defaultVal : val;
        }

        public static string DisplayDerDeviation(this HtmlHelper htmlHelper, string deviation)
        {
            if (string.IsNullOrEmpty(deviation)) return string.Empty;
            switch (deviation)
            {
                case "1":
                    return "fa-arrow-up";
                case "-1":
                    return "fa-arrow-down";
                case "0":
                    return "fa-minus";
                default:
                    return string.Empty;
            }
        }

        public static string DisplayDerRemark(this HtmlHelper htmlHelper, string remark)
        {
            return RemarkToIcon(remark);
        }

        public static MvcHtmlString DisplayDerRemarkJson(this HtmlHelper htmlHelper, string remarkJson, string type)
        {
            if (string.IsNullOrEmpty(remarkJson)) return new MvcHtmlString(string.Empty);
            try
            {
                var jsonRemark = JsonConvert.DeserializeObject<JsonRemark>(remarkJson);

                switch (type.ToLowerInvariant())
                {
                    case "daily":
                    case "as of":
                        return RemarkToMvcHtmlString(jsonRemark.Daily);
                    case "mtd":
                        return RemarkToMvcHtmlString(jsonRemark.Mtd);
                    case "ytd":
                        return RemarkToMvcHtmlString(jsonRemark.Ytd);
                    default:
                        return new MvcHtmlString(string.Empty);
                }
            }
            catch (JsonSerializationException exception)
            {
                return new MvcHtmlString(string.Empty);
            }
            catch (Exception exception)
            {
                return new MvcHtmlString(string.Empty);
            }
        }

        public static MvcHtmlString DisplayDerRemarkJsonForLngAndCds(this HtmlHelper htmlHelper, string remarkJson, string type)
        {
            if (string.IsNullOrEmpty(remarkJson)) return new MvcHtmlString(string.Empty);
            try
            {
                var jsonRemark = JsonConvert.DeserializeObject<JsonRemark>(remarkJson);

                switch (type.ToLowerInvariant())
                {
                    case "daily":
                    case "as of":
                        return RemarkToMvcHtmlStringForLngAndCds(jsonRemark.Daily);
                    case "mtd":
                        return RemarkToMvcHtmlStringForLngAndCds(jsonRemark.Mtd);
                    case "ytd":
                        return RemarkToMvcHtmlStringForLngAndCds(jsonRemark.Ytd);
                    default:
                        return new MvcHtmlString(string.Empty);
                }
            }
            catch (JsonSerializationException exception)
            {
                return new MvcHtmlString(string.Empty);
            }
            catch (Exception exception)
            {
                return new MvcHtmlString(string.Empty);
            }
        }

        public static string DisplayDerRemarkWithValue(this HtmlHelper htmlHelper, string deviation)
        {
            if (string.IsNullOrEmpty(deviation)) return string.Empty;
            switch (deviation.ToLowerInvariant())
            {
                case "4":
                case "fulfilled":
                    return "<span class='indicator left-side'><i class='fa fa-check' style='color:green'></i></span>Fulfilled";
                case "3":
                case "on-track":
                    return "<span class='indicator left-side'><i class='fa fa-circle' style='color:green'></i></span>On-track";
                case "2":
                case "loading":
                    return "<span class='indicator left-side'><i class='fa fa-arrow-right' style='color:grey'></i></span>Loading";
                case "1":
                case "need attention":
                    return "<span class='indicator left-side'><i class='fa fa-circle' style='color:orange'></i></span>Need attention";
                case "0":
                case "unfulfilled":
                    return "<span class='indicator left-side'><i class='fa fa-circle' style='color:red'></i></span>Unfulfilled";
                default:
                    return string.Empty;
            }
        }

        public static string DisplayDerRemarkForMarketTone(this HtmlHelper htmlHelper, string deviation)
        {
            if (string.IsNullOrEmpty(deviation)) return string.Empty;
            switch (deviation)
            {
                case "1":
                    return "<span class='comparison'><i class='fa fa-arrow-up' style='color:green'></i></span>";
                case "0":
                    return "<span class='comparison'><i class='fa fa-minus' style='color:orange'></i></span>";
                case "-1":
                    return "<span class='comparison'><i class='fa fa-arrow-down' style='color:red'></i></span>";
                default:
                    return string.Empty;
            }
        }

        public static string DisplayDerValueWithLabelAtFront(this HtmlHelper htmlHelper, string measurement, string val, string defaultMeasurement, string defaultVal = "N/A", bool isRounded = true)
        {
            return !string.IsNullOrEmpty(val) ?
                string.Format("{1} {0}", RoundIt(isRounded, val), string.IsNullOrEmpty(measurement) ? defaultMeasurement : measurement) : defaultVal;
        }

        public static string Divide(this HtmlHelper htmlHelper, string val, int number)
        {
            if (string.IsNullOrEmpty(val)) return val;
            double x = double.Parse(val);
            return (x / number).ToString(CultureInfo.InvariantCulture);
        }

        public static string DisplayDerValueWithHours(this HtmlHelper htmlHelper, string val, string defaultVal = "N/A")
        {
            if (!string.IsNullOrEmpty(val))
            {
                double v = double.Parse(val);
                TimeSpan span = TimeSpan.FromMinutes(v);
                return span.ToString(@"hh\:mm");
            }

            return defaultVal;
        }

        public static string GetCssClassByDerValue(this HtmlHelper htmlHelper, string val, bool isCss = false)
        {
            if (!string.IsNullOrEmpty(val))
            {
                string x = val.Replace("<p>", "").Replace("</p>", "");
                return (isCss) ? x.Replace(' ', '-') : x;
            }

            return string.Empty;
        }
        private static string RoundIt(bool isRounded, string val, int number = 2)
        {
            if (isRounded)
            {
                /*double v = double.Parse(val);
                val = Math.Round(v, 2).ToString(CultureInfo.InvariantCulture);*/
                return ParseToNumber(val, number);
            }

            return val;
        }

        private static string ParseToNumber(string val, int number = 2)
        {
            double x;
            var styles = NumberStyles.AllowParentheses | NumberStyles.AllowTrailingSign | NumberStyles.Float | NumberStyles.AllowDecimalPoint;
            bool isValidDouble = Double.TryParse(val, styles, NumberFormatInfo.InvariantInfo, out x);


            //return isValidDouble ? Str x.ToString("0:0.###") : val;
            //return isValidDouble ? string.Format("{0:0,000.###}", x) : val;
            if (number == 0)
            {
                return isValidDouble ? Math.Round(x).ToString("#,#", CultureInfo.InvariantCulture) : val;
            }
            else if (number == 1)
            {
                return isValidDouble ? Math.Round(x,1).ToString("#,#.#", CultureInfo.InvariantCulture) : val;
            }
            return isValidDouble ? string.Format("{0:#,##0.##}", x) : val;
        }

        private static string RemarkToIcon(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            switch (s)
            {
                case "1":
                    return "fa-circle";
                case "-1":
                    return "fa-times-circle";
                case "0":
                    return "fa-exclamation-circle";
                default:
                    return string.Empty;
            }
        }

        private static MvcHtmlString RemarkToMvcHtmlString(string s)
        {
            if (string.IsNullOrEmpty(s)) return new MvcHtmlString(string.Empty);
            switch (s)
            {
                case "1":
                    return new MvcHtmlString("<span class='indicator absolute'><i class='fa fa-circle'></i></span>");
                case "-1":
                    return new MvcHtmlString("<span class='indicator absolute'><i class='fa fa-times-circle'></i></span>");
                case "0":
                    return new MvcHtmlString("<span class='indicator absolute'><i class='fa fa-exclamation-circle'></i></span>");
                default:
                    return new MvcHtmlString(string.Empty);
            }
        }

        private static MvcHtmlString RemarkToMvcHtmlStringForLngAndCds(string s)
        {
            if (string.IsNullOrEmpty(s)) return new MvcHtmlString(string.Empty);
            switch (s.ToLowerInvariant())
            {
                case "4":
                case "fulfilled":
                    return new MvcHtmlString("<span class='indicator left-side'><i class='fa fa-check' style='color:green'></i></span>Fulfilled");
                case "3":
                case "on-track":
                    return new MvcHtmlString("<span class='indicator left-side'><i class='fa fa-circle' style='color:green'></i></span>On-track");
                case "2":
                case "loading":
                    return new MvcHtmlString("<span class='indicator left-side'><i class='fa fa-arrow-right' style='color:grey'></i></span>Loading");
                case "1":
                case "need attention":
                    return new MvcHtmlString("<span class='indicator left-side'><i class='fa fa-circle' style='color:orange'></i></span>Need attention");

                case "0":
                case "unfulfilled":
                    return new MvcHtmlString("<span class='indicator left-side'><i class='fa fa-circle' style='color:red'></i></span>Unfulfilled");

                default:
                    return new MvcHtmlString(string.Empty);
            }
        }

        public static MvcHtmlString DisplayKpiInformationInput(this HtmlHelper htmlHelper, IList<DerValuesViewModel.KpiInformationValuesViewModel> kpiInformations,int position, string type, string id, string next, string placeholder = "rate", bool previousValue = true, string defaultValue = "") {
            string value = defaultValue;
            var kpiInformation = kpiInformations.First(x => x.Position == position);
            if (previousValue)
            {
                switch (type)
                {
                    case "daily-actual":
                        value = kpiInformation.DailyActual == null ? value : kpiInformation.DailyActual.Value.ToString();
                        break;
                    case "daily-target":
                        value = kpiInformation.DailyTarget == null ? value : kpiInformation.DailyTarget.Value.ToString();
                        break;

                }
            }
            return new MvcHtmlString(string.Format("<input onkeypress=\"moveFieldTo(event, '{1}');\" type=\"text\" value=\"{0}\" class=\"der-text-yesterday form-control\" id=\"{2}\"  placeholder=\"{3}\" />", value, next, id, placeholder));
        }
    }

    public class JsonRemark
    {
        public string Daily { get; set; }
        public string Mtd { get; set; }
        public string Ytd { get; set; }
    }
}