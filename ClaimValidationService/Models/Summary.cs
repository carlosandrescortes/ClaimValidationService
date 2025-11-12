namespace ClaimValidationService.Models
{
    public class Summary
    {
        public int TotalClaims { get; set; }
        public int ValidClaims { get; set; }
        public int InvalidClaims { get; set; }
        public decimal TotalApprovedAmount { get; set; }
    }
}
