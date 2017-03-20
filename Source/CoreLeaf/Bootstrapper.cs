using Autofac;
using Autofac.Extras.DynamicProxy;
using CoreLeaf.Console;
using CoreLeaf.Encryption;
using CoreLeaf.Interception;
using CoreLeaf.Net;
using CoreLeaf.NissanApi.Countries;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace CoreLeaf
{
    public class Bootstrapper
    {
        private IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            //build the configuration
            var configBuilder = new ConfigurationBuilder()
                            .AddJsonFile("appSettings.json");

            builder.RegisterInstance(configBuilder.Build()).As<IConfiguration>();

            //build the app
            builder.RegisterType<App>();

            //crypto
            builder.RegisterType<Blowfish>().As<IBlowfish>();

            //register the console types
            builder.RegisterType<ConsoleHelper>().As<IConsole>().SingleInstance();

            builder.RegisterType<ConsoleInterceptor>();

            //register the REST types
            builder.RegisterType<HttpClientHandler>().As<HttpMessageHandler>();
            builder.RegisterType<RestClient>().As<IRestClient>();
            builder.RegisterType<DefaultHeaderProvider>().As<IHeaderProvider>().SingleInstance();
            builder.RegisterType<FormUrlContentEncoder>().As<IContentEncoder>().SingleInstance();
            builder.RegisterType<JsonResponseDeserializer>().As<IResponseDeserializer>().SingleInstance();

            //register the country client
            builder.RegisterType<CountryClient>().As<ICountryClient>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(ConsoleInterceptor));
            
            return builder.Build();
        }

        public void Run(string[] args)
        {
            var container = ConfigureContainer();

            var console = container.Resolve<IConsole>();
            var cancelToken = console.CancelToken;

            var app = container.Resolve<App>();

            try
            {
                app.Run(args).Wait(cancelToken);

            }
            catch (OperationCanceledException ex)
            {
                //do nothing, the user intentionally cancelled
                console.WriteLine();
                console.WriteLine("Exiting application - user intentionally cancelled", ConsoleColor.Yellow);

            }
            catch(AggregateException ex)
            {
                console.WriteLine();
                console.WriteLine(ex.InnerException.Message, ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                console.WriteLine();
                console.WriteLine(ex.Message, ConsoleColor.Red);
            }
            finally
            {
                if (Debugger.IsAttached)
                {
                    //allow the dev to see what happend before completing and closing the window
                    console.Write("Program complete. Press enter to exit.", ConsoleColor.Green);
                    console.ReadLine();
                }
            }
        }
    }
}
