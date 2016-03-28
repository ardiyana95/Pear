using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using DSLNG.PEAR.Services.Common.PmsSummary;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Der;
using DSLNG.PEAR.Services.Requests.KpiAchievement;
using DSLNG.PEAR.Services.Requests.Measurement;
using DSLNG.PEAR.Services.Requests.PmsSummary;
using DSLNG.PEAR.Services.Requests.Select;
using DSLNG.PEAR.Services.Responses.KpiAchievement;
using DSLNG.PEAR.Services.Responses.Level;
using DSLNG.PEAR.Services.Responses.Measurement;
using DSLNG.PEAR.Services.Responses.PmsSummary;
using DSLNG.PEAR.Services.Responses.Kpi;
using DSLNG.PEAR.Services.Requests.Kpi;
using DSLNG.PEAR.Services.Responses.Select;
using DSLNG.PEAR.Web.ViewModels.Common;
using DSLNG.PEAR.Web.ViewModels.Common.PmsSummary;
using DSLNG.PEAR.Web.ViewModels.Der;
using DSLNG.PEAR.Web.ViewModels.DerLayout;
using DSLNG.PEAR.Web.ViewModels.DerLayout.LayoutType;
using DSLNG.PEAR.Web.ViewModels.Kpi;
using DSLNG.PEAR.Services.Responses.Menu;
using DSLNG.PEAR.Services.Requests.Menu;
using DSLNG.PEAR.Web.ViewModels.KpiAchievement;
using DSLNG.PEAR.Web.ViewModels.Level;
using DSLNG.PEAR.Web.ViewModels.Measurement;
using DSLNG.PEAR.Web.ViewModels.Menu;
using DSLNG.PEAR.Services.Requests.Level;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Services.Responses.User;
using DSLNG.PEAR.Web.ViewModels.OperationConfig;
using DSLNG.PEAR.Web.ViewModels.OperationData;
using DSLNG.PEAR.Web.ViewModels.PmsConfig;
using DSLNG.PEAR.Web.ViewModels.PmsConfigDetails;
using DSLNG.PEAR.Web.ViewModels.PmsSummary;
using DSLNG.PEAR.Web.ViewModels.Select;
using DSLNG.PEAR.Web.ViewModels.User;
using DSLNG.PEAR.Web.ViewModels.RoleGroup;
using DSLNG.PEAR.Services.Responses.RoleGroup;
using DSLNG.PEAR.Services.Requests.RoleGroup;
using DSLNG.PEAR.Web.ViewModels.Type;
using DSLNG.PEAR.Services.Responses.Type;
using DSLNG.PEAR.Services.Requests.Type;
using DSLNG.PEAR.Services.Responses.Pillar;
using DSLNG.PEAR.Services.Requests.Pillar;
using DSLNG.PEAR.Web.ViewModels.Pillar;
using DSLNG.PEAR.Web.ViewModels.Artifact;
using DSLNG.PEAR.Services.Requests.Artifact;
using System;
using EPeriodeType = DSLNG.PEAR.Data.Enums.PeriodeType;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Responses.Artifact;
using System.Linq;
using DSLNG.PEAR.Services.Responses.Group;
using DSLNG.PEAR.Services.Requests.Group;
using DSLNG.PEAR.Web.ViewModels.Group;
using DSLNG.PEAR.Services.Responses.Method;
using DSLNG.PEAR.Web.ViewModels.Method;
using DSLNG.PEAR.Services.Requests.Method;
using DSLNG.PEAR.Services.Requests.Periode;
using DSLNG.PEAR.Web.ViewModels.Periode;
using DSLNG.PEAR.Services.Responses.Periode;
using DSLNG.PEAR.Web.ViewModels.KpiTarget;
using DSLNG.PEAR.Services.Requests.KpiTarget;
using DSLNG.PEAR.Services.Requests.Conversion;
using DSLNG.PEAR.Services.Responses.Conversion;
using DSLNG.PEAR.Web.ViewModels.Conversion;
using DSLNG.PEAR.Services.Responses.KpiTarget;
using DSLNG.PEAR.Web.ViewModels.Template;
using DSLNG.PEAR.Services.Requests.Template;
using Kpi = DSLNG.PEAR.Web.ViewModels.KpiTarget.Kpi;
using DSLNG.PEAR.Services.Responses.Template;
using DSLNG.PEAR.Services.Responses.Config;
using DSLNG.PEAR.Web.ViewModels.Config;
using DSLNG.PEAR.Web.ViewModels.Highlight;
using DSLNG.PEAR.Services.Requests.Highlight;
using DSLNG.PEAR.Services.Responses.Highlight;
using DSLNG.PEAR.Web.ViewModels.Vessel;
using DSLNG.PEAR.Services.Requests.Vessel;
using DSLNG.PEAR.Services.Responses.Vessel;
using DSLNG.PEAR.Web.ViewModels.Buyer;
using DSLNG.PEAR.Services.Requests.Buyer;
using DSLNG.PEAR.Services.Responses.Buyer;
using DSLNG.PEAR.Web.ViewModels.VesselSchedule;
using DSLNG.PEAR.Services.Requests.VesselSchedule;
using DSLNG.PEAR.Services.Responses.VesselSchedule;
using DSLNG.PEAR.Web.ViewModels.NLS;
using DSLNG.PEAR.Services.Requests.NLS;
using DSLNG.PEAR.Services.Responses.NLS;
using DSLNG.PEAR.Web.ViewModels.CalculatorConstant;
using DSLNG.PEAR.Services.Requests.CalculatorConstant;
using DSLNG.PEAR.Services.Responses.CalculatorConstant;
using DSLNG.PEAR.Web.ViewModels.ConstantUsage;
using DSLNG.PEAR.Services.Requests.ConstantUsage;
using DSLNG.PEAR.Services.Responses.ConstantUsage;
using DSLNG.PEAR.Web.ViewModels.Calculator;
using DSLNG.PEAR.Web.ViewModels.Weather;
using DSLNG.PEAR.Services.Requests.Weather;
using DSLNG.PEAR.Services.Responses.Weather;
using DSLNG.PEAR.Services.Responses.HighlightOrder;
using DSLNG.PEAR.Web.ViewModels.HighlightOrder;
using DSLNG.PEAR.Services.Requests.HighlightOrder;
using DSLNG.PEAR.Web.ViewModels.AssumptionCategory;
using DSLNG.PEAR.Services.Requests.AssumptionCategory;
using DSLNG.PEAR.Services.Responses.AssumptionCategory;
using DSLNG.PEAR.Web.ViewModels.OutputCategory;
using DSLNG.PEAR.Services.Requests.OutputCategory;
using DSLNG.PEAR.Services.Responses.OutputCategory;
using DSLNG.PEAR.Web.ViewModels.OperationGroup;
using DSLNG.PEAR.Services.Requests.OperationGroup;
using DSLNG.PEAR.Services.Responses.OperationGroup;
using DSLNG.PEAR.Web.ViewModels.AssumptionConfig;
using DSLNG.PEAR.Services.Requests.AssumptionConfig;
using DSLNG.PEAR.Services.Responses.AssumptionConfig;
using DSLNG.PEAR.Services.Requests.Scenario;
using DSLNG.PEAR.Web.ViewModels.Scenario;
using DSLNG.PEAR.Services.Responses.Scenario;
using DSLNG.PEAR.Web.ViewModels.AssumptionData;
using DSLNG.PEAR.Services.Requests.AssumptionData;
using DSLNG.PEAR.Services.Responses.AssumptionData;
using DSLNG.PEAR.Web.ViewModels.Operation;
using DSLNG.PEAR.Services.Requests.Operation;
using DSLNG.PEAR.Services.Responses.Operation;
using DSLNG.PEAR.Web.ViewModels.OperationalData;
using DSLNG.PEAR.Services.Requests.OperationalData;
using DSLNG.PEAR.Services.Responses.OperationalData;
using DSLNG.PEAR.Web.ViewModels.EconomicSummary;
using DSLNG.PEAR.Services.Requests.EconomicSummary;
using DSLNG.PEAR.Services.Responses.EconomicSummary;
using DSLNG.PEAR.Web.ViewModels.EconomicConfigDetail;
using DSLNG.PEAR.Services.Requests.EconomicConfig;
using DSLNG.PEAR.Services.Responses.EconomicConfig;
using DSLNG.PEAR.Web.ViewModels.HighlightGroup;
using DSLNG.PEAR.Services.Requests.HighlightGroup;
using DSLNG.PEAR.Services.Responses.HighlightGroup;
using DSLNG.PEAR.Web.ViewModels.OutputConfig;
using DSLNG.PEAR.Services.Requests.OutputConfig;
using DSLNG.PEAR.Services.Responses.OutputConfig;
using DSLNG.PEAR.Web.ViewModels.PlanningBlueprint;
using DSLNG.PEAR.Services.Requests.PlanningBlueprint;
using DSLNG.PEAR.Services.Responses.BusinessPosture;
using DSLNG.PEAR.Web.ViewModels.BusinessPosture;
using DSLNG.PEAR.Services.Requests.BusinessPosture;
using DSLNG.PEAR.Services.Responses.EnvironmentScanning;
using DSLNG.PEAR.Web.ViewModels.EnvironmentScanning;
using DSLNG.PEAR.Services.Requests.EnvironmentScanning;
using DSLNG.PEAR.Services.Responses.PlanningBlueprint;
using DSLNG.PEAR.Services.Responses.MidtermFormulation;
using DSLNG.PEAR.Services.Requests.MidtermFormulation;
using DSLNG.PEAR.Web.ViewModels.MidtermFormulation;
using System.Globalization;
using DSLNG.PEAR.Web.ViewModels.MidtermStrategyPlanning;
using DSLNG.PEAR.Services.Requests.MidtermPlanning;
using DSLNG.PEAR.Web.ViewModels.PopDashboard;
using DSLNG.PEAR.Services.Requests.PopDashboard;
using DSLNG.PEAR.Services.Responses.PopDashboard;
using DSLNG.PEAR.Services.Responses.PopInformation;
using DSLNG.PEAR.Services.Requests.PopInformation;
using DSLNG.PEAR.Services.Requests.Signature;
using DSLNG.PEAR.Services.Responses.Der;
using DSLNG.PEAR.Services.Responses.FileManager;
using DSLNG.PEAR.Web.ViewModels;

namespace DSLNG.PEAR.Web.AutoMapper
{
    public class ViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            ConfigureCorporatePortofolio();
            ConfigurePmsSummary();
            ConfigureKpiTarget();
            ConfigureKpiAchievement();
            ConfigureTrafficLight();
            ConfigureSelect();
            ConfigureOperationData();
            ConfigureEconomicSummary();
            ConfigureDerViewModel();
            ConfigureProcessBlueprint();

