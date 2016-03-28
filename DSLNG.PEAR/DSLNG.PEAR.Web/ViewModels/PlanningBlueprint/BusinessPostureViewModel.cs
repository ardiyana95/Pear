

using System.Collections.Generic;
namespace DSLNG.PEAR.Web.ViewModels.PlanningBlueprint
{
    public class BusinessPostureViewModel
    {
        public int Id { get; set; }
        public int PlanningBlueprintId { get; set; }
        public bool IsReviewer { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public bool IsBeingReviewed { get; set; }
        //provoser or reviewer
        public bool IsLocked { get; set; }

        public PostureViewModel ConstructionPosture { get; set; }
        public PostureViewModel OperationPosture { get; set; }
        public PostureViewModel DecommissioningPosture { get; set; }
        public EnvironmentScanning EnvironmentScanningHost { get; set; }

        public class PostureViewModel {
            public int Id { get; set; }
            PostureViewModel() {
                DesiredStates = new List<DesiredStateViewModel>();
                PostureChallenges = new List<PostureChallangeViewModel>();
                PostureConstraints = new List<PostureConstraintViewModel>();
            }
            public IList<DesiredStateViewModel> DesiredStates { get; set; }
            public IList<PostureChallangeViewModel> PostureChallenges { get; set; }
            public IList<PostureConstraintViewModel> PostureConstraints { get; set; }
        }

        public class DesiredStateViewModel {
            public int Id { get; set; }
            public string Description { get; set; }
        }

        public class PostureChallangeViewModel {
            public int Id { get; set; }
            public string Definition { get; set; }
            public bool HasRelation { get; set; }
            public int[] Ids { get; set; }
            public string RelationIdsString 
            {
                get { return string.Join(",", this.Ids); } 
            }
        }

        public class PostureConstraintViewModel {
            public int Id { get; set; }
            public string Definition { get; set; }
            public bool HasRelation { get; set; }
            public int[] Ids { get; set; }
            public string RelationIdsString
            {
                get { return string.Join(",", this.Ids); }
            }
        }


        public class EnvironmentScanning
        {
            public EnvironmentScanning()
            {
                ConstructionPhase = new List<UltimateObjective>();
                OperationPhase = new List<UltimateObjective>();
                ReinventPhase = new List<UltimateObjective>();
                Constraints = new List<Constraint>();
                Challenges = new List<Challenge>();
            }
            public int Id { get; set; }
            public int BusinessPostureId { get; set; }
            public bool IsApproved { get; set; }
            public bool IsLocked { get; set; }
            public IList<UltimateObjective> ConstructionPhase { get; set; }
            public IList<UltimateObjective> OperationPhase { get; set; }
            public IList<UltimateObjective> ReinventPhase { get; set; }
            public IList<Constraint> Constraints { get; set; }
            public IList<Challenge> Challenges { get; set; }

            public class UltimateObjective
            {
                public int Id { get; set; }
                public string Description { get; set; }
            }

            public class Constraint
            {
                public int Id { get; set; }
                public string Definition { get; set; }
                public string Type { get; set; }
                public string Category { get; set; }
            }

            public class Challenge
            {
                public int Id { get; set; }
                public string Definition { get; set; }
                public string Type { get; set; }
                public string Category { get; set; }
            }
        }
    }
}