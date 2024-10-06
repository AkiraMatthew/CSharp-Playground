using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace AspNetCoreIdentity.Extensions
{
    /// TODO
    /// Classes de autorizaçao, filtragem e validaçao da claim customizavel para o usuario <summary>
  
    // 1. Esta classe serve apenas como referencia para validar as claims
    public class CustomAuthorization
    {
        public static Boolean ValidarClaimsUsuario(HttpContext contexto, string claimName, string claimValue)
        { // se o user nao estiver autenticado, nem é necessario retornar a claim
            return contexto.User.Identity.IsAuthenticated && //valida se o user está autenticado
                   contexto.User.Claims.Any(claim => claim.Type == claimName && claim.Value.Contains(claimValue));
        }
    }

    // 3. Esta classe faz o filtro ser utilizado como atributo
    // Na convençao do ASP.NET: public class ClaimsAuthorize : TypeFilterAttribute
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }

    // 2. Esta classe é um filtro do ASP.NET
    public class RequisitoClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequisitoClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext contexto)
        {
            if (!contexto.HttpContext.User.Identity.IsAuthenticated) // se o user nao estiver autenticado
            {
                contexto.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Identity", page = "/Account/Login", ReturnUrl = contexto.HttpContext.Request.Path.ToString() }));
                return;
            }

            if (!CustomAuthorization.ValidarClaimsUsuario(contexto.HttpContext, _claim.Type, _claim.Value))
                    //se ele passar na autorizaçao e ele nao resultar na Claim requisitada, ele vai ter o acesso negado
                    contexto.Result = new ForbidResult();
        }
    }
}

/*namespace AspNetCoreIdentity.Extensions
{
    public class CustomAuthorization
    {
        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }

    }

    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }

    public class RequisitoClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequisitoClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Identity", page = "/Account/Login", ReturnUrl = context.HttpContext.Request.Path.ToString() }));
                return;
            }

            if (!CustomAuthorization.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
*/