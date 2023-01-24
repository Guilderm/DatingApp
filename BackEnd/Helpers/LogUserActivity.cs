using BackEnd.Extensions;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BackEnd.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserId();

            var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            var user = await repo.GetUserByIdAsync(int.Parse(userId));
            user.LastActive  = DateTime.UtcNow;
            await repo.SaveAllAsync();
        }
    }
}