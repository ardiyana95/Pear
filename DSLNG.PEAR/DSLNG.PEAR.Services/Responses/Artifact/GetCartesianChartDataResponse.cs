﻿

using System.Collections.Generic;

namespace DSLNG.PEAR.Services.Responses.Artifact
{
    public class GetCartesianChartDataResponse
    {
        public GetCartesianChartDataResponse()
        {
            Series = new List<SeriesResponse>();
        }
        public string Subtitle { get; set; }
        public string SeriesType { get; set; }
        public string[] Periodes { get; set; }
        public IList<SeriesResponse> Series { get; set; }
        public class SeriesResponse
        {
            public SeriesResponse() {
                Data = new List<double?>();
            }
            public string Name { get; set; }
            public IList<double?> Data { get; set; }
            public string Stack { get; set; }
            public string Color { get; set; }
        }
    }
}
