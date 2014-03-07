using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.PolicyInjection;
using Microsoft.Practices.Unity.InterceptionExtension;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.APIInterception
{
    public class LoggingCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var result = getNext().Invoke(input, getNext);
            // After invoking the method on the original target
            if (result.Exception != null)
            {
                WriteErrorLog(input,result);
            }
            else
            {
                WriteLog(input,result);
            }
            return result;
        }

        private void WriteLog(IMethodInvocation input,IMethodReturn result)
        {
            string value =  JsonConvert.SerializeObject( result.ReturnValue, Formatting.Indented,
                     new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
            LogEntry entry = new LogEntry();
            entry.Priority = 4;
            entry.Message = value;
            entry.Severity = System.Diagnostics.TraceEventType.Information;
            entry.Categories = new string[] { "General" };
            entry.ExtendedProperties = new Dictionary<string, object>() { 
            {"ResultValue", value},
            {"Exception",result.Exception},
            {"Parameter",input.Inputs}
            };
            Logger.Write(entry);
        }

        private void WriteErrorLog(IMethodInvocation input, IMethodReturn result)
        {
            LogEntry entry = new LogEntry();
            entry.Priority = 1;
            entry.Message = "Error";
            entry.Severity = System.Diagnostics.TraceEventType.Error;
            entry.Categories = new string[] { "Error" };
            entry.ExtendedProperties = new Dictionary<string, object>() { 
            {"Exception",result.Exception},
            {"Parameter",input.Inputs}
            };
            Logger.Write(entry);
        }

        public int Order
        {
            get;
            set;
        }
    }
}