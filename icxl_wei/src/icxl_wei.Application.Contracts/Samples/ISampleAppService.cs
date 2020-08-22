using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace icxl_wei.Samples
{
    public interface ISampleAppService : IApplicationService
    {
        Task<SampleDto> GetAsync();

        Task<SampleDto> GetAuthorizedAsync();
    }
}
