using Games.Domain.Shared.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Games.Infra.CrossCutting.Filter
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly NotificationContext _notificationContext;

        public NotificationFilter(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!_notificationContext.IsValid)
            {
                var response = context.HttpContext.Response;

                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.ContentType = "application/json";

                string notifications = JsonConvert.SerializeObject(
                    _notificationContext.Notifications.Select(s => s.Message));

                await response.WriteAsync(notifications);

                return;
            }

            await next();
        }
    }
}