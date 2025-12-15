using System;
using System.Collections.Generic;

namespace API_Exercise.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public DateTime InvoiceDate { get; set; }

    public int PartyId { get; set; }

    public int ProductId { get; set; }

    public decimal Quantity { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual Party Party { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
