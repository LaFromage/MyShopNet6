using System;
using System.Collections.Generic;

namespace MyShopNet6.Entities;

public partial class Product
{
 
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    
    public double UnitPrice { get; set; }

  
    public string? Description { get; set; }

 
    public int CategoryId { get; set; }


    public string SupplierId { get; set; } = null!;

    public int Quantity { get; set; }

    public double Discount { get; set; }

    /*public virtual Category Category { get; set; } = null;

    public virtual Supplier Supplier { get; set; } = null;*/
}
