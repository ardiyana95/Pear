

using System.Collections.Generic;
namespace DSLNG.PEAR.Services.Responses.BusinessPosture
{
    public class GetBusinessPostureResponse
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public bool IsBeingReviewed { get; set; }
        public int PlanningBlueprintId { get; set; }
        public Posture ConstructionPosture { get; set; }
        public Posture OperationPosture { get; set; }
        public Posture DecommissioningPosture { get; set; }
        public EnvironmentScanning EnvironmentScanningHost { get; set; }

        public class Posture
        {
            public int Id { get; set; }
            public IList<DesiredState> DesiredStates { get; set; }
            public IList<PostureChallenge> PostureChallenges { get; set; }
            public IList<PostureConstraint> PostureConstraints { get; set; }
        }

        public class DesiredState
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }

        public class PostureChallenge
        {
            public int Id { get; set; }
            public string Definition { get; set; }
            public bool HasRelation { get; set; }
            public int[] Ids { get; set; }
        }

        public class PostureConstraint
        {
            public int Id { get; set; }
            public string Definition { get; set; }
            public bool HasRelation { get; set; }
            public int[] Ids { get; set; }
        }


        public class EnvironmentScanning
        {
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
