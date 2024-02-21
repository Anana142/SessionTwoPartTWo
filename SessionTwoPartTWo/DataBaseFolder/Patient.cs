using System;
using System.Collections.Generic;

namespace SessionTwoPartTWo.DataBaseFolder;

public partial class Patient
{
    public int MedicCardCode { get; set; }

    public byte[]? Photo { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string PasportSeries { get; set; } = null!;

    public string PasportNumber { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public int GenderId { get; set; }

    public string Adress { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime DateGetMedicCard { get; set; }

    public DateTime? DateLastVisit { get; set; }

    public DateTime? DateNextVisit { get; set; }

    public int? InsuransyPoliceNumber { get; set; }

    public DateTime? StopDateInsuransyPolice { get; set; }

    public string? MedicHistory { get; set; }

    public int? InsuranceCompanyId { get; set; }

    public string? WorkPlase { get; set; }

    public virtual Gender Gender { get; set; } = null!;

    public virtual Hospitalization? Hospitalization { get; set; }

    public virtual InsuranceCompany? InsuranceCompany { get; set; }

    public virtual ICollection<TerapeuticDiagnostic> TerapeuticDiagnostics { get; set; } = new List<TerapeuticDiagnostic>();
}
