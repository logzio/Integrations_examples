using System;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OpenTracing;
using OpenTracing.Tag;
using OpenTracing.Util;

namespace TodoApi
{
    public class Program
    {
        private static ITracer _tracer;
        public static void Main(string[] args)
        {
            var appName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            var hostName = "localhost";
            _tracer =
                new Tracer.Builder(appName)
                    .WithSampler(new ConstSampler(true))
                    .WithReporter(new RemoteReporter.Builder().WithSender(
                            new UdpSender(hostName, 6831, 0)).Build()
                    )
                    .Build();
            GlobalTracer.Register(_tracer);

            Console.WriteLine("Calling initialization service");
            Initialize();
            CreateHostBuilder(args).Build().Run();
        }

        private static void Initialize()
        {
            using (IScope scope = _tracer.BuildSpan("Initialize").StartActive(finishSpanOnDispose: true))
            {
                try
                {
                    Console.WriteLine("Starting service");
                    System.Threading.Thread.Sleep(500);
                    Allocate();
                }
                catch (Exception ex)
                {
                    Tags.Error.Set(scope.Span, true);
                    
                }

                // No need to call scope.Span.Finish() as we've set finishSpanOnDispose:true in StartActive.
            }
            
            
        }

        public static void Allocate()
        {
            using (IScope childScope = _tracer.BuildSpan("Allocate").StartActive(finishSpanOnDispose: true))
            {
                childScope.Span.Log("Allocation memory...");

                System.Threading.Thread.Sleep(150);
                Console.WriteLine("Setting up environment");
                GC();
            }
            
        }

        public static void GC()
        {
            using (IScope childScope = _tracer.BuildSpan("setup").StartActive(finishSpanOnDispose: true))
            {
                    Console.WriteLine("Setting up service");
                    System.Threading.Thread.Sleep(350);
            }
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        
    }

}
