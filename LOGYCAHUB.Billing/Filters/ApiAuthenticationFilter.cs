namespace LOGYCAHUB.Billing.Filters
{
    using System.Threading;
    using System.Web;
    using System.Web.Http.Controllers;
    using LOGYCAHUB.Billing.BLL.Login;

    /// <summary>
    /// Custom Authentication Filter Extending basic Authentication
    /// </summary>
    public class ApiAuthenticationFilter : GenericAuthenticationFilter
    {
        /// <summary>
        /// Default Authentication Constructor
        /// </summary>
        public ApiAuthenticationFilter()
        {
        }

        /// <summary>
        /// AuthenticationFilter constructor with isActive parameter
        /// </summary>
        /// <param name="isActive">isActive</param>
        public ApiAuthenticationFilter(bool isActive)
            : base(isActive)
        {
        }

        /// <summary>
        /// Protected overriden method for authorizing user
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <param name="actionContext">actionContext</param>
        /// <returns>bool</returns>
        protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            var provider = actionContext.ControllerContext.Configuration
                               .DependencyResolver.GetService(typeof(IUserApiServices)) as IUserApiServices;
            if (provider != null)
            {
                var userId = provider.Authenticate(username, password);
                if (userId > 0)
                {
                    var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    if (basicAuthenticationIdentity != null)
                    {
                        basicAuthenticationIdentity.UserId = userId;
                    }

                    return true;
                }
            }

            return false;
        }
    }
}