            Mapper.CreateMap<Dropdown, SelectListItem>();
            Mapper.CreateMap<SearchKpiViewModel, GetKpiToSeriesRequest>();
            Mapper.CreateMap<GetKpiToSeriesResponse, KpiToSeriesViewModel>();
            Mapper.CreateMap<CreateKpiViewModel, CreateKpiRequest>();
            Mapper.CreateMap<DSLNG.PEAR.Web.ViewModels.Kpi.KpiRelationModel, DSLNG.PEAR.Services.Requests.Kpi.KpiRelationModel>();
            Mapper.CreateMap<GetKpiResponse, UpdateKpiViewModel>()
               .ForMember(k => k.LevelId, o => o.MapFrom(x => x.Level.Id))
               .ForMember(k => k.GroupId, o => o.MapFrom(x => x.Group.Id))
               .ForMember(k => k.RoleGroupId, o => o.MapFrom(x => x.RoleGroup.Id))
               .ForMember(k => k.MeasurementId, o => o.MapFrom(x => x.Measurement.Id))
               .ForMember(k => k.MethodId, o => o.MapFrom(x => x.Method.Id))
               .ForMember(k => k.TypeId, o => o.MapFrom(x => x.Type.Id))
               .ForMember(k => k.YtdFormulaValue, o => o.MapFrom(x => x.YtdFormula.ToString()))
               .ForMember(k => k.PeriodeValue, o => o.MapFrom(x => x.Periode.ToString()))
               .ForMember(k => k.RelationModels, o => o.MapFrom(x => x.RelationModels));
            Mapper.CreateMap<DSLNG.PEAR.Services.Responses.Kpi.KpiRelationModel, DSLNG.PEAR.Web.ViewModels.Kpi.KpiRelationModel>();
            Mapper.CreateMap<UpdateKpiViewModel, UpdateKpiRequest>();

            //Mapper.CreateMap<GetMenusResponse.Menu, MenuViewModel>();
            Mapper.CreateMap<CreateMenuViewModel, CreateMenuRequest>();
            Mapper.CreateMap<GetMenuResponse, UpdateMenuViewModel>()
                .ForMember(x => x.RoleGroupIds, o => o.MapFrom(k => k.RoleGroups.Select(x => x.Id).ToList()));
            Mapper.CreateMap<UpdateMenuViewModel, UpdateMenuRequest>();

            Mapper.CreateMap<CreateMeasurementViewModel, CreateMeasurementRequest>();
            Mapper.CreateMap<GetMeasurementResponse, UpdateMeasurementViewModel>();
            Mapper.CreateMap<UpdateMeasurementViewModel, UpdateMeasurementRequest>();
            Mapper.CreateMap<GetMeasurementsResponse.Measurement, MeasurementViewModel>();
            Mapper.CreateMap<GetMeasurementResponse, DSLNG.PEAR.Web.ViewModels.Kpi.Measurement>();

            Mapper.CreateMap<CreateLevelViewModel, CreateLevelRequest>();
            Mapper.CreateMap<GetLevelResponse, UpdateLevelViewModel>();
            Mapper.CreateMap<UpdateLevelViewModel, UpdateLevelRequest>();

            Mapper.CreateMap<CreateUserViewModel, CreateUserRequest>();
            Mapper.CreateMap<GetUserResponse, UpdateUserViewModel>();
            Mapper.CreateMap<UpdateUserViewModel, UpdateUserRequest>();
            Mapper.CreateMap<GetUsersResponse.User, UserViewModel>()
                .ForMember(x => x.RoleName, y => y.MapFrom(z => z.Role.Name));
            Mapper.CreateMap<UserLoginViewModel, LoginUserRequest>();
            Mapper.CreateMap<ChangePasswordViewModel, ChangePasswordRequest>();
            Mapper.CreateMap<ResetPasswordResponseViewModel, ResetPasswordResponse>();
            Mapper.CreateMap<ResetPasswordViewModel, ResetPasswordRequest>();
            Mapper.CreateMap<ResetPasswordResponse, ResetPasswordResponseViewModel>();

            Mapper.CreateMap<GetRoleGroupsResponse.RoleGroup, RoleGroupViewModel>();
            Mapper.CreateMap<CreateRoleGroupViewModel, CreateRoleGroupRequest>();
            Mapper.CreateMap<GetRoleGroupResponse, UpdateRoleGroupViewModel>()
                .ForMember(o => o.LevelId, p => p.MapFrom(k => k.Level.Id));
            Mapper.CreateMap<UpdateRoleGroupViewModel, UpdateRoleGroupRequest>();
            Mapper.CreateMap<GetRoleGroupResponse, DSLNG.PEAR.Web.ViewModels.Kpi.RoleGroup>();

            Mapper.CreateMap<GetTypeResponse, UpdateTypeViewModel>();
            Mapper.CreateMap<GetTypesResponse.Type, TypeViewModel>();
            Mapper.CreateMap<CreateTypeViewModel, CreateTypeRequest>();
            Mapper.CreateMap<UpdateTypeViewModel, UpdateTypeRequest>();
            Mapper.CreateMap<GetTypeResponse, DSLNG.PEAR.Web.ViewModels.Kpi.Type>();

            Mapper.CreateMap<CreatePillarViewModel, CreatePillarRequest>();
            Mapper.CreateMap<GetPillarResponse, UpdatePillarViewModel>();
            Mapper.CreateMap<UpdatePillarViewModel, UpdatePillarRequest>();
            Mapper.CreateMap<GetPillarsResponse, DSLNG.PEAR.Web.ViewModels.Kpi.Pillar>();

            Mapper.CreateMap<CreateMethodViewModel, CreateMethodRequest>();
            Mapper.CreateMap<GetMethodRequest, UpdateMethodViewModel>();
            Mapper.CreateMap<UpdateMethodRequest, UpdateMethodViewModel>();
            Mapper.CreateMap<GetMethodResponse, UpdateMethodViewModel>();
            Mapper.CreateMap<UpdateMethodViewModel, UpdateMethodRequest>();

            Mapper.CreateMap<GetArtifactResponse, ArtifactDesignerViewModel>();
            Mapper.CreateMap<GetArtifactResponse, BarChartViewModel>();
            Mapper.CreateMap<GetArtifactResponse.SeriesResponse, BarChartViewModel.SeriesViewModel>();
            Mapper.CreateMap<GetArtifactResponse.StackResponse, BarChartViewModel.StackViewModel>();
            Mapper.CreateMap<GetArtifactResponse, LineChartViewModel>();
            Mapper.CreateMap<GetArtifactResponse.SeriesResponse, LineChartViewModel.SeriesViewModel>();
            Mapper.CreateMap<GetArtifactResponse, AreaChartViewModel>();
            Mapper.CreateMap<GetArtifactResponse.SeriesResponse, AreaChartViewModel.SeriesViewModel>();
            Mapper.CreateMap<GetArtifactResponse, SpeedometerChartViewModel>()
                .ForMember(x => x.Series, o => o.MapFrom(s => s.Series.FirstOrDefault()));
            Mapper.CreateMap<GetArtifactResponse.SeriesResponse, SpeedometerChartViewModel.SeriesViewModel>();
            Mapper.CreateMap<GetArtifactResponse.PlotResponse, SpeedometerChartViewModel.PlotBand>();
            Mapper.CreateMap<GetArtifactResponse, MultiaxisChartViewModel>()
                .ForMember(x => x.Charts, o => o.Ignore());
            Mapper.CreateMap<GetArtifactResponse.ChartResponse, MultiaxisChartViewModel.ChartViewModel>();
            Mapper.CreateMap<GetArtifactResponse.ChartResponse, LineChartViewModel>();
            Mapper.CreateMap<GetArtifactResponse.ChartResponse, BarChartViewModel>();
            Mapper.CreateMap<GetArtifactResponse.ChartResponse, AreaChartViewModel>();
            Mapper.CreateMap<GetArtifactResponse, ComboChartViewModel>()
             .ForMember(x => x.Charts, o => o.Ignore());
            Mapper.CreateMap<GetArtifactResponse.ChartResponse, ComboChartViewModel.ChartViewModel>();

            //cartesian preview
            Mapper.CreateMap<ArtifactDesignerViewModel, GetCartesianChartDataRequest>()
                .ForMember(x => x.PeriodeType, o => o.MapFrom(s => Enum.Parse(typeof(EPeriodeType), s.PeriodeType)))
                .ForMember(x => x.RangeFilter, o => o.MapFrom(s => Enum.Parse(typeof(RangeFilter), s.RangeFilter)))
                .ForMember(x => x.ValueAxis, o => o.MapFrom(s => Enum.Parse(typeof(ValueAxis), s.ValueAxis)))
                .ForMember(x => x.Start, y => y.MapFrom(z => z.StartAfterParsed))
                .ForMember(x => x.End, y => y.MapFrom(z => z.EndAfterParsed));

            //cartesian preview
            Mapper.CreateMap<ArtifactDesignerViewModel, GetMultiaxisChartDataRequest>()
                .ForMember(x => x.PeriodeType, o => o.MapFrom(s => Enum.Parse(typeof(EPeriodeType), s.PeriodeType)))
                .ForMember(x => x.RangeFilter, o => o.MapFrom(s => Enum.Parse(typeof(RangeFilter), s.RangeFilter)))
                //.ForMember(x => x.ValueAxis, o => o.MapFrom(s => Enum.Parse(typeof(ValueAxis), s.ValueAxis)))
                .ForMember(x => x.Start, y => y.MapFrom(z => z.StartAfterParsed))
                .ForMember(x => x.End, y => y.MapFrom(z => z.EndAfterParsed));

            //cartesian preview
            Mapper.CreateMap<ArtifactDesignerViewModel, GetComboChartDataRequest>()
                .ForMember(x => x.PeriodeType, o => o.MapFrom(s => Enum.Parse(typeof(EPeriodeType), s.PeriodeType)))
                .ForMember(x => x.RangeFilter, o => o.MapFrom(s => Enum.Parse(typeof(RangeFilter), s.RangeFilter)))
                //.ForMember(x => x.ValueAxis, o => o.MapFrom(s => Enum.Parse(typeof(ValueAxis), s.ValueAxis)))
                .ForMember(x => x.Start, y => y.MapFrom(z => z.StartAfterParsed))
                .ForMember(x => x.End, y => y.MapFrom(z => z.EndAfterParsed));

            //bar chart mapping
            Mapper.CreateMap<BarChartViewModel, GetCartesianChartDataRequest>();
            Mapper.CreateMap<BarChartViewModel.SeriesViewModel, GetCartesianChartDataRequest.SeriesRequest>();
            Mapper.CreateMap<BarChartViewModel.StackViewModel, GetCartesianChartDataRequest.StackRequest>();
            Mapper.CreateMap<GetCartesianChartDataResponse.SeriesResponse, BarChartDataViewModel.SeriesViewModel>();
            Mapper.CreateMap<BarChartViewModel, CreateArtifactRequest>()
              .ForMember(x => x.Series, o => o.MapFrom(s => s.Series.MapTo<CreateArtifactRequest.SeriesRequest>()));

            Mapper.CreateMap<BarChartViewModel.SeriesViewModel, CreateArtifactRequest.SeriesRequest>()
               .ForMember(x => x.Stacks, o => o.MapFrom(s => s.Stacks.MapTo<CreateArtifactRequest.StackRequest>()));
            Mapper.CreateMap<BarChartViewModel.StackViewModel, CreateArtifactRequest.StackRequest>();
            Mapper.CreateMap<BarChartViewModel, UpdateArtifactRequest>()
          .ForMember(x => x.Series, o => o.MapFrom(s => s.Series.MapTo<UpdateArtifactRequest.SeriesRequest>()));
            Mapper.CreateMap<BarChartViewModel.SeriesViewModel, UpdateArtifactRequest.SeriesRequest>()
               .ForMember(x => x.Stacks, o => o.MapFrom(s => s.Stacks.MapTo<UpdateArtifactRequest.StackRequest>()));
            Mapper.CreateMap<BarChartViewModel.StackViewModel, UpdateArtifactRequest.StackRequest>();


