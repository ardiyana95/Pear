using System.Data.Entity;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Entities.Der;
using Type = DSLNG.PEAR.Data.Entities.Type;
using DSLNG.PEAR.Data.Entities.EconomicModel;
using System;
using DSLNG.PEAR.Data.Entities.Blueprint;
using DSLNG.PEAR.Data.Entities.Pop;
using DSLNG.PEAR.Data.Entities.Mir;

namespace DSLNG.PEAR.Data.Persistence
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext()
            : base("DefaultConnection")
        {
        }


        public IDbSet<Activity> Activities { get; set; }
        public IDbSet<Artifact> Artifacts { get; set; }
        public IDbSet<ArtifactSerie> ArtifactSeries { get; set; }
        public IDbSet<ArtifactStack> ArtifactStacks { get; set; }
        public IDbSet<ArtifactPlot> ArtifactPlots { get; set; }
        public IDbSet<ArtifactRow> ArtifactRows { get; set; }
        public IDbSet<ArtifactChart> ArtifactCharts { get; set; }
        public IDbSet<Conversion> Conversions { get; set; }
        public IDbSet<DashboardTemplate> DashboardTemplates { get; set; }
        public IDbSet<Group> Groups { get; set; }
        public IDbSet<Kpi> Kpis { get; set; }
        public IDbSet<KpiAchievement> KpiAchievements { get; set; }
        public IDbSet<KpiTarget> KpiTargets { get; set; }
        public IDbSet<LayoutColumn> LayoutColumns { get; set; }
        public IDbSet<LayoutRow> LayoutRows { get; set; }
        public IDbSet<Level> Levels { get; set; }
        public IDbSet<Measurement> Measurements { get; set; }
        public IDbSet<Menu> Menus { get; set; }
        public IDbSet<Method> Methods { get; set; }
        public IDbSet<Periode> Periodes { get; set; }
        public IDbSet<Pillar> Pillars { get; set; }
        public IDbSet<PmsConfig> PmsConfigs { get; set; }
        public IDbSet<PmsConfigDetails> PmsConfigDetails { get; set; }
        public IDbSet<PmsSummary> PmsSummaries { get; set; }
        public IDbSet<RoleGroup> RoleGroups { get; set; }
        public IDbSet<ScoreIndicator> ScoreIndicators { get; set; }
        public IDbSet<Type> Types { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<KpiRelationModel> KpiRelationModels { get; set; }
        public IDbSet<ArtifactTank> ArtifactTanks { get; set; }
        public IDbSet<Highlight> Highlights { get; set; }
        public IDbSet<HighlightGroup> HighlightGroups { get; set; }
        public IDbSet<Select> Selects { get; set; }
        public IDbSet<SelectOption> SelectOptions { get; set; }
        public IDbSet<Vessel> Vessels { get; set; }
        public IDbSet<VesselSchedule> VesselSchedules { get; set; }
        public IDbSet<NextLoadingSchedule> NextLoadingSchedules { get; set; }
        public IDbSet<Buyer> Buyers { get; set; }
        public IDbSet<CalculatorConstant> CalculatorConstants { get; set; }
        public IDbSet<ConstantUsage> ConstantUsages { get; set; }
        public IDbSet<Weather> Weathers { get; set; }
        public IDbSet<KeyAssumptionCategory> KeyAssumptionCategories { get; set; }
        public IDbSet<KeyOutputCategory> KeyOutputCategories { get; set; }
        public IDbSet<KeyAssumptionConfig> KeyAssumptionConfigs { get; set; }
        public IDbSet<Scenario> Scenarios { get; set; }
        public IDbSet<KeyAssumptionData> KeyAssumptionDatas { get; set; }
        public IDbSet<KeyOperationGroup> KeyOperationGroups { get; set; }
        public IDbSet<KeyOperationConfig> KeyOperationConfigs { get; set; }
        public IDbSet<KeyOperationData> KeyOperationDatas { get; set; }
        public IDbSet<EconomicSummary> EconomicSummaries { get; set; }
        public IDbSet<ResetPassword> ResetPasswords { get; set; }
        public IDbSet<KeyOutputConfiguration> KeyOutputConfigs { get; set; }
        public IDbSet<StaticHighlightPrivilege> StaticHighlightPrivileges { get; set; }
        public IDbSet<DerArtifactTank> DerArtifactTanks { get; set; }
        public IDbSet<DerOriginalData> DerOriginalDatas { get; set; }
        public IDbSet<PlanningBlueprint> PlanningBlueprints { get; set; }
        public IDbSet<UltimateObjectivePoint> UltimateObjectivePoints { get; set; }
        public IDbSet<EnvironmentsScanning> EnvironmentsScannings { get; set; }
        public IDbSet<BusinessPostureIdentification> BusinessPostures { get; set; }
        public IDbSet<Posture> Postures { get; set; }
        public IDbSet<DesiredState> DesiredStates { get; set; }
        public IDbSet<PostureChallenge> PostureChalleges { get; set; }
        public IDbSet<PostureConstraint> PostureConstraints { get; set; }
        public IDbSet<EnvironmentalScanning> EnvironmentalScannings { get; set; }
        public IDbSet<Constraint> Constraint { get; set; }
        public IDbSet<Challenge> Challenges { get; set; }
        public IDbSet<DerHighlight> DerHighlights { get; set; }
        public IDbSet<DerStaticHighlight> DerStaticHighlights { get; set; }
        public IDbSet<Der> Ders { get; set; }
        public IDbSet<DerItem> DerItems { get; set; }
        public IDbSet<DerLayout> DerLayouts { get; set; }
        public IDbSet<DerLayoutItem> DerLayoutItems { get; set; }
        public IDbSet<DerArtifact> DerArtifacts { get; set; }
        public IDbSet<DerKpiInformation> DerKpiInformations { get; set; }
        public IDbSet<DerArtifactChart> DerArtifactCharts { get; set; }
        public IDbSet<DerArtifactSerie> DerArtifactSeries { get; set; }
        public IDbSet<Wave> Waves { get; set; }

        public IDbSet<MidtermPhaseFormulationStage> MidtermPhaseFormulationStages { get; set; }
        public IDbSet<MidtermPhaseFormulation> MidtermPhaseFormulations { get; set; }
        public IDbSet<MidtermPhaseDescription> MidtermPhaseDescriptions { get; set; }
        public IDbSet<MidtermPhaseKeyDriver> MidtermPhaseKeyDrivers { get; set; }
        public IDbSet<MidtermStrategyPlanning> MidtermStrategyPlannings { get; set; }
        public IDbSet<MidtermStrategicPlanning> MidtermStrategicPlannings { get; set; }
        public IDbSet<MidtermStrategicPlanningObjective> MidtermStrategicPlanningObjectives { get; set; }
        public IDbSet<PopDashboard> PopDashboards { get; set; }
        public IDbSet<PopInformation> PopInformations { get; set; }
        public IDbSet<Signature> Signatures { get; set; }
        public IDbSet<ESCategory> ESCategories { get; set; }
        public IDbSet<MirDataTable> MirDataTables { get; set; }
        public IDbSet<MirHighlight> MirHighlights { get; set; }
        public IDbSet<MirArtifact> MirArtifacts { get; set; }
        public IDbSet<MirConfiguration> MirConfigurations { get; set; }

        public IDbSet<ProcessBlueprint> ProcessBlueprints { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kpi>()
                        .HasMany(x => x.RelationModels)
                        .WithRequired(x => x.KpiParent);

            modelBuilder.Entity<PmsConfig>()
                .HasMany(x => x.PmsConfigDetailsList)
                .WithOptional(x => x.PmsConfig)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PmsSummary>()
                .HasMany(x => x.PmsConfigs)
                .WithOptional(x => x.PmsSummary)
                .WillCascadeOnDelete();
            modelBuilder.Entity<KeyOutputConfiguration>()
                .Property(x => x.ConversionType).IsOptional();
            modelBuilder.Entity<KeyOutputConfiguration>()
                .Property(x => x.ConversionValue).IsOptional();
            //modelBuilder.Entity<Menu>()
            //    .HasKey(x => x.Id)
            //    .HasOptional(x => x.Parent)
            //    .WithMany()
            //    .HasForeignKey(x => x.ParentId);

            //modelBuilder.Entity<RoleGroup>()
            //    .HasMany(x => x.Menus)
            //    .WithMany(x=>x.RoleGroups)
            //    .Map( x =>
            //        {
            //            x.ToTable("MenusRoleGroups");
            //            x.MapLeftKey("MenuId");
            //            x.MapRightKey("ParentId");
            //        }
            //    );

            //modelBuilder.Entity<Artifact>()
            //   .HasMany(x => x.Series)
            //    .WithOptional()
            //   .WillCascadeOnDelete();
            modelBuilder.Entity<ArtifactSerie>()
              .HasMany(x => x.Stacks)
               .WithOptional()
              .WillCascadeOnDelete();
            //modelBuilder.Entity<Artifact>()
            //   .HasMany(x => x.Plots)
            //      .WithOptional(x => x.Artifact)
            //   .WillCascadeOnDelete();
            modelBuilder.Entity<Artifact>()
               .HasMany(x => x.Charts)
                  .WithOptional()
               .WillCascadeOnDelete();
            modelBuilder.Entity<ArtifactChart>()
           .HasMany(x => x.Series)
            .WithOptional()
           .WillCascadeOnDelete();
            modelBuilder.Entity<ArtifactChart>()
          .HasMany(x => x.Plots)
           .WithOptional(x => x.ArtifactChart)
          .WillCascadeOnDelete();
            modelBuilder.Entity<Artifact>()
               .HasMany(x => x.Rows)
                  .WithOptional()
               .WillCascadeOnDelete();

            modelBuilder.Entity<Select>()
                .HasMany(x => x.Options)
                .WithRequired(x => x.Select)
                .WillCascadeOnDelete();

            modelBuilder.Entity<UltimateObjectivePoint>()
                .HasOptional(x => x.ConstructionPhaseHost)
                .WithMany(x => x.ConstructionPhase)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<UltimateObjectivePoint>()
                .HasOptional(x => x.OperationPhaseHost)
                .WithMany(x => x.OperationPhase)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UltimateObjectivePoint>()
                .HasOptional(x => x.ReinventPhaseHost)
                .WithMany(x => x.ReinventPhase)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EnvironmentsScanning>()
                .HasRequired(x => x.PlanningBlueprint)
                .WithRequiredDependent(x => x.EnvironmentsScanning)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<BusinessPostureIdentification>()
                .HasRequired(x => x.PlanningBlueprint)
                .WithRequiredDependent(x => x.BusinessPostureIdentification)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<MidtermPhaseFormulation>()
               .HasRequired(x => x.PlanningBlueprint)
               .WithRequiredDependent(x => x.MidtermPhaseFormulation)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<MidtermPhaseFormulation>()
                .HasMany(x => x.MidtermPhaseFormulationStages)
                .WithOptional(x => x.MidtermPhaseFormulation)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<MidtermStrategyPlanning>()
             .HasRequired(x => x.PlanningBlueprint)
             .WithRequiredDependent(x => x.MidtermStragetyPlanning)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<EnvironmentsScanning>()
                .HasMany(x => x.Challenges)
                .WithOptional(x => x.EnvironmentScanning)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EnvironmentsScanning>()
            .HasMany(x => x.Constraints)
            .WithOptional(x => x.EnvironmentScanning)
            .WillCascadeOnDelete(true);


            modelBuilder.Entity<EnvironmentalScanning>()
                .HasOptional(x => x.ThreatHost)
                .WithMany(x => x.Threat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EnvironmentalScanning>()
                .HasOptional(x => x.OpportunityHost)
                .WithMany(x => x.Opportunity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EnvironmentalScanning>()
                .HasOptional(x => x.WeaknessHost)
                .WithMany(x => x.Weakness)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EnvironmentalScanning>()
                .HasOptional(x => x.StrengthHost)
                .WithMany(x => x.Strength)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MidtermPhaseFormulationStage>()
                .HasMany(x => x.Descriptions)
                .WithOptional(x => x.Formulation)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<MidtermPhaseFormulationStage>()
                 .HasMany(x => x.KeyDrivers)
                .WithOptional(x => x.Formulation)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<MidtermPhaseFormulationStage>()
                .HasMany(x => x.MidtermStrategicPlannings)
               .WithOptional(x => x.Stage)
               .WillCascadeOnDelete(true);
            modelBuilder.Entity<MidtermStrategicPlanning>()
              .HasMany(x => x.Objectives)
              .WithOptional(x => x.MidtermStrategicPlanning)
              .WillCascadeOnDelete(true);
            modelBuilder.Entity<DerLayoutItem>()
               .HasOptional(x => x.Artifact)
               .WithOptionalDependent()
               .WillCascadeOnDelete();

            modelBuilder.Entity<DerLayoutItem>()
               .HasOptional(x => x.Highlight)
               .WithOptionalDependent()
               .WillCascadeOnDelete();

            modelBuilder.Entity<DerLayoutItem>()
               .HasOptional(x => x.StaticHighlight)
               .WithOptionalDependent()
               .WillCascadeOnDelete();

            modelBuilder.Entity<DerLayoutItem>()
              .HasMany(x => x.KpiInformations)
              .WithOptional()
              .WillCascadeOnDelete();

            modelBuilder.Entity<DerArtifact>()
                .HasMany(x => x.Charts)
                .WithOptional()
                .WillCascadeOnDelete();

            modelBuilder.Entity<DerArtifact>()
                .HasMany(x => x.Series)
                .WithOptional()
                .WillCascadeOnDelete();

            modelBuilder.Entity<DerArtifact>()
                .HasOptional(x => x.Tank)
                .WithOptionalDependent()
                .WillCascadeOnDelete();

            base.OnModelCreating(modelBuilder);
        }
        //public DbEntries 

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOptional(x => x.Role)
                .WithOptionalDependent()
                .WillCascadeOnDelete(true);
        }*/
    }
}
