using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.Common.GettingBoardId
{
    public static class DependencyInjectionBoardId
    {
        public static IServiceCollection AddGettingBoardId(this IServiceCollection services, Assembly assembly)
        {
            var iType = typeof(IBoardId);
            var types = assembly.GetTypes()
                .Where(p => iType.IsAssignableFrom(p) && !p.IsInterface);
            foreach(Type type in types)
            {
                services.AddTransient(iType, type);
            }

            return services;
        }
    }
}