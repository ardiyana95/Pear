
using System;
namespace DSLNG.PEAR.Web.ViewModels.MidtermFormulation
{
    public class MidtermPhaseStageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int MidtermFormulationId { get; set; }
    }
}