using DAL.DBHelper;
using DAL.IRepository;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RealtorAPI.IServices;
using RealtorAPI.Services;
using RealtorAPI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtorAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://example.com",
                                                          "http://www.contoso.com")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowAnyOrigin();
                                  });
            });

            services.AddSwaggerGen();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("MyCorsPolicy",
            //        builder =>
            //        {
            //            builder.WithOrigins("https://*.yellow-chips.com", "http://*.yellow-chips.com", "https://quest.yellow-chips.com", "http://quest.yellow-chips.com", "http://*.harisons.com", "http://po.harisons.com", "http://localhost:60991/", "http://localhost:60991/surrey.html", "*");
            //            builder.SetIsOriginAllowedToAllowWildcardSubdomains();
            //            builder.AllowAnyOrigin();
            //            builder.AllowAnyMethod();
            //            builder.AllowCredentials();
            //            builder.AllowAnyHeader();
            //            builder.Build();
            //        });

            //});

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<IISServerOptions>(option =>
            {
                option.AutomaticAuthentication = false;
            });

            services.AddMvc().AddNewtonsoftJson();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddCors();
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            // .AddJwtBearer(x =>
            // {
            //     x.RequireHttpsMetadata = false;
            //     x.SaveToken = true;
            //     x.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuerSigningKey = true,
            //         IssuerSigningKey = new SymmetricSecurityKey(key),
            //         ValidateIssuer = false,
            //         ValidateAudience = false
            //     };
            // });

            //new
            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(builder =>
            //    {
            //        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
            //    });
            //    options.AddPolicy("MyCorsPolicy",
            //        builder =>
            //        {
            //            builder.WithOrigins("https://*.yellow-chips.com", "http://*.yellow-chips.com", "https://quest.yellow-chips.com", "http://quest.yellow-chips.com", "http://*.harisons.com", "http://po.harisons.com", "http://localhost/*",
            //                             "https://localhost/*", "*");
            //            builder.SetIsOriginAllowedToAllowWildcardSubdomains();
            //            builder.AllowAnyOrigin();
            //            builder.AllowAnyMethod();
            //            builder.AllowCredentials();
            //            builder.AllowAnyHeader();
            //            builder.Build();
            //        });

            //});

            //services.AddCors();

            services.AddControllers();
            services.AddTransient<ISqlHelper, SqlHelper>();

            services.AddTransient<IRealtorService, RealtorService>();
            services.AddTransient<IRapidApiService, RapidApiService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IFeaturedListingService, FeaturedListingService>();
            services.AddTransient<INewsLetterService, NewsLetterService>();
            services.AddTransient<ISavedListingService, SavedListingService>();
            services.AddTransient<IVisitorService, VisitorService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IAgentService, AgentService>();
            services.AddTransient<ICacheService, CacheService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IRapidApiRepository, RapidApiRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IFeaturedListingRepository, FeaturedListingRepository>();
            services.AddTransient<INewsLetterRepository, NewsLetterRepository>();
            services.AddTransient<ISavedListingRepository, SavedListingRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IVisitorRepository,VisitorRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IAgentRepository, AgentRepository>();
            services.AddTransient<IUserRepository, UserRepository>();         

            var connSettingsSection = Configuration.GetSection("ConnectionSettings");
            services.Configure<ConnectionSettings>(connSettingsSection);

            var connSettings = connSettingsSection.Get<ConnectionSettings>();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });


            var rapidApiUrlsSection = Configuration.GetSection("RapidApiUrls");
            services.Configure<RapidApiUrls>(rapidApiUrlsSection);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Realotr API v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
