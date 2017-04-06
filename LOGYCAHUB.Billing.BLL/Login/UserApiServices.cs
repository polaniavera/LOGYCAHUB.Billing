using LOGYCAHUB.Billing.DAL.UnitOfWork;

namespace LOGYCAHUB.Billing.BLL.Login
{
    /// <summary>
    /// Offers services for user specific operations
    /// </summary>
    public class UserApiServices : IUserApiServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public UserApiServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Public method to authenticate user by user name and password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Authenticate(string userName, string password)
        {
            var user = _unitOfWork.UserApiRepository.Get(u => u.STR_USER == userName && u.PASSWORD_USER == password);
            if (user != null && user.ID_USER > 0)
            {
                return user.ID_USER;
            }
            return 0;
        }
    }
}
