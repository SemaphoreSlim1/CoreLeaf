using Autofac;
using CoreLeaf.Console;
using CoreLeaf.Encryption;
using CoreLeaf.Net;
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

            builder.RegisterType<App>();

            builder.RegisterType<Blowfish>().As<IBlowfish>();

            //register the console types
            builder.RegisterType<ConsoleHelper>().As<IConsole>().SingleInstance();
            builder.RegisterType<CursorPreserver>().As<ICursorPreserver>();
            builder.RegisterType<ColorPreserver>().As<IColorPreserver>();

            //register the REST types
            //register the rest types
            builder.RegisterType<HttpClientHandler>().As<HttpMessageHandler>();
            builder.RegisterType<RestClient>().As<IRestClient>();
            builder.RegisterType<DefaultHeaderProvider>().As<IHeaderProvider>().SingleInstance();
            builder.RegisterType<FormUrlContentEncoder>().As<IContentEncoder>().SingleInstance();
            builder.RegisterType<JsonResponseDeserializer>().As<IResponseDeserializer>().SingleInstance();

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
