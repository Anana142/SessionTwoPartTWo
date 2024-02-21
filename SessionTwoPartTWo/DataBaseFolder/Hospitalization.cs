using System;
using System.Collections.Generic;

namespace SessionTwoPartTWo.DataBaseFolder;

public partial class Hospitalization
{
    public int Id { get; set; }

    public int MedicCardCode { get; set; }

    public string? Diagnoz { get; set; }

    public string Purpose { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public int ConditionsId { get; set; }

    public string? Length { get; set; }

    public string? AdditionInformation { get; set; }

    public decimal? Cost { get; set; }

    public DateTime? StartDate { get; set; }

    public string? BedNumber { get; set; }

    public string? RoomNumber { get; set; }

    public virtual Condition Conditions { get; set; } = null!;

    public virtual Department? Department { get; set; }

    public virtual Patient IdNavigation { get; set; } = null!;
}
