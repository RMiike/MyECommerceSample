namespace MECS.Core.Domain.Entities
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string AuthUrl { get; set; }
        public string CatalogUrl { get; set; }
        public string CartUrl { get; set; }
        public string OrderUrl { get; set; }
        public string PaymentUrl { get; set; }
        public string PurshaseBFFUrl { get; set; }
    }
}
