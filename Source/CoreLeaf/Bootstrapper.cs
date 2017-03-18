using Autofac;
using CoreLeaf.Console;
using CoreLeaf.Encryption;
using System;
using System.Diagnostics;

namespace CoreLeaf
{
    public class Bootstrapper
    {
        private IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<App>();

            builder.RegisterType<Blowfish>().As<IBlowfish>();

            builder.RegisterType<ConsoleHelper>().As<IConsole>().SingleInstance();
            builder.RegisterType<CursorPreserver>().As<ICursorPreserver>();
            builder.RegisterType<ColorPreserver>().As<IColorPreserver>();

            return builder.Build();
        }

        public void Run(string[] args)
        {
            var container = ConfigureContainer();
            
            var console = container.Resolve<IConsole>();
            var cancelToken = console.GetCancelToken();

            var app = container.Resolve<App>();
            
            try
            {
                app.Run(args).Wait(cancelToken);

            }catch(OperationCanceledException ex)
            {
                //do nothing, the user intentionally cancelled
                console.WriteLine(string.Empty);
                console.WriteLine("Exiting application - user intentionally cancelled", ConsoleColor.Yellow);
            }
            catch (Exception ex)
            {
                console.WriteLine(string.Empty);
                console.WriteLine(ex.Message, ConsoleColor.Red);
            }
            finally
            {
                if (Debugger.IsAttached)
                {
                    //allow the dev to see what happend before completing and closing the window
                    console.ReadLine();
                }
            }
        }
    }
}
