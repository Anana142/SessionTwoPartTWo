using System;
using System.Collections.Generic;

namespace SessionTwoPartTWo.DataBaseFolder;

public partial class Condition
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Hospitalization> Hospitalizations { get; set; } = new List<Hospitalization>();
}
