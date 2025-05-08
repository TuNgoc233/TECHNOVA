using System;
using System.Collections.Generic;

namespace TECHNOVA.Data;

public partial class Order
{
    public int OrderId { get; set; }

    public string CustomerId { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public DateTime? EstimatedDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string? FullName { get; set; }

    public string Address { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public string ShippingMethod { get; set; } = null!;

    public double ShippingFee { get; set; }

    public int OrderStatus { get; set; }

    public string? EmployeeId { get; set; }

    public string? Notes { get; set; }

    public int? DiscountId { get; set; }

    public string? Phone { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Discount? Discount { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual OrderStatus OrderStatusNavigation { get; set; } = null!;
}
