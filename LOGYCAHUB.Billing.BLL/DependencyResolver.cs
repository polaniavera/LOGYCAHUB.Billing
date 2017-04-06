using LOGYCAHUB.Billing.BLL.Login;
using LOGYCAHUB.Billing.BLL.Token;
using LOGYCAHUB.Billing.Resolver;
using System.ComponentModel.Composition;

namespace LOGYCAHUB.Billing.BLL
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IPricatsEmpresaServices, PricatsEmpresaServices>();
            registerComponent.RegisterType<IUserApiServices, UserApiServices>();
            registerComponent.RegisterType<ITokenServices, TokenServices>();
        }
    }
}
