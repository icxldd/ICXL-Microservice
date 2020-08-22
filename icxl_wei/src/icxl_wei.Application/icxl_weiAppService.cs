using icxl_wei.Localization;
using Volo.Abp.Application.Services;

namespace icxl_wei
{
    public abstract class icxl_weiAppService : ApplicationService
    {
        protected icxl_weiAppService()
        {
            LocalizationResource = typeof(icxl_weiResource);
            ObjectMapperContext = typeof(icxl_weiApplicationModule);
        }
    }
}