            //line chart mapping
            Mapper.CreateMap<LineChartViewModel, GetCartesianChartDataRequest>();
            Mapper.CreateMap<LineChartViewModel.SeriesViewModel, GetCartesianChartDataRequest.SeriesRequest>();
            Mapper.CreateMap<GetCartesianChartDataResponse.SeriesResponse, LineChartDataViewModel.SeriesViewModel>();
            Mapper.CreateMap<LineChartViewModel, CreateArtifactRequest>()
               .ForMember(x => x.Series, o => o.MapFrom(s => s.Series.MapTo<CreateArtifactRequest.SeriesRequest>()));
            Mapper.CreateMap<LineChartViewModel.SeriesViewModel, CreateArtifactRequest.SeriesRequest>();
            Mapper.CreateMap<LineChartViewModel, UpdateArtifactRequest>()
              .ForMember(x => x.Series, o => o.MapFrom(s => s.Series.MapTo<UpdateArtifactRequest.SeriesRequest>()));
            Mapper.CreateMap<LineChartViewModel.SeriesViewModel, UpdateArtifactRequest.SeriesRequest>();

            //area chart mapping
            Mapper.CreateMap<AreaChartViewModel, GetCartesianChartDataRequest>();
            Mapper.CreateMap<AreaChartViewModel.SeriesViewModel, GetCartesianChartDataRequest.SeriesRequest>();
            Mapper.CreateMap<AreaChartViewModel.StackViewModel, GetCartesianChartDataRequest.StackRequest>();
            Mapper.CreateMap<GetCartesianChartDataResponse.SeriesResponse, AreaChartDataViewModel.SeriesViewModel>();
            Mapper.CreateMap<AreaChartViewModel, CreateArtifactRequest>();
            Mapper.CreateMap<AreaChartViewModel.SeriesViewModel, CreateArtifactRequest.SeriesRequest>();
            Mapper.CreateMap<AreaChartViewModel.StackViewModel, CreateArtifactRequest.StackRequest>();
            Mapper.CreateMap<AreaChartViewModel, UpdateArtifactRequest>();
            Mapper.CreateMap<AreaChartViewModel.SeriesViewModel, UpdateArtifactRequest.SeriesRequest>();
            Mapper.CreateMap<AreaChartViewModel.StackViewModel, UpdateArtifactRequest.StackRequest>();
            Mapper.CreateMap<GetArtifactResponse.StackResponse, AreaChartViewModel.StackViewModel>();

            //speedometer chart mapping
            Mapper.CreateMap<ArtifactDesignerViewModel, GetSpeedometerChartDataRequest>()
                .ForMember(x => x.Start, y => y.MapFrom(z => z.StartAfterParsed))
                .ForMember(x => x.End, y => y.MapFrom(z => z.EndAfterParsed));
            Mapper.CreateMap<SpeedometerChartViewModel, GetSpeedometerChartDataRequest>();
            Mapper.CreateMap<SpeedometerChartViewModel.SeriesViewModel, GetSpeedometerChartDataRequest.SeriesRequest>();
            Mapper.CreateMap<SpeedometerChartViewModel.PlotBand, GetSpeedometerChartDataRequest.PlotBandRequest>();
            Mapper.CreateMap<GetSpeedometerChartDataResponse.SeriesResponse, SpeedometerChartDataViewModel.SeriesViewModel>()
                .ForMember(x => x.data, o => o.MapFrom(s => new List<double> { s.data }));
            Mapper.CreateMap<GetSpeedometerChartDataResponse.PlotBandResponse, SpeedometerChartDataViewModel.PlotBandViewModel>();
            Mapper.CreateMap<SpeedometerChartViewModel, CreateArtifactRequest>()
            .ForMember(x => x.Series, o => o.MapFrom(s => new List<CreateArtifactRequest.SeriesRequest> { s.Series.MapTo<CreateArtifactRequest.SeriesRequest>() }))
            .ForMember(x => x.Plots, o => o.MapFrom(s => s.PlotBands.MapTo<CreateArtifactRequest.PlotRequest>()));
            Mapper.CreateMap<SpeedometerChartViewModel.SeriesViewModel, CreateArtifactRequest.SeriesRequest>();
            Mapper.CreateMap<SpeedometerChartViewModel.PlotBand, CreateArtifactRequest.PlotRequest>();
            Mapper.CreateMap<SpeedometerChartViewModel, UpdateArtifactRequest>()
            .ForMember(x => x.Series, o => o.MapFrom(s => new List<UpdateArtifactRequest.SeriesRequest> { s.Series.MapTo<UpdateArtifactRequest.SeriesRequest>() }))
            .ForMember(x => x.Plots, o => o.MapFrom(s => s.PlotBands.MapTo<UpdateArtifactRequest.PlotRequest>()));
            Mapper.CreateMap<SpeedometerChartViewModel.SeriesViewModel, UpdateArtifactRequest.SeriesRequest>();
            Mapper.CreateMap<SpeedometerChartViewModel.PlotBand, UpdateArtifactRequest.PlotRequest>();

            //tabular mapping
            Mapper.CreateMap<TabularViewModel, CreateArtifactRequest>();
            Mapper.CreateMap<TabularViewModel.RowViewModel, CreateArtifactRequest.RowRequest>()
                .ForMember(x => x.PeriodeType, o => o.MapFrom(s => Enum.Parse(typeof(EPeriodeType), s.PeriodeType)))
                .ForMember(x => x.RangeFilter, o => o.MapFrom(s => Enum.Parse(typeof(RangeFilter), s.RangeFilter)));
            Mapper.CreateMap<ArtifactDesignerViewModel, GetTabularDataRequest>();
            Mapper.CreateMap<TabularViewModel, GetTabularDataRequest>();
            Mapper.CreateMap<TabularViewModel.RowViewModel, GetTabularDataRequest.RowRequest>();
            Mapper.CreateMap<GetTabularDataResponse, TabularDataViewModel>();
            Mapper.CreateMap<GetTabularDataResponse.RowResponse, TabularDataViewModel.RowViewModel>();
            Mapper.CreateMap<GetArtifactResponse, TabularViewModel>();
            Mapper.CreateMap<GetArtifactResponse.RowResponse, TabularViewModel.RowViewModel>();

            //tank mapping
            Mapper.CreateMap<TankViewModel, CreateArtifactRequest.TankRequest>();
            Mapper.CreateMap<ArtifactDesignerViewModel, GetTankDataRequest>()
                .ForMember(x => x.Start, y => y.MapFrom(z => z.StartAfterParsed))
                .ForMember(x => x.End, y => y.MapFrom(z => z.EndAfterParsed)); ;
            Mapper.CreateMap<TankViewModel, GetTankDataRequest.TankRequest>();
            Mapper.CreateMap<GetTankDataResponse, TankDataViewModel>();
            Mapper.CreateMap<TankViewModel, UpdateArtifactRequest>();
            Mapper.CreateMap<TankViewModel, UpdateArtifactRequest.TankRequest>();
            Mapper.CreateMap<TabularViewModel, UpdateArtifactRequest>();
            Mapper.CreateMap<TabularViewModel.RowViewModel, UpdateArtifactRequest.RowRequest>();

            //multiaxis mapping
            Mapper.CreateMap<MultiaxisChartViewModel, GetMultiaxisChartDataRequest>();
            Mapper.CreateMap<MultiaxisChartViewModel.ChartViewModel, GetMultiaxisChartDataRequest.ChartRequest>()
                .ForMember(x => x.Series, o => o.ResolveUsing<MultiaxisSeriesValueResolver>());
            Mapper.CreateMap<GetMultiaxisChartDataResponse, MultiaxisChartDataViewModel>();
            Mapper.CreateMap<GetMultiaxisChartDataResponse.ChartResponse, MultiaxisChartDataViewModel.ChartViewModel>();
            Mapper.CreateMap<GetMultiaxisChartDataResponse.ChartResponse.SeriesViewModel, MultiaxisChartDataViewModel.ChartViewModel.SeriesViewModel>();
            Mapper.CreateMap<MultiaxisChartViewModel, CreateArtifactRequest>();
            Mapper.CreateMap<MultiaxisChartViewModel.ChartViewModel, CreateArtifactRequest.ChartRequest>()
                .ForMember(x => x.Series, o => o.ResolveUsing<MultiaxisSeriesCreateResolver>());
            Mapper.CreateMap<MultiaxisChartViewModel, UpdateArtifactRequest>();
            Mapper.CreateMap<MultiaxisChartViewModel.ChartViewModel, UpdateArtifactRequest.ChartRequest>()
                .ForMember(x => x.Series, o => o.ResolveUsing<MultiaxisSeriesUpdateResolver>());

            //combo mapping
            Mapper.CreateMap<ComboChartViewModel, GetComboChartDataRequest>();
            Mapper.CreateMap<ComboChartViewModel.ChartViewModel, GetComboChartDataRequest.ChartRequest>()
                .ForMember(x => x.Series, o => o.ResolveUsing<ComboSeriesValueResolver>());
            Mapper.CreateMap<GetComboChartDataResponse, ComboChartDataViewModel>();
            Mapper.CreateMap<GetComboChartDataResponse.ChartResponse, ComboChartDataViewModel.ChartViewModel>();
            Mapper.CreateMap<GetComboChartDataResponse.ChartResponse.SeriesViewModel, ComboChartDataViewModel.ChartViewModel.SeriesViewModel>();
            Mapper.CreateMap<ComboChartViewModel, CreateArtifactRequest>();
            Mapper.CreateMap<ComboChartViewModel.ChartViewModel, CreateArtifactRequest.ChartRequest>()
                .ForMember(x => x.Series, o => o.ResolveUsing<ComboSeriesCreateResolver>());
            Mapper.CreateMap<ComboChartViewModel, UpdateArtifactRequest>();
            Mapper.CreateMap<ComboChartViewModel.ChartViewModel, UpdateArtifactRequest.ChartRequest>()
                .ForMember(x => x.Series, o => o.ResolveUsing<ComboSeriesUpdateResolver>());
            Mapper.CreateMap<LineChartViewModel.SeriesViewModel, GetComboChartDataRequest.ChartRequest.SeriesRequest>();
            Mapper.CreateMap<AreaChartViewModel.SeriesViewModel, GetComboChartDataRequest.ChartRequest.SeriesRequest>();
            Mapper.CreateMap<AreaChartViewModel.StackViewModel, GetComboChartDataRequest.ChartRequest.StackRequest>();
            Mapper.CreateMap<BarChartViewModel.SeriesViewModel, GetComboChartDataRequest.ChartRequest.SeriesRequest>();
            Mapper.CreateMap<BarChartViewModel.StackViewModel, GetComboChartDataRequest.ChartRequest.StackRequest>();

            //pie mapping
            Mapper.CreateMap<ArtifactDesignerViewModel, GetPieDataRequest>()
                .ForMember(x => x.PeriodeType, o => o.MapFrom(s => Enum.Parse(typeof(EPeriodeType), s.PeriodeType)))
                .ForMember(x => x.RangeFilter, o => o.MapFrom(s => Enum.Parse(typeof(RangeFilter), s.RangeFilter)))
                .ForMember(x => x.ValueAxis, o => o.MapFrom(s => Enum.Parse(typeof(ValueAxis), s.ValueAxis)))
                .ForMember(x => x.Start, y => y.MapFrom(z => z.StartAfterParsed))
                .ForMember(x => x.End, y => y.MapFrom(z => z.EndAfterParsed));
            Mapper.CreateMap<PieViewModel, GetPieDataRequest>();
            Mapper.CreateMap<PieViewModel.SeriesViewModel, GetPieDataRequest.SeriesRequest>();
            Mapper.CreateMap<GetPieDataResponse, PieDataViewModel>();
            Mapper.CreateMap<GetPieDataResponse.SeriesResponse, PieDataViewModel.SeriesResponse>();
            Mapper.CreateMap<PieViewModel, CreateArtifactRequest>()
              .ForMember(x => x.Series, o => o.MapFrom(s => s.Series.MapTo<CreateArtifactRequest.SeriesRequest>()));
            Mapper.CreateMap<PieViewModel.SeriesViewModel, CreateArtifactRequest.SeriesRequest>();
            Mapper.CreateMap<GetArtifactResponse, GetPieDataRequest>();
            Mapper.CreateMap<GetArtifactResponse.SeriesResponse, GetPieDataRequest.SeriesRequest>();
            Mapper.CreateMap<GetArtifactResponse, PieViewModel>();
            Mapper.CreateMap<GetArtifactResponse.SeriesResponse, PieViewModel.SeriesViewModel>();
            Mapper.CreateMap<PieViewModel, UpdateArtifactRequest>();
            Mapper.CreateMap<PieViewModel.SeriesViewModel, UpdateArtifactRequest.SeriesRequest>();



