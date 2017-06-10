using System;

namespace ConsoleAbstractions.Autofac.Interception
{
    /// <summary>
    /// When intercepted, the message that should be written out before execution
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class BeforeMessageAttribute : Attribute
    {
        public string Message { get; private set; }

        public BeforeMessageAttribute(string message)
        {
            Message = message;
        }
    }
}
