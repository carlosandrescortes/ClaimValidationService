using ClaimValidationService.Models;
using ClaimValidationService.Validators;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddScoped<IClaimValidator, ClaimValidator>();

app.MapGet("/claims/validate", (IClaimValidator claimValidator, IEnumerable < Claim> claims) =>
{
    var summary = claimValidator.Validate(claims);
    return Results.Ok(summary);
});

app.Run();