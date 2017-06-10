using System;

namespace ConsoleAbstractions.Autofac.Interception
{
    /// <summary>
    /// When intercepted, the message that should be written out after execution
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AfterMessageAttribute : Attribute
    {
        public string Message { get; private set; }

        public AfterMessageAttribute(string message)
        {
            Message = message;
        }
    }
}
