using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EShop.Data;
using EShop.Handlers;
using EShop.Handlers.Category;
using EShop.Handlers.Item;
using EShop.Models;
using EShop.Readers;
using EShop.Services;
using EShop.Writers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration.UserSecrets;
using EShop.Handlers.Shipment;

//[assembly: UserSecretsId("aspnet-EShop-892b108f-b43f-4bb2-8bd2-1b63766996a6")]
namespace EShop
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                //builder.AddUserSecrets<Startup>();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            // Create the container builder.
            var builder = new ContainerBuilder();

            // Register dependencies, populate the services from
            // the collection, and build the container. If you want
            // to dispose of the container at the end of the app,
            // be sure to keep a reference to it as a property or field.
            builder.RegisterType<ApplicationDbContext>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Reader<>)).As(typeof(IReader<>)).InstancePerLifetimeScope();
            builder.RegisterType<CategoryReader>().As(typeof(IReader<Category>)).InstancePerLifetimeScope();
            builder.RegisterType<RoleReader>().As(typeof(IReader<IdentityRole>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Writer<>)).As(typeof(IWriter<>)).InstancePerLifetimeScope();

            builder.RegisterType<TransactionProvider>().As<ITransactionProvider>().InstancePerLifetimeScope();

            builder.RegisterType<CreateCategoryHandler>().As<ICreateCategoryHandler>().InstancePerLifetimeScope();

            
            builder.RegisterType<ItemReader>().As(typeof(IReader<Item>)).InstancePerLifetimeScope();
            builder.RegisterType<ItemReader>().As<IItemReader>().InstancePerLifetimeScope();
            
            builder.RegisterType<CreateItemHandler>().As<ICreateItemHandler>().InstancePerLifetimeScope();
            builder.RegisterType<EditItemHandler>().As<IEditItemHandler>().InstancePerLifetimeScope();

            builder.RegisterType<ShipmentReader>().As(typeof(IReader<Shipment>)).InstancePerLifetimeScope();
            builder.RegisterType<ShipmentReader>().As<IShipmentReader>().InstancePerLifetimeScope();

            builder.RegisterType<CreateShipmentHandler>().As<ICreateShipmentHandler>().InstancePerLifetimeScope();
            builder.RegisterType<EditShipmentHandler>().As<IEditShipmentHandler>().InstancePerLifetimeScope();
            builder.RegisterType<ItemListHandler>().As<IItemListHandler>().InstancePerLifetimeScope();

            builder.RegisterType<ShipmentItemReader>().As(typeof(IReader<ShipmentItem>)).InstancePerLifetimeScope();
            



            builder.Populate(services);
            ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile("Logs/myapp-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    "default",
                    "{area=Admin}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}