using System;
using System.Collections.Generic;
using System.Linq;

namespace Games.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            Valid = true;
            _notifications = new List<string>();
        }

        public Guid Id { get; protected set; }
        public bool Valid { get; private set; }
        private IList<string> _notifications;
        public IReadOnlyCollection<string> Notifications { get => _notifications.ToArray(); }
        
        public abstract void DoValidations();

        public void AddNotification(string notification)
        {
            Valid = false;
            _notifications.Add(notification);
        }

        public string GetNotification()
            => Notifications.FirstOrDefault();

        public IEnumerable<string> GetNotifications() => Notifications;
    }
}