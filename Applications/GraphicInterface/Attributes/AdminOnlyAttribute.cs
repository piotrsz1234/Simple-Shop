using Data.Dto.Dtos;
using GraphicInterface.Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GraphicInterface.Attributes
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.Session.Get<UserDto>("User");
            if (user is null)
            {
                context.HttpContext.Response.Redirect("/User/Login");
            } else if (!user.IsAdmin)
            {
                context.HttpContext.Response.Redirect("/Home/Index");
            }
            
            base.OnActionExecuting(context);
        }
    }
}