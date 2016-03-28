
using System;
namespace DSLNG.PEAR.Services.Requests.MidtermFormulation
{
    public class AddStageRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MidtermFormulationId { get; set; }
    }
}
