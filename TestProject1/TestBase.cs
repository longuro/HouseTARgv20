using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using HouseTARgv20.ApplicationServices.Services;
using HouseTARgv20.Core.ServiceInterface;
using HouseTARgv20.Data;
using HouseTARgv20.Core.Domain;

namespace HouseTARgv20Shop.Tests
{
    public class TestBase : IDisposable
    {
        protected IServiceProvider serviceProvider { get; }
        public virtual void SetupServices(IServiceCollection services)
        {

            services.AddScoped<IHouseService, HouseServices>();

            services.AddDbContext<HouseDbContext>(x =>
            {
                x.UseInMemoryDatabase("TEST");
                x.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        }
        protected TestBase()
        {
            var service = new ServiceCollection();
            SetupServices(service);
            serviceProvider = service.BuildServiceProvider();
        }
        protected T Svc<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public void Dispose()
        {

        }
    }
}
