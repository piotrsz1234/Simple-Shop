using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Common.Bll.Services
{
    public abstract class ServiceBase
    {
        protected readonly IMapper Mapper;

        public ServiceBase(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}