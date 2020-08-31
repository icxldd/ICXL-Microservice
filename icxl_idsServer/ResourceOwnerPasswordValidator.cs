using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
namespace icxl_idsServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public ResourceOwnerPasswordValidator()
        {
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            int a = Convert.ToInt32(DB.dao.Ado.GetScalar(string.Format("select count(1) from \"Account\" where \"Name\" ='{0}' and \"PassWord\" = '{1}'", context.UserName, context.Password)));
            if (a > 0)
            {
                string id = Convert.ToString(DB.dao.Ado.GetScalar(string.Format("select \"Id\" from \"Account\" where \"Name\" ='{0}' and \"PassWord\" = '{1}'", context.UserName, context.Password)));
                context.Result = new GrantValidationResult(
                 subject: context.UserName,
                 authenticationMethod: "custom",
                 claims: GetUserClaims(id));

            }
            else
            {
                //验证失败
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
            }
        }
        //可以根据需要设置相应的Claim
        private Claim[] GetUserClaims(string id)
        {
            return new Claim[]
            {
            new Claim("icxlID",id),
            //new Claim(JwtClaimTypes.Name,"wjk"),
            //new Claim(JwtClaimTypes.GivenName, "jaycewu"),
            //new Claim(JwtClaimTypes.FamilyName, "yyy"),
            //new Claim(JwtClaimTypes.Email, "977865769@qq.com"),
            //new Claim(JwtClaimTypes.Role,"admin")
            };
        }
    }
}