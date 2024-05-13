using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BirdFood.Infrastructure.Authentication
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public string RoleName { get; set; }
        public string ActionValue { get; set; }
        public CustomAuthorizeAttribute(string roleName, string actionValue) : base(typeof(CustomAuthorizeAttribute))
        {
            RoleName = roleName;
            ActionValue = actionValue;
            Arguments = new object[] { RoleName, ActionValue };
        }
    }
    public class AuthorizeFilter : IAuthorizationFilter
    {
        public AuthorizeFilter(string rolename, string actionValue)
        {
            RoleName = rolename;
            ActionValue = actionValue;
        }

        public string RoleName { get; set; }
        public string ActionValue { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!CanAccessToAction(context.HttpContext))
                context.Result = new ForbidResult();
        }
        private bool CanAccessToAction(HttpContext httpContext)
        {
            var role = 1;
            if (role.Equals(RoleName))
                return true;
            return false;
        }
    }
}
