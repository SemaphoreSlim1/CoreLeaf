using Castle.DynamicProxy;
using System.Linq;

namespace ConsoleAbstractions.Autofac.Interception
{
    public class ConsoleInterceptor : IInterceptor
    {
        private IConsole _console;

        public ConsoleInterceptor(IConsole console)
        {
            _console = console;
        }

        public void Intercept(IInvocation invocation)
        {
            //get the before message
            var beforeMessageAttr = invocation.Method.CustomAttributes.FirstOrDefault(attr => attr.AttributeType == typeof(BeforeMessageAttribute));
            var beforeMessage = beforeMessageAttr == null ? string.Empty : beforeMessageAttr.ConstructorArguments.FirstOrDefault().Value.ToString();

            //get the after message
            var afterMessageAttr = invocation.Method.CustomAttributes.FirstOrDefault(attr => attr.AttributeType == typeof(AfterMessageAttribute));
            var afterMessage = afterMessageAttr == null ? string.Empty : afterMessageAttr.ConstructorArguments.FirstOrDefault().Value.ToString();

            //write out the before message
            var wroteBeforeMessage = false;
            if (string.IsNullOrWhiteSpace(beforeMessage) == false)
            {
                _console.Write(beforeMessage);
                wroteBeforeMessage = true;
            }

            //do the thing that we intercepted
            invocation.Proceed();

            //clear out the before message, if there was one
            if (wroteBeforeMessage)
            { _console.ClearLine(); }

            //then write out the after message, if there is one
            if (string.IsNullOrWhiteSpace(afterMessage) == false)
            {
                _console.WriteLine(afterMessage);
            }
        }
    }
}
