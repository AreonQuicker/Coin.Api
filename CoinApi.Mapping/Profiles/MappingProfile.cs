using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using CoinApi.Domain;
using CoinApi.Domain.Common.Mapping.Interfaces;

namespace CoinApi.Mapping.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(typeof(StartUp).Assembly);
            ApplyMappingsFromAssembly(typeof(Application.Core.StartUp).Assembly);
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var mappingMethod = type.GetMethod("Mapping");

                if (mappingMethod != null)
                {
                    mappingMethod?.Invoke(instance, new object[] {this});

                    return;
                }

                var mappingInterfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>));

                foreach (var mappingInterface in mappingInterfaces)
                {
                    var methodInfo = mappingInterface.GetMethod("Mapping");

                    methodInfo?.Invoke(instance, new object[] {this});
                }
            }
        }
    }
}