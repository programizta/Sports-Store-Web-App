using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Components;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    public class NavigationMenuTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductId = 1, Name = "P1", Category = "Jabłka" },
                new Product { ProductId = 2, Name = "P2", Category = "Jabłka" },
                new Product { ProductId = 3, Name = "P3", Category = "Śliwki" },
                new Product { ProductId = 4, Name = "P4", Category = "Pomarańcze" }
            }).AsQueryable());

            NavigationMenu target = new NavigationMenu(mock.Object);

            string[] results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult)
                .ViewData.Model)
                .ToArray();

            Assert.True(Enumerable.SequenceEqual(new string[] { "Jabłka", "Pomarańcze", "Śliwki" }, results));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            string categoryToSelect = "Jabłka";
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductId = 1, Name = "P1", Category = "Jabłka" },
                new Product { ProductId = 4, Name = "P2", Category = "Pomarańcze" }
            }).AsQueryable());

            NavigationMenu target = new NavigationMenu(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new RouteData()
                }
            };

            target.RouteData.Values["category"] = categoryToSelect;

            string result = (string)(target.Invoke()
                as ViewViewComponentResult).ViewData["SelectedCategory"];

            Assert.Equal(categoryToSelect, result);
        }

        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
                {
                    new Product { ProductId = 1, Name = "P1", Category = "Cat1" },
                    new Product { ProductId = 2, Name = "P2", Category = "Cat2" },
                    new Product { ProductId = 3, Name = "P3", Category = "Cat1" },
                    new Product { ProductId = 4, Name = "P4", Category = "Cat2" },
                    new Product { ProductId = 5, Name = "P5", Category = "Cat3" }
                }
                ).AsQueryable());

            ProductController target = new ProductController(mock.Object);
            target.pageSize = 3;

            Func<ViewResult, ProductListViewModel> GetModel = result =>
                result?.ViewData?.Model as ProductListViewModel;

            int? result1 = GetModel(target.List("Cat1"))?.PagingInfo.TotalItems;
            int? result2 = GetModel(target.List("Cat2"))?.PagingInfo.TotalItems;
            int? result3 = GetModel(target.List("Cat3"))?.PagingInfo.TotalItems;
            int? resultAll = GetModel(target.List(null))?.PagingInfo.TotalItems;

            Assert.Equal(2, result1);
            Assert.Equal(2, result2);
            Assert.Equal(1, result3);
            Assert.Equal(5, resultAll);
        }
    }
}
