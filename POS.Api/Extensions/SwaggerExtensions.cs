using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace POS.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var openApi = new OpenApiInfo
            {
                Title = "POS API",
                Version = "v1",
                Description = "Punto de venta API 2024",
                TermsOfService = new Uri("https://opensource.org/licenses"),
                Contact = new OpenApiContact
                {
                    Name = "SIR TECH S.A.C",
                    Email = "sirtech@gmail.com",
                    Url = new Uri("https://sirtech.com")
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LiCX",
                    Url = new Uri("https://opensource.org/licenses")
                }

                
            };

            services.AddSwaggerGen(x =>
            {
                openApi.Version = "v1";
                x.SwaggerDoc("v1", openApi);

                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "Jwt Authetication",
                    Description = "JWT Bearer Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                x.AddSecurityDefinition(securitySchema.Reference.Id, securitySchema);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securitySchema, new string[] {} }
                });
            });

            return services;
        }

        
    }
}
