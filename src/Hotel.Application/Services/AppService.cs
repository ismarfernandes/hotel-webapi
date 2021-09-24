using AutoMapper;

using Hotel.Application.Interfaces;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services
{
    public abstract class AppService<TEntity> : IAppService<TEntity> where TEntity : Entity
    {
        #region Constructor
        public AppService(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        #region Fields
        protected readonly IMapper _mapper;
        #endregion

        #region Protected Methods
        public TDto MapperEntityToDto<TDto>(TEntity entity)
        {
            return _mapper.Map<TDto>(entity);
        }

        public TEntity MapperDtoToEntity<TDto>(TDto dto)
        {
            return _mapper.Map<TEntity>(dto);
        }
        #endregion
    }
}