            Mapper.CreateMap<LineChartViewModel.SeriesViewModel, GetMultiaxisChartDataRequest.ChartRequest.SeriesRequest>();
            Mapper.CreateMap<AreaChartViewModel.SeriesViewModel, GetMultiaxisChartDataRequest.ChartRequest.SeriesRequest>();
            Mapper.CreateMap<BarChartViewModel.SeriesViewModel, GetMultiaxisChartDataRequest.ChartRequest.SeriesRequest>();
            Mapper.CreateMap<BarChartViewModel.StackViewModel, GetMultiaxisChartDataRequest.ChartRequest.StackRequest>();
            Mapper.CreateMap<AreaChartViewModel.StackViewModel, GetMultiaxisChartDataRequest.ChartRequest.StackRequest>();



            //Mapper.CreateMap<BarChartViewModel.SeriesViewModel, GetSeriesRequest.Series>()
            //    .ForMember(x => x.Stacks, o => o.MapFrom(s => s.Stacks.MapTo<GetSeriesRequest.Stack>()));
            //Mapper.CreateMap<BarChartViewModel.StackViewModel, GetSeriesRequest.Stack>();
            //Mapper.CreateMap<GetSeriesResponse.SeriesResponse, BarChartDataViewModel.SeriesViewModel>();

            Mapper.CreateMap<GetGroupResponse, DSLNG.PEAR.Web.ViewModels.Kpi.Group>();

            Mapper.CreateMap<GetMethodResponse, DSLNG.PEAR.Web.ViewModels.Kpi.Method>();

            Mapper.CreateMap<BarChartViewModel.SeriesViewModel, GetCartesianChartDataRequest.SeriesRequest>()
                .ForMember(x => x.Stacks, o => o.MapFrom(s => s.Stacks.MapTo<GetCartesianChartDataRequest.StackRequest>()));
            Mapper.CreateMap<BarChartViewModel.StackViewModel, GetCartesianChartDataRequest.StackRequest>();
            Mapper.CreateMap<CreateGroupViewModel, CreateGroupRequest>();
            Mapper.CreateMap<GetGroupResponse, UpdateGroupViewModel>();
            Mapper.CreateMap<UpdateGroupViewModel, UpdateGroupRequest>();

            Mapper.CreateMap<CreatePeriodeViewModel, CreatePeriodeRequest>();
            Mapper.CreateMap<GetPeriodeResponse, UpdatePeriodeViewModel>();
            Mapper.CreateMap<UpdatePeriodeViewModel, UpdatePeriodeRequest>();

            Mapper.CreateMap<ArtifactDesignerViewModel, CreateArtifactRequest>()
                .ForMember(x => x.PeriodeType, o => o.MapFrom(s => Enum.Parse(typeof(EPeriodeType), s.PeriodeType)))
                .ForMember(x => x.RangeFilter, o => o.MapFrom(s => Enum.Parse(typeof(RangeFilter), s.RangeFilter)))
                .ForMember(x => x.ValueAxis, o => o.MapFrom(s => Enum.Parse(typeof(ValueAxis), s.ValueAxis)))
                .ForMember(x => x.Start, y => y.MapFrom(z => z.StartAfterParsed))
                .ForMember(x => x.End, y => y.MapFrom(z => z.EndAfterParsed));

            Mapper.CreateMap<ArtifactDesignerViewModel, UpdateArtifactRequest>()
              .ForMember(x => x.PeriodeType, o => o.MapFrom(s => Enum.Parse(typeof(EPeriodeType), s.PeriodeType)))
              .ForMember(x => x.RangeFilter, o => o.MapFrom(s => Enum.Parse(typeof(RangeFilter), s.RangeFilter)))
              .ForMember(x => x.ValueAxis, o => o.MapFrom(s => Enum.Parse(typeof(ValueAxis), s.ValueAxis)))
              .ForMember(x => x.Start, y => y.MapFrom(z => z.StartAfterParsed))
            .ForMember(x => x.End, y => y.MapFrom(z => z.EndAfterParsed));

            Mapper.CreateMap<GetArtifactResponse, GetCartesianChartDataRequest>()
                .ForMember(x => x.Series, o => o.MapFrom(s => s.Series.MapTo<GetCartesianChartDataRequest.SeriesRequest>()));
            Mapper.CreateMap<GetArtifactResponse.SeriesResponse, GetCartesianChartDataRequest.SeriesRequest>()
                .ForMember(x => x.Stacks, o => o.MapFrom(s => s.Stacks.MapTo<GetCartesianChartDataRequest.StackRequest>()));
            Mapper.CreateMap<GetArtifactResponse.StackResponse, GetCartesianChartDataRequest.StackRequest>();

            Mapper.CreateMap<GetArtifactResponse, GetSpeedometerChartDataRequest>()
             .ForMember(x => x.PlotBands, o => o.MapFrom(s => s.PlotBands.MapTo<GetSpeedometerChartDataRequest.PlotBandRequest>()))
             .ForMember(x => x.Series, o => o.MapFrom(s => s.Series[0]));
            Mapper.CreateMap<GetArtifactResponse.PlotResponse, GetSpeedometerChartDataRequest.PlotBandRequest>();
            Mapper.CreateMap<GetArtifactResponse.SeriesResponse, GetSpeedometerChartDataRequest.SeriesRequest>();

            Mapper.CreateMap<GetArtifactResponse, GetTabularDataRequest>();
            Mapper.CreateMap<GetArtifactResponse.RowResponse, GetTabularDataRequest.RowRequest>();
            Mapper.CreateMap<GetArtifactResponse, GetTankDataRequest>();
            Mapper.CreateMap<GetArtifactResponse.TankResponse, GetTankDataRequest.TankRequest>();
            Mapper.CreateMap<GetArtifactResponse.TankResponse, TankViewModel>();
            Mapper.CreateMap<GetArtifactResponse, TankViewModel>();
            //Mapper.CreateMap<GetArtifactResponse., GetSpeedometerChartDataRequest.PlotBandRequest>();
            //Mapper.CreateMap<GetArtifactResponse.SeriesResponse, GetSpeedometerChartDataRequest.SeriesRequest>();

            Mapper.CreateMap<CreateConversionViewModel, CreateConversionRequest>();
            Mapper.CreateMap<GetConversionResponse, UpdateConversionViewModel>()
                .ForMember(x => x.MeasurementFrom, o => o.MapFrom(k => k.From.Id))
                .ForMember(x => x.MeasurementTo, o => o.MapFrom(k => k.To.Id));
            Mapper.CreateMap<UpdateConversionViewModel, UpdateConversionRequest>();

            Mapper.CreateMap<TemplateViewModel, CreateTemplateRequest>();
            Mapper.CreateMap<TemplateViewModel.RowViewModel, CreateTemplateRequest.RowRequest>();
            Mapper.CreateMap<TemplateViewModel.ColumnViewModel, CreateTemplateRequest.ColumnRequest>();

            Mapper.CreateMap<TemplateViewModel, UpdateTemplateRequest>();
            Mapper.CreateMap<TemplateViewModel.RowViewModel, UpdateTemplateRequest.RowRequest>();
            Mapper.CreateMap<TemplateViewModel.ColumnViewModel, UpdateTemplateRequest.ColumnRequest>();

            Mapper.CreateMap<GetTemplateResponse, TemplateViewModel>();
            Mapper.CreateMap<GetTemplateResponse.RowResponse, TemplateViewModel.RowViewModel>();
            Mapper.CreateMap<GetTemplateResponse.ColumnResponse, TemplateViewModel.ColumnViewModel>();

            Mapper.CreateMap<HighlightViewModel, SaveHighlightRequest>();
            Mapper.CreateMap<GetReportHighlightsResponse.HighlightResponse, ArtifactPreviewViewModel.HighlightViewModel>();
            Mapper.CreateMap<VesselViewModel, SaveVesselRequest>();
            Mapper.CreateMap<GetVesselResponse, VesselViewModel>();

            Mapper.CreateMap<BuyerViewModel, SaveBuyerRequest>();
            Mapper.CreateMap<GetBuyerResponse, BuyerViewModel>();

            Mapper.CreateMap<VesselScheduleViewModel, SaveVesselScheduleRequest>();
            Mapper.CreateMap<GetVesselScheduleResponse, VesselScheduleViewModel>();

            Mapper.CreateMap<NLSViewModel, SaveNLSRequest>();
            Mapper.CreateMap<GetNLSResponse, NLSViewModel>();

            Mapper.CreateMap<CalculatorConstantViewModel, SaveCalculatorConstantRequest>();
            Mapper.CreateMap<GetCalculatorConstantResponse, CalculatorConstantViewModel>();

            Mapper.CreateMap<ConstantUsageViewModel, SaveConstantUsageRequest>()
                .ForMember(x => x.CalculatorConstantIds, o => o.MapFrom(s => s.Constants.Select(x => x.Id)));
            Mapper.CreateMap<GetConstantUsageResponse, ConstantUsageViewModel>();
            Mapper.CreateMap<GetConstantUsageResponse.CalculatorConstantResponse, ConstantUsageViewModel.CalculatorConstantViewModel>();
            Mapper.CreateMap<GetConstantUsagesResponse.ConstantUsageResponse, ConstantUsageViewModel>();
            Mapper.CreateMap<GetConstantUsagesResponse.ConstantResponse, ConstantUsageViewModel.CalculatorConstantViewModel>();
            Mapper.CreateMap<ConstantUsageViewModel.CalculatorConstantViewModel, CalculatorConstantViewModel>();
            Mapper.CreateMap<GetHighlightResponse, HighlightViewModel>();

            Mapper.CreateMap<WeatherViewModel, SaveWeatherRequest>();
            Mapper.CreateMap<GetWeatherResponse, WeatherViewModel>();
            Mapper.CreateMap<GetVesselSchedulesResponse.VesselScheduleResponse, DailyExecutionReportViewModel.NLSViewModel>();
            Mapper.CreateMap<GetWeatherResponse, DailyExecutionReportViewModel.WeatherViewModel>();
            Mapper.CreateMap<GetHighlightsResponse.HighlightResponse, DailyExecutionReportViewModel.HighlightViewModel>();
            Mapper.CreateMap<GetHighlightResponse, DailyExecutionReportViewModel.AlertViewModel>();
            Mapper.CreateMap<HighlightOrderViewModel, SaveHighlightOrderRequest>();
            Mapper.CreateMap<HighlightOrderViewModel, SaveStaticHighlightOrderRequest>();
            Mapper.CreateMap<AssumptionCategoryViewModel, SaveAssumptionCategoryRequest>();
            Mapper.CreateMap<GetAssumptionCategoryResponse, AssumptionCategoryViewModel>();

            Mapper.CreateMap<OutputCategoryViewModel, SaveOutputCategoryRequest>();
            Mapper.CreateMap<GetOutputCategoryResponse, OutputCategoryViewModel>();

