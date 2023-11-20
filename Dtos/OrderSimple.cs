using MyShopNet6.Entities;

namespace MyShopNet6.Dtos
{
    public class OrderSimple
    {
    

            public int Id { get; set; }

            public string CustomerId { get; set; } = null!;

            public string Receiver { get; set; } = null!;

            public string Address { get; set; } = null!;

            public string? Description { get; set; }

            public double Amount { get; set; }

        
    }
}
