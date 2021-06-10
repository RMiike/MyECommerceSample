using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace MECS.WebAPI.Core.User
{
    public class AspNetUser : IAspNetUser
    {

        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;
        public Guid GetUserId()
              => IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        public IEnumerable<Claim> GetClaims()
            => _accessor.HttpContext.User.Claims;
        public string GetUserEmail()
            => IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
        public string GetUserToken()
                => IsAuthenticated() ? _accessor.HttpContext.User.GetUserToken() : "";
        public bool HasRole(string role)
            => _accessor.HttpContext.User.IsInRole(role);
        public bool IsAuthenticated()
            => _accessor.HttpContext.User.Identity.IsAuthenticated;
        public HttpContext GetHttpContext()
            => _accessor.HttpContext;
    }
}
