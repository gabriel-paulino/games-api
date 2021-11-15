using Flunt.Notifications;
using System;

namespace Games.Domain.Entities.Base
{
    public abstract class BaseEntity : Notifiable<Notification>
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; protected set; }
    }
}