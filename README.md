# Claim Validation Service API

### Overview
This project is a small API for validating healthcare claims and generating summary reports.

### Author
**Carlos Andres Cortes**  
Contact: contact@carlosandrescortes.com

### Requirements
- .NET 8 SDK or higher
- NUnit for tests

### How to Run
```bash
dotnet build
dotnet run
```
The API will be available at: http://localhost:5000/claims/validate

### How to Test
```bash
dotnet test
```

### Example Request
```bash
curl -X POST http://localhost:5000/claims/validate \
-H "Content-Type: application/json" \
-d '[{"Id":1,"Provider":"Clinic A","Amount":100.5,"DiagnosisCode":"A01","Status":"Approved"}]'
```

### Example Response
```bash
{
  "totalClaims": 1,
  "validClaims": 1,
  "invalidClaims": 0,
  "totalApprovedAmount": 100.5
}
```
