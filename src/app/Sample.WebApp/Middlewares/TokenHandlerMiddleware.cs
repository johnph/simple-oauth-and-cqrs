namespace Sample.WebApp.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Sample.WebApp.Helpers;
    using System;
    using System.Threading.Tasks;

    public class TokenHandlerMiddleware : IMiddleware
    {
        private readonly ISession _session;
        private readonly ISecurityTokenHelper _securityTokenHelper;

        public TokenHandlerMiddleware(ISession session, ISecurityTokenHelper securityTokenHelper)
        {
            _session = session;
            _securityTokenHelper = securityTokenHelper;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                if(context.User.Identity.IsAuthenticated)
                {
                    var sessionAuthToken = _session.GetString($"{_session.Id}-authtoken");

                    if(sessionAuthToken == null)
                    {
                        var newSecurityToken = await _securityTokenHelper.GetAccessToken();
                        _session.SetString($"{_session.Id}-authtoken", newSecurityToken);
                    }
                }

                await next(context);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
