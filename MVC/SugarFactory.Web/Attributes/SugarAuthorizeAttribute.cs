using System.Linq;
using System.Web.Mvc;

namespace SugarFactory.Web.Attributes
{
    public class SugarAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string[] roles = this.Roles.Split(',');

            if (filterContext.HttpContext.Request.IsAuthenticated &&
                !roles.Any(s => filterContext.HttpContext.User.IsInRole(s)))
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "~/Views/Shared/SugarUnauthorized.cshtml"
                };

            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
         
        }
    }
}