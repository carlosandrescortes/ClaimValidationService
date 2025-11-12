using ClaimValidationService.Validators;

namespace ClaimValidationServiceTests.Integration
{
    [TestFixture]
    public class ClaimValidatorTests
    {
        private IClaimValidator _claimValidator;

        [SetUp]
        public void Setup()
        {

            _claimValidator = new ClaimValidator();
        }

        [Test]
        public void Validate_WhenGivenInvalidAmounts_ReturnsExpectedSummary()
        {
            // Arrange
            var claims = new List<ClaimValidationService.Models.Claim>
            {
                new ClaimValidationService.Models.Claim
                {
                    ID = 1,
                    Amount = 0,
                    DiagnosisCode = "A123",
                    Status = ClaimValidationService.Models.Status.Approved
                },
                new ClaimValidationService.Models.Claim
                {
                    ID = 2,
                    Amount = -100,
                    DiagnosisCode = "B456",
                    Status = ClaimValidationService.Models.Status.Pending
                },
                new ClaimValidationService.Models.Claim
                {
                    ID = 3,
                    Amount = -1000,
                    DiagnosisCode = "C78",
                    Status = ClaimValidationService.Models.Status.Rejected
                },
                new ClaimValidationService.Models.Claim
                {
                    ID = 4,
                    DiagnosisCode = "D90",
                    Status = ClaimValidationService.Models.Status.Approved
                }
            };

            // Act
            var summary = _claimValidator.Validate(claims);

            // Assert
            Assert.That(summary.TotalClaims, Is.EqualTo(4));
            Assert.That(summary.ValidClaims, Is.EqualTo(0));
            Assert.That(summary.InvalidClaims, Is.EqualTo(4));
            Assert.That(summary.TotalApprovedAmount, Is.EqualTo(0));
        }

        [Test]
        public void Validate_WhenGivenInvalidDiagnosisCodes_ReturnsExpectedSummary()
        {
            // Arrange
            var claims = new List<ClaimValidationService.Models.Claim>
            {
                new ClaimValidationService.Models.Claim
                {
                    ID = 1,
                    Amount = 100,
                    DiagnosisCode = "",
                    Status = ClaimValidationService.Models.Status.Approved
                },
                new ClaimValidationService.Models.Claim
                {
                    ID = 2,
                    Amount = 100,
                    DiagnosisCode = "A",
                    Status = ClaimValidationService.Models.Status.Pending
                },
                new ClaimValidationService.Models.Claim
                {
                    ID = 3,
                    Amount = 100,
                    DiagnosisCode = "A1",
                    Status = ClaimValidationService.Models.Status.Rejected
                },
                new ClaimValidationService.Models.Claim
                {
                    ID = 4,
                    Amount = 100,
                    DiagnosisCode = "A23456",
                    Status = ClaimValidationService.Models.Status.Approved
                },
                new ClaimValidationService.Models.Claim
                {
                    ID = 5,
                    Amount = 100,
                    DiagnosisCode = "INVALID",
                    Status = ClaimValidationService.Models.Status.Pending
                }
            };

            // Act
            var summary = _claimValidator.Validate(claims);

            // Assert
            Assert.That(summary.TotalClaims, Is.EqualTo(5));
            Assert.That(summary.ValidClaims, Is.EqualTo(0));
            Assert.That(summary.InvalidClaims, Is.EqualTo(5));
            Assert.That(summary.TotalApprovedAmount, Is.EqualTo(0));
        }

        [Test]
        public void Validate_WhenGivenMixedClaims_ReturnsExpectedSummary()
        {
            // Arrange
            var claims = new List<ClaimValidationService.Models.Claim>
            {
                new ClaimValidationService.Models.Claim
                {
                    ID = 1,
                    Amount = 1000,
                    DiagnosisCode = "A12",
                    Status = ClaimValidationService.Models.Status.Approved
                },
                new ClaimValidationService.Models.Claim
                {
                    ID = 2,
                    Amount = -500,
                    DiagnosisCode = "B345",
                    Status = ClaimValidationService.Models.Status.Rejected
                },
                new ClaimValidationService.Models.Claim
                {
                    ID = 3,
                    Amount = 750,
                    DiagnosisCode = "C6789",
                    Status = ClaimValidationService.Models.Status.Approved
                },
                new ClaimValidationService.Models.Claim
                {
                    ID = 4,
                    Amount = 0,
                    DiagnosisCode = "INVALID",
                    Status = ClaimValidationService.Models.Status.Pending
                }
            };

            // Act
            var summary = _claimValidator.Validate(claims);

            // Assert
            Assert.That(summary.TotalClaims, Is.EqualTo(4));
            Assert.That(summary.ValidClaims, Is.EqualTo(2));
            Assert.That(summary.InvalidClaims, Is.EqualTo(2));
            Assert.That(summary.TotalApprovedAmount, Is.EqualTo(1750));
        }
    }
}