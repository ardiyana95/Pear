

using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.OutputConfig;
using DSLNG.PEAR.Services.Responses.OutputConfig;
using System.Linq;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Data.Entities;
using System.Collections;
using DSLNG.PEAR.Data.Entities.EconomicModel;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Collections.Generic;
using DSLNG.PEAR.Data.Enums;
using System;
using System.Globalization;
using DSLNG.PEAR.Common.Calculator;
using DSLNG.PEAR.Common.Helpers;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services
{
    public class OutputConfigService : BaseService, IOutputConfigService
    {

        public OutputConfigService(IDataContext dataContext) : base(dataContext) { }

        public GetKpisResponse GetKpis(GetKpisRequest request)
        {
            return new GetKpisResponse
            {
                KpiList = DataContext.Kpis.Include(x => x.Measurement).Where(x => x.Name.Contains(request.Term) && x.IsEconomic == true).Take(20).ToList().MapTo<GetKpisResponse.Kpi>()
            };
        }


        public GetKeyAssumptionsResponse GetKeyAssumptions(GetKeyAssumptionsRequest request)
        {
            return new GetKeyAssumptionsResponse
            {
                KeyAssumptions = DataContext.KeyAssumptionConfigs.Where(x => x.Name.Contains(request.Term)).Take(20).ToList().MapTo<GetKeyAssumptionsResponse.KeyAssumption>()
            };
        }

        public SaveOutputConfigResponse Save(SaveOutputConfigRequest request)
        {
            try
            {
                var outputConfig = request.MapTo<KeyOutputConfiguration>();
                if (request.Id != 0)
                {
                    outputConfig = DataContext.KeyOutputConfigs.Include(x => x.Measurement)
                        .Include(x => x.Category)
                       .Include(x => x.Kpis)
                       .Include(x => x.KeyAssumptions)
                       .First(x => x.Id == request.Id);
                    request.MapPropertiesToInstance<KeyOutputConfiguration>(outputConfig);
                    foreach (var kpi in outputConfig.Kpis.ToList())
                    {
                        outputConfig.Kpis.Remove(kpi);
                    }
                    foreach (var assumption in outputConfig.KeyAssumptions.ToList())
                    {
                        outputConfig.KeyAssumptions.Remove(assumption);
                    }
                    outputConfig.Measurement = DataContext.Measurements.FirstOrDefault(x => x.Id == request.MeasurementId);
                    if (request.CategoryId != outputConfig.Category.Id)
                    {
                        var category = new KeyOutputCategory { Id = request.CategoryId };
                        DataContext.KeyOutputCategories.Attach(category);
                        outputConfig.Category = category;
                    }

                }
                else
                {
                    if (request.MeasurementId != 0)
                    {
                        var measurement = new Measurement { Id = request.MeasurementId };
                        DataContext.Measurements.Attach(measurement);
                        outputConfig.Measurement = measurement;
                    }
                    var category = new KeyOutputCategory { Id = request.CategoryId };
                    DataContext.KeyOutputCategories.Attach(category);
                    outputConfig.Category = category;

                    DataContext.KeyOutputConfigs.Add(outputConfig);
                }
                foreach (var kpiId in request.KpiIds)
                {
                    var kpi = new Kpi { Id = kpiId };
                    if (DataContext.Kpis.Local.FirstOrDefault(x => x.Id == kpiId) == null)
                    {
                        DataContext.Kpis.Attach(kpi);
                    }
                    else
                    {
                        kpi = DataContext.Kpis.Local.FirstOrDefault(x => x.Id == kpiId);
                    }
                    outputConfig.Kpis.Add(kpi);
                }
                foreach (var assumptionId in request.KeyAssumptionIds)
                {
                    var assumption = new KeyAssumptionConfig { Id = assumptionId };
                    if (DataContext.KeyAssumptionConfigs.Local.FirstOrDefault(x => x.Id == assumptionId) == null)
                    {
                        DataContext.KeyAssumptionConfigs.Attach(assumption);
                    }
                    else
                    {
                        assumption = DataContext.KeyAssumptionConfigs.Local.FirstOrDefault(x => x.Id == assumptionId);
                    }
                    outputConfig.KeyAssumptions.Add(assumption);
                }
                DataContext.SaveChanges();
                return new SaveOutputConfigResponse
                {
                    IsSuccess = true,
                    Message = "The item has been saved successfully"
                };
            }
            catch
            {
                return new SaveOutputConfigResponse
                {
                    IsSuccess = false,
                    Message = "An Error Occured please contact the administrator for further information"
                };
            }
        }


        public GetOutputConfigResponse Get(GetOutputConfigRequest request)
        {
            var config = DataContext.KeyOutputConfigs.Include(x => x.Measurement)
                .Include(x => x.Category)
                .Include(x => x.Kpis)
                .Include(x => x.KeyAssumptions)
                .FirstOrDefault(x => x.Id == request.Id);
            return config.MapTo<GetOutputConfigResponse>();
        }
        public GetOutputConfigsResponse GetOutputConfigs(GetOutputConfigsRequest request)
        {
            int totalRecords;
            var data = SortData(request.Search, request.SortingDictionary, out totalRecords);
            if (request.Take != -1)
            {
                data = data.Skip(request.Skip).Take(request.Take);
            }

            return new GetOutputConfigsResponse
            {
                TotalRecords = totalRecords,
                OutputConfigs = data.ToList().MapTo<GetOutputConfigsResponse.OutputConfig>()
            };
        }


        public IEnumerable<KeyOutputConfiguration> SortData(string search, IDictionary<string, SortOrder> sortingDictionary, out int TotalRecords)
        {
            var data = DataContext.KeyOutputConfigs.Include(x => x.Category).Include(x => x.Measurement).Include(x => x.KeyAssumptions).Include(x => x.Kpis).AsQueryable();
            if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
            {
                data = data.Where(x => x.Name.Contains(search) || x.Category.Name.Contains(search) || x.Measurement.Name.Contains(search));
            }

            foreach (var sortOrder in sortingDictionary)
            {
                switch (sortOrder.Key)
                {
                    case "Name":
                        data = sortOrder.Value == SortOrder.Ascending
                            ? data.OrderBy(x => x.Name).ThenBy(x => x.Order)
                            : data.OrderByDescending(x => x.Name).ThenBy(x => x.Order);
                        break;
                    case "Category":
                        data = sortOrder.Value == SortOrder.Ascending
                            ? data.OrderBy(x => x.Category.Name).ThenBy(x => x.Order)
                            : data.OrderByDescending(x => x.Category.Name).ThenBy(x => x.Order);
                        break;
                    case "Measurement":
                        data = sortOrder.Value == SortOrder.Ascending
                            ? data.OrderBy(x => x.Measurement.Name).ThenBy(x => x.Order)
                            : data.OrderByDescending(x => x.Measurement.Name).ThenBy(x => x.Order);
                        break;
                    case "Formula":
                        data = sortOrder.Value == SortOrder.Ascending
                            ? data.OrderBy(x => x.Formula).ThenBy(x => x.Order)
                            : data.OrderByDescending(x => x.Formula).ThenBy(x => x.Order);
                        break;
                    case "Order":
                        data = sortOrder.Value == SortOrder.Ascending
                            ? data.OrderBy(x => x.Order)
                            : data.OrderByDescending(x => x.Order);
                        break;
                    case "IsActive":
                        data = sortOrder.Value == SortOrder.Ascending
                            ? data.OrderBy(x => x.IsActive).ThenBy(x => x.Order)
                            : data.OrderByDescending(x => x.IsActive).ThenBy(x => x.Order);
                        break;
                }
            }

            TotalRecords = data.Count();
            return data;
        }




        public CalculateOutputResponse CalculateOputput(CalculateOutputRequest request)
        {
            var resp = SerializationHelper.DeSerializeObject<CalculateOutputResponse>("output-scenario-" + request.ScenarioId);
            if (resp != null && !request.UpdateResult)
            {
                return resp;
            }
            resp = new CalculateOutputResponse();
            var categories = DataContext.KeyOutputCategories
                 .Include(x => x.KeyOutputs)
                 .Include(x => x.KeyOutputs.Select(y => y.Measurement))
                 .Include(x => x.KeyOutputs.Select(y => y.Kpis))
                 .Include(x => x.KeyOutputs.Select(y => y.KeyAssumptions))
                 .Where(x => x.IsActive && x.KeyOutputs.Any(y => y.IsActive)).OrderBy(x => x.Order).ToList();
            foreach (var category in categories)
            {
                var categoryResp = category.MapTo<CalculateOutputResponse.OutputCategoryResponse>();
                foreach (var keyOutput in category.KeyOutputs.Where(x => x.IsActive).OrderBy(x => x.Order).ToList())
                {
                    var keyOutputResp = this.CalculateFormula(keyOutput, request.ScenarioId);
                    categoryResp.KeyOutputs.Add(keyOutputResp);
                }
                resp.OutputCategories.Add(categoryResp);
            }
                SerializationHelper.SerializeObject<CalculateOutputResponse>(resp, "output-scenario-" + request.ScenarioId);
            return resp;
        }

        private CalculateOutputResponse.KeyOutputResponse CalculateFormula(KeyOutputConfiguration keyOutput, int scenarioId)
        {
            var resp = keyOutput.MapTo<CalculateOutputResponse.KeyOutputResponse>();
            switch (keyOutput.Formula)
            {
                case Formula.SUM:
                    {
                        var result = this.Sum(keyOutput, scenarioId);
                        resp.Actual = result.Actual;
                        resp.Forecast = result.Forecast;
                    }
                    break;
                case Formula.AVERAGE:
                    {
                        var result = this.Average(keyOutput, scenarioId);
                        resp.Actual = result.Actual;
                        resp.Forecast = result.Forecast;
                    }
                    break;
                case Formula.MIN:
                    {
                        var result = this.Min(keyOutput, scenarioId);
                        resp.Actual = result.Actual;
                        resp.Forecast = result.Forecast;
                    }
                    break;
                //is it daily
                case Formula.MINDATE:
                    {
                        var result = this.MinDate(keyOutput, scenarioId);
                        resp.Actual = result.Actual;
                        resp.Forecast = result.Forecast;
                    }
                    break;
                case Formula.BREAKEVENTYEAR:
                    {
                        var result = this.BreakEventYear(keyOutput, scenarioId);
                        resp.Actual = result.Actual;
                        resp.Forecast = result.Forecast;
                    }
                    break;
                case Formula.PAYBACK:
                    {
                        var result = this.Payback(keyOutput, scenarioId);
                        resp.Actual = result.Actual;
                        resp.Forecast = result.Forecast;
                    }
                    break;
                case Formula.PROFITINVESTMENTRATIO:
                    {
                        var result = this.ProfitInvestmentRatio(keyOutput, scenarioId);
                        resp.Actual = result.Actual;
                        resp.Forecast = result.Forecast;
                    }
                    break;
                case Formula.COMPLETIONDATE:
                    {
                        var result = this.CompletionDate(keyOutput, scenarioId);
                        resp.Actual = result.Actual;
                        resp.Forecast = result.Forecast;
                    }
                    break;
                case Formula.GROSSPROFIT:
                    {
                        var result = this.HeaderSubstruction(keyOutput, scenarioId);
                        resp.Actual = result.Actual;
                        resp.Forecast = result.Forecast;
                    }
                    break;
                case Formula.NETBACKVALUE:
                    {
                        var result = this.HeaderSubstruction(keyOutput, scenarioId, true);
                        resp.Actual = result.Actual;
                        resp.Forecast = result.Forecast;
                    }
                    break;
                case Formula.PROJECTIRR:
                case Formula.EQUITYIRR:
                    {
                        var result = this.Irr(keyOutput, scenarioId);
                        resp.Actual = result.Actual;
                        resp.Forecast = result.Forecast;
                    }
                    break;
            }
            if (keyOutput.ConversionType.HasValue && keyOutput.ConversionValue.HasValue) {
                if (!string.IsNullOrEmpty(resp.Actual)) {
                    switch (keyOutput.ConversionType) { 
                        case ConversionType.Division:
                            resp.Actual = (double.Parse(resp.Actual) / keyOutput.ConversionValue.Value).ToString();
                            break;
                        default:
                             resp.Actual = (double.Parse(resp.Actual) * keyOutput.ConversionValue.Value).ToString();
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(resp.Forecast)) {
                    switch (keyOutput.ConversionType)
                    {
                        case ConversionType.Division:
                            resp.Forecast = (double.Parse(resp.Forecast) / keyOutput.ConversionValue.Value).ToString();
                            break;
                        default:
                            resp.Forecast = (double.Parse(resp.Forecast) * keyOutput.ConversionValue.Value).ToString();
                            break;
                    }
                }
            }
            return resp;
        }

        private bool IsStartAndEndValid(string start, string end, out DateTime startDate, out DateTime endDate)
        {
            if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                var startValid = false;
                if (start.Length == 4)
                {
                    startValid = DateTime.TryParseExact("01-01-" + start, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out startDate);
                }
                else if (start.Length > 5 && start.Length <= 7)
                {
                    if (start.Length == 6)
                    {
                        startValid = DateTime.TryParseExact("01-0" + start, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out startDate);
                    }
                    else
                    {
                        startValid = DateTime.TryParseExact("01-" + start, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out startDate);
                    }
                }
                else
                {
                    startValid = DateTime.TryParseExact(start, "d-M-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out startDate);
                }
                var endValid = false;
                if (end.Length == 4)
                {
                    endValid = DateTime.TryParseExact("01-01-" + end, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out endDate);
                }
                else if (end.Length > 5 && end.Length <= 7)
                {
                    if (end.Length == 6)
                    {
                        endValid = DateTime.TryParseExact("01-0" + end, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out endDate);
                    }
                    else
                    {
                        endValid = DateTime.TryParseExact("01-" + end, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out endDate);
                    }
                }
                else
                {
                    endValid = DateTime.TryParseExact(end, "d-M-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out endDate);
                }
                return startValid && endValid;
            }
            startDate = DateTime.Now;
            endDate = DateTime.Now;
            return false;
        }


        private OutputResult Sum(KeyOutputConfiguration keyOutput, int scenarioId, int kpiIdIndex = 0)
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var result = new OutputResult();
            var kpiIds = keyOutput.KpiIds.Split(',').Select(x => int.Parse(x)).ToList();
            var kpiId = keyOutput.Kpis.First(x => x.Id == kpiIds[kpiIdIndex]).Id;
            var assumptionIds = keyOutput.KeyAssumptionIds.Split(',').Select(x => int.Parse(x)).ToList();
            var startId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[0]).Id;
            var endId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[1]).Id;
            var startAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == startId);
            var endAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == endId);
            if (startAssumption == null || endAssumption == null)
            {
                return new OutputResult();
            }
            DateTime startForecast;
            DateTime endForecast;
            if (IsStartAndEndValid(startAssumption.ForecastValue, endAssumption.ForecastValue, out startForecast, out endForecast))
            {
                var forecastValue = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId && x.Scenario.Id == scenarioId
                    && x.Periode.Year >= startForecast.Year && x.Periode.Year <= endForecast.Year && x.PeriodeType == PeriodeType.Yearly)
                    .Sum(x => x.Value);
                if (forecastValue.HasValue) result.Forecast = forecastValue.ToString();
            }

            DateTime startActual;
            DateTime endActual;
            if (IsStartAndEndValid(startAssumption.ActualValue, endAssumption.ActualValue, out startActual, out endActual))
            {
                var pastValue = DataContext.KpiAchievements.Where(x => x.Kpi.Id == kpiId
                    && x.Periode.Year >= startActual.Year && x.Periode.Year < currentYear && x.PeriodeType == PeriodeType.Yearly).Sum(x => x.Value);
                var futureValue = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId
                    && x.Periode.Year <= endActual.Year && x.Periode.Year > currentYear && x.PeriodeType == PeriodeType.Yearly && x.Scenario.Id == scenarioId).Sum(x => x.Value);
                var untilNowThisYearValue = DataContext.KpiAchievements.Where(x => x.Kpi.Id == kpiId && x.Periode.Year == currentYear
                    && x.Periode.Month < currentMonth && x.PeriodeType == PeriodeType.Monthly).Sum(x => x.Value);
                var thisYearForecastValue = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId && x.Periode.Year == currentYear
                    && x.Periode.Month >= currentMonth && x.PeriodeType == PeriodeType.Monthly && x.Scenario.Id == scenarioId).Sum(x => x.Value);
                var actualValues =
                    new List<double?> { pastValue, futureValue, untilNowThisYearValue, thisYearForecastValue };
                if (actualValues.Any(x => x.HasValue))
                {
                    result.Actual = actualValues.Sum().ToString();
                }
            }
            return result;
        }

        private OutputResult Average(KeyOutputConfiguration keyOutput, int scenarioId)
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var result = new OutputResult();
            var kpiIds = keyOutput.KpiIds.Split(',').Select(x => int.Parse(x)).ToList();
            var kpiId = keyOutput.Kpis.First(x => x.Id == kpiIds[0]).Id;
            var assumptionIds = keyOutput.KeyAssumptionIds.Split(',').Select(x => int.Parse(x)).ToList();
            var startId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[0]).Id;
            var endId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[1]).Id;
            var startAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == startId);
            var endAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == endId);
            if (startAssumption == null || endAssumption == null)
            {
                return new OutputResult();
            }
            DateTime startForecast;
            DateTime endForecast;
            if (IsStartAndEndValid(startAssumption.ForecastValue, endAssumption.ForecastValue, out startForecast, out endForecast))
            {
                var forecastValue = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId && x.Scenario.Id == scenarioId
                && x.Periode.Year >= startForecast.Year && x.Periode.Year <= endForecast.Year && x.PeriodeType == PeriodeType.Yearly)
                .Average(x => x.Value);
                if (forecastValue.HasValue) result.Forecast = forecastValue.ToString();
            }

            DateTime startActual;
            DateTime endActual;
            if (IsStartAndEndValid(startAssumption.ActualValue, endAssumption.ActualValue, out startActual, out endActual))
            {
                var pastValues = DataContext.KpiAchievements.Where(x => x.Kpi.Id == kpiId && x.Periode.Year >= startActual.Year
                    && x.Periode.Year < currentYear && x.PeriodeType == PeriodeType.Yearly).Select(x => x.Value).ToList();
                var futureValues = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId
                    && x.Periode.Year <= endActual.Year && x.Periode.Year > currentYear && x.PeriodeType == PeriodeType.Yearly && x.Scenario.Id == scenarioId).Select(x => x.Value).ToList();

                var untilNowThisYearValues = DataContext.KpiAchievements.Where(x => x.Kpi.Id == kpiId && x.Periode.Year == currentYear
                    && x.Periode.Month < currentMonth && x.PeriodeType == PeriodeType.Monthly).Select(x => x.Value).ToList();
                var thisYearForecastValues = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId && x.Periode.Year == currentYear
                    && x.Periode.Month >= currentMonth && x.PeriodeType == PeriodeType.Monthly && x.Scenario.Id == scenarioId).Select(x => x.Value).ToList();
                var currentYearValue = untilNowThisYearValues.Concat(thisYearForecastValues).Sum();
                pastValues.Add(currentYearValue);

                var actualValue = pastValues.Concat(futureValues).Average();
                if (actualValue.HasValue) result.Actual = actualValue.ToString();
            }
            return result;
        }

        private OutputResult Min(KeyOutputConfiguration keyOutput, int scenarioId)
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var currentDate = DateTime.Now;
            var result = new OutputResult();
            var kpiIds = keyOutput.KpiIds.Split(',').Select(x => int.Parse(x)).ToList();
            var kpiId = keyOutput.Kpis.First(x => x.Id == kpiIds[0]).Id;
            var assumptionIds = keyOutput.KeyAssumptionIds.Split(',').Select(x => int.Parse(x)).ToList();
            var startId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[0]).Id;
            var endId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[1]).Id;
            var startAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == startId);
            var endAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == endId);
            if (startAssumption == null || endAssumption == null)
            {
                return new OutputResult();
            }
            DateTime startForecast;
            DateTime endForecast;
            if (IsStartAndEndValid(startAssumption.ForecastValue, endAssumption.ForecastValue, out startForecast, out endForecast))
            {
                var forecastValue = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId && x.Scenario.Id == scenarioId
                    && x.Periode >= startForecast && x.Periode <= endForecast && x.PeriodeType == PeriodeType.Monthly
                    && x.Value != keyOutput.ExcludeValue)
                    .Min(x => x.Value);
                if (forecastValue.HasValue) result.Forecast = forecastValue.Value.ToString();
            }
            DateTime startActual;
            DateTime endActual;
            if (IsStartAndEndValid(startAssumption.ActualValue, endAssumption.ActualValue, out startActual, out endActual))
            {
                var pastValues = DataContext.KpiAchievements.Where(x => x.Kpi.Id == kpiId
                   && x.Periode >= startActual && x.Periode < currentDate && x.PeriodeType == PeriodeType.Monthly
                   && x.Value != keyOutput.ExcludeValue).Select(x => x.Value).ToList();
                var futureValues = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId
                    && x.Periode >= currentDate && x.Periode <= endForecast && x.PeriodeType == PeriodeType.Monthly && x.Scenario.Id == scenarioId
                    && x.Value != keyOutput.ExcludeValue).Select(x => x.Value).ToList();
                var minActual = pastValues.Concat(futureValues).Min();
                if (minActual != null) result.Actual = minActual.ToString();
            }
            return result;
        }

        private OutputResult MinDate(KeyOutputConfiguration keyOutput, int scenarioId)
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var currentDate = DateTime.Now;
            var result = new OutputResult();
            var kpiIds = keyOutput.KpiIds.Split(',').Select(x => int.Parse(x)).ToList();
            var kpiId = keyOutput.Kpis.First(x => x.Id == kpiIds[0]).Id;
            var assumptionIds = keyOutput.KeyAssumptionIds.Split(',').Select(x => int.Parse(x)).ToList();
            var startId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[0]).Id;
            var endId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[1]).Id;
            var startAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == startId);
            var endAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == endId);
            if (startAssumption == null || endAssumption == null)
            {
                return new OutputResult();
            }
            DateTime startForecast;
            DateTime endForecast;
            if (IsStartAndEndValid(startAssumption.ForecastValue, endAssumption.ForecastValue, out startForecast, out endForecast))
            {
                var minForecast = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId && x.Scenario.Id == scenarioId
                    && x.Periode >= startForecast && x.Periode <= endForecast && x.PeriodeType == PeriodeType.Monthly
                    && x.Value != keyOutput.ExcludeValue)
                    .OrderBy(x => x.Value).FirstOrDefault();
                if (minForecast != null) result.Forecast = minForecast.Periode.AddMonths(1).AddDays(-1).ToString();
            }
            DateTime startActual;
            DateTime endActual;
            if (IsStartAndEndValid(startAssumption.ActualValue, endAssumption.ActualValue, out startActual, out endActual))
            {
                var pastValues = DataContext.KpiAchievements.Where(x => x.Kpi.Id == kpiId
                    && x.Periode >= startActual && x.Periode < currentDate && x.PeriodeType == PeriodeType.Monthly
                    && x.Value != keyOutput.ExcludeValue).Select(x => new
                    {
                        Periode = x.Periode,
                        Value = x.Value
                    });
                var futureValues = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId
                    && x.Periode >= currentDate && x.Periode <= endForecast && x.PeriodeType == PeriodeType.Monthly && x.Scenario.Id == scenarioId
                    && x.Value != keyOutput.ExcludeValue).Select(x => new
                    {
                        Periode = x.Periode,
                        Value = x.Value
                    });

                var minActual = pastValues.Concat(futureValues).OrderBy(x => x.Value).FirstOrDefault();
                if (minActual != null) result.Actual = minActual.Periode.AddMonths(1).AddDays(-1).ToString();
            }
            return result;
        }

        private OutputResult BreakEventYear(KeyOutputConfiguration keyOutput, int scenarioId)
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var currentDate = DateTime.Now;
            var result = new OutputResult();
            var kpiIds = keyOutput.KpiIds.Split(',').Select(x => int.Parse(x)).ToList();
            var kpiId = keyOutput.Kpis.First(x => x.Id == kpiIds[0]).Id;
            var assumptionIds = keyOutput.KeyAssumptionIds.Split(',').Select(x => int.Parse(x)).ToList();
            var startId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[0]).Id;
            var endId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[1]).Id;
            var startAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == startId);
            var endAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == endId);
            var payback = Payback(keyOutput, scenarioId, false);
            if (startAssumption == null || endAssumption == null)
            {
                return new OutputResult();
            }
            DateTime startForecast;
            DateTime endForecast;
            if (!string.IsNullOrEmpty(payback.Forecast) && IsStartAndEndValid(startAssumption.ForecastValue, endAssumption.ForecastValue, out startForecast, out endForecast))
            {
                var breakEventYear = startForecast.AddYears((int)Math.Floor(decimal.Parse(payback.Forecast)));
                var month = Math.Round((decimal.Parse(payback.Forecast) - Math.Floor(decimal.Parse(payback.Forecast))) * 12,0);
                breakEventYear = breakEventYear.AddMonths((int)month);
                result.Forecast = breakEventYear.ToString();
            }

            DateTime startActual;
            DateTime endActual;
            if (!string.IsNullOrEmpty(payback.Actual) && IsStartAndEndValid(startAssumption.ActualValue, endAssumption.ActualValue, out startActual, out endActual))
            {
                var breakEventYear = startActual.AddYears((int)Math.Floor(decimal.Parse(payback.Actual)));
                var month = Math.Round((decimal.Parse(payback.Actual) - Math.Floor(decimal.Parse(payback.Actual))) * 12, 0);
                breakEventYear = breakEventYear.AddMonths((int)month);
                result.Actual = breakEventYear.ToString();
            }
            return result;
        }

        private OutputResult Payback(KeyOutputConfiguration keyOutput, int scenarioId, bool fromCOD = true)
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var currentDate = DateTime.Now;
            var result = new OutputResult();
            var kpiIds = keyOutput.KpiIds.Split(',').Select(x => int.Parse(x)).ToList();
            var kpiId = keyOutput.Kpis.First(x => x.Id == kpiIds[0]).Id;
            var assumptionIds = keyOutput.KeyAssumptionIds.Split(',').Select(x => int.Parse(x)).ToList();
            var startId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[0]).Id;
            var endId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[1]).Id;
            var commercialId = 0;
            KeyAssumptionData commercialAssumption = null;
            if (fromCOD)
            {
                commercialId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[2]).Id;
                commercialAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == commercialId);
            }
            var startAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == startId);
            var endAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == endId);
            
          
            if (startAssumption == null || endAssumption == null)
            {
                return new OutputResult();
            }
            DateTime startForecast;
            DateTime endForecast;
            if (IsStartAndEndValid(startAssumption.ForecastValue, endAssumption.ForecastValue, out startForecast, out endForecast))
            {
                var forecastList = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId && x.Scenario.Id == scenarioId
                   && x.Periode.Year >= startForecast.Year && x.Periode.Year <= endForecast.Year
                   && x.PeriodeType == PeriodeType.Yearly).ToList();
                var accumulationForecastList = new List<KeyOperationData>();
                foreach (var fc in forecastList.OrderBy(x => x.Periode).ToList()) {
                    var accForecast = new KeyOperationData
                    {
                        Periode = fc.Periode,
                        PeriodeType = fc.PeriodeType,
                        Value = forecastList.Where(x => x.Periode <= fc.Periode).Sum(x => x.Value)
                    };
                    accumulationForecastList.Add(accForecast);
                }
                var forecast = accumulationForecastList.OrderBy(x => x.Periode).FirstOrDefault(x => x.Value > 0);
                double breakEventYearWeight = 1;
                if (forecast != null && forecast.Periode.Year != startForecast.Year)
                {
                    var prev = accumulationForecastList.FirstOrDefault(x => x.Periode.Year == (forecast.Periode.Year - 1));
                    if (prev != null && prev.Value - forecast.Value != 0)
                    {
                        breakEventYearWeight = double.Parse(string.Format("{0:0.0}", (prev.Value / (prev.Value - forecast.Value))));
                    }
                    if (prev != null)
                    {
                        result.Forecast = (forecast.Periode.Year - startForecast.Year + breakEventYearWeight).ToString();
                        DateTime constForecast;
                        DateTime commercialForecast;
                        if (fromCOD && IsStartAndEndValid(startAssumption.ForecastValue, commercialAssumption.ForecastValue, out constForecast, out commercialForecast))
                        {
                            result.Forecast = (double.Parse(result.Forecast) - ((commercialForecast - constForecast).Days / 365.00)).ToString();
                        }
                    }
                   
                }
            }
            DateTime startActual;
            DateTime endActual;
            if (IsStartAndEndValid(startAssumption.ActualValue, endAssumption.ActualValue, out startActual, out endActual))
            {
                var pastValues = DataContext.KpiAchievements.Where(x => x.Kpi.Id == kpiId
                   && x.Periode.Year >= startActual.Year && x.Periode.Year < currentYear
                   && x.PeriodeType == PeriodeType.Yearly).Select(x => new { Value = x.Value, Periode = x.Periode }).ToList();
                var futureValues = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId
                    && x.Periode.Year <= endActual.Year && x.Periode.Year > currentYear
                    && x.PeriodeType == PeriodeType.Yearly && x.Scenario.Id == scenarioId).Select(x => new { Value = x.Value, Periode = x.Periode }).ToList();
                var untilNowThisYearValue = DataContext.KpiAchievements.Where(x => x.Kpi.Id == kpiId
                    && x.Periode.Year == currentYear && x.Periode.Month < currentMonth
                    && x.PeriodeType == PeriodeType.Monthly).Sum(x => x.Value);
                var thisYearForecastValue = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId
                    && x.Periode.Year == currentYear && x.Periode.Month >= currentMonth
                    && x.PeriodeType == PeriodeType.Monthly && x.Scenario.Id == scenarioId).Sum(x => x.Value);
                var currentYearValue = new
                {
                    Value = new List<double?> { thisYearForecastValue, untilNowThisYearValue }.Sum(),
                    Periode = DateTime.Now
                };
                pastValues.Add(currentYearValue);
                var actualList = pastValues.Concat(futureValues);
                var accActualList = new List<KeyOperationData>();

                foreach (var ac in accActualList.OrderBy(x => x.Periode).ToList())
                {
                    var accForecast = new KeyOperationData
                    {
                        Periode = ac.Periode,
                        PeriodeType = ac.PeriodeType,
                        Value = accActualList.Where(x => x.Periode <= ac.Periode).Sum(x => x.Value)
                    };
                    accActualList.Add(accForecast);
                }

                var actual = accActualList.OrderBy(x => x.Periode).FirstOrDefault(x => x.Value > 0);

                double actualBreakEventYearWeight = 1;
                if (actual != null && actual.Periode.Year != startForecast.Year)
                {
                    var prev = accActualList.FirstOrDefault(x => x.Periode.Year == (actual.Periode.Year - 1));
                    if (prev != null && prev.Value - actual.Value != 0)
                    {
                        actualBreakEventYearWeight = double.Parse(string.Format("{0:0.0}", (prev.Value / (prev.Value - actual.Value))));
                    }
                    if (prev != null)
                    {
                        result.Actual = (actual.Periode.Year - startForecast.Year + actualBreakEventYearWeight).ToString();
                        DateTime constActual;
                        DateTime commercialActual;
                        if (fromCOD && IsStartAndEndValid(startAssumption.ActualValue, commercialAssumption.ActualValue, out constActual, out commercialActual))
                        {
                            result.Actual = (double.Parse(result.Actual) - ((commercialActual - constActual).Days / 365.00)).ToString();
                        }
                    }
                    
                }
            }

            return result;

        }

        private OutputResult ProfitInvestmentRatio(KeyOutputConfiguration keyOutput, int scenarioId)
        {
            var fcf = this.Sum(keyOutput, scenarioId);
            var projectCost = this.Sum(keyOutput, scenarioId, 1);
            var result = new OutputResult();
            if (!string.IsNullOrEmpty(fcf.Actual) && !string.IsNullOrEmpty(projectCost.Actual))
            {
                result.Actual = String.Format("{0:0.0}", double.Parse(fcf.Actual) /
                    double.Parse(projectCost.Actual));
            }
            if (!string.IsNullOrEmpty(fcf.Forecast) && !string.IsNullOrEmpty(projectCost.Forecast))
            {
                result.Forecast = String.Format("{0:0.0}", double.Parse(fcf.Forecast) /
                    double.Parse(projectCost.Forecast));
            }
            return result;
        }

        private OutputResult CompletionDate(KeyOutputConfiguration keyOutput, int scenarioId)
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var currentDate = DateTime.Now;
            var result = new OutputResult();
            var kpiId = keyOutput.Kpis[0].Id;
            var completionId = keyOutput.KeyAssumptions[0].Id;
            var completionAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == completionId);
            if (completionAssumption == null)
            {
                return new OutputResult();
            }

            DateTime completionForecast;
            DateTime completionActual;
            if (IsStartAndEndValid(completionAssumption.ForecastValue, completionAssumption.ActualValue, out completionForecast, out completionActual))
            {
                var forecast = DataContext.KeyOperationDatas.FirstOrDefault(x => x.Kpi.Id == kpiId && x.Scenario.Id == scenarioId
                    && x.PeriodeType == PeriodeType.Monthly
                    && x.Periode.Year == completionForecast.Year && x.Periode.Month == completionForecast.Month);
                if (forecast != null) result.Forecast = forecast.Value.ToString();

                if (currentYear == completionActual.Year)
                {
                    var pastMonthsThisYear = DataContext.KpiAchievements.Where(x => x.Kpi.Id == kpiId
                    && x.PeriodeType == PeriodeType.Monthly
                    && x.Periode.Year == completionForecast.Year && x.Periode.Month < currentMonth).Sum(x => x.Value);

                    var nextMonthThisYear = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId && scenarioId == x.Scenario.Id
                    && x.PeriodeType == PeriodeType.Monthly
                    && x.Periode.Year == completionForecast.Year && x.Periode.Month > currentMonth).Sum(x => x.Value);

                    if (pastMonthsThisYear.HasValue || nextMonthThisYear.HasValue)
                    {
                        result.Actual = (pastMonthsThisYear + nextMonthThisYear).ToString();
                    }
                }
                else
                {
                    var actual = DataContext.KpiAchievements.FirstOrDefault(x => x.Kpi.Id == kpiId
                    && x.PeriodeType == PeriodeType.Monthly
                    && x.Periode.Year == completionForecast.Year && x.Periode.Month == completionForecast.Month);
                    if (actual != null) result.Actual = actual.ToString();
                }
            }
            return result;
        }

        //by default it will use sum as aggregator
        private OutputResult HeaderSubstruction(KeyOutputConfiguration keyOutput, int scenarioId, bool useAverage = false)
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var currentDate = DateTime.Now;
            var kpiIds = keyOutput.KpiIds.Split(',').Select(x => int.Parse(x)).ToList();
            var headId = kpiIds[0];
            var result = new OutputResult();
            var assumptionIds = keyOutput.KeyAssumptionIds.Split(',').Select(x => int.Parse(x)).ToList();
            var startId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[0]).Id;
            var endId = keyOutput.KeyAssumptions.First(x => x.Id == assumptionIds[1]).Id;
            var startAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == startId);
            var endAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == endId);
            if (startAssumption == null || endAssumption == null)
            {
                return new OutputResult();
            }

            var headKeyOutpu = new KeyOutputConfiguration();
            headKeyOutpu.KeyAssumptions = keyOutput.KeyAssumptions;
            headKeyOutpu.KeyAssumptionIds = keyOutput.KeyAssumptionIds;
            headKeyOutpu.Kpis = new List<Kpi> { keyOutput.Kpis.First(x => x.Id == headId) };
            headKeyOutpu.KpiIds = headId.ToString();
            var headResult = new OutputResult();
            if (useAverage)
            {
                headResult = Average(headKeyOutpu, scenarioId);
            }
            else
            {
                headResult = Sum(headKeyOutpu, scenarioId);
            }
            var restResults = new List<OutputResult>();
            foreach (var kpiId in kpiIds)
            {
                if (kpiId != headId)
                {
                    var newKeyOutput = new KeyOutputConfiguration();
                    newKeyOutput.KeyAssumptions = keyOutput.KeyAssumptions;
                    newKeyOutput.KeyAssumptionIds = keyOutput.KeyAssumptionIds;
                    newKeyOutput.Kpis = new List<Kpi> { keyOutput.Kpis.First(x => x.Id == kpiId) };
                    newKeyOutput.KpiIds = kpiId.ToString();
                    if (useAverage)
                    {
                        restResults.Add(Average(newKeyOutput, scenarioId));
                    }
                    else
                    {
                        restResults.Add(Sum(newKeyOutput, scenarioId));
                    }
                }

            }
            if (!string.IsNullOrEmpty(headResult.Forecast))
            {
                result.Forecast = (double.Parse(headResult.Forecast) - restResults.Select(x => x.Forecast).Sum(x => string.IsNullOrEmpty(x) ? 0.00 : double.Parse(x))).ToString();
            }
            if (!string.IsNullOrEmpty(headResult.Actual))
            {
                result.Actual = (double.Parse(headResult.Actual) - restResults.Select(x => x.Actual).Sum(x => string.IsNullOrEmpty(x) ? 0.00 : double.Parse(x))).ToString();
            }
            return result;
        }

        private OutputResult Irr(KeyOutputConfiguration keyOutput, int scenarioId)
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var currentDate = DateTime.Now;
            var result = new OutputResult();
            var kpiId = keyOutput.Kpis[0].Id;
            var startId = keyOutput.KeyAssumptions[0].Id;
            var endId = keyOutput.KeyAssumptions[1].Id;

            var startAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == startId);
            var endAssumption = DataContext.KeyAssumptionDatas.FirstOrDefault(x => x.KeyAssumptionConfig.Id == endId);
            if (startAssumption == null || endAssumption == null)
            {
                return new OutputResult();
            }
            DateTime startForecast;
            DateTime endForecast;
            if (IsStartAndEndValid(startAssumption.ForecastValue, endAssumption.ForecastValue, out startForecast, out endForecast))
            {
                var forecast = DataContext.KeyOperationDatas.Where(x => x.Scenario.Id == scenarioId && x.Kpi.Id == kpiId
                        && startForecast <= x.Periode && x.Periode <= endForecast
                    && x.PeriodeType == PeriodeType.Yearly).OrderBy(x => x.Periode).Select(x => x.Value.Value).ToList().ToArray();
                if (forecast.Length >= 2)
                {
                    var forecastIrrCalculator = new NewtonRaphsonIRRCalculator(forecast);
                    result.Forecast = forecastIrrCalculator.Execute().ToString();
                }
            }
            DateTime startActual;
            DateTime endActual;
            if (IsStartAndEndValid(startAssumption.ActualValue, endAssumption.ActualValue, out startActual, out endActual))
            {
                var past = DataContext.KpiAchievements.Where(x => x.Kpi.Id == kpiId
                    && startActual <= x.Periode && x.Periode.Year < currentYear
                    && x.PeriodeType == PeriodeType.Yearly).OrderBy(x => x.Periode).Select(x => x.Value).ToList();
                var future = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId && x.Scenario.Id == scenarioId
                    && x.Periode <= endActual && x.Periode.Year > currentYear
                    && x.PeriodeType == PeriodeType.Yearly).OrderBy(x => x.Periode).Select(x => x.Value).ToList();
                var currentPastMonths = DataContext.KpiAchievements.Where(x => x.Kpi.Id == kpiId
                    && x.Periode.Year == currentYear && x.Periode.Month < currentMonth
                    && x.PeriodeType == PeriodeType.Monthly).OrderBy(x => x.Periode).Sum(x => x.Value);
                var currentNextMonths = DataContext.KeyOperationDatas.Where(x => x.Kpi.Id == kpiId && x.Scenario.Id == scenarioId
                    && x.Periode.Year == currentYear && x.Periode.Month >= currentMonth
                    && x.PeriodeType == PeriodeType.Monthly).OrderBy(x => x.Periode).Sum(x => x.Value);
                past.Add(currentPastMonths + currentNextMonths);
                var actualArray = past.Concat(future).ToArray();
                if (actualArray.Length >= 2)
                {
                    var actualIrrCalculator = new NewtonRaphsonIRRCalculator(actualArray.Select(x => x.HasValue ? x.Value : 0).ToArray());
                    result.Actual = actualIrrCalculator.Execute().ToString();
                }
            }
            return result;

        }


        public DeleteOutputConfigResponse DeleteOutput(DeleteOutputConfigRequest request)
        {
            var output = DataContext.KeyOutputConfigs.FirstOrDefault(x => x.Id == request.Id);
            if (output != null)
            {
                DataContext.KeyOutputConfigs.Attach(output);
                DataContext.KeyOutputConfigs.Remove(output);
                DataContext.SaveChanges();
            }
            return new DeleteOutputConfigResponse
            {
                IsSuccess = true,
                Message = "The Output Config has been deleted successfully"
            };
        }
    }


    public class OutputResult
    {
        public string Actual { get; set; }
        public string Forecast { get; set; }
    }
}
