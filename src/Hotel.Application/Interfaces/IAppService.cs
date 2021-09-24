using Hotel.Domain.Entities;

namespace Hotel.Application.Interfaces
{
    public interface IAppService<TEntity> where TEntity : Entity
    {
    }
}
