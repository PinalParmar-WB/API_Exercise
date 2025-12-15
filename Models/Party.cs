using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_Exercise.Models;

public partial class Party
{
    public int PartyId { get; set; }

    public string PartyName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
