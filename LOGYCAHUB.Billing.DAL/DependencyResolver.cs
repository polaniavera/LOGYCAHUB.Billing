namespace LOGYCAHUB.Billing.DAL
{
    using System.ComponentModel.Composition;
    using System.Data.Entity;
    using LOGYCAHUB.Billing.Resolver;
    using UnitOfWork;

    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}
