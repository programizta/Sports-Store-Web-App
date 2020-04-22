﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        public int pageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult List(int productPage = 1) => View(new ProductListViewModel
        {
            Products = productRepository.Products
            .OrderBy(p => p.ProductId)
            .Skip((productPage - 1) * pageSize)
            .Take(pageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = pageSize,
                TotalItems = productRepository.Products.Count()
            }
        });
    }
}