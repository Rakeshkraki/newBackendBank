using System;
using System.Collections.Generic;

namespace newBackendBank.Models;

public partial class UserDetail
{
    public int UserDetailsId { get; set; }

    public int? SelectedBranch { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public string OccupationType { get; set; } = null!;

    public decimal AnnualIncome { get; set; }

    public string EducationalQualification { get; set; } = null!;

    public string Pan { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string Aadhar { get; set; } = null!;

    public string? VoterId { get; set; }

    public string? DrivingLicense { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Pin { get; set; } = null!;

    public string CountryName { get; set; } = null!;

    public byte[] Photo { get; set; } = null!;

    public string Place { get; set; } = null!;

    public DateOnly DeclarationDate { get; set; }

    public byte[] Signature { get; set; } = null!;

    public string? ProfilePhoto { get; set; }

    public string? SignaturePhoto { get; set; }

    public virtual GeneratedDetail? GeneratedDetail { get; set; }
}
