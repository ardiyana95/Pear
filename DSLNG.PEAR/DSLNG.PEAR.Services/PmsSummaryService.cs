﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.PmsSummary;
using DSLNG.PEAR.Services.Responses.PmsSummary;
using System.Data.Entity;
using NCalc;
using PeriodeType = DSLNG.PEAR.Data.Enums.PeriodeType;

namespace DSLNG.PEAR.Services
{
    public class PmsSummaryService : BaseService, IPmsSummaryService
    {
        public PmsSummaryService(IDataContext dataContext)
            : base(dataContext)
        {
        }

        #region PMS summary report

        public GetPmsSummaryReportResponse GetPmsSummaryReport(GetPmsSummaryReportRequest request)
        {
            var response = new GetPmsSummaryReportResponse();
            try
            {
                //var xxx = DataContext.PmsSummaries.Include(x => x.PmsSummaryScoringIndicators.Select(a => a.)).ToList();
                var pmsSummary = DataContext.PmsSummaries
                                            .Include(x => x.ScoreIndicators)
                                            .Include("PmsConfigs.Pillar")
                                            .Include("PmsConfigs.ScoreIndicators")
                                            .Include("PmsConfigs.PmsConfigDetailsList.Kpi.Measurement")
                                            .Include("PmsConfigs.PmsConfigDetailsList.Kpi.KpiAchievements")
                                            .Include("PmsConfigs.PmsConfigDetailsList.Kpi.KpiTargets")
                                            .Include("PmsConfigs.PmsConfigDetailsList.Kpi.Pillar")
                                            .Include("PmsConfigs.PmsConfigDetailsList.ScoreIndicators")
                                            .FirstOrDefault(x => x.IsActive && x.Year == request.Year);
                if (pmsSummary != null)
                {
                    response.Title = pmsSummary.Title;
                    foreach (var pmsConfig in pmsSummary.PmsConfigs)
                    {
                        foreach (var pmsConfigDetails in pmsConfig.PmsConfigDetailsList)
                        {
                            var kpiData = new GetPmsSummaryReportResponse.KpiData();
                            kpiData.Id = pmsConfigDetails.Id;
                            kpiData.Pillar = pmsConfig.Pillar.Name;
                            kpiData.PillarId = pmsConfig.Pillar.Id;
                            kpiData.Kpi = pmsConfigDetails.Kpi.Name;
                            kpiData.Unit = pmsConfigDetails.Kpi.Measurement.Name;
                            kpiData.Weight = pmsConfigDetails.Weight;
                            kpiData.PillarOrder = pmsConfigDetails.Kpi.Pillar.Order;
                            kpiData.KpiOrder = pmsConfigDetails.Kpi.Order;
                            kpiData.PillarWeight = pmsConfig.Weight;
                            kpiData.ScoringType = pmsConfigDetails.ScoringType;
                            kpiData.YtdFormula = pmsConfigDetails.Kpi.YtdFormula;

                            #region KPI Achievement

                            var kpiAchievementYearly =
                                pmsConfigDetails.Kpi.KpiAchievements.FirstOrDefault(x => x.PeriodeType == PeriodeType.Yearly && x.Periode.Year == request.Year);
                            if (kpiAchievementYearly != null && kpiAchievementYearly.Value != null)
                                kpiData.ActualYearly = kpiAchievementYearly.Value.Value;


                            var kpiAchievementMonthly =
                                pmsConfigDetails.Kpi.KpiAchievements.FirstOrDefault(
                                    x => x.PeriodeType == PeriodeType.Monthly && x.Periode.Month == request.Month && x.Periode.Year == request.Year);
                            if (kpiAchievementMonthly != null && kpiAchievementMonthly.Value.HasValue)
                                kpiData.ActualMonthly = kpiAchievementMonthly.Value.Value;


                            var kpiAchievementYtd = pmsConfigDetails.Kpi.KpiAchievements.Where(
                                x =>
                                x.PeriodeType == PeriodeType.Monthly && x.Value.HasValue &&
                                (x.Periode.Month >= 1 && x.Periode.Month <= request.Month && x.Periode.Year == request.Year)).ToList();
                            if (kpiAchievementYtd.Count > 0) kpiData.ActualYtd = 0;
                            foreach (var achievementYtd in kpiAchievementYtd)
                            {
                                if (achievementYtd.Value.HasValue)
                                    kpiData.ActualYtd += achievementYtd.Value;
                            }

                            if (kpiData.YtdFormula == YtdFormula.Average)
                            {
                                if (kpiData.ActualYtd.HasValue)
                                {
                                    kpiData.ActualYtd = kpiData.ActualYtd / request.Month;
                                }
                            }

                            #endregion

                            #region KPI Target

                            var kpiTargetYearly =
                                pmsConfigDetails.Kpi.KpiTargets.FirstOrDefault(x => x.PeriodeType == PeriodeType.Yearly && x.Periode.Year == request.Year);
                            if (kpiTargetYearly != null && kpiTargetYearly.Value != null)
                                kpiData.TargetYearly = kpiTargetYearly.Value.Value;


                            var kpiTargetMonthly =
                                pmsConfigDetails.Kpi.KpiTargets.FirstOrDefault(
                                    x => x.PeriodeType == PeriodeType.Monthly && x.Periode.Month == request.Month && x.Periode.Year == request.Year);
                            if (kpiTargetMonthly != null && kpiTargetMonthly.Value.HasValue)
                                kpiData.TargetMonthly = kpiTargetMonthly.Value.Value;


                            var kpiTargetYtd = pmsConfigDetails.Kpi.KpiTargets.Where(
                                x =>
                                x.PeriodeType == PeriodeType.Monthly && x.Value.HasValue &&
                                (x.Periode.Month >= 1 && x.Periode.Month <= request.Month && x.Periode.Year == request.Year)).ToList();
                            if (kpiTargetYtd.Count > 0) kpiData.TargetYtd = 0;
                            foreach (var targetYtd in kpiTargetYtd)
                            {
                                if (targetYtd.Value.HasValue)
                                {
                                    kpiData.TargetYtd += targetYtd.Value;
                                }
                            }

                            if (kpiData.YtdFormula == YtdFormula.Average)
                            {
                                if (kpiData.TargetYtd.HasValue)
                                {
                                    kpiData.TargetYtd = kpiData.TargetYtd/request.Month;
                                }
                            }

                            #endregion

                            #region Score

                            if (kpiData.ActualYtd.HasValue && kpiData.TargetYtd.HasValue)
                            {
                                var indexYtd = (kpiData.ActualYtd.Value / kpiData.TargetYtd.Value);

                                switch (pmsConfigDetails.ScoringType)
                                {
                                    case ScoringType.Positive:
                                        kpiData.Score = pmsConfigDetails.Weight * indexYtd;
                                        break;
                                    case ScoringType.Negative:
                                        if (indexYtd == 0)
                                        {
                                            response.IsSuccess = false;
                                            response.Message =
                                                string.Format(
                                                    @"KPI {0} memiliki nilai index YTD 0 dengan Nilai Scoring Type negative yang mengakibatkan terjadinya nilai infinity",
                                                    pmsConfigDetails.Kpi.Name);
                                            return response;
                                        }
                                        kpiData.Score = pmsConfigDetails.Weight / indexYtd;
                                        break;
                                    case ScoringType.Boolean:
                                        bool isMoreThanZero = false;
                                        var kpiAchievement =
                                            pmsConfigDetails.Kpi.KpiAchievements.Where(x => x.Value.HasValue && x.Periode.Year == request.Year).ToList();
                                        bool isNull = kpiAchievement.Count == 0;
                                        foreach (var achievement in kpiAchievement)
                                        {
                                            if (achievement.Value > 0)
                                            {
                                                isMoreThanZero = true;
                                                break;
                                            }
                                                
                                        }

                                        if (!isNull)
                                        {
                                            kpiData.Score = isMoreThanZero ? 0 : Double.Parse(kpiData.Weight.ToString());
                                        }

                                        break;
                                }

                            }

                            #endregion

                            kpiData.KpiColor = GetScoreColor(kpiData.Score, pmsConfigDetails.ScoreIndicators);

                            response.KpiDatas.Add(kpiData);
                        }

                        response.KpiDatas = SetPmsConfigColor(response.KpiDatas, pmsConfig.ScoreIndicators,
                                                              pmsConfig.Pillar.Id);
                    }

                    response.KpiDatas = SetPmsSummaryColor(response.KpiDatas, pmsSummary.ScoreIndicators);
                }

                response.IsSuccess = true;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }
            catch (NullReferenceException nullReferenceException)
            {
                response.Message = nullReferenceException.Message;
            }

            return response;
        }

