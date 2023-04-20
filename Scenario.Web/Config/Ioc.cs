using Scenario.Data.EFCore;
using Scenario.Data.EFCore.Repositories;
using Scenario.Data.EFCore.UoW;
using Scenario.Domain.Interfaces;
using Scenario.Domain.Interfaces.Repository;
using Scenario.Web.Logger;

namespace FinancialFusion.Config
{
    public static class Ioc
    {
        public static void AddIoc(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddScoped<DataContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<ILoggerService, LoggerService>();
        }
    }
}
