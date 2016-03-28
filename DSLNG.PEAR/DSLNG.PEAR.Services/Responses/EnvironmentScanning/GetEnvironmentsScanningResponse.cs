using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.EnvironmentScanning
{
    public class GetEnvironmentsScanningResponse
    {
        public int Id { get; set; }
        public int BusinessPostureId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsBeingReviewed { get; set; }
        public bool IsRejected { get; set; }
        public bool IsLocked { get; set; }
        public IList<UltimateObjective> ConstructionPhase { get; set; }
        public IList<UltimateObjective> OperationPhase { get; set; }
        public IList<UltimateObjective> ReinventPhase { get; set; }
        public IList<Environmental> Threat { get; set; }
        public IList<Environmental> Opportunity { get; set; }
        public IList<Environmental> Weakness { get; set; }
        public IList<Environmental> Strength { get; set; }
        public IList<Constraint> Constraints { get; set; }
        public IList<Challenge> Challenges { get; set; }


        public class UltimateObjective
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }

        public class Environmental
        {
            public int Id { get; set; }
            public string Desc { get; set; }
        }

        public class Constraint
        {
            public int Id { get; set; }
            public IList<Environmental> Relation { get; set; }
            public string Definition { get; set; }
            public string Type { get; set; }
            public int CategoryId { get; set; }
            public string Category { get; set; }
            public int[] RelationIds { get; set; }
            public int[] ThreatIds { get; set; }
            public int[] OpportunityIds { get; set; }
            public int[] WeaknessIds { get; set; }
            public int[] StrengthIds { get; set; }
        }

        public class Challenge
        {
            public int Id { get; set; }
            public IList<Environmental> Relation { get; set; }
            public string Definition { get; set; }
            public string Type { get; set; }
            public int CategoryId { get; set; }
            public string Category { get; set; }
            public int[] RelationIds { get; set; }
            public int[] ThreatIds { get; set; }
            public int[] OpportunityIds { get; set; }
            public int[] WeaknessIds { get; set; }
            public int[] StrengthIds { get; set; }
        }
    }
}