            Mapper.CreateMap<OperationGroupViewModel, SaveOperationGroupRequest>();
            Mapper.CreateMap<GetOperationGroupResponse, OperationGroupViewModel>();

            Mapper.CreateMap<AssumptionConfigViewModel, SaveAssumptionConfigRequest>();
            Mapper.CreateMap<GetAssumptionConfigResponse, AssumptionConfigViewModel>();

            Mapper.CreateMap<ScenarioViewModel, SaveScenarioRequest>();
            Mapper.CreateMap<GetScenarioResponse, ScenarioViewModel>();

            Mapper.CreateMap<AssumptionDataViewModel, SaveAssumptionDataRequest>();
            Mapper.CreateMap<GetAssumptionDataResponse, AssumptionDataViewModel>();

            Mapper.CreateMap<OperationViewModel, SaveOperationRequest>();
            Mapper.CreateMap<GetOperationResponse, OperationViewModel>();

            Mapper.CreateMap<OperationalDataViewModel, SaveOperationalDataRequest>();
            Mapper.CreateMap<GetOperationalDataResponse, OperationalDataViewModel>();

            Mapper.CreateMap<EconomicConfigViewModel, SaveEconomicConfigRequest>();
            Mapper.CreateMap<GetEconomicConfigResponse, EconomicConfigViewModel>();
            Mapper.CreateMap<HighlightGroupViewModel, SaveHighlightGroupRequest>();
            Mapper.CreateMap<GetHighlightGroupResponse, HighlightGroupViewModel>();

            Mapper.CreateMap<GetHighlightGroupsResponse.HighlightGroupResponse, DailyExecutionReportViewModel.HighlightGroupViewModel>();
            Mapper.CreateMap<GetHighlightGroupsResponse.HighlightTypeResponse, DailyExecutionReportViewModel.HighlightTypeViewModel>();
            Mapper.CreateMap<GetDynamicHighlightsResponse.HighlightGroupResponse, DailyExecutionReportViewModel.HighlightGroupViewModel>();
            Mapper.CreateMap<GetDynamicHighlightsResponse.HighlightResponse, DailyExecutionReportViewModel.HighlightViewModel>();

            Mapper.CreateMap<GetScenarioResponse, AssumptionDataInputViewModel.ScenarioViewModel>();
            Mapper.CreateMap<GetAssumptionCategoriesResponse.AssumptionCategory, AssumptionDataInputViewModel.AssumptionCategoryViewModel>();
            Mapper.CreateMap<GetAssumptionCategoriesResponse.Assumption, AssumptionDataInputViewModel.AssumptionViewModel>();
            Mapper.CreateMap<GetAssumptionDatasResponse.AssumptionData, AssumptionDataInputViewModel.AssumptionDataViewModel>();

            Mapper.CreateMap<OutputConfigViewModel, SaveOutputConfigRequest>();
            Mapper.CreateMap<GetOutputConfigResponse, OutputConfigViewModel>();
            Mapper.CreateMap<GetOutputConfigResponse.KeyAssumptionConfig, SelectListItem>()
                .ForMember(x => x.Value, o => o.MapFrom(s => s.Id.ToString()))
                .ForMember(x => x.Text, o => o.MapFrom(s => s.Name));
            Mapper.CreateMap<GetOutputConfigResponse.Kpi, SelectListItem>()
                .ForMember(x => x.Value, o => o.MapFrom(s => s.Id.ToString()))
                .ForMember(x => x.Text, o => o.MapFrom(s => s.Name));

            Mapper.CreateMap<CalculateOutputResponse.OutputCategoryResponse, ScenarioResultViewModel.OutputCategoryViewModel>();
            Mapper.CreateMap<CalculateOutputResponse.KeyOutputResponse, ScenarioResultViewModel.KeyOutputViewModel>();
            Mapper.CreateMap<CalculateOutputResponse, ScenarioResultViewModel>();

            Mapper.CreateMap<OperationConfigUpdateViewModel, UpdateOperationRequest>();
            Mapper.CreateMap<PlanningBlueprintViewModel, SavePlanningBlueprintRequest>();
            Mapper.CreateMap<GetPlanningBlueprintResponse, PlanningBlueprintViewModel>();
            Mapper.CreateMap<GetPlanningBlueprintResponse.KeyOutputResponse, PlanningBlueprintViewModel.KeyOutputViewModel>();
            Mapper.CreateMap<GetKpiDetailResponse, DetailKpiViewModel>();
            Mapper.CreateMap<SaveESCategoryViewModel, SaveESCategoryRequest>();
            Mapper.CreateMap<GetESCategoryResponse, GetESCategoryViewModel>();
            Mapper.CreateMap<GetESCategoryViewModel, SaveESCategoryRequest>();


            Mapper.CreateMap<GetBusinessPostureResponse, BusinessPostureViewModel>();
            Mapper.CreateMap<GetBusinessPostureResponse.Posture, BusinessPostureViewModel.PostureViewModel>();
            Mapper.CreateMap<GetBusinessPostureResponse.DesiredState, BusinessPostureViewModel.DesiredStateViewModel>();
            Mapper.CreateMap<GetBusinessPostureResponse.PostureChallenge, BusinessPostureViewModel.PostureChallangeViewModel>();
            Mapper.CreateMap<GetBusinessPostureResponse.PostureConstraint, BusinessPostureViewModel.PostureConstraintViewModel>();
            Mapper.CreateMap<DesiredStateViewModel, SaveDesiredStateRequest>();
            Mapper.CreateMap<PostureChallengeViewModel, SavePostureChallengeRequest>();
            Mapper.CreateMap<PostureConstraintViewModel, SavePostureConstraintRequest>();
            Mapper.CreateMap<GetBusinessPostureResponse.EnvironmentScanning, BusinessPostureViewModel.EnvironmentScanning>();
            Mapper.CreateMap<GetBusinessPostureResponse.EnvironmentScanning.UltimateObjective, BusinessPostureViewModel.EnvironmentScanning.UltimateObjective>();
            Mapper.CreateMap<GetBusinessPostureResponse.EnvironmentScanning.Constraint, BusinessPostureViewModel.EnvironmentScanning.Constraint>();
            Mapper.CreateMap<GetBusinessPostureResponse.EnvironmentScanning.Challenge, BusinessPostureViewModel.EnvironmentScanning.Challenge>();

            Mapper.CreateMap<GetEnvironmentsScanningResponse, EnvironmentScanningViewModel>();
            Mapper.CreateMap<GetEnvironmentsScanningResponse.UltimateObjective, EnvironmentScanningViewModel.UltimateObjective>();
            Mapper.CreateMap<GetEnvironmentsScanningResponse.Environmental, EnvironmentScanningViewModel.Environmental>();
            Mapper.CreateMap<EnvironmentScanningViewModel.CreateViewModel, SaveEnvironmentScanningRequest>();
            Mapper.CreateMap<EnvironmentScanningViewModel.CreateEnvironmentalViewModel, SaveEnvironmentalScanningRequest>();
            Mapper.CreateMap<GetEnvironmentsScanningResponse.Constraint, EnvironmentScanningViewModel.Constraint>();
            Mapper.CreateMap<GetEnvironmentsScanningResponse.Challenge, EnvironmentScanningViewModel.Challenge>();
            Mapper.CreateMap<EnvironmentScanningViewModel.Constraint, SaveConstraintRequest>();
            Mapper.CreateMap<EnvironmentScanningViewModel.Challenge, SaveChallengeRequest>();

            Mapper.CreateMap<GetVoyagePlanResponse, VoyagePlanViewModel>();
            Mapper.CreateMap<GetVoyagePlanResponse.UltimateObjectivePoint, VoyagePlanViewModel.UltimateObjectivePointViewModel>();
            Mapper.CreateMap<GetVoyagePlanResponse.Challenge, VoyagePlanViewModel.ChallengeViewModel>();
            Mapper.CreateMap<GetVoyagePlanResponse.Constraint, VoyagePlanViewModel.ConstraintViewModel>();
            Mapper.CreateMap<GetVoyagePlanResponse.Posture, VoyagePlanViewModel.PostureViewModel>();
            Mapper.CreateMap<GetVoyagePlanResponse.DesiredState, VoyagePlanViewModel.DesiredStateViewModel>();
            Mapper.CreateMap<GetVoyagePlanResponse.PostureChallenge, VoyagePlanViewModel.PostureChallengeViewModel>();
            Mapper.CreateMap<GetVoyagePlanResponse.PostureConstraint, VoyagePlanViewModel.PostureConstraintViewModel>();
            Mapper.CreateMap<GetVoyagePlanResponse.KeyOutputResponse, VoyagePlanViewModel.KeyOutputViewModel>();
            Mapper.CreateMap<GetConstraintResponse, GetConstraintViewModel>();
            Mapper.CreateMap<GetConstraintResponse.Environmental, GetConstraintViewModel.Environmental>();
            Mapper.CreateMap<GetChallengeResponse, GetChallengeViewModel>();
            Mapper.CreateMap<GetChallengeResponse.Environmental, GetChallengeViewModel.Environmental>();
            Mapper.CreateMap<GetPostureChallengeResponse, PostureChalengeListViewModel>();
            Mapper.CreateMap<GetPostureChallengeResponse.DesiredState, PostureChalengeListViewModel.DesiredState>();
            Mapper.CreateMap<GetPostureConstraintResponse, PostureConstraintListViewModel>();
            Mapper.CreateMap<GetPostureConstraintResponse.DesiredState, PostureConstraintListViewModel.DesiredState>();

            Mapper.CreateMap<GetMidtermFormulationResponse, MidtermFormulationViewModel>();
            Mapper.CreateMap<GetMidtermFormulationResponse.Posture, MidtermFormulationViewModel.PostureViewModel>();
            Mapper.CreateMap<GetMidtermFormulationResponse.DesiredState, MidtermFormulationViewModel.DesiredStateViewModel>();
            Mapper.CreateMap<GetMidtermFormulationResponse.MidtermFormulationStage, MidtermFormulationViewModel.MidtermFormulationStageViewModel>();
            Mapper.CreateMap<GetMidtermFormulationResponse.MidtermPhaseDescription, MidtermFormulationViewModel.MidtermPhaseDescriptionViewModel>();
            Mapper.CreateMap<GetMidtermFormulationResponse.MidtermPhaseKeyDriver, MidtermFormulationViewModel.MidtermPhaseKeyDriverViewModel>();

