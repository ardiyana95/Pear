

using System;
using System.Collections.Generic;
namespace DSLNG.PEAR.Services.Responses.MidtermFormulation
{
    public class GetMidtermFormulationResponse
    {
        public GetMidtermFormulationResponse() {
            MidtermFormulationStages = new List<MidtermFormulationStage>();
        }
        public int Id { get; set; }
        public int MidtermPlanningId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public bool IsBeingReviewed { get; set; }
        public bool IsLocked { get; set; }
        public Posture ConstructionPosture { get; set; }
        public Posture OperationPosture { get; set; }
        public Posture DecommissioningPosture { get; set; }
        public IList<MidtermFormulationStage> MidtermFormulationStages { get; set; }
        public class MidtermFormulationStage {
            public MidtermFormulationStage() {
                Descriptions = new List<MidtermPhaseDescription>();
                KeyDrivers = new List<MidtermPhaseKeyDriver>();
            }
            public int Id { get; set; }
            public string Title { get; set; }
            public int Order { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public IList<MidtermPhaseDescription> Descriptions { get; set; }
            public IList<MidtermPhaseKeyDriver> KeyDrivers { get; set; }
        }
        public class MidtermPhaseDescription{
            public int Id {get;set;}
            public string Value {get;set;}
        }
        public class MidtermPhaseKeyDriver{
            public int Id {get;set;}
            public string Value {get;set;}
        }
        public class Posture
        {
            public Posture() {
                DesiredStates = new List<DesiredState>();
            }
            public int Id { get; set; }
            public IList<DesiredState> DesiredStates { get; set; }
        }
        public class DesiredState {
            public int Id { get; set; }
            public string Description { get; set; }
        }
    }
}
