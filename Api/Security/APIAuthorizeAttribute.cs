using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Api.Security
{
    public class APIAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var actionRoles = Roles;
            var username = HttpContext.Current.User.Identity.Name;// nameden rolu bul

            string kayitlikullanıcırol = "A"; // Dbden gelen kullanıcı rolu
            if (kayitlikullanıcırol != null && actionRoles.Contains(kayitlikullanıcırol))
            {
                //erişim var
            }
            else
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }

            base.OnAuthorization(actionContext);
        }
    }
}