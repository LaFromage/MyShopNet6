using System;
using System.Collections.Generic;

namespace MyShopNet6.Entities;

public partial class Customer
{
    /// <summary>
    /// Mã khách hàng
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Mật khẩu đăng nhập
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Họ và tên
    /// </summary>
    public string Fullname { get; set; } = null!;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
