using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace LOGYCAHUB.Billing.Filters
{
    /// <summary>
    /// Basic Authentication identity
    /// </summary>
    public class BasicAuthenticationIdentity : GenericIdentity
    {
        /// <summary>
        /// Gets or sets get/Set for password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets get/Set for UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets get/Set for UserId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Basic Authentication Identity Constructor
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="password">password</param>
        public BasicAuthenticationIdentity(string userName, string password)
            : base(userName, "Basic")
        {
            Password = password;
            UserName = userName;
        }
    }
}