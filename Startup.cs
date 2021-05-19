using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoWebApiCore.IMongoDbRepo;
using MongoWebApiCore.Iservices;
using MongoWebApiCore.MongoDbRepo;
using MongoWebApiCore.MongoDbRepository;
using MongoWebApiCore.Services;
using MongoWebApiCore.Settings;

namespace MongoWebApiCore
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
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));            
            services.AddSingleton<IMongoClient>(serviceProvioder => {
                var settings = Configuration.GetSection(nameof(MongoDbSettings))
                               .Get<MongoDbSettings>();
            return new MongoClient(settings.ConnectionString);
            });
            services.AddSingleton<MongoDBContext>();
            services.AddScoped(typeof(IMongoDbrepository<>),typeof(MongoDbrepository<>));
            services.AddScoped<IProduct,ProductService>();
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