            Mapper.CreateMap<MidtermPhaseStageViewModel, AddStageRequest>()
                .ForMember(d => d.StartDate, o => o.MapFrom(s => string.IsNullOrEmpty(s.StartDate) ? (DateTime?)null : DateTime.ParseExact("01/" + s.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(d => d.EndDate, o => o.MapFrom(s => string.IsNullOrEmpty(s.StartDate) ? (DateTime?)null : DateTime.ParseExact("01/" + s.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)));
            Mapper.CreateMap<MidtermStageDefinitionViewModel, AddDefinitionRequest>();

            Mapper.CreateMap<AddMidtermPlanningViewModel, AddMidtermPlanningRequest>()
               .ForMember(d => d.StartDate, o => o.MapFrom(s => string.IsNullOrEmpty(s.StartDate) ? (DateTime?)null : DateTime.ParseExact("01/" + s.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
               .ForMember(d => d.EndDate, o => o.MapFrom(s => string.IsNullOrEmpty(s.StartDate) ? (DateTime?)null : DateTime.ParseExact("01/" + s.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

            Mapper.CreateMap<GetActiveOutputCategoriesResponse, EconomicIndicatorsViewModel>();
            Mapper.CreateMap<GetActiveOutputCategoriesResponse.OutputCategoryResponse, EconomicIndicatorsViewModel.OutputCategoryViewModel>();
            Mapper.CreateMap<GetActiveOutputCategoriesResponse.KeyOutputResponse, EconomicIndicatorsViewModel.KeyOutputViewModel>();
            Mapper.CreateMap<SavePopDashboardViewModel, SavePopDashboardRequest>();
            Mapper.CreateMap<GetPopDashboardResponse, SavePopDashboardViewModel>();
            Mapper.CreateMap<GetPopDashboardResponse, GetPopDashboardViewModel>();
            Mapper.CreateMap<GetPopDashboardResponse.PopInformation, GetPopDashboardViewModel.PopInformation>();
            Mapper.CreateMap<GetPopDashboardResponse.Signature, SignatureViewModel>();
            Mapper.CreateMap<SavePopInformationViewModel, SavePopInformationRequest>();
            Mapper.CreateMap<GetPopDashboardViewModel, SaveSignatureRequest>();


            Mapper.CreateMap<SaveApprovalViewModel, ApproveSignatureRequest>();


            Mapper.CreateMap<KpiTargetInputViewModel, KpiTargetInputRequest>()
                .ForMember(d => d.Start, o => o.MapFrom(s => DateTime.ParseExact(s.Start, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(d => d.End, o => o.MapFrom(s => DateTime.ParseExact(s.End, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

            Mapper.CreateMap<KpiEconomicInputViewModel, KpiEconomicInputRequest>()
            .ForMember(d => d.Start, o => o.MapFrom(s => DateTime.ParseExact(s.Start, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
            .ForMember(d => d.End, o => o.MapFrom(s => DateTime.ParseExact(s.End, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

            Mapper.CreateMap<RejectVoyagePlanViewModel, RejectVoyagePlanRequest>();
            Mapper.CreateMap<RejectMidtermStrategyViewModel, RejectMidtermStrategyRequest>();

            base.Configure();
        }

        private void ConfigureProcessBlueprint()
        {
            Mapper.CreateMap<GetFilesResponse.ProcessBlueprint, ProcessBlueprintViewModel>();
            Mapper.CreateMap<GetFileResponse, ProcessBlueprintViewModel>();
            Mapper.CreateMap<GetFilesResponse.ProcessBlueprint, ProcessBlueprintFileItem>()
                .ForMember(x => x.FileID, y => y.MapFrom(z => z.Id));
            Mapper.CreateMap<GetFileResponse, ProcessBlueprintFileItem>()
                .ForMember(dest => dest.FileID, opt => opt.MapFrom(src => src.Id));
        }
        
        private void ConfigureEconomicSummary()
        {
            Mapper.CreateMap<EconomicSummaryCreateViewModel, SaveEconomicSummaryRequest>();
            Mapper.CreateMap<EconomicSummaryCreateViewModel.Scenario, SaveEconomicSummaryRequest.Scenario>();
            Mapper.CreateMap<GetEconomicSummaryResponse, EconomicSummaryViewModel>();
            Mapper.CreateMap<GetEconomicSummariesResponse.EconomicSummary, EconomicSummaryViewModel>();
            Mapper.CreateMap<GetEconomicSummaryResponse, EconomicSummaryCreateViewModel>();
            Mapper.CreateMap<GetEconomicSummaryResponse.Scenario, EconomicSummaryCreateViewModel.Scenario>();
            Mapper.CreateMap<GetEconomicSummaryReportResponse, EconomicSummaryReportViewModel>();
            Mapper.CreateMap<GetEconomicSummaryReportResponse.Group, EconomicSummaryReportViewModel.Group>();
            Mapper.CreateMap<GetEconomicSummaryReportResponse.Scenario, EconomicSummaryReportViewModel.Scenario>();
            Mapper.CreateMap<GetEconomicSummaryReportResponse.OutputResult, EconomicSummaryReportViewModel.OutputResult>();
            Mapper.CreateMap<GetEconomicSummaryReportResponse.KeyOutput, EconomicSummaryReportViewModel.KeyOutput>();

        }

        private void ConfigureOperationData()
        {
            Mapper.CreateMap<GetOperationDataConfigurationResponse, OperationDataConfigurationViewModel>();
            Mapper.CreateMap<GetOperationDataConfigurationResponse.Kpi, OperationDataConfigurationViewModel.Kpi>();
            Mapper.CreateMap<GetOperationDataConfigurationResponse.OperationData, OperationDataConfigurationViewModel.OperationData>();
            Mapper.CreateMap<UpdateOperationDataViewModel, UpdateOperationDataRequest>();
            Mapper.CreateMap<OperationDataParamConfigurationViewModel, GetOperationDataConfigurationRequest>();
            //.ForMember(x => x.PeriodeType, y => y.MapFrom(z => (Data.Enums.PeriodeType)Enum.Parse(typeof(Data.Enums.PeriodeType), z.PeriodeType)));
        }

        private void ConfigureSelect()
        {
            Mapper.CreateMap<CreateSelectViewModel, CreateSelectRequest>()
                .ForMember(x => x.Type, o => o.MapFrom(s => (SelectType)Enum.Parse(typeof(SelectType), s.Type, true)));
            Mapper.CreateMap<SelectOptionViewModel, CreateSelectRequest.SelectOption>();
            Mapper.CreateMap<GetSelectResponse, UpdateSelectViewModel>()
                .ForMember(x => x.ParentOptions, o => o.Ignore());
            Mapper.CreateMap<GetSelectResponse.SelectOptionResponse, SelectOptionViewModel>();
            Mapper.CreateMap<UpdateSelectViewModel, UpdateSelectRequest>()
                .ForMember(x => x.Type, o => o.MapFrom(s => (SelectType)Enum.Parse(typeof(SelectType), s.Type, true)));
            Mapper.CreateMap<SelectOptionViewModel, UpdateSelectRequest.SelectOption>();
            //Mapper.CreateMap<GetSelectsResponse, Ind>()
        }

        private void ConfigureTrafficLight()
        {
            Mapper.CreateMap<ArtifactDesignerViewModel, GetTrafficLightChartDataRequest>()
                .ForMember(x => x.Start, y => y.MapFrom(z => z.StartAfterParsed))
                .ForMember(x => x.End, y => y.MapFrom(z => z.EndAfterParsed));

            Mapper.CreateMap<TrafficLightChartViewModel, GetTrafficLightChartDataRequest>();
            Mapper.CreateMap<TrafficLightChartViewModel.SeriesViewModel, GetTrafficLightChartDataRequest.SeriesRequest>();
            Mapper.CreateMap<TrafficLightChartViewModel.PlotBand, GetTrafficLightChartDataRequest.PlotBandRequest>();
            Mapper.CreateMap<GetTrafficLightChartDataResponse.SeriesResponse, TrafficLightChartDataViewModel.SeriesViewModel>()
                .ForMember(x => x.data, o => o.MapFrom(s => new List<double> { s.data }));
            Mapper.CreateMap<GetTrafficLightChartDataResponse.PlotBandResponse, TrafficLightChartDataViewModel.PlotBandViewModel>();
            Mapper.CreateMap<TrafficLightChartViewModel, CreateArtifactRequest>()
            .ForMember(x => x.Series, o => o.MapFrom(s => new List<CreateArtifactRequest.SeriesRequest> { s.Series.MapTo<CreateArtifactRequest.SeriesRequest>() }))
            .ForMember(x => x.Plots, o => o.MapFrom(s => s.PlotBands.MapTo<CreateArtifactRequest.PlotRequest>()));
            Mapper.CreateMap<TrafficLightChartViewModel.SeriesViewModel, CreateArtifactRequest.SeriesRequest>();
            Mapper.CreateMap<TrafficLightChartViewModel.PlotBand, CreateArtifactRequest.PlotRequest>();
            Mapper.CreateMap<TrafficLightChartViewModel, UpdateArtifactRequest>()
            .ForMember(x => x.Series, o => o.MapFrom(s => new List<UpdateArtifactRequest.SeriesRequest> { s.Series.MapTo<UpdateArtifactRequest.SeriesRequest>() }))
            .ForMember(x => x.Plots, o => o.MapFrom(s => s.PlotBands.MapTo<UpdateArtifactRequest.PlotRequest>()));
            Mapper.CreateMap<TrafficLightChartViewModel.SeriesViewModel, UpdateArtifactRequest.SeriesRequest>();
            Mapper.CreateMap<TrafficLightChartViewModel.PlotBand, UpdateArtifactRequest.PlotRequest>();

            Mapper.CreateMap<GetArtifactResponse, GetTrafficLightChartDataRequest>()
             .ForMember(x => x.PlotBands, o => o.MapFrom(s => s.PlotBands.MapTo<GetTrafficLightChartDataRequest.PlotBandRequest>()))
             .ForMember(x => x.Series, o => o.MapFrom(s => s.Series[0]));
            Mapper.CreateMap<GetArtifactResponse.PlotResponse, GetTrafficLightChartDataRequest.PlotBandRequest>();
            Mapper.CreateMap<GetArtifactResponse.SeriesResponse, GetTrafficLightChartDataRequest.SeriesRequest>();

            Mapper.CreateMap<GetArtifactResponse, TrafficLightChartViewModel>()
                .ForMember(x => x.Series, o => o.MapFrom(s => s.Series.FirstOrDefault()));
            Mapper.CreateMap<GetArtifactResponse.SeriesResponse, TrafficLightChartViewModel.SeriesViewModel>();
            Mapper.CreateMap<GetArtifactResponse.PlotResponse, TrafficLightChartViewModel.PlotBand>();

            Mapper.CreateMap<GetOperationalDataDetailResponse, OperationDataDetailViewModel>();
            Mapper.CreateMap<GetOperationalDataDetailResponse.KeyOperationConfig, OperationDataDetailViewModel.KeyOperationConfigViewModel>();
            Mapper.CreateMap<GetOperationalDataDetailResponse.KeyOperationGroup, OperationDataDetailViewModel.KeyOperationGroupViewModel>();
            Mapper.CreateMap<GetOperationalDataDetailResponse.Kpi, OperationDataDetailViewModel.KpiViewModel>();

            Mapper.CreateMap<MidtermPlanningObjectiveViewModel, AddObjectiveRequest>();
            Mapper.CreateMap<AddPlanningKpiViewModel, AddPlanningKpiRequest>();
        }

        private void ConfigureCorporatePortofolio()
        {
            Mapper.CreateMap<GetPmsSummaryListResponse.PmsSummary, PmsSummaryConfigurationViewModel.CorporatePortofolio>();
            Mapper.CreateMap<GetPmsSummaryConfigurationResponse.PmsConfig, PmsSummaryDetailsViewModel.PmsConfig>();
            Mapper.CreateMap<GetPmsSummaryConfigurationResponse.PmsConfigDetails, PmsSummaryDetailsViewModel.PmsConfigDetails>()
                .ForMember(x => x.KpiId, y => y.MapFrom(z => z.Kpi.Id))
                .ForMember(x => x.KpiName, y => y.MapFrom(z => z.Kpi.Name))
                .ForMember(x => x.KpiMeasurement, y => y.MapFrom(z => z.Kpi.Measurement))
                .ForMember(x => x.ScoringType, y => y.MapFrom(z => z.ScoringType.ToString()));

            Mapper.CreateMap<GetPmsSummaryConfigurationResponse, PmsSummaryDetailsViewModel>()
                  .ForMember(x => x.Kpis, y => y.MapFrom(z => z.Kpis.Select(a => new SelectListItem
                      {
                          Text = a.Name,
                          Value = a.Id.ToString()
                      })))
                  .ForMember(x => x.Pillars, y => y.MapFrom(z => z.Pillars.Select(a => new SelectListItem
                      {
                          Text = a.Name,
                          Value = a.Id.ToString()
                      })));



            Mapper.CreateMap<GetScoreIndicatorsResponse, ScoreIndicatorDetailsViewModel>();
            Mapper.CreateMap<ScoreIndicator, ScoreIndicatorViewModel>();

            Mapper.CreateMap<ScoreIndicatorViewModel, ScoreIndicator>();
            Mapper.CreateMap<ScoreIndicator, ScoreIndicatorViewModel>();
            Mapper.CreateMap<GetHighlightOrdersResponse.HighlightOrderResponse, HighlightOrderViewModel>();
            Mapper.CreateMap<GetNLSListResponse.NLSResponse, NLSViewModel>();
        }

        private void ConfigurePmsSummary()
        {
            Mapper.CreateMap<CreatePmsSummaryViewModel, CreatePmsSummaryRequest>()
                .ForMember(x => x.ScoreIndicators, o => o.MapFrom(s => s.ScoreIndicators.Where(x => !string.IsNullOrEmpty(x.Color) && !string.IsNullOrEmpty(x.Expression))));
            Mapper.CreateMap<GetPmsSummaryResponse, UpdatePmsSummaryViewModel>();
            Mapper.CreateMap<UpdatePmsSummaryViewModel, UpdatePmsSummaryRequest>()
                .ForMember(x => x.ScoreIndicators, o => o.MapFrom(s => s.ScoreIndicators.Where(x => !string.IsNullOrEmpty(x.Color) && !string.IsNullOrEmpty(x.Expression))));

            Mapper.CreateMap<GetPmsSummaryReportResponse.KpiData, PmsSummaryViewModel>();
            Mapper.CreateMap<GetPmsDetailsResponse, PmsReportDetailsViewModel>();
            Mapper.CreateMap<GetPmsDetailsResponse.KpiAchievment, PmsReportDetailsViewModel.KpiAchievment>();
            Mapper.CreateMap<GetPmsDetailsResponse.KpiRelation, PmsReportDetailsViewModel.KpiRelation>();
            Mapper.CreateMap<GetPmsDetailsResponse.Group, PmsReportDetailsViewModel.Group>();
            ConfigurePmsConfig();
            ConfigurePmsConfigDetails();
        }

        private void ConfigurePmsConfig()
        {
            Mapper.CreateMap<CreatePmsConfigViewModel, CreatePmsConfigRequest>()
                .ForMember(x => x.ScoreIndicators, o => o.MapFrom(s => s.ScoreIndicators.Where(x => !string.IsNullOrEmpty(x.Color) && !string.IsNullOrEmpty(x.Expression))));
            Mapper.CreateMap<GetPmsConfigResponse, UpdatePmsConfigViewModel>();
            Mapper.CreateMap<UpdatePmsConfigViewModel, UpdatePmsConfigRequest>()
                .ForMember(x => x.ScoreIndicators, o => o.MapFrom(s => s.ScoreIndicators.Where(x => !string.IsNullOrEmpty(x.Color) && !string.IsNullOrEmpty(x.Expression))));
        }

        private void ConfigurePmsConfigDetails()
        {
            Mapper.CreateMap<CreatePmsConfigDetailsViewModel, CreatePmsConfigDetailsRequest>()
                .ForMember(x => x.ScoreIndicators, o => o.MapFrom(s => s.ScoreIndicators.Where(x => !string.IsNullOrEmpty(x.Color) && !string.IsNullOrEmpty(x.Expression))));
            Mapper.CreateMap<GetPmsConfigDetailsResponse, UpdatePmsConfigDetailsViewModel>();
            Mapper.CreateMap<UpdatePmsConfigDetailsViewModel, UpdatePmsConfigDetailsRequest>()
                .ForMember(x => x.ScoreIndicators, o => o.MapFrom(s => s.ScoreIndicators.Where(x => !string.IsNullOrEmpty(x.Color) && !string.IsNullOrEmpty(x.Expression))));
        }

        private void ConfigureKpiTarget()
        {
            Mapper.CreateMap<GetPmsConfigsResponse.Kpi, Kpi>()
                .ForMember(k => k.Unit, o => o.MapFrom(k => k.Measurement.Name));

            Mapper.CreateMap<GetKpiTargetResponse, UpdateKpiTargetViewModel>();
            Mapper.CreateMap<GetKpiTargetResponse.Kpi, UpdateKpiTargetViewModel.Kpi>();
            Mapper.CreateMap<GetKpiTargetResponse.KpiTarget, UpdateKpiTargetViewModel.KpiTarget>();
            Mapper.CreateMap<GetKpiTargetResponse.Pillar, UpdateKpiTargetViewModel.Pillar>();

            Mapper.CreateMap<UpdateKpiTargetViewModel, UpdateKpiTargetRequest>();
            Mapper.CreateMap<UpdateKpiTargetViewModel.Kpi, UpdateKpiTargetRequest.Kpi>();
            Mapper.CreateMap<UpdateKpiTargetViewModel.KpiTarget, UpdateKpiTargetRequest.KpiTarget>();
            Mapper.CreateMap<UpdateKpiTargetViewModel.Pillar, UpdateKpiTargetRequest.Pillar>();

            Mapper.CreateMap<AllKpiTargetsResponse, IndexKpiTargetViewModel>();
            Mapper.CreateMap<AllKpiTargetsResponse.Kpi, IndexKpiTargetViewModel.Kpi>();
            Mapper.CreateMap<AllKpiTargetsResponse.RoleGroup, IndexKpiTargetViewModel.RoleGroup>();

            Mapper.CreateMap<GetKpiTargetsConfigurationResponse, ConfigurationKpiTargetsViewModel>();
            Mapper.CreateMap<GetKpiTargetsConfigurationResponse.Kpi, ConfigurationKpiTargetsViewModel.Kpi>();
            Mapper.CreateMap<GetKpiTargetsConfigurationResponse.KpiTarget, ConfigurationKpiTargetsViewModel.KpiTarget>();
            Mapper.CreateMap<GetKpiTargetsConfigurationResponse, ConfigurationViewModel>();
            Mapper.CreateMap<GetKpiTargetsConfigurationResponse.Kpi, ConfigurationViewModel.Kpi>();
            Mapper.CreateMap<GetKpiTargetsConfigurationResponse.KpiTarget, ConfigurationViewModel.KpiTarget>();
            Mapper.CreateMap<ConfigurationViewModel.KpiTarget, ConfigurationViewModel.Item>();
            Mapper.CreateMap<ConfigurationViewModel.KpiAchievement, ConfigurationViewModel.Item>();
            Mapper.CreateMap<ConfigurationViewModel.Economic, ConfigurationViewModel.Item>();

            Mapper.CreateMap<UpdateKpiTargetViewModel.KpiTargetItem, ConfigurationViewModel.Item>();
            Mapper.CreateMap<GetKpiTargetItemResponse.KpiResponse, ConfigurationViewModel.Kpi>();
            Mapper.CreateMap<GetKpiTargetItemResponse, ConfigurationViewModel.KpiTarget>();
            Mapper.CreateMap<GetKpiTargetItemResponse, ConfigurationViewModel.Item>();

            Mapper.CreateMap<KpiTargetItem, CreateKpiTargetRequest>();
            Mapper.CreateMap<KpiTargetItem, UpdateKpiTargetItemRequest>()
                .ForMember(x => x.PeriodeType, o => o.MapFrom(x => (DSLNG.PEAR.Data.Enums.PeriodeType)x.PeriodeType));
            Mapper.CreateMap<KpiTargetItem, SaveKpiTargetRequest>();
            Mapper.CreateMap<UpdateKpiTargetViewModel.KpiTargetItem, SaveKpiTargetRequest>();
        }

        private void ConfigureKpiAchievement()
        {
            Mapper.CreateMap<GetKpiAchievementsResponse, UpdateKpiAchievementsViewModel>();
            Mapper.CreateMap<GetKpiAchievementsResponse.Kpi, UpdateKpiAchievementsViewModel.Kpi>();
            Mapper.CreateMap<GetKpiAchievementsResponse.KpiAchievement, UpdateKpiAchievementsViewModel.KpiAchievement>();
            Mapper.CreateMap<GetKpiAchievementsResponse.Pillar, UpdateKpiAchievementsViewModel.Pillar>();

            Mapper.CreateMap<UpdateKpiAchievementsViewModel, UpdateKpiAchievementsRequest>();
            Mapper.CreateMap<UpdateKpiAchievementsViewModel.Kpi, UpdateKpiAchievementsRequest.Kpi>();
            Mapper.CreateMap<UpdateKpiAchievementsViewModel.KpiAchievement, UpdateKpiAchievementsRequest.KpiAchievement>();
            Mapper.CreateMap<UpdateKpiAchievementsViewModel.Pillar, UpdateKpiAchievementsRequest.Pillar>();

            Mapper.CreateMap<AllKpiAchievementsResponse, IndexKpiAchievementViewModel>();
            Mapper.CreateMap<AllKpiAchievementsResponse.Kpi, IndexKpiAchievementViewModel.Kpi>();
            Mapper.CreateMap<AllKpiAchievementsResponse.RoleGroup, IndexKpiAchievementViewModel.RoleGroup>();

            Mapper.CreateMap<GetKpiAchievementsConfigurationResponse, ConfigurationKpiAchievementsViewModel>();
            Mapper.CreateMap<GetKpiAchievementsConfigurationResponse.Kpi, ConfigurationKpiAchievementsViewModel.Kpi>();
            Mapper.CreateMap<GetKpiAchievementsConfigurationResponse.KpiAchievement, ConfigurationKpiAchievementsViewModel.KpiAchievement>();

            Mapper.CreateMap<GetKpiAchievementsConfigurationResponse, ConfigurationViewModel>();
            Mapper.CreateMap<GetKpiAchievementsConfigurationResponse.Kpi, ConfigurationViewModel.Kpi>();
            Mapper.CreateMap<GetKpiAchievementsConfigurationResponse.KpiAchievement, ConfigurationViewModel.KpiAchievement>();

            Mapper.CreateMap<GetConfigurationResponse, ConfigurationViewModel>();
            Mapper.CreateMap<GetConfigurationResponse.Kpi, ConfigurationViewModel.Kpi>();
            Mapper.CreateMap<GetConfigurationResponse.KpiAchievement, ConfigurationViewModel.KpiAchievement>();
            Mapper.CreateMap<GetConfigurationResponse.KpiTarget, ConfigurationViewModel.KpiTarget>();
            Mapper.CreateMap<GetConfigurationResponse.Economic, ConfigurationViewModel.Economic>();

            Mapper.CreateMap<UpdateKpiAchievementsViewModel.KpiAchievementItem, ConfigurationViewModel.Item>();
            Mapper.CreateMap<GetKpiAchievementsResponse.KpiAchievement, ConfigurationViewModel.Item>();
            Mapper.CreateMap<UpdateKpiAchievementItemRequest, ConfigurationViewModel.Item>();

            Mapper.CreateMap<UpdateKpiAchievementsViewModel.KpiAchievementItem, UpdateKpiAchievementItemRequest>()
                .ForMember(x => x.PeriodeType, o => o.MapFrom(x => (DSLNG.PEAR.Data.Enums.PeriodeType)x.PeriodeType));

        }

        private void ConfigureDerViewModel()
        {
            Mapper.CreateMap<GetDerResponse, DerViewModel>();
            Mapper.CreateMap<DerViewModel, CreateOrUpdateDerRequest>();
            Mapper.CreateMap<GetActiveDerResponse, DerIndexViewModel>();
            Mapper.CreateMap<GetActiveDerResponse.DerItem, DerIndexViewModel.DerItem>();
            Mapper.CreateMap<ManageDerItemViewModel, GetDerItemRequest>();
            Mapper.CreateMap<GetDerItemResponse, ManageDerItemViewModel>();
            Mapper.CreateMap<GetDerLayoutitemsResponse, DerLayoutConfigViewModel>();
            Mapper.CreateMap<GetDerLayoutitemsResponse.LayoutItem, DerLayoutConfigViewModel.LayoutItem>();
            
            Mapper.CreateMap<DerLayoutItemViewModel, SaveLayoutItemRequest>();
            Mapper.CreateMap<DerLayoutLineViewModel, SaveLayoutItemRequest.LayoutItemArtifact>();
            Mapper.CreateMap<LineChartViewModel, SaveLayoutItemRequest.LayoutItemArtifactLine>();
            Mapper.CreateMap<DerLayoutLineViewModel, SaveLayoutItemRequest.LayoutItemArtifactSerie>();

            Mapper.CreateMap<GetDerLayoutResponse, DerDisplayViewModel >();
            /*Mapper.CreateMap<GetDerLayoutResponse.DerArtifact, DerDisplayViewModel.DerArtifact>();
            Mapper.CreateMap<GetDerLayoutResponse.DerArtifactSerie, DerDisplayViewModel.DerArtifactSerie>();*/
            Mapper.CreateMap<GetDerLayoutResponse.DerLayoutItem, DerDisplayViewModel.DerLayoutItem>();

            //artifact DER
            Mapper.CreateMap<DerLayoutItemViewModel, SaveLayoutItemRequest>();
            Mapper.CreateMap<DerLayoutItemViewModel.DerLayoutItemArtifactViewModel, SaveLayoutItemRequest.LayoutItemArtifact>();
            Mapper.CreateMap<GetDerLayoutitemResponse, DerLayoutItemViewModel>();
            Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifact, DerLayoutItemViewModel.DerLayoutItemArtifactViewModel>();
            //Mapper.CreateMap<GetDerLayoutitemRespons.e.DerArtifactChart, LineChartViewModel.SeriesViewModel>();

            //DER Line
            Mapper.CreateMap<LineChartViewModel, SaveLayoutItemRequest.LayoutItemArtifactLine>();
            Mapper.CreateMap<LineChartViewModel.SeriesViewModel, SaveLayoutItemRequest.LayoutItemArtifactSerie>();
            Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifact, LineChartViewModel>();
            Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifactSerie, LineChartViewModel.SeriesViewModel>();

            //DER Multiaxis
            Mapper.CreateMap<MultiaxisChartViewModel, SaveLayoutItemRequest.LayoutItemArtifactMultiAxis>();
            Mapper.CreateMap<MultiaxisChartViewModel.ChartViewModel, SaveLayoutItemRequest.LayoutItemArtifactChart>()
                .ForMember(x => x.Series, y => y.MapFrom(z => z.LineChart.Series));
            Mapper.CreateMap<GetDerLayoutitemResponse, MultiaxisChartViewModel>();
            Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifactChart, MultiaxisChartViewModel.ChartViewModel>();
            Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifact, MultiaxisChartViewModel>()
                .ForMember(x => x.Charts, o => o.Ignore());
            Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifactChart, LineChartViewModel>();
            //Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifactSerie, MultiaxisChartViewModel.ChartViewModel>();

            Mapper.CreateMap<GetDerLayoutitemResponse, GetCartesianChartDataRequest>()
                  .ForMember(x => x.GraphicType, y => y.MapFrom(z => z.Artifact.GraphicType))
                  .ForMember(x => x.HeaderTitle, y => y.MapFrom(z => z.Artifact.HeaderTitle))
                  .ForMember(x => x.MeasurementId, y => y.MapFrom(z => z.Artifact.MeasurementId))
                  .ForMember(x => x.PeriodeType, y => y.MapFrom(z => DSLNG.PEAR.Data.Enums.PeriodeType.Daily))
                  .ForMember(x => x.RangeFilter, y => y.MapFrom(z => DSLNG.PEAR.Data.Enums.RangeFilter.Interval))
                  .ForMember(x => x.ValueAxis, y => y.MapFrom(z => DSLNG.PEAR.Data.Enums.ValueAxis.KpiActual));

            Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifactSerie, GetCartesianChartDataRequest.SeriesRequest>();
            Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifactChart, GetMultiaxisChartDataRequest.ChartRequest>();
            Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifactSerie, GetMultiaxisChartDataRequest.ChartRequest.SeriesRequest>();

            Mapper.CreateMap<GetDerLayoutitemResponse, GetMultiaxisChartDataRequest>()
                  .ForMember(x => x.PeriodeType, y => y.MapFrom(z => DSLNG.PEAR.Data.Enums.PeriodeType.Daily))
                  .ForMember(x => x.RangeFilter, y => y.MapFrom(z => DSLNG.PEAR.Data.Enums.RangeFilter.Interval));

            
            //DER Pie
            Mapper.CreateMap<PieViewModel, SaveLayoutItemRequest.LayoutItemArtifactPie>();
            Mapper.CreateMap<PieViewModel.SeriesViewModel, SaveLayoutItemRequest.LayoutItemArtifactSerie>();
            Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifact, PieViewModel>();
            Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifactSerie, PieViewModel.SeriesViewModel>();

            //DER Tank
            Mapper.CreateMap<TankViewModel, SaveLayoutItemRequest.LayoutItemArtifactTank>();
            Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifactTank, GetTankDataRequest.TankRequest>();
            Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifactTank, TankViewModel>();
            Mapper.CreateMap<GetDerLayoutitemResponse.DerArtifact, TankViewModel>();

            //Der KpiInformations
            Mapper.CreateMap<DerLayoutItemViewModel.DerKpiInformationViewModel, SaveLayoutItemRequest.DerKpiInformationRequest>()
                .ForMember(x => x.ConfigType, y=> y.MapFrom(z => (ConfigType)Enum.Parse(typeof(ConfigType), z.ConfigType)));
            Mapper.CreateMap<GetDerLayoutitemResponse.KpiInformationResponse, DerLayoutItemViewModel.DerKpiInformationViewModel>();

            //DER original data
            Mapper.CreateMap<GetOriginalDataResponse, DisplayOriginalDataViewModel>();
            Mapper.CreateMap<GetOriginalDataResponse.OriginalDataResponse, DisplayOriginalDataViewModel.OriginalDataViewModel>()
                .ForMember(x => x.PeriodeType, y => y.MapFrom(z => z.PeriodeType.ToString()));
            Mapper.CreateMap<DisplayOriginalDataViewModel, SaveOriginalDataRequest>();
            Mapper.CreateMap<DisplayOriginalDataViewModel.OriginalDataViewModel, SaveOriginalDataRequest.OriginalDataRequest>();

            /*Mapper.CreateMap<DerLayoutItemViewModel, SaveLayoutItemRequest>();
            Mapper.CreateMap<DerLayoutLineViewModel, SaveLayoutItemRequest.LayoutItemArtifact>();
            Mapper.CreateMap<LineChartViewModel, SaveLayoutItemRequest.LayoutItemArtifactLine>();*/

        }

    }

    public class MultiaxisSeriesValueResolver : ValueResolver<MultiaxisChartViewModel.ChartViewModel, IList<GetMultiaxisChartDataRequest.ChartRequest.SeriesRequest>>
    {
        protected override IList<GetMultiaxisChartDataRequest.ChartRequest.SeriesRequest> ResolveCore(MultiaxisChartViewModel.ChartViewModel source)
        {
            switch (source.GraphicType)
            {
                case "line":
                    return source.LineChart.Series.MapTo<GetMultiaxisChartDataRequest.ChartRequest.SeriesRequest>();
                case "area":
                    return source.AreaChart.Series.MapTo<GetMultiaxisChartDataRequest.ChartRequest.SeriesRequest>();
                default:
                    return source.BarChart.Series.MapTo<GetMultiaxisChartDataRequest.ChartRequest.SeriesRequest>();

            }
        }
    }
    public class MultiaxisSeriesCreateResolver : ValueResolver<MultiaxisChartViewModel.ChartViewModel, IList<CreateArtifactRequest.SeriesRequest>>
    {
        protected override IList<CreateArtifactRequest.SeriesRequest> ResolveCore(MultiaxisChartViewModel.ChartViewModel source)
        {
            switch (source.GraphicType)
            {
                case "line":
                    return source.LineChart.Series.MapTo<CreateArtifactRequest.SeriesRequest>();
                case "area":
                    return source.AreaChart.Series.MapTo<CreateArtifactRequest.SeriesRequest>();
                default:
                    return source.BarChart.Series.MapTo<CreateArtifactRequest.SeriesRequest>();

            }
        }
    }


    public class MultiaxisSeriesUpdateResolver : ValueResolver<MultiaxisChartViewModel.ChartViewModel, IList<UpdateArtifactRequest.SeriesRequest>>
    {
        protected override IList<UpdateArtifactRequest.SeriesRequest> ResolveCore(MultiaxisChartViewModel.ChartViewModel source)
        {
            switch (source.GraphicType)
            {
                case "line":
                    return source.LineChart.Series.MapTo<UpdateArtifactRequest.SeriesRequest>();
                case "area":
                    return source.AreaChart.Series.MapTo<UpdateArtifactRequest.SeriesRequest>();
                default:
                    return source.BarChart.Series.MapTo<UpdateArtifactRequest.SeriesRequest>();

            }
        }
    }

    public class ComboSeriesValueResolver : ValueResolver<ComboChartViewModel.ChartViewModel, IList<GetComboChartDataRequest.ChartRequest.SeriesRequest>>
    {
        protected override IList<GetComboChartDataRequest.ChartRequest.SeriesRequest> ResolveCore(ComboChartViewModel.ChartViewModel source)
        {
            switch (source.GraphicType)
            {
                case "line":
                    return source.LineChart.Series.MapTo<GetComboChartDataRequest.ChartRequest.SeriesRequest>();
                case "area":
                    return source.AreaChart.Series.MapTo<GetComboChartDataRequest.ChartRequest.SeriesRequest>();
                default:
                    return source.BarChart.Series.MapTo<GetComboChartDataRequest.ChartRequest.SeriesRequest>();

            }
        }
    }

    public class ComboSeriesCreateResolver : ValueResolver<ComboChartViewModel.ChartViewModel, IList<CreateArtifactRequest.SeriesRequest>>
    {
        protected override IList<CreateArtifactRequest.SeriesRequest> ResolveCore(ComboChartViewModel.ChartViewModel source)
        {
            switch (source.GraphicType)
            {
                case "line":
                    return source.LineChart.Series.MapTo<CreateArtifactRequest.SeriesRequest>();
                case "area":
                    return source.AreaChart.Series.MapTo<CreateArtifactRequest.SeriesRequest>();
                default:
                    return source.BarChart.Series.MapTo<CreateArtifactRequest.SeriesRequest>();

            }
        }
    }


    public class ComboSeriesUpdateResolver : ValueResolver<ComboChartViewModel.ChartViewModel, IList<UpdateArtifactRequest.SeriesRequest>>
    {
        protected override IList<UpdateArtifactRequest.SeriesRequest> ResolveCore(ComboChartViewModel.ChartViewModel source)
        {
            switch (source.GraphicType)
            {
                case "line":
                    return source.LineChart.Series.MapTo<UpdateArtifactRequest.SeriesRequest>();
                case "area":
                    return source.AreaChart.Series.MapTo<UpdateArtifactRequest.SeriesRequest>();
                default:
                    return source.BarChart.Series.MapTo<UpdateArtifactRequest.SeriesRequest>();

            }
        }
    }
}