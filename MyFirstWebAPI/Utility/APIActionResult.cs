using MyFirstWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MyFirstWebAPI.Utility
{
    public class APIActionResult : IHttpActionResult
    {
        public String resultcode {get;set;}
        public int resultvalue {get;set;}
        private APIResponseWrapper data;
        private HttpRequestMessage request;

        public APIActionResult(APIResponseWrapper d, HttpRequestMessage request)
        {
            this.data = d;
            this.request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = this.request.CreateResponse(HttpStatusCode.OK, data);
            return Task.FromResult(response);
        }
    }
}