using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Entities.Der;
using Type = DSLNG.PEAR.Data.Entities.Type;
using DSLNG.PEAR.Data.Entities.EconomicModel;
using DSLNG.PEAR.Data.Entities.Blueprint;
using DSLNG.PEAR.Data.Entities.Pop;

namespace DSLNG.PEAR.Data.Persistence
{
    public interface IDataContext
    {
        IDbSet<Activity> Activities { get; set; }
        IDbSet<Artifact> Artifacts { get; set; }
        IDbSet<ArtifactSerie> ArtifactSeries { get; set; }
        IDbSet<ArtifactStack> ArtifactStacks { get; set; }
        IDbSet<ArtifactPlot> ArtifactPlots { get; set; }
        IDbSet<ArtifactRow> ArtifactRows { get; set; }
        IDbSet<ArtifactChart> ArtifactCharts { get; set; }
        IDbSet<Conversion> Conversions { get; set; }
        IDbSet<DashboardTemplate> DashboardTemplates { get; set; }
        IDbSet<Group> Groups { get; set; }
        IDbSet<Kpi> Kpis { get; set; }
        IDbSet<KpiAchievement> KpiAchievements { get; set; }
        IDbSet<KpiTarget> KpiTargets { get; set; }
        IDbSet<LayoutColumn> LayoutColumns { get; set; }
        IDbSet<LayoutRow> LayoutRows { get; set; }
        IDbSet<Level> Levels { get; set; }
        IDbSet<Measurement> Measurements { get; set; }
        IDbSet<Menu> Menus { get; set; }
        IDbSet<Method> Methods { get; set; }
        IDbSet<Periode> Periodes { get; set; }
        IDbSet<Pillar> Pillars { get; set; }
        IDbSet<PmsConfig> PmsConfigs { get; set; }
        IDbSet<PmsConfigDetails> PmsConfigDetails { get; set; }
        IDbSet<PmsSummary> PmsSummaries { get; set; }
        IDbSet<RoleGroup> RoleGroups { get; set; }
        IDbSet<ScoreIndicator> ScoreIndicators { get; set; }
        IDbSet<Type> Types { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<KpiRelationModel> KpiRelationModels { get; set; }
        IDbSet<ArtifactTank> ArtifactTanks { get; set; }
        IDbSet<Highlight> Highlights { get; set; }
        IDbSet<HighlightGroup> HighlightGroups { get; set; }
        IDbSet<Select> Selects { get; set; }
        IDbSet<SelectOption> SelectOptions { get; set; }
        IDbSet<Vessel> Vessels { get; set; }
        IDbSet<VesselSchedule> VesselSchedules { get; set; }
        IDbSet<NextLoadingSchedule> NextLoadingSchedules { get; set; }
        IDbSet<Buyer> Buyers { get; set; }
        IDbSet<CalculatorConstant> CalculatorConstants { get; set; }
        IDbSet<ConstantUsage> ConstantUsages { get; set; }
        IDbSet<Weather> Weathers { get; set; }
        IDbSet<KeyAssumptionCategory> KeyAssumptionCategories { get; set; }
        IDbSet<KeyOutputCategory> KeyOutputCategories { get; set; }
        IDbSet<KeyAssumptionConfig> KeyAssumptionConfigs { get; set; }
        IDbSet<ResetPassword> ResetPasswords { get; set; }
        IDbSet<Scenario> Scenarios { get; set; }
        IDbSet<KeyAssumptionData> KeyAssumptionDatas { get; set; }
        IDbSet<KeyOperationConfig> KeyOperationConfigs { get; set; }
        IDbSet<KeyOperationData> KeyOperationDatas { get; set; }
        IDbSet<KeyOperationGroup> KeyOperationGroups { get; set; }
        IDbSet<EconomicSummary> EconomicSummaries { get; set; }
        IDbSet<KeyOutputConfiguration> KeyOutputConfigs { get; set; }
        IDbSet<StaticHighlightPrivilege> StaticHighlightPrivileges { get; set; }
        IDbSet<Der> Ders { get; set; }
        IDbSet<DerItem> DerItems { get; set; }
        IDbSet<DerLayout> DerLayouts { get; set; }
        IDbSet<DerLayoutItem> DerLayoutItems { get; set; }
        IDbSet<DerArtifact> DerArtifacts { get; set; }
        IDbSet<DerKpiInformation> DerKpiInformations { get; set; }
        IDbSet<DerArtifactChart> DerArtifactCharts { get; set; }
        IDbSet<DerArtifactSerie> DerArtifactSeries { get; set; }
        IDbSet<DerArtifactTank> DerArtifactTanks { get; set; }
        IDbSet<DerOriginalData> DerOriginalDatas { get; set; }
        IDbSet<PlanningBlueprint> PlanningBlueprints { get; set; }
        IDbSet<UltimateObjectivePoint> UltimateObjectivePoints { get; set; }
        IDbSet<EnvironmentsScanning> EnvironmentsScannings { get; set; }
        IDbSet<BusinessPostureIdentification> BusinessPostures { get; set; }
        IDbSet<Posture> Postures { get; set; }
        IDbSet<DesiredState> DesiredStates { get; set; }
        IDbSet<PostureChallenge> PostureChalleges { get; set; }
        IDbSet<PostureConstraint> PostureConstraints { get; set; }
        IDbSet<EnvironmentalScanning> EnvironmentalScannings { get; set; }
        IDbSet<Constraint> Constraint { get; set; }
        IDbSet<Challenge> Challenges { get; set; }
        IDbSet<MidtermPhaseFormulationStage> MidtermPhaseFormulationStages { get; set; }
        IDbSet<MidtermPhaseFormulation> MidtermPhaseFormulations { get; set; }
        IDbSet<MidtermPhaseDescription> MidtermPhaseDescriptions { get; set; }
        IDbSet<MidtermPhaseKeyDriver> MidtermPhaseKeyDrivers { get; set; }
        IDbSet<MidtermStrategyPlanning> MidtermStrategyPlannings { get; set; }
        IDbSet<MidtermStrategicPlanning> MidtermStrategicPlannings { get; set; }
        IDbSet<MidtermStrategicPlanningObjective> MidtermStrategicPlanningObjectives { get; set; }
        IDbSet<PopDashboard> PopDashboards { get; set; }
        IDbSet<PopInformation> PopInformations { get; set; }
        IDbSet<Signature> Signatures { get; set; }
        IDbSet<ESCategory> ESCategories { get; set; }
        IDbSet<DerHighlight> DerHighlights { get; set; }
        IDbSet<DerStaticHighlight> DerStaticHighlights { get; set; }
        IDbSet<Wave> Waves { get; set; }

        IDbSet<ProcessBlueprint> ProcessBlueprints { get; set; }
        Database Database { get; }
        int SaveChanges();
        DbEntityEntry Entry(object entity);
    }
}
