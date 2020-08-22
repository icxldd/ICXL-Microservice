namespace icxl_wei.EntityFrameworkCore
{
    /* This class can be used as a base class for EF Core integration tests,
     * while SampleRepository_Tests uses a different approach.
     */
    public abstract class icxl_weiEntityFrameworkCoreTestBase : icxl_weiTestBase<icxl_weiEntityFrameworkCoreTestModule>
    {

    }
}