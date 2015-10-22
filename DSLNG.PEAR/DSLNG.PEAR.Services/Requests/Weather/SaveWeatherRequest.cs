﻿

using DSLNG.PEAR.Data.Enums;
using System;
namespace DSLNG.PEAR.Services.Requests.Weather
{
    public class SaveWeatherRequest
    {
        public int Id { get; set; }
        public PeriodeType PeriodeType { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }
        public string Temperature { get; set; }
    }
}
