using ApiTasks.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tasks.Repository.Connection;
using Tasks.Repository.Interfaces;
using Tasks.Services;
using Tasks.Services.Interfaces;

namespace Tasks
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITaskListRepository, TaskListRepository>();
            services.AddSingleton<ITaskService, TaskService>();
            services.AddSingleton<IConnection, Connection>();
            services.AddMvcCore();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    context.Response.Headers.Add("Content-Type", "text/html; charset=utf-8");
                    await context.Response.WriteAsync("Task List API");
                });

                endpoints.MapControllers();
            });
        }
    }
}
