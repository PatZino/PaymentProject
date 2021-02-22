using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentAPI.Common;
using PaymentAPI.Models;
using PaymentAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentAPI.Interfaces;
using PaymentAPI.Services;

namespace PaymentAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient(typeof(ICheapPaymentGateway), typeof(CheapPaymentGatewayService));
            services.AddTransient(typeof(IExpensivePaymentGateway), typeof(ExpensivePaymentGatewayService));
            services.AddTransient(typeof(IPremiumPaymentService), typeof(PremiumPaymentService));
            services.AddTransient(typeof(IPaymentGateway), typeof(PaymentGatewayService));
            services.AddTransient(typeof(IPaymentRequestModel), typeof(PaymentRequestModelService));
            services.AddTransient(typeof(IRequestRepository), typeof(RequestRepository));
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
