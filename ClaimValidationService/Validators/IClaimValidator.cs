using ClaimValidationService.Models;

namespace ClaimValidationService.Validators
{
    public interface IClaimValidator
    {
        public Summary Validate(IEnumerable<Claim> claims);
    }
}
