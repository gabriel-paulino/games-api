using Games.Domain.Entities.Base;
using System.Collections.Generic;

namespace Games.Application.ViewModel.Generic.Output
{
    public class EntityNotificationOutput<T> where T : BaseEntity
    {
        public EntityNotificationOutput(T entity)
        {
            Errors = entity.GetNotifications();
        }

        public IEnumerable<string> Errors { get; private set; }
    }
}