        public GetPmsDetailsResponse GetPmsDetails(GetPmsDetailsRequest request)
        {
            var response = new GetPmsDetailsResponse();
            try
            {
                var config = DataContext.PmsConfigDetails
                                        .Include(x => x.PmsConfig.ScoreIndicators)
                                        .Include(x => x.PmsConfig.PmsSummary)
                                        .Include(x => x.Kpi)
                                        .Include(x => x.Kpi.Group)
                                        .Include(x => x.Kpi.KpiAchievements)
                                        .Include(x => x.Kpi.Measurement)
                                        .Include(x => x.Kpi.RelationModels)
                                        .Include(x => x.Kpi.RelationModels.Select(y => y.Kpi))
                                        .Include(
                                            x => x.Kpi.RelationModels.Select(y => y.Kpi).Select(z => z.KpiAchievements))
                                        .Include(x => x.Kpi.RelationModels.Select(y => y.Kpi).Select(z => z.Measurement))
                                        .FirstOrDefault(x => x.Id == request.Id);

                if (config != null)
                {
                    response.Title = config.PmsConfig.PmsSummary.Title;
                    response.Year = config.PmsConfig.PmsSummary.Year;
                    response.KpiGroup = config.Kpi.Group != null ? config.Kpi.Group.Name : "";
                    response.KpiName = config.Kpi.Name;
                    response.KpiId = config.Kpi.Id;
                    response.MeasurementId = config.Kpi.Measurement != null ? config.Kpi.Measurement.Id : 0;
                    response.KpiUnit = config.Kpi.Measurement != null ? config.Kpi.Measurement.Name : "";
                    response.KpiPeriod = config.Kpi.Period.ToString();
                    
                    response.ScoreIndicators = config.ScoreIndicators.MapTo<Common.PmsSummary.ScoreIndicator>();
                    response.Weight = config.Weight;
                    response.ScoringType = config.ScoringType.ToString();
                    var kpiActualYearly =
                        config.Kpi.KpiAchievements.FirstOrDefault(x => x.PeriodeType == Data.Enums.PeriodeType.Yearly);
                    if (kpiActualYearly != null)
                    {
                        response.KpiActualYearly = kpiActualYearly.Value;
                        response.KpiPeriodYearly = kpiActualYearly.Periode.Year.ToString();
                        response.KpiTypeYearly = kpiActualYearly.PeriodeType.ToString();
                        response.KpiRemarkYearly = kpiActualYearly.Remark;
                    }
                    var kpiActualMonthly =
                        config.Kpi.KpiAchievements.Where(x => x.PeriodeType == Data.Enums.PeriodeType.Monthly && x.Periode.Year == request.Year)
                        .OrderBy(x => x.Periode.Month)
                        .ToList();
                    response.KpiAchievmentMonthly = new List<GetPmsDetailsResponse.KpiAchievment>();
                    if (kpiActualMonthly.Count > 0)
                    {
                        var kpiActualMonth = kpiActualMonthly.FirstOrDefault(x => x.Periode.Month == request.Month);
                        response.KpiActualMonthly = kpiActualMonth != null ? kpiActualMonth.Value : null;
                        response.KpiAchievmentMonthly =
                            kpiActualMonthly.MapTo<GetPmsDetailsResponse.KpiAchievment>();
                    }

                    response.KpiRelations = new List<GetPmsDetailsResponse.KpiRelation>();
                    var kpiRelationModel = config.Kpi.RelationModels;
                    if (kpiRelationModel != null)
                    {
                        foreach (var item in kpiRelationModel)
                        {
                            var actualYearly =
                                item.Kpi.KpiAchievements.FirstOrDefault(
                                    x => x.PeriodeType == Data.Enums.PeriodeType.Yearly);
                            var actualMonthly =
                                item.Kpi.KpiAchievements.Where(x => x.PeriodeType == Data.Enums.PeriodeType.Monthly)
                                    .OrderBy(x => x.Periode.Month)
                                    .ToList();
                            response.KpiRelations.Add(new GetPmsDetailsResponse.KpiRelation
                            {
                                Name = item.Kpi.Name,
                                Unit = item.Kpi.Measurement.Name,
                                Method = item.Method,
                                ActualYearly = actualYearly != null ? actualYearly.Value : null,
                                ActualMonthly = actualMonthly.Count > 0 ? actualMonthly.Sum(x => x.Value) : null
                            });
                        }
                    }
                }

                response.IsSuccess = true;
            }
            catch (ArgumentNullException argumentNullException)
            {
                response.Message = argumentNullException.Message;
            }


            return response;
        }

