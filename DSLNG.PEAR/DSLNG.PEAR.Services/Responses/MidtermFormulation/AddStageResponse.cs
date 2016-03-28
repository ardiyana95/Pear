

namespace DSLNG.PEAR.Services.Responses.MidtermFormulation
{
    public class AddStageResponse : BaseResponse
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}
