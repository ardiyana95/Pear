

using DSLNG.PEAR.Services.Requests.MidtermFormulation;
using DSLNG.PEAR.Services.Responses;
using DSLNG.PEAR.Services.Responses.MidtermFormulation;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IMidtermFormulationService
    {
        GetMidtermFormulationResponse Get(int id);
        GetMidtermFormulationResponse GetStagesByPbId(int id);
        AddStageResponse SaveStage(AddStageRequest request);
        AddDefinitionResponse AddDefinition(AddDefinitionRequest request);
        BaseResponse DeleteStage(int id);
        BaseResponse DeleteStageDesc(int id);
        BaseResponse DeleteStageKey(int id);
        SubmitMidtermFormulationResponse SubmitMidtermFormulation(int id);
    }
}