        #endregion

        #region PMS summary

        public CreatePmsSummaryResponse CreatePmsSummary(CreatePmsSummaryRequest request)
        {
            var response = new CreatePmsSummaryResponse();
            try
            {
                var pmsSummary = request.MapTo<PmsSummary>();
                DataContext.PmsSummaries.Add(pmsSummary);
                DataContext.SaveChanges();
                response.Message = "Configuration has been added successfully";
                response.IsSuccess = true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public GetPmsSummaryResponse GetPmsSummary(int id)
        {
            var response = new GetPmsSummaryResponse();
            try
            {
                var pmsSummary = DataContext.PmsSummaries
                                            .Include(x => x.ScoreIndicators)
                                            .First(x => x.Id == id);
                response = pmsSummary.MapTo<GetPmsSummaryResponse>();
                response.IsSuccess = true;
            }
            catch (ArgumentNullException argumentNullException)
            {
                response.Message = argumentNullException.Message;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }

            return response;
        }

        public GetPmsSummaryListResponse GetPmsSummaryList(GetPmsSummaryListRequest request)
        {
            var response = new GetPmsSummaryListResponse();
            try
            {
                var summaries = DataContext.PmsSummaries.ToList();
                response.PmsSummaryList = summaries.MapTo<GetPmsSummaryListResponse.PmsSummary>();
                response.IsSuccess = true;
            }
            catch (ArgumentNullException exception)
            {
                response.Message = exception.Message;
            }

            return response;
        }

        public UpdatePmsSummaryResponse UpdatePmsSummary(UpdatePmsSummaryRequest request)
        {
            var response = new UpdatePmsSummaryResponse();
            try
            {
                var updatedPmsSummary = request.MapTo<PmsSummary>();
                var existedPmsSummary = DataContext.PmsSummaries
                    .Where(x => x.Id == request.Id)
                    .Include(x => x.ScoreIndicators)
                    .Single();
                var existedPmsSummaryEntry = DataContext.Entry(existedPmsSummary);
                existedPmsSummaryEntry.CurrentValues.SetValues(updatedPmsSummary);

                foreach (var scoreIndicator in updatedPmsSummary.ScoreIndicators)
                {
                    var existedScoreIndicator = existedPmsSummary.ScoreIndicators.SingleOrDefault(x => x.Id == scoreIndicator.Id && x.Id != 0);
                    if (existedScoreIndicator != null)
                    {
                        var scoreIndicatorEntry = DataContext.Entry(existedScoreIndicator);
                        scoreIndicatorEntry.CurrentValues.SetValues(scoreIndicator);
                    }
                    else
                    {
                        scoreIndicator.Id = 0;
                        existedPmsSummary.ScoreIndicators.Add(scoreIndicator);
                    }
                }

                foreach (var item in existedPmsSummary.ScoreIndicators.Where(x => x.Id != 0).ToList())
                {
                    if (updatedPmsSummary.ScoreIndicators.All(x => x.Id != item.Id))
                    {
                        DataContext.ScoreIndicators.Remove(item);
                    }
                }

                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Pms Summary has been updated";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public GetPmsSummaryConfigurationResponse GetPmsSummaryConfiguration(GetPmsSummaryConfigurationRequest request)
        {
            var response = new GetPmsSummaryConfigurationResponse();
            try
            {
                var pmsSummary = DataContext.PmsSummaries
                    .Include(x => x.PmsConfigs.Select(y => y.PmsConfigDetailsList.Select(z => z.ScoreIndicators)))
                    .First(x => x.Id == request.Id);

                response.Pillars = DataContext.Pillars.ToList().MapTo<GetPmsSummaryConfigurationResponse.Pillar>();
                response.Kpis = DataContext.Kpis.Include(x => x.Measurement).ToList().MapTo<GetPmsSummaryConfigurationResponse.Kpi>();
                response.PmsConfigs = pmsSummary.PmsConfigs.MapTo<GetPmsSummaryConfigurationResponse.PmsConfig>();

                response.IsSuccess = true;
            }
            catch (ArgumentNullException argumentNullException)
            {
                response.Message = argumentNullException.Message;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }

            return response;
        }

        public GetScoreIndicatorsResponse GetScoreIndicators(GetScoreIndicatorRequest request)
        {
            var response = new GetScoreIndicatorsResponse();
            try
            {
                if (request.PmsConfigDetailId > 0)
                {
                    var pmsConfigDetails = DataContext.PmsConfigDetails
                                                      .Include(x => x.ScoreIndicators)
                                                      .Single(x => x.Id == request.PmsConfigDetailId);
                    response.ScoreIndicators =
                        pmsConfigDetails.ScoreIndicators.MapTo<Common.PmsSummary.ScoreIndicator>();
                }
                else
                {
                    var pmsSummary = DataContext.PmsSummaries
                                                      .Include(x => x.ScoreIndicators)
                                                      .Single(x => x.Id == request.PmsSummaryId);
                    response.ScoreIndicators =
                        pmsSummary.ScoreIndicators.MapTo<Common.PmsSummary.ScoreIndicator>();
                }
                
                response.IsSuccess = true;
            }
            catch (ArgumentNullException argumentNullException)
            {
                response.Message = argumentNullException.Message;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }

            return response;
        }

        #endregion

        #region PMS Config

        public CreatePmsConfigResponse CreatePmsConfig(CreatePmsConfigRequest request)
        {
            var response = new CreatePmsConfigResponse();

            try
            {
                var pmsConfig = request.MapTo<PmsConfig>();
                pmsConfig.Pillar = DataContext.Pillars.First(x => x.Id == request.PillarId);
                pmsConfig.PmsSummary = DataContext.PmsSummaries.First(x => x.Id == request.PmsSummaryId);
                pmsConfig.IsActive = true;
                DataContext.PmsConfigs.Add(pmsConfig);
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "New Pillar has been addeed succefully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }

            return response;
        }
        
        public UpdatePmsConfigResponse UpdatePmsConfig(UpdatePmsConfigRequest request)
        {
            var response = new UpdatePmsConfigResponse();
            try
            {
                var updatedPmsConfig = request.MapTo<PmsConfig>();
                var existedPmsConfig = DataContext.PmsConfigs
                    .Where(x => x.Id == request.Id)
                    .Include(x => x.PmsSummary)
                    .Include(x => x.ScoreIndicators)
                    .Single();
                var existedPmsConfigEntry = DataContext.Entry(existedPmsConfig);
                existedPmsConfigEntry.CurrentValues.SetValues(updatedPmsConfig);

                foreach (var scoreIndicator in updatedPmsConfig.ScoreIndicators)
                {
                    var existedScoreIndicator = existedPmsConfig.ScoreIndicators.SingleOrDefault(x => x.Id == scoreIndicator.Id && x.Id != 0);
                    if (existedScoreIndicator != null)
                    {
                        var scoreIndicatorEntry = DataContext.Entry(existedScoreIndicator);
                        scoreIndicatorEntry.CurrentValues.SetValues(scoreIndicator);
                    }
                    else
                    {
                        scoreIndicator.Id = 0;
                        existedPmsConfig.ScoreIndicators.Add(scoreIndicator);
                    }
                }

                foreach (var item in existedPmsConfig.ScoreIndicators.Where(x => x.Id != 0).ToList())
                {
                    if (updatedPmsConfig.ScoreIndicators.All(x => x.Id != item.Id))
                    {
                        DataContext.ScoreIndicators.Remove(item);
                    }
                }

                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.PmsSummaryId = existedPmsConfig.PmsSummary.Id;
                response.Message = "Pms Config has been updated";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }
        
        public GetPmsConfigResponse GetPmsConfig(int id)
        {
            var response = new GetPmsConfigResponse();
            try
            {
                var pmsConfig = DataContext.PmsConfigs
                    .Include(x => x.ScoreIndicators)
                    .Include(x => x.Pillar)
                    .Single(x => x.Id == id);

                response = pmsConfig.MapTo<GetPmsConfigResponse>();
                response.IsSuccess = true;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }
            catch (ArgumentNullException argumentNullException)
            {
                response.Message = argumentNullException.Message;
            }
            return response;
        }

        #endregion

        #region PMS Config Details

        public GetPmsConfigDetailsResponse GetPmsConfigDetails(int id)
        {
            var response = new GetPmsConfigDetailsResponse();
            try
            {
                var pmsConfigDetails =
                    DataContext.PmsConfigDetails
                    .Include(x => x.ScoreIndicators)
                    .Include(x => x.Kpi.Pillar)
                    .First(x => x.Id == id);
                response = pmsConfigDetails.MapTo<GetPmsConfigDetailsResponse>();
                response.IsSuccess = true;
            }
            catch (ArgumentNullException argumentNullException)
            {
                response.Message = argumentNullException.Message;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }

            return response;
        }
        
        public CreatePmsConfigDetailsResponse CreatePmsConfigDetails(CreatePmsConfigDetailsRequest request)
        {
            var response = new CreatePmsConfigDetailsResponse();
            try
            {
                var pmsConfigDetails = request.MapTo<PmsConfigDetails>();
                pmsConfigDetails.PmsConfig = DataContext.PmsConfigs
                    .Include(x => x.PmsSummary)
                    .Single(x => x.Id == request.PmsConfigId);
                pmsConfigDetails.Kpi = DataContext.Kpis.Single(x => x.Id == request.KpiId);
                pmsConfigDetails.IsActive = true;
                DataContext.PmsConfigDetails.Add(pmsConfigDetails);
                DataContext.SaveChanges();
                response.PmsSummaryId = pmsConfigDetails.PmsConfig.PmsSummary.Id;
                response.IsSuccess = true;
                response.Message = "KPI has been added successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }
            catch (ArgumentNullException argumentNullException)
            {
                response.Message = argumentNullException.Message;
            }

            return response;
        }
        
        public UpdatePmsConfigDetailsResponse UpdatePmsConfigDetails(UpdatePmsConfigDetailsRequest request)
        {
            var response = new UpdatePmsConfigDetailsResponse();
            try
            {
                var updatedPmsConfigDetails = request.MapTo<PmsConfigDetails>();
                var existedPmsConfigDetails = DataContext.PmsConfigDetails
                    .Where(x => x.Id == request.Id)
                    .Include(x => x.PmsConfig.PmsSummary)
                    .Include(x => x.ScoreIndicators)
                    .Single();
                var existedPmsConfigDetailsEntry = DataContext.Entry(existedPmsConfigDetails);
                existedPmsConfigDetailsEntry.CurrentValues.SetValues(updatedPmsConfigDetails);

                foreach (var scoreIndicator in updatedPmsConfigDetails.ScoreIndicators)
                {
                    var existedScoreIndicator = existedPmsConfigDetails.ScoreIndicators.SingleOrDefault(x => x.Id == scoreIndicator.Id && x.Id != 0);
                    if (existedScoreIndicator != null)
                    {
                        var scoreIndicatorEntry = DataContext.Entry(existedScoreIndicator);
                        scoreIndicatorEntry.CurrentValues.SetValues(scoreIndicator);
                    }
                    else
                    {
                        scoreIndicator.Id = 0;
                        existedPmsConfigDetails.ScoreIndicators.Add(scoreIndicator);
                    }
                }

                foreach (var item in existedPmsConfigDetails.ScoreIndicators.Where(x => x.Id != 0).ToList())
                {
                    if (updatedPmsConfigDetails.ScoreIndicators.All(x => x.Id != item.Id))
                    {
                        DataContext.ScoreIndicators.Remove(item);
                    }
                }

                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.PmsSummaryId = existedPmsConfigDetails.PmsConfig.PmsSummary.Id;
                response.Message = "KPI has been updated successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        #endregion  

        public GetKpisByPillarIdResponse GetKpis(int pillarId)
        {
            var response = new GetKpisByPillarIdResponse();
            try
            {
                var kpis = DataContext.Kpis.Include(x => x.Pillar).Where(x => x.Pillar.Id == pillarId).ToList();
                response.Kpis = kpis.MapTo<GetKpisByPillarIdResponse.Kpi>();
                response.IsSuccess = true;
                return response;
            }
            catch (ArgumentNullException argumentNullException)
            {
                response.Message = argumentNullException.Message;
            }

            return response;
        }

        private IList<GetPmsSummaryReportResponse.KpiData> SetPmsConfigColor(IList<GetPmsSummaryReportResponse.KpiData> kpiDatas, ICollection<ScoreIndicator> scoreIndicators, int pillarId)
        {
            var data = kpiDatas.Where(x => x.PillarId == pillarId && x.Score.HasValue).ToList();
            double? totalScore = null;
            if (data.Count > 0)
            {
                totalScore = 0;
                foreach (var datum in data)
                {
                    //totalScore += datum.Score/100 * datum.PillarWeight;
                    totalScore += datum.Score;
                }
            }

            var groupedPillar = kpiDatas.Where(x => x.PillarId == pillarId).ToList();
            foreach (var item in groupedPillar)
            {
                item.PillarColor = GetScoreColor(totalScore, scoreIndicators);
            }

            return kpiDatas;
        }

        private IList<GetPmsSummaryReportResponse.KpiData> SetPmsSummaryColor(IList<GetPmsSummaryReportResponse.KpiData> kpiDatas, ICollection<ScoreIndicator> scoreIndicators)
        {
            var groupedPillars = kpiDatas.GroupBy(x => x.Pillar);
            double? totalScore = null;
            foreach (var groupedPillar in groupedPillars)
            {
                var notNullPillar = groupedPillar.Where(x => x.Score.HasValue).ToList();
                if (notNullPillar.Count > 0)
                    totalScore = totalScore.HasValue ? totalScore.Value : 0;

                foreach (var item in notNullPillar)
                {
                    if (item.Score.HasValue)
                    {
                        totalScore += item.Score.Value / 100 * item.PillarWeight;
                    }
                }
            }

            return kpiDatas.Select(x =>
            {
                x.TotalScoreColor = GetScoreColor(totalScore, scoreIndicators);
                return x;
            }).ToList();
        }
        
        private string GetScoreColor(double? score, IEnumerable<ScoreIndicator> scoreIndicators)
        {
            if (score.HasValue)
            {
                foreach (var scoreIndicator in scoreIndicators)
                {
                    Expression e = new Expression(scoreIndicator.Expression.Replace("x", score.ToString()));
                    bool isPassed = (bool)e.Evaluate();
                    if (isPassed)
                    {
                        return scoreIndicator.Color;
                    }
                }
            }

            return "grey";
        }

        private IList<GetPmsSummaryReportResponse.KpiData> SetPillarAndTotalScoreColor(IList<GetPmsSummaryReportResponse.KpiData> kpiDatas, PmsSummaryScoringIndicator pillarScoringIndicators, PmsSummaryScoringIndicator totalScoreScoringIndicators)
        {
            IDictionary<string, double?[]> totalPillar = new Dictionary<string, double?[]>();
            var groupedPillars = kpiDatas.GroupBy(x => x.Pillar);
            foreach (var groupedPillar in groupedPillars)
            {
                double? totalScore = null;
                var notNullPillar = groupedPillar.Where(x => x.Score.HasValue).ToList();
                if (notNullPillar.Count > 0)
                    totalScore = 0;

                foreach (var item in notNullPillar)
                {
                    if (item.Score.HasValue)
                    {
                        totalScore += item.Score.Value;
                    }
                }

                totalPillar.Add(groupedPillar.Key, new double?[] { totalScore, groupedPillar.First().PillarWeight });
            }

            double? allTotalScore = null;
            if (totalPillar.Count > 0)
                allTotalScore = 0;

            foreach (var tp in totalPillar)
            {
                if (tp.Value[0].HasValue && tp.Value[1].HasValue)
                {
                    allTotalScore += tp.Value[0] / 100 * tp.Value[1];
                }
                var kpiWithPillars = kpiDatas.Where(x => x.Pillar == tp.Key).ToList();
                foreach (var kpiWithPillar in kpiWithPillars)
                {
                    kpiWithPillar.PillarColor = GetScoreColor(tp.Value[0], pillarScoringIndicators.ScoreIndicators);
                }
            }

            return kpiDatas.Select(x =>
            {
                x.TotalScoreColor = GetScoreColor(allTotalScore, totalScoreScoringIndicators.ScoreIndicators);
                return x;
            }).ToList();
        }

        public DeletePmsResponse DeletePmsConfig(int id)
        {
            var response = new DeletePmsResponse();
            try
            {
                var pmsConfig = new PmsConfig { Id = id };
                DataContext.PmsConfigs.Attach(pmsConfig);
                DataContext.Entry(pmsConfig).State = EntityState.Deleted;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Pms Config item has been deleted successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }


        public DeletePmsResponse DeletePmsSummary(int id)
        {
            var response = new DeletePmsResponse();
            try
            {
                var pmsSummary = new PmsSummary { Id = id };
                DataContext.PmsSummaries.Attach(pmsSummary);
                DataContext.Entry(pmsSummary).State = EntityState.Deleted;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Pms Summary item has been deleted successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public DeletePmsResponse DeletePmsConfigDetails(int id)
        {
            var response = new DeletePmsResponse();
            try
            {
                var pmsConfigDetails = new PmsConfigDetails { Id = id };
                DataContext.PmsConfigDetails.Attach(pmsConfigDetails);
                DataContext.Entry(pmsConfigDetails).State = EntityState.Deleted;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Pms Config Detail item has been deleted successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }
    }
}
