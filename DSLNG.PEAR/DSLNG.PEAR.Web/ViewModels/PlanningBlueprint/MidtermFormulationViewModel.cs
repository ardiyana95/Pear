
using System;
using System.Collections.Generic;
namespace DSLNG.PEAR.Web.ViewModels.PlanningBlueprint
{
    public class MidtermFormulationViewModel
    {
        public int Id { get; set; }
        public bool IsLocked { get; set; }

        public bool IsReviewer { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public bool IsBeingReviewed { get; set; }

        public bool IsDashboard { get; set; }
        public bool IsDashboardExist { get; set; }

        public PostureViewModel ConstructionPosture { get; set; }
        public PostureViewModel OperationPosture { get; set; }
        public PostureViewModel DecommissioningPosture { get; set; }
        public IList<MidtermFormulationStageViewModel> MidtermFormulationStages { get; set; }
        public class MidtermFormulationStageViewModel
        {
            public MidtermFormulationStageViewModel()
            {
                Descriptions = new List<MidtermPhaseDescriptionViewModel>();
                KeyDrivers = new List<MidtermPhaseKeyDriverViewModel>();
            }
            public int Id { get; set; }
            public string Title { get; set; }
            public int Order { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public IList<MidtermPhaseDescriptionViewModel> Descriptions { get; set; }
            public IList<MidtermPhaseKeyDriverViewModel> KeyDrivers { get; set; }
        }
        public class MidtermPhaseDescriptionViewModel
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }
        public class MidtermPhaseKeyDriverViewModel
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }
        public class PostureViewModel
        {
            public PostureViewModel()
            {
                DesiredStates = new List<DesiredStateViewModel>();
            }
            public int Id { get; set; }
            public IList<DesiredStateViewModel> DesiredStates { get; set; }
        }
        public class DesiredStateViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }
    }
}