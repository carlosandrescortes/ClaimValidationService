namespace ClaimValidationService.Models
{
    public class Claim
    {
        public int ID { get; set; }
        public string Provider { get; set; }
        public decimal Amount { get; set; }
        public string DiagnosisCode { get; set; }
        public Status Status { get; set; }
    }
}
