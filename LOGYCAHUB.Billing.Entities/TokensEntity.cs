namespace LOGYCAHUB.Billing.Entities
{
    using LOGYCAHUB.Billing.DAL;

    public class TokensEntity
    {
        public int ID_TOKEN { get; set; }

        public int ID_USER { get; set; }

        public string AUTH_TOKEN { get; set; }

        public System.DateTime EMISION { get; set; }

        public System.DateTime EXPIRACION { get; set; }

        public virtual USER_API USER_API { get; set; }
    }
}
