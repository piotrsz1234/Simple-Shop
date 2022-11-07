using Autofac;
using AutoMapper;
using Common.Bll.Helpers;
using Common.Bll.Services.Interfaces;
using Data.EF.Contexts;

namespace TextInterface
{
    internal static class AutofacConfig
    {
        private static IContainer? _currentContainer;
        
        public static IContainer GetContainer()
        {
            if (_currentContainer != null)
                return _currentContainer;
            
            var builder = new ContainerBuilder();

            foreach (var type in AutofacHelper.GetTypes()) {
                builder.RegisterType(type.Value).As(type.Key);
            }

            builder.Register<IMapper>((c, p) => MapperProfile.GetMapperConfiguration().CreateMapper());
            builder.RegisterType<ShopContext>();
            
            _currentContainer = builder.Build();
            return _currentContainer;
        }
    }
}