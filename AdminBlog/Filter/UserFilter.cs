using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdminBlog.Filter
{
    public class UserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            int? userId = context.HttpContext.Session.GetInt32("id");

            if (!userId.HasValue)
            {
                var currentAction = context.RouteData.Values["action"]?.ToString();
                var currentController = context.RouteData.Values["controller"]?.ToString();

               
                if (currentController == "Home" && (currentAction == "Index" || currentAction == "Register"))
                {
                    return;
                }

                // Kullanıcı giriş yapmamışsa Index sayfasına yönlendir
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Index" }  
                });
            }
        }
    }
}
