using Games.Domain.Entities.Base;

namespace Games.Application.ViewModel.Generic.Output
{
    public class NotFoundOutput<T> where T : BaseEntity
    {
        public NotFoundOutput(T entity)
        {
            Message = $"Não existe este {entity.GetType().Name}";
        }

        public string Message { get; private set; }
    }
}
