using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Restaurant.Common.Behaviours;
using Restaurant.Common.Filters;
using Restaurant.Data;

namespace Restaurant.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(x => { x.Filters.Add(typeof(ApiExceptionFilter)); })
            .AddFluentValidation()
            .AddNewtonsoftJson(x =>
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(Startup));
            services.AddValidatorsFromAssemblyContaining(typeof(Startup));
            services.AddMediatR(typeof(Startup));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Restaurant";
                    document.Info.Description = "Manages Restaurant web APIs";
                };
            });

            return services;
        }

        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RestaurantDbContext>(x =>
            {
                x.UseNpgsql(configuration.GetConnectionString("DevConnection"))
                .UseSnakeCaseNamingConvention();
            });
            var idpOptions = configuration.GetSection("Idp");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(x =>
                    {
                        x.Authority = idpOptions["Authority"];
                        x.Audience = idpOptions["Audience"];

                    });

            return services;
        }
    }
}
