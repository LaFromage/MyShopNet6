using System;
using System.Collections.Generic;

namespace MyShopNet6.Entities;

public partial class Product
{
    /// <summary>
    /// Mã hàng hóa
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Tên hàng hóa
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Đơn giá
    /// </summary>
    public double UnitPrice { get; set; }

    /// <summary>
    /// Mô tả hàng hóa
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Mã loại, FK
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Mã nhà cung cấp
    /// </summary>
    public string SupplierId { get; set; } = null!;

    public int Quantity { get; set; }

    public double Discount { get; set; }

    /*public virtual Category Category { get; set; } = null;

    public virtual Supplier Supplier { get; set; } = null;*/
}
