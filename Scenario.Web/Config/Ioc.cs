using Microsoft.EntityFrameworkCore;
using Scenario.Data.EFCore;
using Scenario.Data.EFCore.Repositories;
using Scenario.Data.EFCore.UoW;
using Scenario.Domain.Interfaces;
using Scenario.Domain.Interfaces.Repository;
using Scenario.Web.Logger;
using System.Configuration;
using System;
using Microsoft.AspNetCore.Identity;

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
            //// Đăng ký AppDbContext
            //services.AddDbContext<DataContext>(options => {
            //    // Đọc chuỗi kết nối
            //    string connectstring = Configuration.GetConnectionString("AppDbContext");
            //    // Sử dụng MS SQL Server
            //    options.UseSqlServer(connectstring);
            //});
            services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<DataContext>()
        .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Login/NotFound");

        }
    }
}
