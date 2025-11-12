using ClaimValidationService.Validators;
using Moq;

namespace ClaimValidationServiceTests.Unit
{
    [TestFixture]
    internal class ClaimValidatorTests
    {
        private Mock<IClaimValidator> _mockValidator;

        [SetUp]
        public void Setup()
        {
            _mockValidator = new Mock<IClaimValidator>();
        }

        [Test]
        public void Validate_WhenGivenValidClaims_ReturnsExpectedSummary()
        {
            // Arrange
            var claims = new List<ClaimValidationService.Models.Claim>
            {
                new ClaimValidationService.Models.Claim
                {
                    ID = 1,
                    Amount = 1000,
                    DiagnosisCode = "A123",
                    Status = ClaimValidationService.Models.Status.Approved
                },
                new ClaimValidationService.Models.Claim
                {
                    ID = 2,
                    Amount = -500,
                    DiagnosisCode = "B456",
                    Status = ClaimValidationService.Models.Status.Rejected
                },
                new ClaimValidationService.Models.Claim
                {
                    ID = 3,
                    Amount = 750,
                    DiagnosisCode = "C78",
                    Status = ClaimValidationService.Models.Status.Pending
                },
                new ClaimValidationService.Models.Claim
                {
                    ID = 4,
                    Amount = 0,
                    DiagnosisCode = "D910",
                    Status = ClaimValidationService.Models.Status.Approved
                }
            };

            var expectedSummary = new ClaimValidationService.Models.Summary
            {
                TotalClaims = 4,
                ValidClaims = 2,
                InvalidClaims = 2,
                TotalApprovedAmount = 1000
            };

            _mockValidator
                .Setup(x => x.Validate(claims))
                .Returns(expectedSummary);

            var validator = _mockValidator.Object;

            // Act
            var result = validator.Validate(claims);

            // Assert
            Assert.That(result.TotalClaims, Is.EqualTo(expectedSummary.TotalClaims));
            Assert.That(result.ValidClaims, Is.EqualTo(expectedSummary.ValidClaims));
            Assert.That(result.InvalidClaims, Is.EqualTo(expectedSummary.InvalidClaims));
            Assert.That(result.TotalApprovedAmount, Is.EqualTo(expectedSummary.TotalApprovedAmount));
        }
    }
}
