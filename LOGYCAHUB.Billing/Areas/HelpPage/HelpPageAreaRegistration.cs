using System.Web.Http;
using System.Web.Mvc;

namespace LOGYCAHUB.Billing.Areas.HelpPage
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        /// <inheritdoc/>
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        /// <inheritdoc/>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default",
                "Help/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}