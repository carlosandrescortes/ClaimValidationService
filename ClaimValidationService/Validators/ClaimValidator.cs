using ClaimValidationService.Models;

namespace ClaimValidationService.Validators
{
    public class ClaimValidator : IClaimValidator
    {
        public Summary Validate(IEnumerable<Claim> claims)
        {
            var totalClaims = claims.Count();
            var validClaims = claims.Where(x => IsValidClaim(x) == true);
            var validClaimsCount = validClaims.Count();
            var invalidClaimsCount = totalClaims - validClaimsCount;
            var totalApprovedAmount = validClaims
                .Where(x => x.Status == Status.Approved)
                .Sum(x => x.Amount);

            var summary = new Summary
            {
                TotalClaims = totalClaims,
                ValidClaims = validClaimsCount,
                InvalidClaims = invalidClaimsCount,
                TotalApprovedAmount = totalApprovedAmount
            };
            return summary;
        }

        private bool IsValidClaim(Claim claim)
        {
            if (claim.Amount <= 0)
            {
                return false;
            }

            if (!IsValidDiagnosisCode(claim.DiagnosisCode))
            {
                return false;
            }

            return true;
        }

        private bool IsValidDiagnosisCode(string code)
        {
            var regExp = @"^[A-Za-z]\d{2,4}$";
            var result = System.Text.RegularExpressions.Regex.IsMatch(code, regExp);
            return result;
        }
    }
}
