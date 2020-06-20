using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpaceServices.Data;
using ThaiSpaceApi.Options;
using ThaiSpaceApi.Models;
using ThaiSpaceApi.Services;
using Microsoft.AspNetCore.Mvc.Versioning;
using SpaceServices.Links;
using AutoMapper;
using SpaceServices.Articles;
using SpaceServices.ArticleLinkRegistrations;
using Hangfire;
using Hangfire.MemoryStorage;

namespace ThaiSpaceApi
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
            services.AddControllers();
            services.Configure<MyDatabaseSettings>(Configuration.GetSection(nameof(MyDatabaseSettings)));
            services.AddSingleton<IMyDatabaseSettings>(s => s.GetRequiredService<IOptions<MyDatabaseSettings>>().Value);

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            });

            services.AddDbContext<SpaceIdentityDbContext>(options => options
                                                            .UseSqlServer(Configuration.GetConnectionString("SpaceDb")));
            services.AddIdentity<SpaceUser, IdentityRole>()
                .AddEntityFrameworkStores<SpaceIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireDigit = false;
                options.SignIn.RequireConfirmedEmail = true;
            });

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromSeconds(60);
            });

            services.AddScoped<IUserClaimsPrincipalFactory<SpaceUser>, SpaceUserClaimsPrincipalFactory>();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ThaiSpaceApi", Version = "1.0" });
            });
            services.AddApiVersioning(x => 
            {
                x.ReportApiVersions = true;
                //x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
                x.ApiVersionReader = new MediaTypeApiVersionReader("version");
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
            });
            
            services.AddHangfire(config => {
                config.UseMemoryStorage();
            });
            services.AddHangfireServer();

            services.AddAutoMapper(typeof(AutoMapping));

            services.AddTransient<ILinkService, LinkService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IArticleLinkRegistrationService, ArticleLinkRegistrationService>();
            services.AddSingleton<LinkStore>();
            services.AddSingleton<ArticleStore>();
            services.AddSingleton<ArticleLinkRegistrationStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger(options =>
            {
                options.RouteTemplate = swaggerOptions.JsonRoute;
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });

            app.UseHangfireDashboard();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
