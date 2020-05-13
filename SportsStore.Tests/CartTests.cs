﻿using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target
                .Lines
                .OrderBy(p => p.Product.ProductId)
                .ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };
            Product p3 = new Product { ProductId = 3, Name = "P3" };

            Cart target = new Cart();

            target.AddItem(p1, 2);
            target.AddItem(p2, 6);
            target.AddItem(p3, 3);
            target.AddItem(p1, 8);

            target.RemoveLine(p1);

            Assert.Empty(target.Lines.Where(p => p.Product == p1));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 100m };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 50m };

            Cart target = new Cart();

            target.AddItem(p1, 2);
            target.AddItem(p2, 2);
            target.AddItem(p1, 4);

            var totalPrice = target.ComputeTotalValue();

            Assert.Equal(700m, totalPrice);
        }

        [Fact]
        public void Can_Clear_Basket()
        {
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 75m };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 55.55m };
            Product p3 = new Product { ProductId = 3, Name = "P3", Price = 192.45m };

            Cart target = new Cart();

            target.AddItem(p1, 2);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);
            target.AddItem(p3, 1);

            target.Clear();

            Assert.Empty(target.Lines);
        }
    }
}
