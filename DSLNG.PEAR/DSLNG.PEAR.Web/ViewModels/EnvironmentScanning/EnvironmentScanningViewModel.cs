using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.EnvironmentScanning
{
    public class EnvironmentScanningViewModel
    {
        public EnvironmentScanningViewModel()
        {
            ConstructionPhase = new List<UltimateObjective>();
            OperationPhase = new List<UltimateObjective>();
            ReinventPhase = new List<UltimateObjective>();
            Threat = new List<Environmental>();
            Opportunity = new List<Environmental>();
            Weakness = new List<Environmental>();
            Strength = new List<Environmental>();
            Relation = new List<Environmental>();
            Types = new List<SelectListItem>();
            ConstraintCategories = new List<SelectListItem>();
            ChallengeCategories = new List<SelectListItem>();
            Constraints = new List<Constraint>();
            Challenges = new List<Challenge>();
            
        }

        //provoser or reviewer
        public bool IsReviewer { get; set; }
        public bool IsBeingReviewed { get; set; }
        public bool IsRejected { get; set; }

        public int Id { get; set; }
        public int BusinessPostureId { get; set; }
        public bool IsApproved { get; set; }
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


        //for entry Constraint / challenge
        public IList<Environmental> Relation { get; set; }
        public string Definition { get; set; }
        public string Type { get; set; }
        public int Category { get; set; }
        public List<SelectListItem> Types { get; set; }
        public List<SelectListItem> ConstraintCategories { get; set; }
        public List<SelectListItem> ChallengeCategories { get; set; }


        public class UltimateObjective
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }

        public class Environmental
        {
            public int Id { get; set; }
            public string Desc { get; set;  }
        }

        public class CreateViewModel
        {
            public int Id { get; set; }
            public int EsId { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
        }

        public class DeleteViewModel
        {
            public int Id { get; set; }
        }

        public class CreateEnvironmentalViewModel
        {
            public int Id { get; set; }
            public int EsId { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
        }

        public class Constraint
        {
            public int Id { get; set; }
            public IList<Environmental> Relation { get; set; }
            public string Definition { get; set; }
            public string Type { get; set; }
            public int CategoryId { get; set; }
            public string Category { get; set; }
            public int EnviId { get; set; }
            public int[] RelationIds { get; set; }

            public int[] ThreatIds { get; set; }
            public int[] OpportunityIds { get; set; }
            public int[] WeaknessIds { get; set; }
            public int[] StrengthIds { get; set; }


            public string RelationIdsString 
            {
                get { return string.Join(",", this.RelationIds); }
            }
            public string ThreatIdString
            {
                get { return string.Join(",", this.ThreatIds); }
            }
            public string OpportunityIdString
            {
                get { return string.Join(",", this.OpportunityIds); }
            }
            public string WeaknessIdString
            {
                get { return string.Join(",", this.WeaknessIds); }
            }
            public string StrengthIdString
            {
                get { return string.Join(",", this.StrengthIds); }
            }

               
        }

        public class Challenge
        {
            public int Id { get; set; }
            public IList<Environmental> Relation { get; set; }
            public string Definition { get; set; }
            public string Type { get; set; }
            public int CategoryId { get; set; }
            public string Category { get; set; }
            public int EnviId { get; set; }
            public int[] RelationIds { get; set; }

            public int[] ThreatIds { get; set; }
            public int[] OpportunityIds { get; set; }
            public int[] WeaknessIds { get; set; }
            public int[] StrengthIds { get; set; }

            public string RelationIdsString
            {
                get { return string.Join(",", this.RelationIds); }
            }
            public string ThreatIdString
            {
                get { return string.Join(",", this.ThreatIds); }
            }
            public string OpportunityIdString
            {
                get { return string.Join(",", this.OpportunityIds); }
            }
            public string WeaknessIdString
            {
                get { return string.Join(",", this.WeaknessIds); }
            }
            public string StrengthIdString
            {
                get { return string.Join(",", this.StrengthIds); }
            }


        }       

    }


    public class GetConstraintViewModel
    {
        public GetConstraintViewModel()
        {
            Relations = new List<Environmental>();
        }

        public int Id { get; set; }
        public List<Environmental> Relations { get; set; }
        public List<Environmental> ThreatIds { get; set; }
        public List<Environmental> Opportunitys { get; set; }
        public List<Environmental> WeaknessIds { get; set; }
        public List<Environmental> StrengthIds { get; set; }

        public class Environmental
        {
            public int Id { get; set; }
            public string Desc { get; set; }

        }
    }


    public class GetChallengeViewModel
    {
        public GetChallengeViewModel()
        {
            Relations = new List<Environmental>();
        }
        public int Id { get; set; }
        public List<Environmental> Relations { get; set; }
        public List<Environmental> ThreatIds { get; set; }
        public List<Environmental> Opportunitys { get; set; }
        public List<Environmental> WeaknessIds { get; set; }
        public List<Environmental> StrengthIds { get; set; }


        public class Environmental
        {
            public int Id { get; set; }
            public string Desc { get; set; }
        }
    }
}