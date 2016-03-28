

using DSLNG.PEAR.Services.Requests.MirConfiguration;
using DSLNG.PEAR.Services.Responses.MirConfiguration;
namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IMirConfigurationService
    {
        GetsMirConfigurationsResponse Gets(GetMirConfigurationsRequest request);
        SaveMirConfigurationResponse Save(SaveMirConfigurationRequest request);
        GetMirConfigurationsResponse Get(int id);
    }
}
