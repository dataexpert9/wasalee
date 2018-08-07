using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;
using BLL.Interface;
using BLL.Implementation;
using AutoMapper;
using Wasalee.DTOs;
using Swashbuckle.AspNetCore.Swagger;
using BLL;
using System.Collections.Generic;
using Wasalee.Swagger;

namespace NetCoreWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetValue<string>("JwtIssuer"),
                        ValidAudience = Configuration.GetValue<string>("JwtAudience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSecretKey")))
                    };
                });




            services.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=ISB-APPS-F136; initial catalog=Wasalee;User Id=sa; Password=Axact123;integrated security=false", b => b.UseRowNumberForPaging()));

            services.AddTransient<IBOUser, BOUser>();
            services.AddTransient<IBOSettings, BLLSettings>();
            services.AddTransient<IBOStore, BOStore>();
            services.AddTransient<IBOFetch, BOFetch>();
            services.AddTransient<IBODriver, BODriver>();
            services.AddTransient<IBOCommon, BOCommon>();







            //services.AddTransient(BLL.BLLSettings) 


            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Wasalee API",
                    Description = "Wasalee API",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "N/A", Email = "N/A", Url = "N/A" }
                });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });


                c.OperationFilter<FileUploadOperation>();
                c.DescribeAllEnumsAsStrings();
            });

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Cuisine, CuisineDTO>();
                cfg.CreateMap<Store, StoreDTO>();
                cfg.CreateMap<List<Store>, List<StoreDTO>>();
                cfg.CreateMap<List<Cuisine>, List<CuisineDTO>>();
                cfg.CreateMap<Driver,DriverDTO>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              
            }

            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.DocExpansion(DocExpansion.None);
            });

            dbContext.Database.EnsureCreated();
        }

    }
}
