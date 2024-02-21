using System;
using System.Collections.Generic;

namespace SessionTwoPartTWo.DataBaseFolder;

public partial class Type
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TerapeuticDiagnostic> TerapeuticDiagnostics { get; set; } = new List<TerapeuticDiagnostic>();
}
