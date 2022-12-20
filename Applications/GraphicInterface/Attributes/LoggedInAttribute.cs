using Data.Dto.Dtos;
using GraphicInterface.Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GraphicInterface.Attributes
{
    public class LoggedInAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.Get<UserDto>("User") is null) {
                context.HttpContext.Response.Redirect("/User/Login");
            }
            base.OnActionExecuting(context);
        }
    }
}