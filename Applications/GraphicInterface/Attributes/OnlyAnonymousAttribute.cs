using Data.Dto.Dtos;
using GraphicInterface.Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GraphicInterface.Attributes
{
    public class OnlyAnonymousAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.Get<UserDto>("User") != null) {
                context.HttpContext.Response.Redirect("/Home/Index");
            }
            base.OnActionExecuting(context);
        }
    }
}