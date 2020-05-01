using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
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
    }
}
