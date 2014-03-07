using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.APIInterception
{
    public class LoggingCallHandlerAttribute : HandlerAttribute

    {
        private readonly int order;

        public LoggingCallHandlerAttribute(int order)
        {
            this.order = order;
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new LoggingCallHandler() { Order = order };
        }
    }
}