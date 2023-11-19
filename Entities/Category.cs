using System;
using System.Collections.Generic;

namespace MyShopNet6.Entities;

public partial class Category
{
    /// <summary>
    /// Mã loại
    /// </summary>
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
