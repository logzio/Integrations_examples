
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTracing;
using OpenTracing.Util;
 

namespace TodoApi
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
            services.AddRazorPages();
            services.AddOpenTracing();

            services.AddSingleton<ITracer>(serviceProvider =>
            {
                string serviceName = serviceProvider.GetRequiredService<IWebHostEnvironment>().ApplicationName;
                string hostName = "localhost";
                var tracer =
                        new Tracer.Builder(serviceName)
                            .WithSampler(new ConstSampler(true))
                            .WithReporter(new RemoteReporter.Builder().WithSender(
                                new UdpSender(hostName, 6831, 0)).Build()
                            )
                    .Build();
                    GlobalTracer.Register(tracer);
                    return tracer;
                }
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
