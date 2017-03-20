using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLeaf.Interception
{
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
