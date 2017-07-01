using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solution.Base.Implementation.Model;
using System.Security.Principal;
using Solution.Base.Interfaces.Persistance;
using Microsoft.AspNet.Identity;
using Solution.Base.Interfaces.Model;
using System.Web;

namespace Solution.Base.Infrastructure
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IIdentity _identity;
        private readonly IDbContextFactory _dbContextFactory;
        private readonly HttpContextBase _httpContext;
        private User _user;

        public CurrentUser(IIdentity identity, IDbContextFactory dbContextFactory, HttpContextBase httpContext)
        {
            _identity = identity;
            _dbContextFactory = dbContextFactory;
        }

        public bool IsLoggedIn()
        {
            return _identity.IsAuthenticated;
        }

        public void LogOut()
        {
            _httpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public User User
        {
            get
            {
                if (_user is null)
                {
                    using (var dbContext = _dbContextFactory.CreateDefault())
                    {
                        _user = dbContext.Users.Find(_identity.GetUserId());
                    }
                }
                return _user;
            }
        }
    }
}
