using Autofac;
using Autofac.Extras.DynamicProxy;
using ConsoleAbstractions;
using ConsoleAbstractions.Autofac.Interception;
using CoreLeaf.Configuration;
using CoreLeaf.Encryption;
using CoreLeaf.NissanApi.Countries;
using CoreLeaf.NissanApi.Initial;
using CoreLeaf.NissanApi.Login;
using LeafStandard.NissanApi.Status;
using Microsoft.Extensions.Configuration;
using RestAbstractions;
using RestAbstractions.Json;
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
                            .AddJsonFile("appSettings.json")
                            .AddJsonFile("appSecrets.json");

            var config = configBuilder.Build();
            builder.RegisterInstance(config).As<IConfiguration>();

            //build the app
            builder.RegisterType<App>();

            //crypto
            builder.RegisterType<Blowfish>().As<IBlowfish>();

            //register the console types
            builder.RegisterType<ConsoleHelper>().As<IConsole>().SingleInstance();

            builder.RegisterType<ConsoleInterceptor>();

            //register the REST types
            builder.RegisterType<HttpClientHandler>().As<HttpMessageHandler>();
            builder.RegisterType<RestClient>().As<IRestClient>()
                .WithParameter("baseUri", new Uri(config[ConfigurationKeys.NissanBaseUri]))
                .WithParameter("timeout", TimeSpan.Parse(config[ConfigurationKeys.DefaultHttpTimeout]));

            builder.RegisterType<FormUrlContentEncoder>().As<IContentEncoder>().SingleInstance();
            builder.RegisterType<JsonResponseDeserializer>().As<IResponseDeserializer>().SingleInstance();

            //register the country client
            builder.RegisterType<CountryClient>().As<ICountryClient>()
                .WithParameter("countryRoute", config[ConfigurationKeys.CountryRoute])
                .WithParameter("apiKey", config[ConfigurationKeys.NissanApiKey])
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(ConsoleInterceptor));

            //register the initial app client
            builder.RegisterType<InitialAppClient>().As<IInitialAppClient>()
                .WithParameter("initialAppRoute", config[ConfigurationKeys.InitialAppRoute])
                .WithParameter("apiKey", config[ConfigurationKeys.NissanApiKey])
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(ConsoleInterceptor));

            //register login client
            builder.RegisterType<LoginClient>().As<ILoginClient>()
                .WithParameter("loginRoute", config[ConfigurationKeys.LoginRoute])
                .WithParameter("apiKey", config[ConfigurationKeys.NissanApiKey])
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(ConsoleInterceptor));

            //register battery status client
            builder.RegisterType<BatteryStatusClient>().As<IBatteryStatusClient>()
                .WithParameter("batteryStatusRoute", config[ConfigurationKeys.BatteryStatusRoute])
                .WithParameter("apiKey", config[ConfigurationKeys.NissanApiKey])
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
            catch (AggregateException ex)
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
