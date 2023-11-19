using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyShopNet6.Entities;

public partial class MyShopContext : DbContext
{
    public MyShopContext()
    {
    }

    public MyShopContext(DbContextOptions<MyShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleAction> RoleActions { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<WebAction> WebActions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ASUS-ROGSTRIX-G;Initial Catalog=MyShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Mã loại");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .HasComment("Mã khách hàng");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasComment("Email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasComment("Họ và tên");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasComment("Mật khẩu đăng nhập");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Mã hóa đơn");
            entity.Property(e => e.Address)
                .HasMaxLength(60)
                .HasComment("Địa chỉ nhận");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(20)
                .HasComment("Mã khách hàng");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasComment("Ghi chú thêm");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày đặt hàng")
                .HasColumnType("datetime");
            entity.Property(e => e.Receiver)
                .HasMaxLength(50)
                .HasComment("Tên người nhận");

            /*entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Orders_Customers");*/
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Mã chi tiết");
            entity.Property(e => e.OrderId).HasComment("Mã hóa đơn");
            entity.Property(e => e.ProductId).HasComment("Mã hàng hóa");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasComment("Số lượng mua");
            entity.Property(e => e.UnitPrice).HasComment("Đơn giá bán");

            /*entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderDetails_Orders");*/
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Mã hàng hóa");
            entity.Property(e => e.CategoryId).HasComment("Mã loại, FK");
            entity.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasComment("Mô tả hàng hóa");
            entity.Property(e => e.Discount).HasDefaultValueSql("(rand())");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .HasComment("Tên hàng hóa");
            entity.Property(e => e.Quantity).HasDefaultValue(100);
            entity.Property(e => e.SupplierId)
                .HasMaxLength(50)
                .HasDefaultValue("NK")
                .HasComment("Mã nhà cung cấp");
            entity.Property(e => e.UnitPrice).HasComment("Đơn giá");

            /*entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Suppliers1");*/
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<RoleAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Permissions");

            entity.Property(e => e.RoleId).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.RoleActions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Permissions_Roles");

            entity.HasOne(d => d.WebAction).WithMany(p => p.RoleActions)
                .HasForeignKey(d => d.WebActionId)
                .HasConstraintName("FK_Permissions_Actions");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasComment("Mã nhà cung cấp");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Tên công ty");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasComment("Số điện thoại liên lạc");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserInRoles");

            entity.Property(e => e.RoleId).HasMaxLength(50);
            entity.Property(e => e.UserId).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_UserRole_Role");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserRole_User");
        });

        modelBuilder.Entity<WebAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Actions");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
