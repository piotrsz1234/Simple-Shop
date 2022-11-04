using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Common.Bll.Services
{
    public abstract class ServiceBase
    {
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;

        public ServiceBase(ILogger logger, IMapper mapper)
        {
            Logger = logger;
            Mapper = mapper;
        }
    }
}