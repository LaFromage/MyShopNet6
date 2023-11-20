using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace MyShopNet6.Entities;

public partial class OrderDetail
{

    public int Id { get; set; }


    public int OrderId { get; set; }


    public int ProductId { get; set; }

    /*public virtual Order Order { get; set; } = null!;

    /*public OrderDetail(Order order, object context)
    {
        // Kiểm tra xem order có tồn tại không để tránh lỗi
        if (order != null)
        {
            OrderId = order.Id;
        }
        else
        {
            // Nếu order là null, tạo một đơn hàng mới và lưu vào cơ sở dữ liệu để có Id hợp lệ
            Order newOrder = new Order
            {
                // Các thuộc tính của đơn hàng mới
            };

            // Thêm đơn hàng mới vào DbContext
            _context.Order.Add(newOrder);

            // Lưu thay đổi để có được Id hợp lệ
            _context.SaveChanges();

            // Gán OrderId từ đơn hàng mới đã tạo
            OrderId = newOrder.Id;
        }
        _context = context;
    }*/
}
