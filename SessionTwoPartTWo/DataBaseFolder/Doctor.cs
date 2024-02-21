using System;
using System.Collections.Generic;

namespace SessionTwoPartTWo.DataBaseFolder;

public partial class Doctor
{
    public int Id { get; set; }

    public string FiratName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public virtual ICollection<TerapeuticDiagnostic> TerapeuticDiagnostics { get; set; } = new List<TerapeuticDiagnostic>();
}
