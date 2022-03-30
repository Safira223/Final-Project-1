namespace PaymentAPI.Models
{
    public class PaymentData
    {
        public int Id { get; set; }
        public string cardOwnerName { get; set; }
        public string expirationDate { get; set; }
        public string securityCode { get; set; }
    }
}