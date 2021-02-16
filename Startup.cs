using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetcabulary.Models;
using System.Text.Json;
using System.IO;

namespace Tweetcabulary
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
            services.AddControllersWithViews();
            services.AddSingleton<ITwitAPI, TwitAPI>();
            services.AddSingleton<ISpellCheck, SpellCheck>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ITwitAPI twitService, ISpellCheck spellService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //Get auth keys
            string keyFile = File.ReadAllText(env.ContentRootPath + "\\APIKeys.json");
            JsonDocument keys = JsonDocument.Parse(keyFile);
            JsonElement root = keys.RootElement;

            string APIKey = root.GetProperty("APIKey").ToString();
            string APISecret = root.GetProperty("APISecret").ToString();
            string bearer = root.GetProperty("Bearer").ToString();

            twitService.Authenticate(APIKey, APISecret, bearer);
            spellService.loadWords(env.ContentRootPath + "\\English.dic");
        }
    }
}
