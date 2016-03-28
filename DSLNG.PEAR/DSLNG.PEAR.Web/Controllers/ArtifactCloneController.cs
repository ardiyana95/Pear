using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Artifact;
using DSLNG.PEAR.Web.ViewModels.Artifact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Requests.Highlight;

namespace DSLNG.PEAR.Web.Controllers
{
    public class ArtifactCloneController : Controller
    {
        public IArtifactService _artifactService;
        public IHighlightService _highlightService;
        public static string SecretNumber { get; set; }

        public ArtifactCloneController(IArtifactService artifactService, IHighlightService highlightService) {
            _artifactService = artifactService;
            _highlightService = highlightService;
        }

        public ActionResult Display(int id, string secretNumber) {
            ViewBag.Id = id;
            ViewBag.SecretNumber = secretNumber;
            return View();
        }

        public ActionResult View(int id, string secretNumber) {
            var artifactResp = _artifactService.GetArtifact(new GetArtifactRequest { Id = id });
            var previewViewModel = new ArtifactPreviewViewModel();
            previewViewModel.FractionScale = artifactResp.FractionScale;
            previewViewModel.MaxFractionScale = artifactResp.MaxFractionScale;
            switch (artifactResp.GraphicType)
            {
                case "line":
                    {
                        var chartData = _artifactService.GetChartData(artifactResp.MapTo<GetCartesianChartDataRequest>());
                        var reportHighlights = _highlightService.GetReportHighlights(new GetReportHighlightsRequest
                        {
                            TimePeriodes = chartData.TimePeriodes,
                            Type = "Overall",
                            PeriodeType = artifactResp.PeriodeType
                        });
                        previewViewModel.PeriodeType = artifactResp.PeriodeType.ToString();
                        previewViewModel.TimePeriodes = chartData.TimePeriodes;
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.Highlights = reportHighlights.Highlights.MapTo<ArtifactPreviewViewModel.HighlightViewModel>();
                        previewViewModel.LineChart = new LineChartDataViewModel();
                        previewViewModel.LineChart.Title = artifactResp.HeaderTitle;
                        previewViewModel.LineChart.Subtitle = chartData.Subtitle;
                        previewViewModel.LineChart.ValueAxisTitle = artifactResp.Measurement;
                        previewViewModel.LineChart.Series = chartData.Series.MapTo<LineChartDataViewModel.SeriesViewModel>();
                        previewViewModel.LineChart.Periodes = chartData.Periodes;
                    }
                    break;
                case "area":
                    {
                        var chartData = _artifactService.GetChartData(artifactResp.MapTo<GetCartesianChartDataRequest>());
                        var reportHighlights = _highlightService.GetReportHighlights(new GetReportHighlightsRequest
                        {
                            TimePeriodes = chartData.TimePeriodes,
                            Type = "Overall",
                            PeriodeType = artifactResp.PeriodeType
                        });
                        previewViewModel.PeriodeType = artifactResp.PeriodeType.ToString();
                        previewViewModel.TimePeriodes = chartData.TimePeriodes;
                        previewViewModel.Highlights = reportHighlights.Highlights.MapTo<ArtifactPreviewViewModel.HighlightViewModel>();
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.AreaChart = new AreaChartDataViewModel();
                        previewViewModel.AreaChart.Title = artifactResp.HeaderTitle;
                        previewViewModel.AreaChart.Subtitle = chartData.Subtitle;
                        previewViewModel.AreaChart.ValueAxisTitle = artifactResp.Measurement;
                        previewViewModel.AreaChart.Series = chartData.Series.MapTo<AreaChartDataViewModel.SeriesViewModel>();
                        previewViewModel.AreaChart.Periodes = chartData.Periodes;
                    }
                    break;
                case "multiaxis":
                    {
                        var chartData = _artifactService.GetMultiaxisChartData(artifactResp.MapTo<GetMultiaxisChartDataRequest>());
                        var reportHighlights = _highlightService.GetReportHighlights(new GetReportHighlightsRequest
                        {
                            TimePeriodes = chartData.TimePeriodes,
                            Type = "Overall",
                            PeriodeType = artifactResp.PeriodeType
                        });
                        previewViewModel.PeriodeType = artifactResp.PeriodeType.ToString();
                        previewViewModel.TimePeriodes = chartData.TimePeriodes;
                        previewViewModel.Highlights = reportHighlights.Highlights.MapTo<ArtifactPreviewViewModel.HighlightViewModel>();
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.MultiaxisChart = chartData.MapTo<MultiaxisChartDataViewModel>();
                        previewViewModel.MultiaxisChart.Title = artifactResp.HeaderTitle;
                    }
                    break;
                case "combo":
                    {
                        var chartData = _artifactService.GetComboChartData(artifactResp.MapTo<GetComboChartDataRequest>());
                        var reportHighlights = _highlightService.GetReportHighlights(new GetReportHighlightsRequest
                        {
                            TimePeriodes = chartData.TimePeriodes,
                            Type = "Overall",
                            PeriodeType = artifactResp.PeriodeType
                        });
                        previewViewModel.PeriodeType = artifactResp.PeriodeType.ToString();
                        previewViewModel.TimePeriodes = chartData.TimePeriodes;
                        previewViewModel.Highlights = reportHighlights.Highlights.MapTo<ArtifactPreviewViewModel.HighlightViewModel>();
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.ComboChart = chartData.MapTo<ComboChartDataViewModel>();
                        previewViewModel.ComboChart.Title = artifactResp.HeaderTitle;
                    }
                    break;
                case "speedometer":
                    {
                        var chartData = _artifactService.GetSpeedometerChartData(artifactResp.MapTo<GetSpeedometerChartDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.SpeedometerChart = new SpeedometerChartDataViewModel();
                        previewViewModel.SpeedometerChart.Title = artifactResp.HeaderTitle;
                        previewViewModel.SpeedometerChart.Subtitle = chartData.Subtitle;
                        previewViewModel.SpeedometerChart.ValueAxisTitle = artifactResp.Measurement;
                        previewViewModel.SpeedometerChart.Series = chartData.Series.MapTo<SpeedometerChartDataViewModel.SeriesViewModel>();
                        previewViewModel.SpeedometerChart.PlotBands = chartData.PlotBands.MapTo<SpeedometerChartDataViewModel.PlotBandViewModel>();
                    }
                    break;
                case "trafficlight":
                    {
                        var chartData = _artifactService.GetTrafficLightChartData(artifactResp.MapTo<GetTrafficLightChartDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.TrafficLightChart = new TrafficLightChartDataViewModel();
                        previewViewModel.TrafficLightChart.Title = artifactResp.HeaderTitle;
                        previewViewModel.TrafficLightChart.Subtitle = chartData.Subtitle;
                        previewViewModel.TrafficLightChart.ValueAxisTitle = artifactResp.Measurement;
                        previewViewModel.TrafficLightChart.Series = chartData.Series.MapTo<TrafficLightChartDataViewModel.SeriesViewModel>();
                        previewViewModel.TrafficLightChart.PlotBands = chartData.PlotBands.MapTo<TrafficLightChartDataViewModel.PlotBandViewModel>();
                    }
                    break;
                case "tabular":
                    {
                        var chartData = _artifactService.GetTabularData(artifactResp.MapTo<GetTabularDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.Tabular = new TabularDataViewModel();
                        chartData.MapPropertiesToInstance<TabularDataViewModel>(previewViewModel.Tabular);
                        previewViewModel.Tabular.Title = artifactResp.HeaderTitle;
                        //previewViewModel.SpeedometerChart.Series = chartData.Series.MapTo<SpeedometerChartDataViewModel.SeriesViewModel>();
                        //previewViewModel.SpeedometerChart.PlotBands = chartData.PlotBands.MapTo<SpeedometerChartDataViewModel.PlotBandViewModel>();
                    }
                    break;
                case "tank":
                    {
                        var chartData = _artifactService.GetTankData(artifactResp.MapTo<GetTankDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.Tank = new TankDataViewModel();
                        chartData.MapPropertiesToInstance<TankDataViewModel>(previewViewModel.Tank);
                        previewViewModel.Tank.Title = artifactResp.HeaderTitle;
                        previewViewModel.Tank.Subtitle = chartData.Subtitle;
                        previewViewModel.Tank.Id = artifactResp.Tank.Id;
                        //previewViewModel.SpeedometerChart.Series = chartData.Series.MapTo<SpeedometerChartDataViewModel.SeriesViewModel>();
                        //previewViewModel.SpeedometerChart.PlotBands = chartData.PlotBands.MapTo<SpeedometerChartDataViewModel.PlotBandViewModel>();
                    }
                    break;
                case "pie":
                    {
                        var chartData = _artifactService.GetPieData(artifactResp.MapTo<GetPieDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.Pie = chartData.MapTo<PieDataViewModel>();
                        previewViewModel.Pie.Title = artifactResp.HeaderTitle;
                        previewViewModel.Pie.Subtitle = chartData.Subtitle;
                        previewViewModel.Pie.Is3D = artifactResp.Is3D;
                        previewViewModel.Pie.ShowLegend = artifactResp.ShowLegend;
                    }
                    break;
                default:
                    {
                        var chartData = _artifactService.GetChartData(artifactResp.MapTo<GetCartesianChartDataRequest>());
                        if (!artifactResp.AsNetbackChart)
                        {
                            var reportHighlights = _highlightService.GetReportHighlights(new GetReportHighlightsRequest
                            {
                                TimePeriodes = chartData.TimePeriodes,
                                Type = "Overall",
                                PeriodeType = artifactResp.PeriodeType
                            });
                            previewViewModel.Highlights = reportHighlights.Highlights.MapTo<ArtifactPreviewViewModel.HighlightViewModel>();
                        }
                        previewViewModel.PeriodeType = artifactResp.PeriodeType.ToString();
                        previewViewModel.TimePeriodes = chartData.TimePeriodes;
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.BarChart = new BarChartDataViewModel();
                        previewViewModel.BarChart.Title = artifactResp.HeaderTitle;
                        previewViewModel.BarChart.Subtitle = chartData.Subtitle;
                        previewViewModel.BarChart.ValueAxisTitle = artifactResp.Measurement; //.GetMeasurement(new GetMeasurementRequest { Id = viewModel.MeasurementId }).Name;
                        previewViewModel.BarChart.Series = chartData.Series.MapTo<BarChartDataViewModel.SeriesViewModel>();
                        previewViewModel.BarChart.Periodes = chartData.Periodes;
                        previewViewModel.BarChart.SeriesType = chartData.SeriesType;
                    }
                    break;
            }
            return Json(previewViewModel, JsonRequestBehavior.AllowGet);
        }
	}
}