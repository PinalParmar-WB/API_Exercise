using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_Exercise.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal ProductRate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
