using System;
using System.Collections.Generic;

namespace SessionTwoPartTWo.DataBaseFolder;

public partial class TerapeuticDiagnostic
{
    public int Id { get; set; }

    public int MedicCardCode { get; set; }

    public DateTime? StartDate { get; set; }

    public int DoctorId { get; set; }

    public int? TypeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Result { get; set; }

    public string? Recommendations { get; set; }

    public decimal? Cost { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient MedicCardCodeNavigation { get; set; } = null!;

    public virtual Type? Type { get; set; }
}
