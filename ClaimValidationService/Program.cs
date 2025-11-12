using ClaimValidationService.Models;
using ClaimValidationService.Validators;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IClaimValidator, ClaimValidator>();
var app = builder.Build();

app.MapGet("/claims/validate", (IClaimValidator claimValidator, IEnumerable < Claim> claims) =>
{
    var summary = claimValidator.Validate(claims);
    return Results.Ok(summary);
});

app.Run();