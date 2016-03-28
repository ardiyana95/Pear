// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services;
using DSLNG.PEAR.Services.Interfaces;
using StructureMap.Web.Pipeline;

namespace DSLNG.PEAR.Web.DependencyResolution {
    using DSLNG.PEAR.Web.Controllers;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
	
    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
					scan.With(new ControllerConvention());
                });
            //For<IExample>().Use<Example>();
            //For<IDataContext>().Use<DataContext>();
            For<IUserService>().Use<UserService>();
            For<ILevelService>().Use<LevelService>();
            For<IPillarService>().Use<PillarService>();
            For<IMenuService>().Use<MenuService>();
            For<IGroupService>().Use<GroupService>();
            For<IKpiService>().Use<KpiService>();
            For<IMeasurementService>().Use<MeasurementService>();
            For<IMethodService>().Use<MethodService>();
            For<IDataContext>().LifecycleIs<HttpContextLifecycle>().Use<DataContext>();
            For<IRoleGroupService>().Use<RoleGroupService>();
            For<ITypeService>().Use<TypeService>();
            For<IPmsSummaryService>().Use<PmsSummaryService>();
            For<IArtifactService>().Use<ArtifactService>();
            For<IPeriodeService>().Use<PeriodeService>();
            For<IKpiTargetService>().Use<KpiTargetService>();
            For<IConversionService>().Use<ConversionService>();
            For<IDropdownService>().Use<DropdownService>();
            For<ITemplateService>().Use<TemplateService>();
            For<IKpiAchievementService>().Use<KpiAchievementService>();
            For<IHighlightService>().Use<HighlightService>();
            For<ISelectService>().Use<SelectService>();
            For<IVesselService>().Use<VesselService>();
            For<IBuyerService>().Use<BuyerService>();
            For<IVesselScheduleService>().Use<VesselScheduleService>();
            For<INLSService>().Use<NLSService>();
            For<ICalculatorConstantService>().Use<CalculatorConstantService>();
            For<IConstantUsageService>().Use<ConstantUsageService>();
            For<IWeatherService>().Use<WeatherService>();
            For<IHighlightOrderService>().Use<HighlightOrderService>();
            For<IAssumptionCategoryService>().Use<AssumptionCategoryService>();
            For<IOutputCategoryService>().Use<OutputCategoryService>();
            For<IOperationGroupService>().Use<OperationGroupService>();
            For<IAssumptionConfigService>().Use<AssumptionConfigService>();
            For<IScenarioService>().Use<ScenarioService>();
            For<IAssumptionDataService>().Use<AssumptionDataService>();
            For<IOperationConfigService>().Use<OperationConfigService>();
            For<IOperationDataService>().Use<OperationDataService>();
            For<IEconomicSummaryService>().Use<EconomicSummaryService>();
            For<IHighlightGroupService>().Use<HighlightGroupService>();
            For<IOutputConfigService>().Use<OutputConfigService>();
            For<IDerService>().Use<DerService>();
            For<IPlanningBlueprintService>().Use<PlanningBlueprintService>();
			For<IBusinessPostureIdentificationService>().Use<BusinessPostureIdentificationService>();
            For<IEnvironmentScanningService>().Use<EnvironmentScanningService>();
            For<IMidtermFormulationService>().Use<MidtermFormulationService>();
            For<IMidtermPlanningService>().Use<MidtermPlanningService>();
            For<IPopDashboardService>().Use<PopDashboardService>();
            For<IPopInformationService>().Use<PopInformationService>();
            For<ISignatureService>().Use<SignatureService>();
            For<IProcessBlueprintServices>().Use<ProcessBlueprintServices>();
        }

        #endregion
    }
}