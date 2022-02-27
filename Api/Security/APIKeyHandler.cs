using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Api.Security
{
    public class APIKeyHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiKey = request.Headers.GetValues("apiKey").FirstOrDefault();
            var token = ConfigurationManager.AppSettings["Token"];
            string company = ConfigurationManager.AppSettings["Company"];

            if (apiKey == token)
            {
                var principal = new ClaimsPrincipal(new GenericIdentity(company, "APIKey"));
                HttpContext.Current.User = principal;
            }

            return base.SendAsync(request, cancellationToken);
        }

    }
}