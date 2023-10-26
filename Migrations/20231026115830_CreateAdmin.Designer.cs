﻿// <auto-generated />
using System;
using BookstoreAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookstoreAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231026115830_CreateAdmin")]
    partial class CreateAdmin
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookstoreAPI.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("email");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("password");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("RoleId");

                    b.ToTable("admins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "store.admin@bookstore.com",
                            IsActive = false,
                            Password = "password",
                            RoleId = 1,
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "store.admin2@bookstore.com",
                            IsActive = false,
                            Password = "password",
                            RoleId = 1,
                            Username = "admin2"
                        },
                        new
                        {
                            Id = 3,
                            Email = "store.admin3@bookstore.com",
                            IsActive = false,
                            Password = "password",
                            RoleId = 1,
                            Username = "admin3"
                        });
                });

            modelBuilder.Entity("BookstoreAPI.Models.BookGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("book_genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Mystery"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Thriller"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Science Fiction"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Romance"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Historical Fiction"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Biography"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Autobiography"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Non-Fiction"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Self-Help"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Horror"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Comedy"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Drama"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Travel"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Children's"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Cookbook"
                        },
                        new
                        {
                            Id = 18,
                            Name = "True Crime"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Philosophy"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Poetry"
                        });
                });

            modelBuilder.Entity("BookstoreAPI.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("cart_items");
                });

            modelBuilder.Entity("BookstoreAPI.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BillingAddress")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("billing_address");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("DeliveryFees")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("delivery_fees");

                    b.Property<string>("DeliveryMethod")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("delivery_method");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("order_date");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("payment_method");

                    b.Property<string>("PaymentStatus")
                        .HasColumnType("text")
                        .HasColumnName("payment_status");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("ShippingAddress")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("shipping_address");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("order_status");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric")
                        .HasColumnName("total_amount");

                    b.Property<string>("TrackingId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("tracking_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("TrackingId");

                    b.HasIndex("UserId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("BookstoreAPI.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("order_items");
                });

            modelBuilder.Entity("BookstoreAPI.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("author");

                    b.Property<int>("BookGenreId")
                        .HasColumnType("int")
                        .HasColumnName("Book_Genre_id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean")
                        .HasColumnName("Is_available");

                    b.Property<int>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.Property<string>("ProductSlug")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("product_slug");

                    b.Property<string>("ProductUrl")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("product_url");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("title");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("BookGenreId");

                    b.HasIndex("Price");

                    b.HasIndex("ProductSlug")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("BookstoreAPI.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Author"
                        },
                        new
                        {
                            Id = 3,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("BookstoreAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("email");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Otp")
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("otp");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("password");

                    b.Property<string>("ResetToken")
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("reset_token");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ResetToken");

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("users");
                });

            modelBuilder.Entity("BookstoreAPI.Models.Admin", b =>
                {
                    b.HasOne("BookstoreAPI.Models.Role", "Role")
                        .WithMany("Admins")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BookstoreAPI.Models.CartItem", b =>
                {
                    b.HasOne("BookstoreAPI.Models.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookstoreAPI.Models.User", "User")
                        .WithMany("CartItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookstoreAPI.Models.Order", b =>
                {
                    b.HasOne("BookstoreAPI.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookstoreAPI.Models.OrderItem", b =>
                {
                    b.HasOne("BookstoreAPI.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookstoreAPI.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BookstoreAPI.Models.Product", b =>
                {
                    b.HasOne("BookstoreAPI.Models.BookGenre", "BookGenre")
                        .WithMany("Products")
                        .HasForeignKey("BookGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookstoreAPI.Models.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookGenre");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookstoreAPI.Models.User", b =>
                {
                    b.HasOne("BookstoreAPI.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BookstoreAPI.Models.BookGenre", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("BookstoreAPI.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("BookstoreAPI.Models.Product", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("BookstoreAPI.Models.Role", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("BookstoreAPI.Models.User", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("Orders");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
