using System;
using System.Collections.Generic;

namespace TECHNOVA.Data;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public string? CustomerId { get; set; }

    public int ProductId { get; set; }

    public string? FullName { get; set; }

    public string Email { get; set; } = null!;

    public DateTime SentDate { get; set; }

    public string? Notes { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product Product { get; set; } = null!;
}
