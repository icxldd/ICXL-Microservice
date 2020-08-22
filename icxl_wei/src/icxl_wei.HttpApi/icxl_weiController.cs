using icxl_wei.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace icxl_wei
{
    public abstract class icxl_weiController : AbpController
    {
        protected icxl_weiController()
        {
            LocalizationResource = typeof(icxl_weiResource);
        }
    }
}
